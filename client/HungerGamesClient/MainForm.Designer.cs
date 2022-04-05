namespace HungerGamesClient
{
    partial class MainForm
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
            this.logPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Dropdown = new System.Windows.Forms.ComboBox();
            this.PrimaryTextbox = new System.Windows.Forms.TextBox();
            this.andLabel = new System.Windows.Forms.Label();
            this.secondaryTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.loginLabel = new System.Windows.Forms.Label();
            this.votingBoothButton = new System.Windows.Forms.Button();
            this.editSimButton = new System.Windows.Forms.Button();
            this.editScenesButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.logPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logPanel
            // 
            this.logPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logPanel.AutoScroll = true;
            this.logPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.logPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logPanel.Controls.Add(this.panel1);
            this.logPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.logPanel.Location = new System.Drawing.Point(12, 110);
            this.logPanel.Name = "logPanel";
            this.logPanel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.logPanel.Size = new System.Drawing.Size(947, 473);
            this.logPanel.TabIndex = 13;
            this.logPanel.WrapContents = false;
            this.logPanel.SizeChanged += new System.EventHandler(this.logPanel_ChangeSize);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(20, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 0);
            this.panel1.TabIndex = 1;
            // 
            // Dropdown
            // 
            this.Dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dropdown.FormattingEnabled = true;
            this.Dropdown.Items.AddRange(new object[] {
            "Show entire log",
            "Show most recent...",
            "Show between..."});
            this.Dropdown.Location = new System.Drawing.Point(3, 3);
            this.Dropdown.Name = "Dropdown";
            this.Dropdown.Size = new System.Drawing.Size(194, 30);
            this.Dropdown.TabIndex = 17;
            // 
            // PrimaryTextbox
            // 
            this.PrimaryTextbox.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrimaryTextbox.Location = new System.Drawing.Point(203, 3);
            this.PrimaryTextbox.Name = "PrimaryTextbox";
            this.PrimaryTextbox.Size = new System.Drawing.Size(38, 29);
            this.PrimaryTextbox.TabIndex = 18;
            this.PrimaryTextbox.Visible = false;
            // 
            // andLabel
            // 
            this.andLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.andLabel.AutoSize = true;
            this.andLabel.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.andLabel.Location = new System.Drawing.Point(247, 7);
            this.andLabel.Name = "andLabel";
            this.andLabel.Size = new System.Drawing.Size(38, 22);
            this.andLabel.TabIndex = 19;
            this.andLabel.Text = "and";
            this.andLabel.Visible = false;
            // 
            // secondaryTextBox
            // 
            this.secondaryTextBox.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryTextBox.Location = new System.Drawing.Point(291, 3);
            this.secondaryTextBox.Name = "secondaryTextBox";
            this.secondaryTextBox.Size = new System.Drawing.Size(38, 29);
            this.secondaryTextBox.TabIndex = 20;
            this.secondaryTextBox.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.Dropdown);
            this.flowLayoutPanel1.Controls.Add(this.PrimaryTextbox);
            this.flowLayoutPanel1.Controls.Add(this.andLabel);
            this.flowLayoutPanel1.Controls.Add(this.secondaryTextBox);
            this.flowLayoutPanel1.Controls.Add(this.RefreshButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(468, 35);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Image = global::HungerGamesClient.Properties.Resources.refresh;
            this.RefreshButton.Location = new System.Drawing.Point(335, 3);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(130, 30);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RefreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // loginLabel
            // 
            this.loginLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginLabel.Font = new System.Drawing.Font("Baskerville Old Face", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.loginLabel.Location = new System.Drawing.Point(483, 68);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(476, 38);
            this.loginLabel.TabIndex = 22;
            this.loginLabel.Text = "User:";
            this.loginLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.loginLabel.Click += new System.EventHandler(this.loginLabel_Click);
            // 
            // votingBoothButton
            // 
            this.votingBoothButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.votingBoothButton.Image = global::HungerGamesClient.Properties.Resources.voteHand;
            this.votingBoothButton.Location = new System.Drawing.Point(163, 12);
            this.votingBoothButton.Name = "votingBoothButton";
            this.votingBoothButton.Size = new System.Drawing.Size(145, 53);
            this.votingBoothButton.TabIndex = 15;
            this.votingBoothButton.Text = "Voting Booth";
            this.votingBoothButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.votingBoothButton.UseVisualStyleBackColor = true;
            this.votingBoothButton.Click += new System.EventHandler(this.votingBoothButton_Click);
            // 
            // editSimButton
            // 
            this.editSimButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSimButton.Image = global::HungerGamesClient.Properties.Resources.gear;
            this.editSimButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.editSimButton.Location = new System.Drawing.Point(465, 12);
            this.editSimButton.Name = "editSimButton";
            this.editSimButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.editSimButton.Size = new System.Drawing.Size(145, 52);
            this.editSimButton.TabIndex = 25;
            this.editSimButton.Text = "Game Manager";
            this.editSimButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editSimButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editSimButton.UseVisualStyleBackColor = true;
            this.editSimButton.Click += new System.EventHandler(this.editSimButton_Click);
            // 
            // editScenesButton
            // 
            this.editScenesButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editScenesButton.Image = global::HungerGamesClient.Properties.Resources.notes1;
            this.editScenesButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.editScenesButton.Location = new System.Drawing.Point(314, 12);
            this.editScenesButton.Name = "editScenesButton";
            this.editScenesButton.Size = new System.Drawing.Size(145, 52);
            this.editScenesButton.TabIndex = 24;
            this.editScenesButton.Text = "Scene Manager";
            this.editScenesButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editScenesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editScenesButton.UseVisualStyleBackColor = true;
            this.editScenesButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::HungerGamesClient.Properties.Resources.roster;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "Current Roster";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 595);
            this.Controls.Add(this.votingBoothButton);
            this.Controls.Add(this.editSimButton);
            this.Controls.Add(this.editScenesButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.logPanel);
            this.MaximumSize = new System.Drawing.Size(2083, 1200);
            this.MinimumSize = new System.Drawing.Size(636, 200);
            this.Name = "MainForm";
            this.Text = "Hunger Games";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.logPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel logPanel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button votingBoothButton;
        private System.Windows.Forms.ComboBox Dropdown;
        private System.Windows.Forms.TextBox PrimaryTextbox;
        private System.Windows.Forms.Label andLabel;
        private System.Windows.Forms.TextBox secondaryTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button editScenesButton;
        private System.Windows.Forms.Button editSimButton;
        private System.Windows.Forms.Panel panel1;
    }
}

