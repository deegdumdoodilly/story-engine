namespace HungerGamesClient
{
    partial class EditSim
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
            this.actorListBox = new System.Windows.Forms.ListBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.environmentBox = new System.Windows.Forms.TextBox();
            this.statusListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.removeStatus = new System.Windows.Forms.Button();
            this.addStatus = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.resolvePerformanceButton = new System.Windows.Forms.Button();
            this.assignScenesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dayInput = new System.Windows.Forms.NumericUpDown();
            this.timeInput = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resetVotingHistoryButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flagBox = new System.Windows.Forms.ListBox();
            this.addFlag = new System.Windows.Forms.Button();
            this.removeFlag = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dayInput)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // actorListBox
            // 
            this.actorListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.actorListBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actorListBox.FormattingEnabled = true;
            this.actorListBox.ItemHeight = 18;
            this.actorListBox.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3"});
            this.actorListBox.Location = new System.Drawing.Point(6, 17);
            this.actorListBox.Name = "actorListBox";
            this.actorListBox.Size = new System.Drawing.Size(208, 274);
            this.actorListBox.TabIndex = 3;
            this.actorListBox.SelectedIndexChanged += new System.EventHandler(this.actorListBox_SelectedIndexChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Font = new System.Drawing.Font("Baskerville Old Face", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(10, 21);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(634, 31);
            this.nameTextBox.TabIndex = 4;
            this.nameTextBox.TextChanged += new System.EventHandler(this.multi_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Environment:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // environmentBox
            // 
            this.environmentBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.environmentBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.environmentBox.Location = new System.Drawing.Point(108, 90);
            this.environmentBox.Name = "environmentBox";
            this.environmentBox.Size = new System.Drawing.Size(537, 26);
            this.environmentBox.TabIndex = 6;
            this.environmentBox.TextChanged += new System.EventHandler(this.multi_TextChanged);
            // 
            // statusListBox
            // 
            this.statusListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusListBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusListBox.FormattingEnabled = true;
            this.statusListBox.ItemHeight = 18;
            this.statusListBox.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3"});
            this.statusListBox.Location = new System.Drawing.Point(11, 19);
            this.statusListBox.Name = "statusListBox";
            this.statusListBox.Size = new System.Drawing.Size(299, 94);
            this.statusListBox.TabIndex = 7;
            this.statusListBox.SelectedIndexChanged += new System.EventHandler(this.statusListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.actorListBox);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(876, 358);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actors";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::HungerGamesClient.Properties.Resources.smallMinus;
            this.button2.Location = new System.Drawing.Point(113, 315);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 37);
            this.button2.TabIndex = 11;
            this.button2.Text = "Remove Actor";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::HungerGamesClient.Properties.Resources.smallPlus;
            this.button1.Location = new System.Drawing.Point(6, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add    Actor";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.urlBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.saveButton);
            this.groupBox3.Controls.Add(this.nameTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.environmentBox);
            this.groupBox3.Location = new System.Drawing.Point(220, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(650, 331);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Edit Actor";
            // 
            // urlBox
            // 
            this.urlBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlBox.Location = new System.Drawing.Point(108, 58);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(537, 26);
            this.urlBox.TabIndex = 13;
            this.urlBox.TextChanged += new System.EventHandler(this.multi_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Image URL:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Enabled = false;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Image = global::HungerGamesClient.Properties.Resources.smallSave;
            this.saveButton.Location = new System.Drawing.Point(476, 289);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(169, 36);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // removeStatus
            // 
            this.removeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeStatus.Image = global::HungerGamesClient.Properties.Resources.smallMinus;
            this.removeStatus.Location = new System.Drawing.Point(113, 119);
            this.removeStatus.Name = "removeStatus";
            this.removeStatus.Size = new System.Drawing.Size(96, 30);
            this.removeStatus.TabIndex = 10;
            this.removeStatus.Text = "Remove";
            this.removeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.removeStatus.UseVisualStyleBackColor = true;
            this.removeStatus.Click += new System.EventHandler(this.multi_TextChanged);
            // 
            // addStatus
            // 
            this.addStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addStatus.Image = global::HungerGamesClient.Properties.Resources.smallPlus;
            this.addStatus.Location = new System.Drawing.Point(11, 119);
            this.addStatus.Name = "addStatus";
            this.addStatus.Size = new System.Drawing.Size(96, 30);
            this.addStatus.TabIndex = 9;
            this.addStatus.Text = "Add";
            this.addStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addStatus.UseVisualStyleBackColor = true;
            this.addStatus.Click += new System.EventHandler(this.addStatus_Click);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(12, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(93, 39);
            this.resetButton.TabIndex = 12;
            this.resetButton.Text = "Reset Simulation";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // resolvePerformanceButton
            // 
            this.resolvePerformanceButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resolvePerformanceButton.Location = new System.Drawing.Point(210, 12);
            this.resolvePerformanceButton.Name = "resolvePerformanceButton";
            this.resolvePerformanceButton.Size = new System.Drawing.Size(93, 39);
            this.resolvePerformanceButton.TabIndex = 14;
            this.resolvePerformanceButton.Text = "Resolve Current Scenes";
            this.resolvePerformanceButton.UseVisualStyleBackColor = true;
            this.resolvePerformanceButton.Click += new System.EventHandler(this.resolvePerformanceButton_Click);
            // 
            // assignScenesButton
            // 
            this.assignScenesButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignScenesButton.Location = new System.Drawing.Point(111, 12);
            this.assignScenesButton.Name = "assignScenesButton";
            this.assignScenesButton.Size = new System.Drawing.Size(93, 39);
            this.assignScenesButton.TabIndex = 13;
            this.assignScenesButton.Text = "Assign New Scenes";
            this.assignScenesButton.UseVisualStyleBackColor = true;
            this.assignScenesButton.Click += new System.EventHandler(this.assignScenesButton_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(748, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Day:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(743, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Time:";
            // 
            // dayInput
            // 
            this.dayInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dayInput.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayInput.Location = new System.Drawing.Point(783, 8);
            this.dayInput.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.dayInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dayInput.Name = "dayInput";
            this.dayInput.Size = new System.Drawing.Size(98, 22);
            this.dayInput.TabIndex = 24;
            this.dayInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dayInput.ValueChanged += new System.EventHandler(this.dayInput_ValueChanged);
            // 
            // timeInput
            // 
            this.timeInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeInput.FormattingEnabled = true;
            this.timeInput.Items.AddRange(new object[] {
            "Morning",
            "Afternoon",
            "Evening",
            "Night"});
            this.timeInput.Location = new System.Drawing.Point(783, 29);
            this.timeInput.Name = "timeInput";
            this.timeInput.Size = new System.Drawing.Size(98, 21);
            this.timeInput.TabIndex = 25;
            this.timeInput.SelectedIndexChanged += new System.EventHandler(this.dayInput_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.resetVotingHistoryButton);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 421);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(876, 51);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Users";
            // 
            // resetVotingHistoryButton
            // 
            this.resetVotingHistoryButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetVotingHistoryButton.Location = new System.Drawing.Point(6, 19);
            this.resetVotingHistoryButton.Name = "resetVotingHistoryButton";
            this.resetVotingHistoryButton.Size = new System.Drawing.Size(154, 24);
            this.resetVotingHistoryButton.TabIndex = 0;
            this.resetVotingHistoryButton.Text = "Reset voting history";
            this.resetVotingHistoryButton.UseVisualStyleBackColor = true;
            this.resetVotingHistoryButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(575, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Seed (leave blank for random):";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(578, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 25);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.groupBox4.Controls.Add(this.statusListBox);
            this.groupBox4.Controls.Add(this.addStatus);
            this.groupBox4.Controls.Add(this.removeStatus);
            this.groupBox4.Location = new System.Drawing.Point(11, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(316, 155);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Statuses";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.groupBox5.Controls.Add(this.flagBox);
            this.groupBox5.Controls.Add(this.addFlag);
            this.groupBox5.Controls.Add(this.removeFlag);
            this.groupBox5.Location = new System.Drawing.Point(333, 122);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(311, 155);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Flags";
            // 
            // flagBox
            // 
            this.flagBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagBox.FormattingEnabled = true;
            this.flagBox.ItemHeight = 18;
            this.flagBox.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3"});
            this.flagBox.Location = new System.Drawing.Point(11, 19);
            this.flagBox.Name = "flagBox";
            this.flagBox.Size = new System.Drawing.Size(294, 94);
            this.flagBox.TabIndex = 7;
            this.flagBox.SelectedIndexChanged += new System.EventHandler(this.flagBox_SelectedIndexChanged);
            // 
            // addFlag
            // 
            this.addFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addFlag.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFlag.Image = global::HungerGamesClient.Properties.Resources.smallPlus;
            this.addFlag.Location = new System.Drawing.Point(11, 119);
            this.addFlag.Name = "addFlag";
            this.addFlag.Size = new System.Drawing.Size(96, 30);
            this.addFlag.TabIndex = 9;
            this.addFlag.Text = "Add";
            this.addFlag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addFlag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addFlag.UseVisualStyleBackColor = true;
            this.addFlag.Click += new System.EventHandler(this.addFlag_Click);
            this.addFlag.Click += new System.EventHandler(this.multi_TextChanged);
            // 
            // removeFlag
            // 
            this.removeFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeFlag.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeFlag.Image = global::HungerGamesClient.Properties.Resources.smallMinus;
            this.removeFlag.Location = new System.Drawing.Point(113, 119);
            this.removeFlag.Name = "removeFlag";
            this.removeFlag.Size = new System.Drawing.Size(96, 30);
            this.removeFlag.TabIndex = 10;
            this.removeFlag.Text = "Remove";
            this.removeFlag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeFlag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.removeFlag.UseVisualStyleBackColor = true;
            this.removeFlag.Click += new System.EventHandler(this.removeFlag_Click);
            this.removeFlag.Click += new System.EventHandler(this.multi_TextChanged);
            // 
            // EditSim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 484);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dayInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.timeInput);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resolvePerformanceButton);
            this.Controls.Add(this.assignScenesButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(647, 492);
            this.Name = "EditSim";
            this.Text = "EditSim";
            this.Load += new System.EventHandler(this.EditSim_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dayInput)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox actorListBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox environmentBox;
        private System.Windows.Forms.ListBox statusListBox;
        private System.Windows.Forms.Button addStatus;
        private System.Windows.Forms.Button removeStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button resolvePerformanceButton;
        private System.Windows.Forms.Button assignScenesButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown dayInput;
        private System.Windows.Forms.ComboBox timeInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button resetVotingHistoryButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox flagBox;
        private System.Windows.Forms.Button addFlag;
        private System.Windows.Forms.Button removeFlag;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}