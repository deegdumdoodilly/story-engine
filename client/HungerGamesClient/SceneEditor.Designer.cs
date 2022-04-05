namespace HungerGamesClient
{
    partial class SceneEditor
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
            if (this.outEditor != null && !this.outEditor.IsDisposed)
            {
                outEditor.Close();
            }
            if (this.reqEditor != null && !this.reqEditor.IsDisposed)
            {
                reqEditor.Close();
            }
            base.Dispose(disposing);
            if (this.runScene != null && !this.runScene.IsDisposed)
            {
                runScene.Close();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sceneListBox = new System.Windows.Forms.ListBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.briefBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.parentSceneDropdown = new System.Windows.Forms.ComboBox();
            this.requirementsBox = new System.Windows.Forms.ListBox();
            this.outcomesBox = new System.Windows.Forms.ListBox();
            this.participantsBox = new System.Windows.Forms.NumericUpDown();
            this.priorityBox = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.addReq = new System.Windows.Forms.Button();
            this.editReq = new System.Windows.Forms.Button();
            this.removeReq = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.addOut = new System.Windows.Forms.Button();
            this.editOut = new System.Windows.Forms.Button();
            this.removeOut = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.addScene = new System.Windows.Forms.Button();
            this.removeScene = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.removeImage = new System.Windows.Forms.Button();
            this.addImage = new System.Windows.Forms.Button();
            this.imageListBox = new System.Windows.Forms.ListBox();
            this.testSceneButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.participantsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // sceneListBox
            // 
            this.sceneListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sceneListBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sceneListBox.FormattingEnabled = true;
            this.sceneListBox.ItemHeight = 20;
            this.sceneListBox.Location = new System.Drawing.Point(9, 16);
            this.sceneListBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.sceneListBox.Name = "sceneListBox";
            this.sceneListBox.Size = new System.Drawing.Size(348, 444);
            this.sceneListBox.TabIndex = 2;
            this.sceneListBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Font = new System.Drawing.Font("Baskerville Old Face", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(381, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(241, 35);
            this.nameBox.TabIndex = 3;
            this.nameBox.TextChanged += new System.EventHandler(this.MarkUnsavedChanges);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionBox.Location = new System.Drawing.Point(6, 19);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(543, 56);
            this.descriptionBox.TabIndex = 5;
            this.descriptionBox.TextChanged += new System.EventHandler(this.MarkUnsavedChanges);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(3, 78);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(103, 15);
            this.label.TabIndex = 6;
            this.label.Text = "Brief description:";
            // 
            // briefBox
            // 
            this.briefBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.briefBox.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.briefBox.Location = new System.Drawing.Point(3, 94);
            this.briefBox.Name = "briefBox";
            this.briefBox.Size = new System.Drawing.Size(543, 22);
            this.briefBox.TabIndex = 7;
            this.briefBox.TextChanged += new System.EventHandler(this.MarkUnsavedChanges);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(378, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "# of participants:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(430, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Priority:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(628, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Parent scene:";
            // 
            // parentSceneDropdown
            // 
            this.parentSceneDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parentSceneDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parentSceneDropdown.FormattingEnabled = true;
            this.parentSceneDropdown.Location = new System.Drawing.Point(730, 11);
            this.parentSceneDropdown.MaxDropDownItems = 16;
            this.parentSceneDropdown.Name = "parentSceneDropdown";
            this.parentSceneDropdown.Size = new System.Drawing.Size(203, 21);
            this.parentSceneDropdown.TabIndex = 13;
            this.parentSceneDropdown.SelectedIndexChanged += new System.EventHandler(this.MarkUnsavedChanges);
            // 
            // requirementsBox
            // 
            this.requirementsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requirementsBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requirementsBox.FormattingEnabled = true;
            this.requirementsBox.Location = new System.Drawing.Point(6, 16);
            this.requirementsBox.Name = "requirementsBox";
            this.requirementsBox.Size = new System.Drawing.Size(340, 82);
            this.requirementsBox.TabIndex = 20;
            this.requirementsBox.SelectedIndexChanged += new System.EventHandler(this.requirementsBox_SelectedIndexChanged);
            // 
            // outcomesBox
            // 
            this.outcomesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outcomesBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outcomesBox.FormattingEnabled = true;
            this.outcomesBox.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3"});
            this.outcomesBox.Location = new System.Drawing.Point(6, 16);
            this.outcomesBox.Name = "outcomesBox";
            this.outcomesBox.Size = new System.Drawing.Size(543, 95);
            this.outcomesBox.TabIndex = 21;
            this.outcomesBox.SelectedIndexChanged += new System.EventHandler(this.outcomesBox_SelectedIndexChanged);
            // 
            // participantsBox
            // 
            this.participantsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participantsBox.Location = new System.Drawing.Point(488, 178);
            this.participantsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.participantsBox.Name = "participantsBox";
            this.participantsBox.Size = new System.Drawing.Size(49, 26);
            this.participantsBox.TabIndex = 23;
            this.participantsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.participantsBox.ValueChanged += new System.EventHandler(this.participantsBox_ValueChanged);
            // 
            // priorityBox
            // 
            this.priorityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priorityBox.Location = new System.Drawing.Point(488, 210);
            this.priorityBox.Name = "priorityBox";
            this.priorityBox.Size = new System.Drawing.Size(49, 26);
            this.priorityBox.TabIndex = 24;
            this.priorityBox.ValueChanged += new System.EventHandler(this.MarkUnsavedChanges);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.addReq, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.editReq, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.removeReq, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(346, 47);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // addReq
            // 
            this.addReq.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addReq.AutoSize = true;
            this.addReq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addReq.Image = global::HungerGamesClient.Properties.Resources.plus;
            this.addReq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addReq.Location = new System.Drawing.Point(3, 3);
            this.addReq.Name = "addReq";
            this.addReq.Size = new System.Drawing.Size(109, 41);
            this.addReq.TabIndex = 17;
            this.addReq.Text = "      Add";
            this.addReq.UseVisualStyleBackColor = true;
            this.addReq.Click += new System.EventHandler(this.addReq_Click);
            // 
            // editReq
            // 
            this.editReq.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editReq.AutoSize = true;
            this.editReq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editReq.Image = global::HungerGamesClient.Properties.Resources.pencilIcon;
            this.editReq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editReq.Location = new System.Drawing.Point(118, 3);
            this.editReq.Name = "editReq";
            this.editReq.Size = new System.Drawing.Size(109, 41);
            this.editReq.TabIndex = 16;
            this.editReq.Text = "      Edit";
            this.editReq.UseVisualStyleBackColor = true;
            this.editReq.Click += new System.EventHandler(this.editReq_Click);
            // 
            // removeReq
            // 
            this.removeReq.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeReq.AutoSize = true;
            this.removeReq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.removeReq.Image = global::HungerGamesClient.Properties.Resources.minusIcon;
            this.removeReq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeReq.Location = new System.Drawing.Point(233, 3);
            this.removeReq.Name = "removeReq";
            this.removeReq.Size = new System.Drawing.Size(110, 41);
            this.removeReq.TabIndex = 18;
            this.removeReq.Text = "      Remove";
            this.removeReq.UseVisualStyleBackColor = true;
            this.removeReq.Click += new System.EventHandler(this.removeReq_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.requirementsBox);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(581, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 153);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Requirements";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.descriptionBox);
            this.groupBox2.Controls.Add(this.label);
            this.groupBox2.Controls.Add(this.briefBox);
            this.groupBox2.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(378, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(555, 119);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Controls.Add(this.outcomesBox);
            this.groupBox3.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(378, 331);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 167);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Possible outcomes";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.addOut, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.editOut, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.removeOut, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 118);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(540, 46);
            this.tableLayoutPanel2.TabIndex = 22;
            // 
            // addOut
            // 
            this.addOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addOut.AutoSize = true;
            this.addOut.Image = global::HungerGamesClient.Properties.Resources.plus;
            this.addOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addOut.Location = new System.Drawing.Point(3, 3);
            this.addOut.Name = "addOut";
            this.addOut.Size = new System.Drawing.Size(174, 40);
            this.addOut.TabIndex = 26;
            this.addOut.Text = "  Add";
            this.addOut.UseVisualStyleBackColor = true;
            this.addOut.Click += new System.EventHandler(this.addOut_Click);
            // 
            // editOut
            // 
            this.editOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editOut.AutoSize = true;
            this.editOut.Image = global::HungerGamesClient.Properties.Resources.pencilIcon;
            this.editOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editOut.Location = new System.Drawing.Point(183, 3);
            this.editOut.Name = "editOut";
            this.editOut.Size = new System.Drawing.Size(174, 40);
            this.editOut.TabIndex = 19;
            this.editOut.Text = "  Edit";
            this.editOut.UseVisualStyleBackColor = true;
            this.editOut.Click += new System.EventHandler(this.editOut_Click);
            // 
            // removeOut
            // 
            this.removeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeOut.AutoSize = true;
            this.removeOut.Image = global::HungerGamesClient.Properties.Resources.minusIcon;
            this.removeOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeOut.Location = new System.Drawing.Point(363, 3);
            this.removeOut.Name = "removeOut";
            this.removeOut.Size = new System.Drawing.Size(174, 40);
            this.removeOut.TabIndex = 27;
            this.removeOut.Text = "  Remove";
            this.removeOut.UseVisualStyleBackColor = true;
            this.removeOut.Click += new System.EventHandler(this.removeOut_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.sceneListBox);
            this.groupBox4.Controls.Add(this.addScene);
            this.groupBox4.Controls.Add(this.removeScene);
            this.groupBox4.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(360, 522);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scenes";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::HungerGamesClient.Properties.Resources.copyIcon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(124, 476);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 40);
            this.button2.TabIndex = 30;
            this.button2.Text = "         Copy";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // addScene
            // 
            this.addScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addScene.Image = global::HungerGamesClient.Properties.Resources.newIcon;
            this.addScene.Location = new System.Drawing.Point(6, 476);
            this.addScene.Name = "addScene";
            this.addScene.Size = new System.Drawing.Size(112, 40);
            this.addScene.TabIndex = 29;
            this.addScene.Text = "     New";
            this.addScene.UseVisualStyleBackColor = true;
            this.addScene.Click += new System.EventHandler(this.addScene_Click);
            // 
            // removeScene
            // 
            this.removeScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeScene.Image = global::HungerGamesClient.Properties.Resources.garbageIcon;
            this.removeScene.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeScene.Location = new System.Drawing.Point(242, 476);
            this.removeScene.Name = "removeScene";
            this.removeScene.Size = new System.Drawing.Size(112, 40);
            this.removeScene.TabIndex = 28;
            this.removeScene.Text = "         Delete";
            this.removeScene.UseVisualStyleBackColor = true;
            this.removeScene.Click += new System.EventHandler(this.removeScene_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.removeImage);
            this.groupBox5.Controls.Add(this.addImage);
            this.groupBox5.Controls.Add(this.imageListBox);
            this.groupBox5.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(378, 242);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(197, 83);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Suplemental Images";
            // 
            // removeImage
            // 
            this.removeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.removeImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeImage.Location = new System.Drawing.Point(100, 52);
            this.removeImage.Name = "removeImage";
            this.removeImage.Size = new System.Drawing.Size(91, 25);
            this.removeImage.TabIndex = 19;
            this.removeImage.Text = "Remove";
            this.removeImage.UseVisualStyleBackColor = true;
            // 
            // addImage
            // 
            this.addImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addImage.Location = new System.Drawing.Point(5, 52);
            this.addImage.Name = "addImage";
            this.addImage.Size = new System.Drawing.Size(89, 25);
            this.addImage.TabIndex = 18;
            this.addImage.Text = "Add";
            this.addImage.UseVisualStyleBackColor = true;
            // 
            // imageListBox
            // 
            this.imageListBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageListBox.FormattingEnabled = true;
            this.imageListBox.Location = new System.Drawing.Point(5, 16);
            this.imageListBox.Name = "imageListBox";
            this.imageListBox.Size = new System.Drawing.Size(186, 30);
            this.imageListBox.TabIndex = 0;
            // 
            // testSceneButton
            // 
            this.testSceneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testSceneButton.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testSceneButton.Image = global::HungerGamesClient.Properties.Resources.actionSlide;
            this.testSceneButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.testSceneButton.Location = new System.Drawing.Point(595, 501);
            this.testSceneButton.Name = "testSceneButton";
            this.testSceneButton.Size = new System.Drawing.Size(165, 40);
            this.testSceneButton.TabIndex = 38;
            this.testSceneButton.Text = "         Test Scene";
            this.testSceneButton.UseVisualStyleBackColor = true;
            this.testSceneButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Image = global::HungerGamesClient.Properties.Resources.saveIcon;
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(766, 501);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(167, 40);
            this.saveButton.TabIndex = 22;
            this.saveButton.Text = "      Save scene";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // SceneEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 546);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.testSceneButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.priorityBox);
            this.Controls.Add(this.participantsBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.parentSceneDropdown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameBox);
            this.MinimumSize = new System.Drawing.Size(961, 585);
            this.Name = "SceneEditor";
            this.Text = "SceneEditor";
            this.Load += new System.EventHandler(this.SceneEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.participantsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox sceneListBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox briefBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox parentSceneDropdown;
        private System.Windows.Forms.Button editOut;
        private System.Windows.Forms.ListBox requirementsBox;
        private System.Windows.Forms.ListBox outcomesBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.NumericUpDown participantsBox;
        private System.Windows.Forms.NumericUpDown priorityBox;
        private System.Windows.Forms.Button editReq;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button removeReq;
        private System.Windows.Forms.Button addReq;
        private System.Windows.Forms.Button addOut;
        private System.Windows.Forms.Button removeOut;
        private System.Windows.Forms.Button removeScene;
        private System.Windows.Forms.Button addScene;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button testSceneButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button removeImage;
        private System.Windows.Forms.Button addImage;
        private System.Windows.Forms.ListBox imageListBox;
    }
}