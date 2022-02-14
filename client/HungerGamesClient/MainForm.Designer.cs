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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.logPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.votingBoothButton = new System.Windows.Forms.Button();
            this.Dropdown = new System.Windows.Forms.ComboBox();
            this.PrimaryTextbox = new System.Windows.Forms.TextBox();
            this.andLabel = new System.Windows.Forms.Label();
            this.secondaryTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loginLabel = new System.Windows.Forms.Label();
            this.logPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
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
            this.logPanel.Controls.Add(this.tableLayoutPanel2);
            this.logPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.logPanel.Location = new System.Drawing.Point(12, 72);
            this.logPanel.Name = "logPanel";
            this.logPanel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.logPanel.Size = new System.Drawing.Size(1043, 480);
            this.logPanel.TabIndex = 13;
            this.logPanel.WrapContents = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox3, 0, 0);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 3);
            this.tableLayoutPanel2.MaximumSize = new System.Drawing.Size(10000, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(991, 1);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(4, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(323, 1);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(370, 3);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(86, 30);
            this.RefreshButton.TabIndex = 14;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // votingBoothButton
            // 
            this.votingBoothButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.votingBoothButton.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.votingBoothButton.Location = new System.Drawing.Point(906, 15);
            this.votingBoothButton.Name = "votingBoothButton";
            this.votingBoothButton.Size = new System.Drawing.Size(149, 30);
            this.votingBoothButton.TabIndex = 15;
            this.votingBoothButton.Text = "Voting Booth";
            this.votingBoothButton.UseVisualStyleBackColor = true;
            this.votingBoothButton.Click += new System.EventHandler(this.votingBoothButton_Click);
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
            this.Dropdown.Size = new System.Drawing.Size(229, 30);
            this.Dropdown.TabIndex = 17;
            // 
            // PrimaryTextbox
            // 
            this.PrimaryTextbox.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrimaryTextbox.Location = new System.Drawing.Point(238, 3);
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
            this.andLabel.Location = new System.Drawing.Point(282, 7);
            this.andLabel.Name = "andLabel";
            this.andLabel.Size = new System.Drawing.Size(38, 22);
            this.andLabel.TabIndex = 19;
            this.andLabel.Text = "and";
            this.andLabel.Visible = false;
            // 
            // secondaryTextBox
            // 
            this.secondaryTextBox.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryTextBox.Location = new System.Drawing.Point(326, 3);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(462, 54);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // loginLabel
            // 
            this.loginLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginLabel.Font = new System.Drawing.Font("Baskerville Old Face", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.loginLabel.Location = new System.Drawing.Point(802, 48);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(256, 18);
            this.loginLabel.TabIndex = 22;
            this.loginLabel.Text = "User:";
            this.loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.loginLabel.Click += new System.EventHandler(this.loginLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 564);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.votingBoothButton);
            this.Controls.Add(this.logPanel);
            this.MaximumSize = new System.Drawing.Size(1083, 1200);
            this.MinimumSize = new System.Drawing.Size(300, 39);
            this.Name = "MainForm";
            this.Text = "Hunger Games";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel logPanel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button votingBoothButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox Dropdown;
        private System.Windows.Forms.TextBox PrimaryTextbox;
        private System.Windows.Forms.Label andLabel;
        private System.Windows.Forms.TextBox secondaryTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label loginLabel;
    }
}

