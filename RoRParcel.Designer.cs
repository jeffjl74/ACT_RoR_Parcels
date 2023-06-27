using System.Windows.Forms;

namespace ACT_RoR_Parcels
{
    partial class RoRParcel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.InRaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T1Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.T2Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T2DateColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lootersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonShare = new System.Windows.Forms.Button();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.checkBoxImport = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripLoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripInRaid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripAlts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manageAltsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.precludeT1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Player = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.looterListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lootersBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStripLoot.SuspendLayout();
            this.contextMenuStripInRaid.SuspendLayout();
            this.contextMenuStripAlts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.looterListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Player,
            this.InRaid,
            this.T1Count,
            this.DateColumn,
            this.T2Count,
            this.T2DateColumn});
            this.dataGridView1.DataSource = this.lootersBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(686, 349);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridView1_CellContextMenuStripNeeded);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridView1_CellToolTipTextNeeded);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // InRaid
            // 
            this.InRaid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.InRaid.DataPropertyName = "InRaid";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "Y;?; ";
            this.InRaid.DefaultCellStyle = dataGridViewCellStyle1;
            this.InRaid.HeaderText = "In Raid";
            this.InRaid.Name = "InRaid";
            this.InRaid.Width = 66;
            // 
            // T1Count
            // 
            this.T1Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.T1Count.DataPropertyName = "T1Count";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T1Count.DefaultCellStyle = dataGridViewCellStyle2;
            this.T1Count.HeaderText = "T1 Count";
            this.T1Count.Name = "T1Count";
            this.T1Count.ReadOnly = true;
            this.T1Count.Width = 76;
            // 
            // DateColumn
            // 
            this.DateColumn.HeaderText = "T1 Dates";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DateColumn.Width = 130;
            // 
            // T2Count
            // 
            this.T2Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.T2Count.DataPropertyName = "T2Count";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T2Count.DefaultCellStyle = dataGridViewCellStyle3;
            this.T2Count.HeaderText = "T2 Count";
            this.T2Count.Name = "T2Count";
            this.T2Count.ReadOnly = true;
            this.T2Count.Width = 76;
            // 
            // T2DateColumn
            // 
            this.T2DateColumn.HeaderText = "T2 Dates";
            this.T2DateColumn.Name = "T2DateColumn";
            this.T2DateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.T2DateColumn.Width = 130;
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
            // contextMenuStripLoot
            // 
            this.contextMenuStripLoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLootToolStripMenuItem,
            this.deleteLootToolStripMenuItem});
            this.contextMenuStripLoot.Name = "contextMenuStrip1";
            this.contextMenuStripLoot.Size = new System.Drawing.Size(144, 48);
            // 
            // addLootToolStripMenuItem
            // 
            this.addLootToolStripMenuItem.Name = "addLootToolStripMenuItem";
            this.addLootToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addLootToolStripMenuItem.Text = "Add Loot...";
            this.addLootToolStripMenuItem.Click += new System.EventHandler(this.addLootToolStripMenuItem_Click);
            // 
            // deleteLootToolStripMenuItem
            // 
            this.deleteLootToolStripMenuItem.Name = "deleteLootToolStripMenuItem";
            this.deleteLootToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteLootToolStripMenuItem.Text = "Delete Loot...";
            this.deleteLootToolStripMenuItem.Click += new System.EventHandler(this.deleteLootToolStripMenuItem_Click);
            // 
            // contextMenuStripInRaid
            // 
            this.contextMenuStripInRaid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem,
            this.precludeT1ToolStripMenuItem});
            this.contextMenuStripInRaid.Name = "contextMenuStrip2";
            this.contextMenuStripInRaid.Size = new System.Drawing.Size(181, 70);
            this.contextMenuStripInRaid.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripInRaid_Opening);
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleToolStripMenuItem.Text = "Toggle";
            this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
            // 
            // contextMenuStripAlts
            // 
            this.contextMenuStripAlts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageAltsToolStripMenuItem});
            this.contextMenuStripAlts.Name = "contextMenuStripAlts";
            this.contextMenuStripAlts.Size = new System.Drawing.Size(150, 26);
            // 
            // manageAltsToolStripMenuItem
            // 
            this.manageAltsToolStripMenuItem.Name = "manageAltsToolStripMenuItem";
            this.manageAltsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.manageAltsToolStripMenuItem.Text = "Manage Alts...";
            this.manageAltsToolStripMenuItem.Click += new System.EventHandler(this.manageAltsToolStripMenuItem_Click);
            // 
            // precludeT1ToolStripMenuItem
            // 
            this.precludeT1ToolStripMenuItem.Name = "precludeT1ToolStripMenuItem";
            this.precludeT1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.precludeT1ToolStripMenuItem.Text = "Preclude";
            this.precludeT1ToolStripMenuItem.Click += new System.EventHandler(this.precludeT1ToolStripMenuItem_Click);
            // 
            // Player
            // 
            this.Player.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Player.DataPropertyName = "Player";
            this.Player.HeaderText = "Player";
            this.Player.Name = "Player";
            this.Player.ReadOnly = true;
            this.Player.Width = 61;
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
            this.contextMenuStripLoot.ResumeLayout(false);
            this.contextMenuStripInRaid.ResumeLayout(false);
            this.contextMenuStripAlts.ResumeLayout(false);
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
        private Label labelInstructions;
        private Button buttonShare;
        private Button buttonTest;
        private ToolTip toolTip1;
        private Button buttonHelp;

        #endregion
        private ContextMenuStrip contextMenuStripLoot;
        private ToolStripMenuItem addLootToolStripMenuItem;
        private ToolStripMenuItem deleteLootToolStripMenuItem;
        private ContextMenuStrip contextMenuStripInRaid;
        private ToolStripMenuItem toggleToolStripMenuItem;
        private ContextMenuStrip contextMenuStripAlts;
        private ToolStripMenuItem manageAltsToolStripMenuItem;
        private DataGridViewTextBoxColumn Player;
        private DataGridViewTextBoxColumn InRaid;
        private DataGridViewTextBoxColumn T1Count;
        private DataGridViewComboBoxColumn DateColumn;
        private DataGridViewTextBoxColumn T2Count;
        private DataGridViewComboBoxColumn T2DateColumn;
        private ToolStripMenuItem precludeT1ToolStripMenuItem;
    }
}
