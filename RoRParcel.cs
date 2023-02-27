using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Linq;

[assembly: AssemblyTitle("RoR Parcel Plugin")]
[assembly: AssemblyDescription("Tracks looting of Locked Parcels in Renewal of Ro raid zones")]
[assembly: AssemblyCompany("Mineeme")]
[assembly: AssemblyVersion("1.2.0.0")]

namespace ACT_RoR_Parcels
{
    public partial class RoRParcel : UserControl, IActPluginV1
	{

        string[] T1Zones = { "The Hunt", "Standing Storm" };
        string[] T2Zones = { "Boundless Gulf" };
        List<string> T1ZoneList;
        List<string> T2ZoneList;

        Label lblStatus;    // The status label that appears in ACT's Plugin tab

        // looter data and persistence
        public List<Looter> looterList { get; set; }
        string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\RoR_Parcels.config.xml");
		XmlSerializer xmlSerializer;

        // log line parsing
		const string logTimeStampRegexStr = @"^\((?<secs>\d{10})\)\[.{24}\] ";
		const int timeLen = 39;
        Regex reLoot = new Regex(logTimeStampRegexStr + @"(?<who>\w+) loots?[^:]+:Locked Parcel: (?<where>[^\\]+)", RegexOptions.Compiled);
        Regex regexWhoRaid = new Regex(logTimeStampRegexStr + @"\/whoraid search results", RegexOptions.Compiled);
        Regex regexWho = new Regex(logTimeStampRegexStr + @"\[[^]]+\] (?<member>\w+) ", RegexOptions.Compiled);
        Regex regexWhoEnd = new Regex(logTimeStampRegexStr + @"\d+ players found", RegexOptions.Compiled);
        Regex regexJoined = new Regex(logTimeStampRegexStr + @"(?<member>\w+) has joined the raid", RegexOptions.Compiled);
        Regex regexLeft = new Regex(logTimeStampRegexStr + @"(?<member>\w+) has left the raid", RegexOptions.Compiled);

        // date/time combo box selection support
        ComboBox comboTime = null;
        bool isT1Combo = false;
        bool isT2Combo = false;
        
        // /whoraid support
        enum tWhoStates { idle, watching }; // /whoraid state machine states
        private tWhoStates eState = tWhoStates.idle;
        List<string> whoRaid = new List<string>(); //collects /whoraid names
        
        // UI thread support
        WindowsFormsSynchronizationContext mUiContext = new WindowsFormsSynchronizationContext();
        bool importChecked;

        public RoRParcel()
		{
			InitializeComponent();
		}


		#region IActPluginV1 Members
		
		public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
		{
			lblStatus = pluginStatusText;	        // save the status label's reference to our local var
			pluginScreenSpace.Controls.Add(this);	// Add this UserControl to the tab ACT provides
			this.Dock = DockStyle.Fill;             // Expand the UserControl to fill the tab's client space

			xmlSerializer = new XmlSerializer(typeof(List<Looter>));
			LoadSettings();

            T1ZoneList = T1Zones.ToList();
            T2ZoneList = T2Zones.ToList();

            labelInstructions.Text = "";

            // Create some sort of parsing event handler.  After the "+=" hit TAB twice and the code will be generated for you.
            ActGlobals.oFormActMain.OnLogLineRead += OFormActMain_OnLogLineRead;
            ActGlobals.oFormActMain.XmlSnippetAdded += OFormActMain_XmlSnippetAdded;

            if (ActGlobals.oFormActMain.GetAutomaticUpdatesAllowed())
            {
                // If ACT is set to automatically check for updates, check for updates to the plugin
                // If we don't put this on a separate thread, web latency will delay the plugin init phase
                new Thread(new ThreadStart(oFormActMain_UpdateCheckClicked)).Start();
            }

            lblStatus.Text = "Plugin Started";
		}

        public void DeInitPlugin()
		{
			// Unsubscribe from any events you listen to when exiting!
			ActGlobals.oFormActMain.OnLogLineRead -= OFormActMain_OnLogLineRead;
            ActGlobals.oFormActMain.XmlSnippetAdded -= OFormActMain_XmlSnippetAdded;
            
            SaveSettings();
			lblStatus.Text = "Plugin Exited";
		}

        #endregion

