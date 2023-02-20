using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.IO;
using System.Reflection;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

[assembly: AssemblyTitle("RoR Parcel Plugin")]
[assembly: AssemblyDescription("Tracks looting of Locked Parcels in Return of Ro raid zones")]
[assembly: AssemblyCompany("Mineeme")]
[assembly: AssemblyVersion("1.0.0.0")]

namespace ACT_RoR_Parcels
{
	public class RoRParcel : UserControl, IActPluginV1
	{

		#region Designer Created Code (Avoid editing)
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DateColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InRaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lootersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonShare = new System.Windows.Forms.Button();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.checkBoxImport = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonHelp = new System.Windows.Forms.Button();
            this.playerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.looterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lootersBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.looterListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerDataGridViewTextBoxColumn,
            this.countDataGridViewTextBoxColumn,
            this.DateColumn,
            this.InRaid});
            this.dataGridView1.DataSource = this.lootersBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(686, 349);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // DateColumn
            // 
            this.DateColumn.HeaderText = "Dates";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DateColumn.Width = 130;
            // 
            // InRaid
            // 
            this.InRaid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InRaid.DataPropertyName = "InRaid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "Y;?; ";
            this.InRaid.DefaultCellStyle = dataGridViewCellStyle2;
            this.InRaid.HeaderText = "InRaid";
            this.InRaid.Name = "InRaid";
            this.InRaid.Width = 63;
            // 
            // lootersBindingSource
            // 
            this.lootersBindingSource.DataMember = "Looters";
            this.lootersBindingSource.DataSource = this.looterListBindingSource;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNext.Location = new System.Drawing.Point(113, 7);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Pick Next";
            this.toolTip1.SetToolTip(this.buttonNext, "Sorts the list and suggests a /ran command.\r\nA /whoraid will inform the plugin ab" +
        "out raid members.");
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 349);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonHelp);
            this.panel2.Controls.Add(this.buttonTest);
            this.panel2.Controls.Add(this.buttonShare);
            this.panel2.Controls.Add(this.labelInstructions);
            this.panel2.Controls.Add(this.checkBoxImport);
            this.panel2.Controls.Add(this.buttonNext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 349);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(686, 35);
            this.panel2.TabIndex = 4;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(506, 5);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 6;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonShare
            // 
            this.buttonShare.Location = new System.Drawing.Point(32, 7);
            this.buttonShare.Name = "buttonShare";
            this.buttonShare.Size = new System.Drawing.Size(75, 23);
            this.buttonShare.TabIndex = 5;
            this.buttonShare.Text = "Share...";
            this.toolTip1.SetToolTip(this.buttonShare, "Share your data with other plugin users");
            this.buttonShare.UseVisualStyleBackColor = true;
            this.buttonShare.Click += new System.EventHandler(this.buttonShare_Click);
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Location = new System.Drawing.Point(194, 12);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(62, 13);
            this.labelInstructions.TabIndex = 4;
            this.labelInstructions.Text = "placeholder";
            // 
            // checkBoxImport
            // 
            this.checkBoxImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxImport.AutoSize = true;
            this.checkBoxImport.Location = new System.Drawing.Point(588, 11);
            this.checkBoxImport.Name = "checkBoxImport";
            this.checkBoxImport.Size = new System.Drawing.Size(90, 17);
            this.checkBoxImport.TabIndex = 3;
            this.checkBoxImport.Text = "Parse Imports";
            this.toolTip1.SetToolTip(this.checkBoxImport, "Check to have the plugin process imported files");
            this.checkBoxImport.UseVisualStyleBackColor = true;
            this.checkBoxImport.CheckedChanged += new System.EventHandler(this.checkBoxImport_CheckedChanged);
            // 
            // buttonHelp
            // 
            this.buttonHelp.AutoSize = true;
            this.buttonHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonHelp.Location = new System.Drawing.Point(3, 7);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(23, 23);
            this.buttonHelp.TabIndex = 7;
            this.buttonHelp.Text = "?";
            this.toolTip1.SetToolTip(this.buttonHelp, "Visit the help on the project web page");
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // playerDataGridViewTextBoxColumn
            // 
            this.playerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.playerDataGridViewTextBoxColumn.DataPropertyName = "Player";
            this.playerDataGridViewTextBoxColumn.HeaderText = "Player";
            this.playerDataGridViewTextBoxColumn.Name = "playerDataGridViewTextBoxColumn";
            this.playerDataGridViewTextBoxColumn.ReadOnly = true;
            this.playerDataGridViewTextBoxColumn.Width = 61;
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.countDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.countDataGridViewTextBoxColumn.HeaderText = "Count";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.ReadOnly = true;
            this.countDataGridViewTextBoxColumn.Width = 60;
            // 
            // looterListBindingSource
            // 
            this.looterListBindingSource.DataSource = typeof(ACT_RoR_Parcels.LooterList);
            // 
            // RoRParcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "RoRParcel";
            this.Size = new System.Drawing.Size(686, 384);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lootersBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.looterListBindingSource)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private DataGridView dataGridView1;
        private Button buttonNext;
        private Panel panel1;
        private Panel panel2;
        private BindingSource looterListBindingSource;
        private BindingSource lootersBindingSource;
        private CheckBox checkBoxImport;
        private DataGridViewTextBoxColumn playerDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn DateColumn;
        private DataGridViewTextBoxColumn InRaid;
        private Label labelInstructions;
        private Button buttonShare;
        private Button buttonTest;

        #endregion

        Label lblStatus;    // The status label that appears in ACT's Plugin tab

        // looter data and persistence
        public List<Looter> looterList { get; set; }
        string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\RoR_Parcels.config.xml");
		XmlSerializer xmlSerializer;

        // log line parsing
		const string logTimeStampRegexStr = @"^\((?<secs>\d{10})\)\[.{24}\] ";
		const int timeLen = 39;
        Regex reLoot = new Regex(logTimeStampRegexStr + @"(?<who>\w+) loots?[^:]+:Locked Parcel:", RegexOptions.Compiled);
        Regex regexWhoRaid = new Regex(logTimeStampRegexStr + @"\/whoraid search results", RegexOptions.Compiled);
        Regex regexWho = new Regex(logTimeStampRegexStr + @"\[[^]]+\] (?<member>\w+) ", RegexOptions.Compiled);
        Regex regexWhoEnd = new Regex(logTimeStampRegexStr + @"\d+ players found", RegexOptions.Compiled);
        Regex regexJoined = new Regex(logTimeStampRegexStr + @"(?<member>\w+) has joined the raid", RegexOptions.Compiled);
        Regex regexLeft = new Regex(logTimeStampRegexStr + @"(?<member>\w+) has left the raid", RegexOptions.Compiled);

        // date/time combo box selection support
        ComboBox comboTime = null;
        
        // /whoraid support
        enum tWhoStates { idle, watching }; // /whoraid state machine states
        private tWhoStates eState = tWhoStates.idle;
        List<string> whoRaid = new List<string>(); //collects /whoraid names
        
        // UI thread support
        WindowsFormsSynchronizationContext mUiContext = new WindowsFormsSynchronizationContext();
        private ToolTip toolTip1;
        private Button buttonHelp;
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
                ActGlobals.oFormActMain.WriteExceptionLog(ex, "RoR Parcels Plugin Update Download");
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
                    string[] csv = player.Split(',');
                    Looter looter = new Looter();
                    for (int i = 0; i < csv.Length; i++)
                    {
                        string s = csv[i];
                        if (i == 0)
                        {
                            looter = looterList.Find(x => x.Player == s);
                            if (looter == null)
                            {
                                looter = new Looter { Player = s };
                                looterList.Add(looter);
                            }
                        }
                        else if (looter != null)
                        {
                            Int64 t;
                            if (Int64.TryParse(s, out t))
                            {
                                DateTime logTime = UnixSecondsToDateTime(t);
                                if (!looter.lootDates.Contains(logTime))
                                {
                                    looter.lootDates.Add(logTime);
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
					DateTime logTime = UnixSecondsToDateTime(secs);
					Looter looter = looterList.Find(x => x.Player == who);
					if (looter == null)
                    {
						looter = new Looter();
						looter.Player = who;
						looter.lootDates.Add(logTime);
						looterList.Add(looter);
                        mUiContext.Post(UiUpdateGrid, null);
                    }
                    else
                    {
						if(!looter.lootDates.Contains(logTime))
                        {
							looter.lootDates.Add(logTime);
						}
					}
                }
                else if((match = regexJoined.Match(logInfo.logLine)).Success)
                {
                    whoRaid.Add(match.Groups["member"].Value);
                    mUiContext.Post(UiWhoRaid, null);
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
        }

        private void UiWhoRaid( object o)
        {
            foreach(Looter looter in looterList)
            {
                if (whoRaid.Contains(looter.Player))
                    looter.InRaid = 1;
                else
                    looter.InRaid = 0;
            }
            foreach(string s in whoRaid)
            {
                Looter looter = looterList.Find(x => x.Player == s);
                if(looter == null)
                {
                    // add the /whoraid player to the looter list
                    looter = new Looter();
                    looter.Player = s;
                    looterList.Add(looter);
                }
            }
            lootersBindingSource.ResetBindings(false);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            LooterSorter looterSorter = new LooterSorter("InRaid", SortOrder.Descending);
            looterList.Sort(looterSorter);
            lootersBindingSource.ResetBindings(false);
            if (looterList.Count > 0)
            {
                if (looterList[0].InRaid == 1)
                {
                    int lowest = looterList[0].Count;
                    int i = 1;
                    for (; i < looterList.Count; i++)
                    {
                        if (looterList[i].Count > lowest || looterList[i].InRaid < 1)
                            break;
                    }
                    labelInstructions.Text = $"/ran {i} to choose the next looter.";
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
            //DataGridViewComboBoxCell comboCell = dataGridView1.Rows[e.RowIndex].Cells["DateColumn"] as DataGridViewComboBoxCell;
            //if (comboTime != null)
            //	comboCell.Value = comboTime.SelectedItem;
            //else if (comboCell.Items.Count > 0)
            //	comboCell.Value = comboCell.Items[comboCell.Items.Count-1];
            //else
            //	comboCell.Value = "";
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
                if (cell.ReadOnly)
                {
                    Debug.WriteLine($"Setting combo {e.RowIndex}/{e.ColumnIndex} to read/write");
                    cell.ReadOnly = false;
                }

                if (cell.DataSource == null)
                {
                    // Set the DataSource of the ComboBox based on the current row's custom class instance
                    cell.DataSource = looterList[e.RowIndex].lootDates;
                    cell.ValueType = typeof(DateTime);

                    int dates = looterList[e.RowIndex].DateCount();
                    if (dates > 0)
                    {
                        cell.Value = looterList[e.RowIndex].lootDates[dates - 1];
                    }
                }
                else
                {
                    if (comboTime != null)
                        if(looterList[e.RowIndex].DateCount() > comboTime.SelectedIndex)
                            cell.Value = looterList[e.RowIndex].lootDates[comboTime.SelectedIndex];
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
            LooterSorter looterSorter = new LooterSorter(colHdr, (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag);
            looterList.Sort(looterSorter);
            lootersBindingSource.ResetBindings(false);
            dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = (SortOrder)dataGridView1.Columns[e.ColumnIndex].Tag;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
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
            //string data = File.ReadAllText(ActGlobals.oFormActMain.GameMacroFolder + @"\raid-parcel1.txt");
            //string p = data.Replace(" r <Parcel P='", "").Replace("' />", "");
            //UiParseXml(p);

            Task<FileInfo> ftask = Task.Run(() => { return GetRemoteFileAsync(); });
            ftask.Wait();
            if (ftask.Result != null)
            {
                Debug.WriteLine($"download ok: {ftask.Result.FullName}");
            }

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/jeffjl74/ACT_RoR_Parcels#Locked-Parcel-Plugin-for-Advanced-Combat-Tracker");
        }
    }
}
