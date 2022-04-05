namespace HungerGamesClient
{
    partial class RunScene
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
            if(createAndResolve != null && !createAndResolve.IsDisposed)
                createAndResolve.Close();
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
            this.participant1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.participant2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.participant3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.participant4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.participant5 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.participant6 = new System.Windows.Forms.ComboBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.executeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.resolveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // participant1
            // 
            this.participant1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant1.FormattingEnabled = true;
            this.participant1.Location = new System.Drawing.Point(15, 209);
            this.participant1.Name = "participant1";
            this.participant1.Size = new System.Drawing.Size(133, 29);
            this.participant1.TabIndex = 0;
            this.participant1.Visible = false;
            this.participant1.SelectedIndexChanged += new System.EventHandler(this.participant1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Baskerville Old Face", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 36);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Participant 1";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(150, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Participant 2";
            this.label3.Visible = false;
            // 
            // participant2
            // 
            this.participant2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant2.FormattingEnabled = true;
            this.participant2.Location = new System.Drawing.Point(154, 209);
            this.participant2.Name = "participant2";
            this.participant2.Size = new System.Drawing.Size(133, 29);
            this.participant2.TabIndex = 3;
            this.participant2.Visible = false;
            this.participant2.SelectedIndexChanged += new System.EventHandler(this.participant2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(289, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Participant 3";
            this.label4.Visible = false;
            // 
            // participant3
            // 
            this.participant3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant3.FormattingEnabled = true;
            this.participant3.Location = new System.Drawing.Point(293, 209);
            this.participant3.Name = "participant3";
            this.participant3.Size = new System.Drawing.Size(133, 29);
            this.participant3.TabIndex = 5;
            this.participant3.Visible = false;
            this.participant3.SelectedIndexChanged += new System.EventHandler(this.participant3_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(428, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Participant 4";
            this.label5.Visible = false;
            // 
            // participant4
            // 
            this.participant4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant4.FormattingEnabled = true;
            this.participant4.Location = new System.Drawing.Point(432, 209);
            this.participant4.Name = "participant4";
            this.participant4.Size = new System.Drawing.Size(133, 29);
            this.participant4.TabIndex = 7;
            this.participant4.Visible = false;
            this.participant4.SelectedIndexChanged += new System.EventHandler(this.participant4_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(567, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "Participant 5";
            this.label6.Visible = false;
            // 
            // participant5
            // 
            this.participant5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant5.FormattingEnabled = true;
            this.participant5.Location = new System.Drawing.Point(571, 209);
            this.participant5.Name = "participant5";
            this.participant5.Size = new System.Drawing.Size(133, 29);
            this.participant5.TabIndex = 9;
            this.participant5.Visible = false;
            this.participant5.SelectedIndexChanged += new System.EventHandler(this.participant5_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(706, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 21);
            this.label7.TabIndex = 12;
            this.label7.Text = "Participant 6";
            this.label7.Visible = false;
            // 
            // participant6
            // 
            this.participant6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.participant6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participant6.FormattingEnabled = true;
            this.participant6.Location = new System.Drawing.Point(710, 209);
            this.participant6.Name = "participant6";
            this.participant6.Size = new System.Drawing.Size(133, 29);
            this.participant6.TabIndex = 11;
            this.participant6.Visible = false;
            this.participant6.SelectedIndexChanged += new System.EventHandler(this.participant6_SelectedIndexChanged);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionBox.Location = new System.Drawing.Point(15, 244);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(828, 112);
            this.descriptionBox.TabIndex = 13;
            // 
            // testButton
            // 
            this.testButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testButton.Location = new System.Drawing.Point(438, 362);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(130, 53);
            this.testButton.TabIndex = 14;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // executeButton
            // 
            this.executeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeButton.Location = new System.Drawing.Point(574, 362);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(130, 53);
            this.executeButton.TabIndex = 15;
            this.executeButton.Text = "Create Performance";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(154, 80);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(133, 123);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(293, 80);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(133, 123);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(432, 80);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(133, 123);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(571, 80);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(133, 123);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(710, 80);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(133, 123);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 21;
            this.pictureBox6.TabStop = false;
            // 
            // resolveButton
            // 
            this.resolveButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resolveButton.Location = new System.Drawing.Point(710, 362);
            this.resolveButton.Name = "resolveButton";
            this.resolveButton.Size = new System.Drawing.Size(130, 53);
            this.resolveButton.TabIndex = 22;
            this.resolveButton.Text = "Create and Resolve Performance";
            this.resolveButton.UseVisualStyleBackColor = true;
            this.resolveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // RunScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 427);
            this.Controls.Add(this.resolveButton);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.participant6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.participant5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.participant4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.participant3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.participant2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.participant1);
            this.Name = "RunScene";
            this.Text = "Scene Testing";
            this.Load += new System.EventHandler(this.RunScene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox participant1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox participant2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox participant3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox participant4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox participant5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox participant6;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button resolveButton;
    }
}