        void oFormActMain_UpdateCheckClicked()
        {
            try
            {
                Version localVersion = this.GetType().Assembly.GetName().Version;
                Task<Version> vtask = Task.Run(() => { return GetRemoteVersionAsync(); });
                vtask.Wait();
                if (vtask.Result > localVersion)
                {
                    DialogResult result = MessageBox.Show("There is an updated version of the Parcels Plugin.\n\nSee the changes by clicking the About link in the plugin.\n\nUpdate it now?\n\n(If there is an update to ACT, you should click No and update ACT first.)",
                        "New Version", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Task<FileInfo> ftask = Task.Run(() => { return GetRemoteFileAsync(); });
                        ftask.Wait();
                        if (ftask.Result != null)
                        {
                            ActPluginData pluginData = ActGlobals.oFormActMain.PluginGetSelfData(this);
                            pluginData.pluginFile.Delete();
                            File.Move(ftask.Result.FullName, pluginData.pluginFile.FullName);
                            Application.DoEvents();
                            ThreadInvokes.CheckboxSetChecked(ActGlobals.oFormActMain, pluginData.cbEnabled, false);
                            Application.DoEvents();
                            ThreadInvokes.CheckboxSetChecked(ActGlobals.oFormActMain, pluginData.cbEnabled, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(ex, "RoR Parcels Plugin Update Download:" + ex.Message);
            }
        }

        private async Task<Version> GetRemoteVersionAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ProductInfoHeaderValue hdr = new ProductInfoHeaderValue("ACT_Ror_Parcels", "1");
                    client.DefaultRequestHeaders.UserAgent.Add(hdr);
                    HttpResponseMessage response = await client.GetAsync(@"https://api.github.com/repos/jeffjl74/ACT_RoR_Parcels/releases/latest");
                    if (response.IsSuccessStatusCode)
                    {
                        //response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Regex reVer = new Regex(@".tag_name.:.v([^""]+)""");
                        Match match = reVer.Match(responseBody);
                        if (match.Success)
                            return new Version(match.Groups[1].Value);
                    }
                    return new Version("0.0.0");
                }
            }
            catch { return new Version("0.0.0"); }
        }

        private async Task<FileInfo> GetRemoteFileAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ProductInfoHeaderValue hdr = new ProductInfoHeaderValue("ACT_RoR_Parcels", "1");
                    client.DefaultRequestHeaders.UserAgent.Add(hdr);
                    HttpResponseMessage response = await client.GetAsync(@"https://github.com/jeffjl74/ACT_RoR_Parcels/releases/latest/download/Parcels.dll");
                    if (response.IsSuccessStatusCode)
                    {
                        string tmp = Path.GetTempFileName();
                        var stream = await response.Content.ReadAsStreamAsync();
                        var fileStream = new FileStream(tmp, FileMode.Create);
                        await stream.CopyToAsync(fileStream);
                        fileStream.Close();
                        Application.DoEvents();
                        FileInfo fi = new FileInfo(tmp);
                        return fi;
                    }
                }
                return null;
            }
            catch { return null; }
        }


        void LoadSettings()
		{
            dataGridView1.AutoGenerateColumns = false;

            if (File.Exists(settingsFile))
			{
				try
				{
					using (FileStream fs = new FileStream(settingsFile, FileMode.Open))
					{
						looterList = (List<Looter>)xmlSerializer.Deserialize(fs);
                        if(looterList != null)
                        {
                            looterList.Sort();
                        }
                        else
                        {
                            looterList = new List<Looter>();
                        }
                    }
                }
				catch (Exception ex)
				{
					lblStatus.Text = "Error loading settings: " + ex.Message;
				    looterList = new List<Looter>();
                }
            }
            else
            {
				looterList = new List<Looter>();
            }
            lootersBindingSource.DataSource = looterList;
            lootersBindingSource.ResetBindings(false);
        }

        void SaveSettings()
		{
			using (TextWriter writer = new StreamWriter(settingsFile))
			{
				xmlSerializer.Serialize(writer, looterList);
				writer.Close();
			}
		}

		public static DateTime UnixSecondsToDateTime(long unixSeconds)
		{
			// Unix timestamp is seconds past epoch (1970-01-01T00:00:00Z)
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime unixTime = epoch.AddSeconds(unixSeconds);
			return unixTime.ToLocalTime();
		}

        private void OFormActMain_XmlSnippetAdded(object sender, XmlSnippetEventArgs e)
        {
            if(e.ShareType == "Parcel")
            {
                string data;
                if (e.XmlAttributes.TryGetValue("P", out data))
                {
                    mUiContext.Post(UiParseXml, data);
                }
            }
        }

        private void UiParseXml(object o)
        {
            string data = o as string;
            if(o != null)
            {
                string[] players = data.Split(':');
                foreach (string player in players)
                {
                    Looter looter = new Looter();
                    int tier = 0;
                    string[] tiers = player.Split(')');
                    for(int j=0; j<tiers.Length; j++)
                    {
                        string[] csv = tiers[j].Split(',');
                        for (int i = 0; i < csv.Length; i++)
                        {
                            string param = csv[i];
                            if (string.IsNullOrEmpty(param))
                                continue;
                            if (i == 0 && j == 0)
                            {
                                // player slot
                                looter = looterList.Find(x => x.Player == param);
                                if (looter == null)
                                {
                                    looter = new Looter { Player = param };
                                    looterList.Add(looter);
                                }
                            }
                            else if((i == 1 && j == 0) || (i == 0 && j > 0))
                            {
                                // time slot with tier
                                int eq = param.IndexOf('=');
                                if(eq >= 0)
                                {
                                    string digits = param.Substring(1, eq - 1);
                                    if (Int32.TryParse(digits, out tier))
                                    {
                                        string rest = param.Substring(eq + 1);
                                        Int64 t;
                                        if (Int64.TryParse(rest, out t))
                                        {
                                            DateTime logTime = UnixSecondsToDateTime(t);
                                            looter.AddTime(tier, logTime);
                                        }
                                    }
                                }
                            }
                            else if (looter != null)
                            {
                                Int64 t;
                                if (Int64.TryParse(param, out t))
                                {
                                    DateTime logTime = UnixSecondsToDateTime(t);
                                    looter.AddTime(tier, logTime);
                                }
                            }
                        }
                    }
                }
                lootersBindingSource.ResetBindings(false);
            }
        }

        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
		{
            bool doLine = !isImport || (isImport && importChecked);

			if (doLine && logInfo.detectedType == 0 && logInfo.logLine.Length > timeLen)
			{
				Match match;
				if((match = reLoot.Match(logInfo.logLine)).Success)
                {
                    labelInstructions.Text = "";

					string who = match.Groups["who"].Value;
					if (who == "You")
						who = ActGlobals.charName;
					Int64 secs = Int64.Parse(match.Groups["secs"].Value);
                    string where = match.Groups["where"].Value;
                    int tier = 1;
                    if (T1ZoneList.Contains(where))
                        tier = 1;
                    else if (T2ZoneList.Contains(where))
                        tier = 2;
                    DateTime logTime = UnixSecondsToDateTime(secs);
					Looter looter = looterList.Find(x => x.Player == who);
					if (looter == null)
                    {
						looter = new Looter();
						looter.Player = who;
                        looter.InRaid = 1;
                        looter.AddTime(tier, logTime);
                        looterList.Add(looter);
                        // add to /whoraid after the looter is in the player list for proper update
                        if (!whoRaid.Contains(who))
                            whoRaid.Add(who);
                        mUiContext.Post(UiUpdateGrid, null);
                    }
                    else
                    {
                        looter.InRaid = 1;
                        if(looter.AddTime(tier, logTime))
                            mUiContext.Post(UiUpdateGrid, null);
                    }
                }
                else if((match = regexJoined.Match(logInfo.logLine)).Success)
                {
                    string who = match.Groups["member"].Value;
                    if (!whoRaid.Contains(who))
                    {
                        whoRaid.Add(who);
                        mUiContext.Post(UiWhoRaid, null);
                    }                
                }
                else if ((match = regexLeft.Match(logInfo.logLine)).Success)
                {
                    whoRaid.Remove(match.Groups["member"].Value);
                    mUiContext.Post(UiWhoRaid, null);
                }
                else if ((match = regexWhoRaid.Match(logInfo.logLine)).Success)
                {
                    eState = tWhoStates.watching; //begin watching for /whoraid list
                    whoRaid.Clear();
                }
                else if(eState == tWhoStates.watching)
                {
                    if ((match = regexWho.Match(logInfo.logLine)).Success)
                    {
                        whoRaid.Add(match.Groups["member"].Value);
                    }
                    else if ((match = regexWhoEnd.Match(logInfo.logLine)).Success)
                    {
                        eState = tWhoStates.idle;
                        mUiContext.Post(UiWhoRaid, null);
                    }
                }
            }
		}

        private void UiUpdateGrid(object o)
        {
            lootersBindingSource.ResetBindings(false);
            SaveSettings();
        }

        private void UiWhoRaid( object o)
        {
            foreach (string s in whoRaid)
            {
                Looter looter = looterList.Find(x => x.Player == s);
                if (looter == null)
                {
                    // add the /whoraid player to the looter list
                    looter = new Looter();
                    looter.Player = s;
                    looterList.Add(looter);
                }
            }
            foreach (Looter looter in looterList)
            {
                if (whoRaid.Contains(looter.Player))
                    looter.InRaid = 1;
                else
                    looter.InRaid = 0;
            }
            lootersBindingSource.ResetBindings(false);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            string sortFor = "T1";
            string zone1 = T1ZoneList.Find(x => ActGlobals.oFormActMain.CurrentZone.Contains(x));
            string zone2 = T2ZoneList.Find(x => ActGlobals.oFormActMain.CurrentZone.Contains(x));
            if (zone1 != null || (zone1 == null && zone2 == null))
            {
                LooterSorter looterSorter = new LooterSorter("In Raid", SortOrder.Descending);
                looterList.Sort(looterSorter);
            }
            else
            {
                LooterSorter looterSorter = new LooterSorter("In Raid2", SortOrder.Descending);
                looterList.Sort(looterSorter);
                sortFor = "T2";
            }

            lootersBindingSource.ResetBindings(false);
            if (looterList.Count > 0)
            {
                if (looterList[0].InRaid == 1)
                {
                    int lowest = looterList[0].T1Count;
                    int i = 1;
                    for (; i < looterList.Count; i++)
                    {
                        if (looterList[i].T1Count > lowest || looterList[i].InRaid < 1)
                            break;
                    }
                    labelInstructions.Text = $"/ran {i} to choose the next {sortFor} looter.";
                }
                else
                {
                    labelInstructions.Text = "No players are flagged as raiding. Did you forget /whoraid?";
                }
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // don't know why this happens after a combobox change. the cell contains a DateTime
            Debug.WriteLine("Data Error: " + e.Exception.Message);
            e.Cancel = true;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isT1Combo = false;
            isT2Combo = false;
            if (e.ColumnIndex == dataGridView1.Columns["DateColumn"].Index)
                isT1Combo = true;
            else if (e.ColumnIndex == dataGridView1.Columns["T2DateColumn"].Index)
                isT2Combo = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // want to get the chosen index later
			comboTime = e.Control as ComboBox;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            comboTime = null;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DateColumn"].Index && e.RowIndex >= 0 && e.RowIndex < looterList.Count)
            {
                // Cast the cell to a DataGridViewComboBoxCell
                var cell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.DataSource == null)
                {
                    // Set the DataSource of the ComboBox based on the current row's custom class instance
                    cell.DataSource = looterList[e.RowIndex].lootDates;
                    cell.ValueType = typeof(DateTime);

                    int dates = looterList[e.RowIndex].T1Count;
                    if (dates > 0)
                    {
                        cell.Value = looterList[e.RowIndex].lootDates[dates - 1];
                    }
                }
                else
                {
                    if (comboTime != null && isT1Combo)
                        if (looterList[e.RowIndex].T1Count > comboTime.SelectedIndex && comboTime.SelectedIndex >= 0)
                            cell.Value = looterList[e.RowIndex].lootDates[comboTime.SelectedIndex];
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["T2DateColumn"].Index && e.RowIndex >= 0 && e.RowIndex < looterList.Count)
            {
                // Cast the cell to a DataGridViewComboBoxCell
                var cell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.DataSource == null)
                {
                    // Set the DataSource of the ComboBox based on the current row's custom class instance
                    cell.DataSource = looterList[e.RowIndex].T2LootDates;
                    cell.ValueType = typeof(DateTime);

                    int dates = looterList[e.RowIndex].T2Count;
                    if (dates > 0)
                    {
                        cell.Value = looterList[e.RowIndex].T2LootDates[dates - 1];
                    }
                }
                else
                {
                    if (comboTime != null && isT2Combo)
                        if (looterList[e.RowIndex].T2Count > comboTime.SelectedIndex && comboTime.SelectedIndex >= 0)
                            cell.Value = looterList[e.RowIndex].T2LootDates[comboTime.SelectedIndex];
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string colHdr = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            if (dataGridView1.Columns[e.ColumnIndex].Tag == null)
                dataGridView1.Columns[e.ColumnIndex].Tag = SortOrder.Descending;
            else
                dataGridView1.Columns[e.ColumnIndex].Tag = (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            LooterSorter looterSorter;
            if(colHdr == "In Raid" && T2ZoneList.Find(x => ActGlobals.oFormActMain.CurrentZone.Contains(x)) != null)
                looterSorter = new LooterSorter(colHdr + "2", (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag);
            else
                looterSorter = new LooterSorter(colHdr, (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag);
            looterList.Sort(looterSorter);
            lootersBindingSource.ResetBindings(false);
            dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // add row number to the row headers
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth-2, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void checkBoxImport_CheckedChanged(object sender, EventArgs e)
        {
            importChecked = checkBoxImport.Checked;
        }

        private void buttonShare_Click(object sender, EventArgs e)
        {
            XmlCopyForm form = new XmlCopyForm("/r", looterList);
            form.Show(this);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            List<string> data = File.ReadLines(ActGlobals.oFormActMain.GameMacroFolder + @"\raid-parcel1.txt").ToList();
            string p = data[0].Replace(" r <Parcel P='", "").Replace("' />", "");
            UiParseXml(p);

            //Task<FileInfo> ftask = Task.Run(() => { return GetRemoteFileAsync(); });
            //ftask.Wait();
            //if (ftask.Result != null)
            //{
            //    Debug.WriteLine($"download ok: {ftask.Result.FullName}");
            //}

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/jeffjl74/ACT_RoR_Parcels#Locked-Parcel-Plugin-for-Advanced-Combat-Tracker");
        }
    }
}
