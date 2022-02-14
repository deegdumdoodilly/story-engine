using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace HungerGamesClient
{

    public partial class VotingBooth : Form
    {
        private List<Ballot> ballotList;
        private List<(CheckBox, Label)> checkboxList;
        private int selectionId;
        private Outcome[] checkboxOutcomes;
        public VotingBooth()
        {
            InitializeComponent();
        }

        private void VotingBooth_Load(object sender, EventArgs e)
        {
            checkboxList = new List<(CheckBox, Label)> {
                (checkBox1, checkboxText1),
                (checkBox2, checkboxText2),
                (checkBox3, checkboxText3),
                (checkBox4, checkboxText4),
                (checkBox5, checkboxText5),
                (checkBox6, checkboxText6),
            };
            foreach((CheckBox checkbox, Label label) in checkboxList)
            {
                checkbox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
                label.Click += new System.EventHandler(this.label_Clicked);
            }
            checkboxOutcomes = new Outcome[6];
            selectionId = -1;
            submitButton.Enabled = false;
            RefreshBooth();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }

        public void RefreshBooth()
        {
            ballotList = new List<Ballot>();
            listBox.Items.Clear();
            foreach(Ballot ballot in MainForm.ballotList)
            {
                ballotList.Add(ballot);
            }
            listBox.DataSource = ballotList;

            UpdateDisplay();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if(listBox.SelectedIndex < 0)
            {
                ClearDisplay();
                return;
            }
            checkboxTable.Visible = false;
            Ballot ballot = ballotList[listBox.SelectedIndex];
            Scene scene = ballot.stackScene.scene;
            portraitPanel.Controls.Clear();
            MainForm.AddPortraits(ballot.stackScene, portraitPanel);
            sceneNameLabel.Text = scene.sceneName;
            descriptionLabel.Text = ballot.stackScene.GetDescription();


            
            for(int i = 0; i < scene.outcomes.Count; i++)
            {
                checkboxList[i].Item1.Visible = true;
                checkboxList[i].Item1.Enabled = !ballot.hasChosenOutcome;
                checkboxList[i].Item1.Checked = ballot.hasChosenOutcome && ballot.chosenOutcome.id == scene.outcomes[i].id;
                checkboxList[i].Item2.Visible = true;
                checkboxList[i].Item2.Text = scene.outcomes[i].GetDescription(ballot.stackScene) + " (" +
                                                     scene.outcomes[i].outcomeType.ToString() + ").";
                checkboxOutcomes[i] = scene.outcomes[i];
            }
            for(int i = scene.outcomes.Count; i < checkboxList.Count; i++)
            {
                checkboxList[i].Item1.Visible = false;
                checkboxList[i].Item1.Enabled = false;
                checkboxList[i].Item1.Checked = false;
                checkboxList[i].Item2.Visible = false;
                checkboxList[i].Item2.Text = "";
                checkboxOutcomes[i] = new Outcome();
            }
            if (ballot.hasChosenOutcome)
            {
                submitButton.Text = "Edit";
                submitButton.Enabled = true;
            }
            else
            {
                submitButton.Text = "Submit";
                submitButton.Enabled = false;
            }
            checkboxTable.Visible = true;
        }

        private void ClearDisplay()
        {
            portraitPanel.Controls.Clear();
            checkboxTable.Visible = false;
            sceneNameLabel.Text = "No scene selected";
            descriptionLabel.Text = "";
            foreach((CheckBox checkbox, Label label) in checkboxList)
            {
                checkbox.Visible = false;
                checkbox.Checked = false;
                label.Visible = false;
                label.Text = "";
            }
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;

            SolidBrush brush;


            // draw the text of the list item, not doing this will only show
            // the background color
            // you will need to get the text of item to display
            if(e.Index >= 0 && e.Index <= listBox.Items.Count)
                g.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(SystemColors.ControlText), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }


        private void CheckAndUncheck(int checkedId)
        {
            for(int i = 0; i < checkboxList.Count; i++)
            {
                checkboxList[i].Item1.Checked = (i == checkedId);
            }
            selectionId = checkedId;
            submitButton.Enabled = (selectionId != -1);
        }

        private bool ignoreRecursiveChecks = false;
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

            if (!ignoreRecursiveChecks)
            {
                CheckBox box = (CheckBox)sender;
                ignoreRecursiveChecks = true;
                if (box.Checked)
                {
                    string name = box.Name;
                    CheckAndUncheck(int.Parse(name.Substring(name.Length - 1)) - 1);
                }
                else
                {
                    CheckAndUncheck(-1);
                }
                ignoreRecursiveChecks = false;
            }
        }

        private void label_Clicked(object sender, EventArgs e)
        {
            CheckBox associatedBox = checkboxList.Find(x => x.Item2.GetHashCode() == sender.GetHashCode()).Item1;
            if (associatedBox.Enabled && associatedBox.Visible)
            {
                associatedBox.Checked = !associatedBox.Checked;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (submitButton.Text == "Submit")
            {
                Ballot ballot = ballotList[listBox.SelectedIndex];
                ballot.chosenOutcome = checkboxOutcomes[selectionId];
                ballot.hasChosenOutcome = true;
                ballotList[listBox.SelectedIndex] = ballot;
                foreach ((CheckBox checkbox, Label label) in checkboxList)
                {
                    checkbox.Enabled = false;
                }

                try
                {
                    using(MySqlConnection cnn = new MySqlConnection(MainForm.GetConnectionString()))
                    {
                        cnn.Open();

                        string query = string.Format("UPDATE hungergames.votes SET chosen_outcome = {0}, has_chosen_outcome = {1} WHERE id = {2};", ballot.chosenOutcome.id, true, ballot.id);
                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error posting vote.\n" + ex.Message + "\n" + ex.StackTrace);
                }

                submitButton.Text = "Edit";
            }
            else
            {
                foreach ((CheckBox checkbox, Label label) in checkboxList)
                {
                    checkbox.Enabled = true;
                }
                submitButton.Text = "Submit";
            }
        }
    }
}
