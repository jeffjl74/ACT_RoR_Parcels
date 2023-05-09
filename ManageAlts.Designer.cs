namespace ACT_RoR_Parcels
{
    partial class ManageAlts
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxAlts = new System.Windows.Forms.ListBox();
            this.labelMain = new System.Windows.Forms.Label();
            this.listBoxRaiders = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxSplit = new System.Windows.Forms.GroupBox();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.groupBoxLink = new System.Windows.Forms.GroupBox();
            this.buttonLink = new System.Windows.Forms.Button();
            this.radioButtonMin = new System.Windows.Forms.RadioButton();
            this.radioButtonMax = new System.Windows.Forms.RadioButton();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBoxSplit.SuspendLayout();
            this.groupBoxLink.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxAlts
            // 
            this.listBoxAlts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxAlts.FormattingEnabled = true;
            this.listBoxAlts.Location = new System.Drawing.Point(12, 30);
            this.listBoxAlts.Name = "listBoxAlts";
            this.listBoxAlts.Size = new System.Drawing.Size(179, 160);
            this.listBoxAlts.TabIndex = 0;
            this.listBoxAlts.SelectedIndexChanged += new System.EventHandler(this.listBoxAlts_SelectedIndexChanged);
            // 
            // labelMain
            // 
            this.labelMain.AutoSize = true;
            this.labelMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMain.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelMain.Location = new System.Drawing.Point(12, 13);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(34, 13);
            this.labelMain.TabIndex = 1;
            this.labelMain.Text = "Main";
            // 
            // listBoxRaiders
            // 
            this.listBoxRaiders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxRaiders.FormattingEnabled = true;
            this.listBoxRaiders.Location = new System.Drawing.Point(233, 30);
            this.listBoxRaiders.Name = "listBoxRaiders";
            this.listBoxRaiders.Size = new System.Drawing.Size(179, 160);
            this.listBoxRaiders.TabIndex = 2;
            this.listBoxRaiders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxRaiders_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Raiders (double-click to add alt)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listBoxRaiders);
            this.panel1.Controls.Add(this.labelMain);
            this.panel1.Controls.Add(this.listBoxAlts);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 212);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxSplit);
            this.panel2.Controls.Add(this.buttonDone);
            this.panel2.Controls.Add(this.groupBoxLink);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 212);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 177);
            this.panel2.TabIndex = 5;
            // 
            // groupBoxSplit
            // 
            this.groupBoxSplit.Controls.Add(this.richTextBox1);
            this.groupBoxSplit.Controls.Add(this.buttonSplit);
            this.groupBoxSplit.Enabled = false;
            this.groupBoxSplit.Location = new System.Drawing.Point(233, 6);
            this.groupBoxSplit.Name = "groupBoxSplit";
            this.groupBoxSplit.Size = new System.Drawing.Size(179, 126);
            this.groupBoxSplit.TabIndex = 2;
            this.groupBoxSplit.TabStop = false;
            this.groupBoxSplit.Text = "Split Alt";
            // 
            // buttonSplit
            // 
            this.buttonSplit.Location = new System.Drawing.Point(51, 91);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(75, 23);
            this.buttonSplit.TabIndex = 1;
            this.buttonSplit.Text = "Split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDone.Location = new System.Drawing.Point(171, 142);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // groupBoxLink
            // 
            this.groupBoxLink.Controls.Add(this.buttonLink);
            this.groupBoxLink.Controls.Add(this.radioButtonMin);
            this.groupBoxLink.Controls.Add(this.radioButtonMax);
            this.groupBoxLink.Controls.Add(this.radioButtonAdd);
            this.groupBoxLink.Enabled = false;
            this.groupBoxLink.Location = new System.Drawing.Point(12, 6);
            this.groupBoxLink.Name = "groupBoxLink";
            this.groupBoxLink.Size = new System.Drawing.Size(179, 126);
            this.groupBoxLink.TabIndex = 0;
            this.groupBoxLink.TabStop = false;
            this.groupBoxLink.Text = "Link Alts";
            // 
            // buttonLink
            // 
            this.buttonLink.Location = new System.Drawing.Point(49, 91);
            this.buttonLink.Name = "buttonLink";
            this.buttonLink.Size = new System.Drawing.Size(75, 23);
            this.buttonLink.TabIndex = 3;
            this.buttonLink.Text = "Link";
            this.buttonLink.UseVisualStyleBackColor = true;
            this.buttonLink.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // radioButtonMin
            // 
            this.radioButtonMin.AutoSize = true;
            this.radioButtonMin.Location = new System.Drawing.Point(6, 65);
            this.radioButtonMin.Name = "radioButtonMin";
            this.radioButtonMin.Size = new System.Drawing.Size(101, 17);
            this.radioButtonMin.TabIndex = 2;
            this.radioButtonMin.Text = "Keep Min Count";
            this.radioButtonMin.UseVisualStyleBackColor = true;
            // 
            // radioButtonMax
            // 
            this.radioButtonMax.AutoSize = true;
            this.radioButtonMax.Checked = true;
            this.radioButtonMax.Location = new System.Drawing.Point(6, 42);
            this.radioButtonMax.Name = "radioButtonMax";
            this.radioButtonMax.Size = new System.Drawing.Size(104, 17);
            this.radioButtonMax.TabIndex = 1;
            this.radioButtonMax.TabStop = true;
            this.radioButtonMax.Text = "Keep Max Count";
            this.radioButtonMax.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.Size = new System.Drawing.Size(104, 17);
            this.radioButtonAdd.TabIndex = 0;
            this.radioButtonAdd.Text = "Add Loot Counts";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(3, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(175, 66);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "If you detach an alt from the selected player, loot counts are not changed. Use t" +
    "he Add Loot and Delete Loot context menus to make changes, if required.";
            // 
            // ManageAlts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 389);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageAlts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ManageAlts";
            this.Load += new System.EventHandler(this.ManageAlts_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBoxSplit.ResumeLayout(false);
            this.groupBoxLink.ResumeLayout(false);
            this.groupBoxLink.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAlts;
        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.ListBox listBoxRaiders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxLink;
        private System.Windows.Forms.RadioButton radioButtonMin;
        private System.Windows.Forms.RadioButton radioButtonMax;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.GroupBox groupBoxSplit;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}