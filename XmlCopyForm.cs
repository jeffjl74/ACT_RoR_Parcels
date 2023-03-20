﻿using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace ACT_RoR_Parcels
{

    public partial class XmlCopyForm : Form
    {
        const int maxChatLen = 240;
        const int maxMacroLen = 990;
        bool _loading = true;
        bool _preIncremet = false;
        bool _autoIncrementing = false;
        string _prefix;
        List<Looter> _looters;
        int fileCount = 0;
        const string doFileName = "raid-parcel{0}.txt";
        StringBuilder macroLines = new StringBuilder();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        List<IntPtr> _handles = new List<IntPtr>();

        // simple class to use as the listbox item
        enum ItemType { Player, Command }
        class ListItem
        {
            public string description;
            public string data;
            public ItemType type;
            public override string ToString()
            {
                return description;
            }
        }

        internal class ShareTrack
        {
            public bool done = false;
            public int playerIndex = 0;
            public string data = "";
        }

        public XmlCopyForm(string prefix, List<Looter> looters)
        {
            InitializeComponent();

            _prefix = prefix;
            _looters = looters;
        }

        private void XmlCopyForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_prefix))
            {
                if (_prefix.StartsWith("/g"))
                    radioButtonG.Checked = true;
                else if (_prefix.StartsWith("/r"))
                    radioButtonR.Checked = true;
                else
                {
                    radioButtonCustom.Checked = true;
                    textBoxCustom.Text = _prefix;
                }
            }

            BuildPlayerList();

            // look for game instances
            Process[] processes = Process.GetProcessesByName("EverQuest2");
            if (processes.Length > 0)
            {
                _handles = new List<IntPtr>();
                foreach (Process p in processes)
                {
                    // only want the main window
                    if (p.MainWindowTitle.StartsWith("EverQuest II ("))
                    {
                        if(!_handles.Contains(p.MainWindowHandle))
                            _handles.Add(p.MainWindowHandle);
                    }
                }
                if (_handles.Count > 0)
                {
                    foreach (IntPtr intPtr in _handles)
                    {
                        comboBoxGame.Items.Add(intPtr);
                    }
                    comboBoxGame.Items.Add(""); // item to allow user to de-select game activation
                    comboBoxGame.SelectedIndex = 0;
                    // switch to macro list?
                    if(buttonMacro.Enabled)
                        buttonMacro_Click(null, null);
                }
            }

            _loading = false;
            this.TopMost = true;
            this.CenterToParent();
        }

        private void BuildPlayerList()
        {
            listBox1.Items.Clear();
            foreach (Looter l in _looters)
            {
                ShareTrack st = new ShareTrack();
                l.RestartIterator();
                do
                {
                    st = BuildSharePlayer(l, st);
                    ListItem item = new ListItem { description = l.Player, data = st.data, type = ItemType.Player };
                    listBox1.Items.Add(item);
                } while (!st.done);
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                toolStripStatusLabel1.Text = "Press [Copy] to copy selection to clipboard";
            }
            else
                toolStripStatusLabel1.Text = string.Empty;

        }

        private ShareTrack BuildSharePlayer(Looter looter, ShareTrack shareTrack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<Parcel P='{looter.Player}");

            int tier = -1;
            int prevTier = -1;
            DateTime time;
            bool stillRoom = false;
            do
            {
                (tier, time) = looter.GetNextTime();
                long unixTimeSeconds = new DateTimeOffset(time).ToUnixTimeSeconds();
                if (tier == 0)
                {
                    if (prevTier != -1)
                        sb.Append(")");
                    break;
                }
                if (tier != prevTier)
                {
                    if (prevTier == -1)
                        sb.Append($",({tier}={unixTimeSeconds}");    // first one needs the comma
                    else
                        sb.Append($")({tier}={unixTimeSeconds}");    // close previous, open new
                    prevTier = tier;
                }
                else
                    sb.Append($",{unixTimeSeconds}");           // continue the list

                stillRoom = (sb.Length + 15) < maxChatLen;

            } while (tier > 0 && stillRoom);

            if (!stillRoom)
                sb.Append(")");

            sb.Append("' />");
            shareTrack.data = sb.ToString();
            if(looter.IterationsDone())
                shareTrack.done = true;
            return shareTrack;
        }

        private void RestartAll()
        {
            foreach (Looter looter in _looters)
                looter.RestartIterator();
        }

        private int BuildShareAll(string prefix)
        {
            fileCount = 0;
            int lineCount = 0;
            macroLines.Clear();
            RestartAll();    
            ShareTrack st = new ShareTrack();
            do
            {
                st = BuildMacroLine(st);
                lineCount++;
                if(lineCount == 16 || st.done)
                {
                    fileCount++;
                    string filePath = string.Format(doFileName, fileCount);
                    if (!ActGlobals.oFormActMain.SendToMacroFile(filePath, macroLines.ToString(), string.Empty))
                    {
                        SimpleMessageBox.Show(this, $"Write to macro file {fileCount} failed", "Macro Error");
                    }
                    macroLines.Clear();
                    lineCount = 0;
                }
            // not sure all corner cases have been tested
            // prevent an infinite loop by limiting the number of files to an arbitrary 10
            } while (!st.done && fileCount < 10);

            return fileCount;
        }

        private ShareTrack BuildMacroLine(ShareTrack st)
        {
            StringBuilder sb = new StringBuilder();
            string macroPrefix = _prefix.Replace("/", "").Trim();
            sb.Append($"{macroPrefix} <Parcel P='");
            bool stillRoom = (sb.Length + _looters[st.playerIndex].Player.Length + 20) < maxMacroLen;
            for (; st.playerIndex < _looters.Count && stillRoom; st.playerIndex++)
            {
                Looter looter = _looters[st.playerIndex];
                sb.Append($"{looter.Player}");
                int tier = -1;
                int prevTier = -1;
                DateTime time;
                do
                {
                    (tier, time) = looter.GetNextTime();
                    long unixTimeSeconds = new DateTimeOffset(time).ToUnixTimeSeconds();
                    if (tier == 0)
                    {
                        if (prevTier != -1)
                            sb.Append(")");
                        break;
                    }
                    if (tier != prevTier)
                    {
                        if(prevTier == -1)
                            sb.Append($",({tier}={unixTimeSeconds}");    // first one needs the comma
                        else
                            sb.Append($")({tier}={unixTimeSeconds}");    // close previous, open new
                        prevTier = tier;
                    }
                    else
                        sb.Append($",{unixTimeSeconds}");           // continue the list

                    stillRoom = (sb.Length + 15) < maxMacroLen;
                } while (tier > 0 && stillRoom);

                // if we quit because the line is getting long, close it and bail
                if (!stillRoom)
                {
                    sb.Append(")");
                    break;
                }

                // more players?
                if(st.playerIndex + 1 < _looters.Count)
                {
                    stillRoom = (sb.Length + _looters[st.playerIndex+1].Player.Length + 20) < maxMacroLen;

                    if (looter.IterationsDone())
                        sb.Append(":");
                }
            }
            macroLines.Append(sb.ToString() + "' />" + Environment.NewLine);
            st.data = sb.ToString();
            if (st.playerIndex == _looters.Count && _looters[_looters.Count-1].IterationsDone())
                st.done = true;
            return st;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (radioButtonG.Checked)
                _prefix = "/g ";
            else if (radioButtonR.Checked)
                _prefix = "/r ";
            else if (!string.IsNullOrEmpty(textBoxCustom.Text))
            {
                _prefix = textBoxCustom.Text;
                if (!_prefix.EndsWith(" "))
                    _prefix = _prefix + " ";
            }

            bool needLoad = true;
            if (listBox1.Items.Count > 0)
            {
                ListItem listItem = (ListItem)listBox1.Items[0];
                if (listItem.type != ItemType.Command)
                {
                    NextListItem(_prefix);
                    needLoad = false;
                }
            }
            if(needLoad)
            {
                this.Text = "XML Share";
                toolTip1.SetToolTip(buttonMacro, "Press to generate and list macro files");
                toolTip1.SetToolTip(buttonCopy, "Press to copy the selected XML item to the clipboard");
                BuildPlayerList();
            }
        }

        private void NextListItem(string prefix)
        {
            try
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    if (_preIncremet)
                    {
                        // select the next item
                        _autoIncrementing = true;
                        if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                            listBox1.SelectedIndex++;
                        else
                        {
                            listBox1.SelectedIndex = -1;
                            toolStripStatusLabel1.Text = "No more items.";
                        }
                        _autoIncrementing = false;
                    }
                    else
                    {
                        // first time through, we use the selected item
                        // next time, we will go to the next item
                        _preIncremet = true;
                    }

                    if (listBox1.SelectedIndex >= 0)
                    {
                        int itemNum = listBox1.SelectedIndex + 1;
                        // copy to the clipboard
                        ListItem item = (ListItem)listBox1.Items[listBox1.SelectedIndex];
                        Clipboard.SetText(prefix + item.data);

                        bool gameActivated = false;
                        if (comboBoxGame.Items.Count > 0 && comboBoxGame.SelectedIndex >= 0)
                        {
                            // if we found an EQII game window, activate it
                            if (!string.IsNullOrEmpty(comboBoxGame.Items[comboBoxGame.SelectedIndex].ToString()))
                            {
                                toolStripStatusLabel1.Text = String.Format(@"<Enter><Ctrl-v> to paste item {0}. {1} for next.", itemNum, item.type == ItemType.Command ? "[Macro]" : "[Copy]");
                                IntPtr handle = (IntPtr)comboBoxGame.Items[comboBoxGame.SelectedIndex];
                                SetForegroundWindow(handle);
                                gameActivated = true;
                            }
                        }
                        if(!gameActivated)
                        {
                            toolStripStatusLabel1.Text = String.Format("Item {0} copied. Press {1} for next.", itemNum, item.type == ItemType.Command ? "[Macro]" : "[Copy]");
                        }
                    }
                }
                else
                {
                    SimpleMessageBox.Show(this, "Select an item to copy to the clipboard.", "Error");
                    toolStripStatusLabel1.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                SimpleMessageBox.Show(this, "Clipboard copy failed. Try again.", "Failed");
                _preIncremet = false;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (radioButtonCustom.Checked)
                    _prefix = textBoxCustom.Text;
                if (listBox1.Items.Count > 0)
                {
                    ListItem listItem = (ListItem)listBox1.Items[0];
                    if (listItem.type == ItemType.Command)
                    {
                        // regenerate the macros
                        listBox1.Items.Clear();
                        buttonMacro_Click(null, null);
                    }
                }
            }
        }

        private void textBoxCustom_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonCustom.Checked)
            {
                _prefix = textBoxCustom.Text;
                if (listBox1.Items.Count > 0)
                {
                    ListItem listItem = (ListItem)listBox1.Items[0];
                    if (listItem.type == ItemType.Command)
                    {
                        // regenerate the macros
                        listBox1.Items.Clear();
                        buttonMacro_Click(null, null);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    ListItem listItem = (ListItem)listBox1.Items[listBox1.SelectedIndex];
                    toolStripStatusLabel1.Text = String.Format("Press {0} to copy selection to clipboard", listItem.type == ItemType.Command ? "[Macro]" : "[Copy]");
                    if (!_autoIncrementing)
                        _preIncremet = false;
                }
            }
        }

        private void buttonMacro_Click(object sender, EventArgs e)
        {
            bool needLoad = true;
            if(listBox1.Items.Count > 0)
            {
                ListItem listItem = (ListItem)listBox1.Items[0];
                if(listItem.type == ItemType.Command)
                {
                    // list is already do_file_commmands, select the next one
                    NextListItem("");
                    needLoad = false;
                }
            }
            if(needLoad)
            {
                string prefix = string.Empty;
                toolTip1.SetToolTip(buttonMacro, "Press to copy the selected item to the clipboard");
                toolTip1.SetToolTip(buttonCopy, "Press to generate and list XML items");
                if (radioButtonG.Checked)
                    prefix = "g ";
                else if (radioButtonR.Checked)
                    prefix = "r ";
                else if (!string.IsNullOrEmpty(textBoxCustom.Text))
                {
                    prefix = textBoxCustom.Text;
                    prefix = prefix.TrimStart('/');
                    if (!prefix.EndsWith(" "))
                        prefix = prefix + " ";
                }

                this.Text = String.Format("XML Share: {0} players", _looters.Count);

                int count = BuildShareAll(prefix);
                listBox1.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    ListItem listItem = new ListItem();
                    listItem.data = string.Format("/do_file_commands raid-parcel{0}.txt", i+1);
                    listItem.description = listItem.data;
                    listItem.type = ItemType.Command;
                    listBox1.Items.Add(listItem);
                }
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }
        }

        private void radioButtonG_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (radioButtonG.Checked)
                {
                    _prefix = "/g";
                    if (listBox1.Items.Count > 0)
                    {
                        ListItem listItem = (ListItem)listBox1.Items[0];
                        if (listItem.type == ItemType.Command)
                        {
                            // regenerate the macros
                            listBox1.Items.Clear();
                            buttonMacro_Click(null, null);
                        }
                    }
                }
            }
        }

        private void radioButtonR_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                if (radioButtonR.Checked)
                {
                    _prefix = "/r";
                    if (listBox1.Items.Count > 0)
                    {
                        ListItem listItem = (ListItem)listBox1.Items[0];
                        if (listItem.type == ItemType.Command)
                        {
                            // regenerate the macros
                            listBox1.Items.Clear();
                            buttonMacro_Click(null, null);
                        }
                    }
                }
            }
        }
    }
}
