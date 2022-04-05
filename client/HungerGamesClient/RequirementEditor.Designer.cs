namespace HungerGamesClient
{
    partial class RequirementEditor
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TypeDropdown1 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.attributeDropdown1 = new System.Windows.Forms.ComboBox();
            this.RawBox1 = new System.Windows.Forms.TextBox();
            this.comparisionBox = new System.Windows.Forms.ComboBox();
            this.TypeDropdown2 = new System.Windows.Forms.ComboBox();
            this.attributeDropdown2 = new System.Windows.Forms.ComboBox();
            this.RawBox2 = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.timeOfDayDropdown = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(46, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(587, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TypeDropdown1
            // 
            this.TypeDropdown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeDropdown1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeDropdown1.FormattingEnabled = true;
            this.TypeDropdown1.Location = new System.Drawing.Point(3, 3);
            this.TypeDropdown1.Name = "TypeDropdown1";
            this.TypeDropdown1.Size = new System.Drawing.Size(102, 23);
            this.TypeDropdown1.TabIndex = 1;
            this.TypeDropdown1.SelectedIndexChanged += new System.EventHandler(this.TypeDropdown1_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.TypeDropdown1);
            this.flowLayoutPanel1.Controls.Add(this.attributeDropdown1);
            this.flowLayoutPanel1.Controls.Add(this.RawBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(46, 56);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(221, 66);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // attributeDropdown1
            // 
            this.attributeDropdown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeDropdown1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attributeDropdown1.FormattingEnabled = true;
            this.attributeDropdown1.Items.AddRange(new object[] {
            "Status",
            "Environment",
            "Name",
            "Flag"});
            this.attributeDropdown1.Location = new System.Drawing.Point(111, 3);
            this.attributeDropdown1.Name = "attributeDropdown1";
            this.attributeDropdown1.Size = new System.Drawing.Size(100, 23);
            this.attributeDropdown1.TabIndex = 4;
            this.attributeDropdown1.SelectedIndexChanged += new System.EventHandler(this.attributeDropdown1_SelectedIndexChanged);
            // 
            // RawBox1
            // 
            this.RawBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawBox1.Location = new System.Drawing.Point(3, 32);
            this.RawBox1.Name = "RawBox1";
            this.RawBox1.Size = new System.Drawing.Size(208, 23);
            this.RawBox1.TabIndex = 2;
            this.RawBox1.TextChanged += new System.EventHandler(this.RawBox1_TextChanged);
            // 
            // comparisionBox
            // 
            this.comparisionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comparisionBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comparisionBox.FormattingEnabled = true;
            this.comparisionBox.Items.AddRange(new object[] {
            "is",
            "is not",
            "contains",
            "does not contain",
            "is greater than",
            "is less than"});
            this.comparisionBox.Location = new System.Drawing.Point(273, 59);
            this.comparisionBox.Name = "comparisionBox";
            this.comparisionBox.Size = new System.Drawing.Size(131, 23);
            this.comparisionBox.TabIndex = 6;
            this.comparisionBox.SelectedIndexChanged += new System.EventHandler(this.comparisionBox_SelectedIndexChanged);
            // 
            // TypeDropdown2
            // 
            this.TypeDropdown2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeDropdown2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeDropdown2.FormattingEnabled = true;
            this.TypeDropdown2.Location = new System.Drawing.Point(3, 3);
            this.TypeDropdown2.Name = "TypeDropdown2";
            this.TypeDropdown2.Size = new System.Drawing.Size(102, 23);
            this.TypeDropdown2.TabIndex = 1;
            this.TypeDropdown2.SelectedIndexChanged += new System.EventHandler(this.TypeDropdown2_SelectedIndexChanged);
            // 
            // attributeDropdown2
            // 
            this.attributeDropdown2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeDropdown2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attributeDropdown2.FormattingEnabled = true;
            this.attributeDropdown2.Items.AddRange(new object[] {
            "Status",
            "Environment",
            "Name"});
            this.attributeDropdown2.Location = new System.Drawing.Point(111, 3);
            this.attributeDropdown2.Name = "attributeDropdown2";
            this.attributeDropdown2.Size = new System.Drawing.Size(100, 23);
            this.attributeDropdown2.TabIndex = 4;
            this.attributeDropdown2.SelectedIndexChanged += new System.EventHandler(this.attributeDropdown2_SelectedIndexChanged);
            // 
            // RawBox2
            // 
            this.RawBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawBox2.Location = new System.Drawing.Point(3, 32);
            this.RawBox2.Name = "RawBox2";
            this.RawBox2.Size = new System.Drawing.Size(102, 23);
            this.RawBox2.TabIndex = 2;
            this.RawBox2.TextChanged += new System.EventHandler(this.RawBox2_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(423, 105);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(210, 28);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.TypeDropdown2);
            this.flowLayoutPanel2.Controls.Add(this.attributeDropdown2);
            this.flowLayoutPanel2.Controls.Add(this.RawBox2);
            this.flowLayoutPanel2.Controls.Add(this.timeOfDayDropdown);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(410, 56);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(223, 30);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // timeOfDayDropdown
            // 
            this.timeOfDayDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeOfDayDropdown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeOfDayDropdown.FormattingEnabled = true;
            this.timeOfDayDropdown.Items.AddRange(new object[] {
            "Morning",
            "Afternoon",
            "Evening",
            "Night"});
            this.timeOfDayDropdown.Location = new System.Drawing.Point(111, 32);
            this.timeOfDayDropdown.Name = "timeOfDayDropdown";
            this.timeOfDayDropdown.Size = new System.Drawing.Size(102, 23);
            this.timeOfDayDropdown.TabIndex = 5;
            this.timeOfDayDropdown.SelectedIndexChanged += new System.EventHandler(this.timeOfDayDropdown_SelectedIndexChanged);
            // 
            // RequirementEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 162);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.comparisionBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RequirementEditor";
            this.Text = "Edit Requirement";
            this.Load += new System.EventHandler(this.RequirementEditor_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox TypeDropdown1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox RawBox1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox attributeDropdown1;
        private System.Windows.Forms.ComboBox TypeDropdown2;
        private System.Windows.Forms.TextBox RawBox2;
        private System.Windows.Forms.ComboBox attributeDropdown2;
        private System.Windows.Forms.ComboBox comparisionBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ComboBox timeOfDayDropdown;
    }
}