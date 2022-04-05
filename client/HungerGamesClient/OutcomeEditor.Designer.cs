namespace HungerGamesClient
{
    partial class OutcomeEditor
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.characterDropdown = new System.Windows.Forms.ComboBox();
            this.functionDropdown = new System.Windows.Forms.ComboBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.prepositionLabel = new System.Windows.Forms.Label();
            this.inputBox2 = new System.Windows.Forms.TextBox();
            this.typeDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.characterDropdown);
            this.flowLayoutPanel1.Controls.Add(this.functionDropdown);
            this.flowLayoutPanel1.Controls.Add(this.inputBox);
            this.flowLayoutPanel1.Controls.Add(this.prepositionLabel);
            this.flowLayoutPanel1.Controls.Add(this.inputBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(289, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 113);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // characterDropdown
            // 
            this.characterDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.characterDropdown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.characterDropdown.FormattingEnabled = true;
            this.characterDropdown.Location = new System.Drawing.Point(3, 3);
            this.characterDropdown.Name = "characterDropdown";
            this.characterDropdown.Size = new System.Drawing.Size(116, 21);
            this.characterDropdown.TabIndex = 3;
            // 
            // functionDropdown
            // 
            this.functionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.functionDropdown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.functionDropdown.FormattingEnabled = true;
            this.functionDropdown.Location = new System.Drawing.Point(125, 3);
            this.functionDropdown.Name = "functionDropdown";
            this.functionDropdown.Size = new System.Drawing.Size(123, 21);
            this.functionDropdown.TabIndex = 1;
            this.functionDropdown.SelectedIndexChanged += new System.EventHandler(this.UpdateEffectInListBox);
            this.functionDropdown.SelectedIndexChanged += new System.EventHandler(this.functionDropdown_SelectedIndexChanged);
            // 
            // inputBox
            // 
            this.inputBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.Location = new System.Drawing.Point(3, 30);
            this.inputBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(245, 22);
            this.inputBox.TabIndex = 2;
            this.inputBox.TextChanged += new System.EventHandler(this.UpdateEffectInListBox);
            this.inputBox.Leave += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // prepositionLabel
            // 
            this.prepositionLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prepositionLabel.Location = new System.Drawing.Point(3, 55);
            this.prepositionLabel.Name = "prepositionLabel";
            this.prepositionLabel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.prepositionLabel.Size = new System.Drawing.Size(22, 29);
            this.prepositionLabel.TabIndex = 5;
            this.prepositionLabel.Text = "to";
            this.prepositionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.prepositionLabel.Visible = false;
            // 
            // inputBox2
            // 
            this.inputBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox2.Location = new System.Drawing.Point(31, 58);
            this.inputBox2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.inputBox2.Name = "inputBox2";
            this.inputBox2.Size = new System.Drawing.Size(217, 22);
            this.inputBox2.TabIndex = 4;
            this.inputBox2.Visible = false;
            this.inputBox2.TextChanged += new System.EventHandler(this.UpdateEffectInListBox);
            // 
            // typeDropdown
            // 
            this.typeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeDropdown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeDropdown.FormattingEnabled = true;
            this.typeDropdown.Items.AddRange(new object[] {
            "Positive",
            "Neutral",
            "Harmful"});
            this.typeDropdown.Location = new System.Drawing.Point(87, 12);
            this.typeDropdown.Name = "typeDropdown";
            this.typeDropdown.Size = new System.Drawing.Size(121, 21);
            this.typeDropdown.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(87, 33);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(636, 72);
            this.descriptionBox.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 19);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.listBox1.MinimumSize = new System.Drawing.Size(150, 100);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(280, 95);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::HungerGamesClient.Properties.Resources.saveIcon;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.Location = new System.Drawing.Point(579, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 45);
            this.button3.TabIndex = 11;
            this.button3.Text = "Save";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(15, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 174);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Image = global::HungerGamesClient.Properties.Resources.smallMinus;
            this.button1.Location = new System.Drawing.Point(93, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 35);
            this.button1.TabIndex = 9;
            this.button1.Text = "Remove";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::HungerGamesClient.Properties.Resources.smallPlus;
            this.button2.Location = new System.Drawing.Point(3, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 36);
            this.button2.TabIndex = 10;
            this.button2.Text = "Add";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Type:";
            // 
            // OutcomeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 297);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.typeDropdown);
            this.Controls.Add(this.button3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(638, 268);
            this.Name = "OutcomeEditor";
            this.Text = "Edit Outcome";
            this.Load += new System.EventHandler(this.OutcomeEditor_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox functionDropdown;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.ComboBox typeDropdown;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox characterDropdown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label prepositionLabel;
        private System.Windows.Forms.TextBox inputBox2;
    }
}