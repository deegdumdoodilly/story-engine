﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungerGamesClient
{

    public partial class VotingBooth : Form
    {
        private List<Ballot> ballotList;
        private List<(CheckBox, Label)> checkboxList;
        private int selectionId;
        private Outcome[] checkboxOutcomes;

        private bool stopRecursive = false;

        public VotingBooth()
        {
            InitializeComponent();
        }

        private void VotingBooth_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }

        public void RefreshBooth()
        {
            stopRecursive = true;
            selectionId = -1;

            submitButton.Enabled = false;

            ballotList = new List<Ballot>();
            if (listBox.DataSource == null)
            {
                listBox.Items.Clear();
            }
            foreach (Ballot ballot in MainForm.ballotList)
            {
                ballotList.Add(ballot);
            }
            listBox.DataSource = ballotList;
            stopRecursive = false;

            UpdateDisplay();
        }

        public void Initialize()
        {
            stopRecursive = true;
            MainForm.FetchBallots();
            
            checkboxList = new List<(CheckBox, Label)> {
                (checkBox1, checkboxText1),
                (checkBox2, checkboxText2),
                (checkBox3, checkboxText3),
                (checkBox4, checkboxText4),
                (checkBox5, checkboxText5),
                (checkBox6, checkboxText6),
            };
            foreach ((CheckBox checkbox, Label label) in checkboxList)
            {
                checkbox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
                label.Click += new System.EventHandler(this.label_Clicked);
            }
            checkboxOutcomes = new Outcome[6];

            stopRecursive = false;
            RefreshBooth();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!stopRecursive)
            {
                stopRecursive = true;
                UpdateDisplay();
                stopRecursive = false;
            }
        }

        private void UpdateVoteNumberDisplay()
        {
            int permittedHarmfulVotes = MainForm.currentUser.positiveVotes;
            permittedHarmfulVotes += MainForm.currentUser.neutralVotes / 3;

            int totalHarmfulVotes = MainForm.currentUser.negativeVotes;
            foreach (Ballot b in ballotList)
            {
                if (b.inProgress && b.voter.id == MainForm.currentUser.id && b.hasChosenOutcome && b.chosenOutcome.outcomeType == OutcomeType.Harmful)
                    totalHarmfulVotes += 1;
            }

            positiveVotes.Text = "Positive votes: " + MainForm.currentUser.positiveVotes;
            neutralVotes.Text = "Neutral votes: " + MainForm.currentUser.neutralVotes;
            harmfulVotes.Text = "Harmful votes: " + totalHarmfulVotes + "/" + permittedHarmfulVotes;

        }

        private void UpdateDisplay()
        {
            UpdateVoteNumberDisplay();

            ballotList.Sort();

            if (listBox.SelectedIndex < 0)
            {
                ClearDisplay();
                return;
            }
            checkboxTable.Visible = false;
            submitButton.Visible = false;
            Ballot ballot = ballotList[listBox.SelectedIndex];
            Scene scene = ballot.performance.scene;
            portraitPanel.Controls.Clear();
            MainForm.AddPortraits(ballot.performance, portraitPanel);
            sceneNameLabel.Text = scene.sceneName;
            if(ballot.voter is null || ballot.voter.id <= 0)
            {
                VoterNameLabel.Text = "Vote generated by system.";
            }
            else
            {
                VoterNameLabel.Text = "Assigned to: " + ballot.voter.username;
            }
            descriptionLabel.Text = ballot.performance.GetDescription();

            bool ballotEditable = true;
            if (!ballot.inProgress)
            {
                ballotEditable = false;
            }

            
            for(int i = 0; i < scene.outcomes.Count; i++)
            {
                checkboxList[i].Item1.Visible = true;
                checkboxList[i].Item1.Enabled = !ballot.hasChosenOutcome && ballotEditable;
                checkboxList[i].Item1.Checked = ballot.hasChosenOutcome && ballot.chosenOutcome.id == scene.outcomes[i].id;
                checkboxList[i].Item2.Visible = true;
                checkboxList[i].Item2.Text = scene.outcomes[i].GetDescription(ballot.performance) + " (" +
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
                checkboxOutcomes[i] = new Outcome(-1, -1, 0, "", "MISSING OUTCOME");
            }
            

            if (ballot.inProgress)
            {
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
            }
            else
            {
                submitButton.Enabled = false;
                if (ballot.hasChosenOutcome)
                {
                    submitButton.Text = "Vote processed";
                }
                else
                {
                    submitButton.Text = "Vote expired";
                }
            }

            checkboxTable.Visible = true;
            submitButton.Visible = true;

            
        }

        private void ClearDisplay()
        {
            portraitPanel.Controls.Clear();
            checkboxTable.Visible = false;
            sceneNameLabel.Text = "No scene selected";
            VoterNameLabel.Text = "";
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

            Ballot ballot = (Ballot)listBox.Items[e.Index];
            string displayText = ballot.ToString();
            SolidBrush brush = new SolidBrush(SystemColors.ControlText);

            if (!ballot.inProgress)
            {
                brush = new SolidBrush(SystemColors.GrayText);
            }else if (!ballot.hasChosenOutcome)
            {
                displayText = "(*) " + displayText;
            }

            // draw the text of the list item, not doing this will only show
            // the background color
            // you will need to get the text of item to display
            if(e.Index >= 0 && e.Index <= listBox.Items.Count)
                g.DrawString(displayText, e.Font, brush, new PointF(e.Bounds.X, e.Bounds.Y));

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
                if(checkboxOutcomes[selectionId].outcomeType == OutcomeType.Harmful)
                {
                    int permittedVotes = MainForm.currentUser.positiveVotes;
                    permittedVotes += MainForm.currentUser.neutralVotes / 3;
                    foreach(Ballot b in ballotList)
                    {
                        if (b.inProgress && b.voter.id == MainForm.currentUser.id && b.hasChosenOutcome && b.chosenOutcome.outcomeType == OutcomeType.Harmful)
                            permittedVotes -= 1;
                    }
                    permittedVotes -= MainForm.currentUser.negativeVotes;

                    if (permittedVotes <= 0)
                    {
                        MessageBox.Show("You cannot cast another 'Harmful' vote until you cast more positive/neutral votes, or remove some pending 'Harmful' votes.", "Harmful vote count exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                Ballot ballot = ballotList[listBox.SelectedIndex];
                ballot.chosenOutcome = checkboxOutcomes[selectionId];
                ballot.hasChosenOutcome = true;
                ballotList[listBox.SelectedIndex] = ballot;
                foreach ((CheckBox checkbox, Label label) in checkboxList)
                {
                    checkbox.Enabled = false;
                }

                //String requestJSON = "{\"id\":" + ballot.id + ",\"outcomeId\":" + ballot.chosenOutcome.id + "}";
                String requestJSON = ballot.chosenOutcome.id.ToString();

                JsonObject.PostWithJson("/votes/set-chosen-outcome?id=" + ballot.id + "&outcomeId=", requestJSON);

                submitButton.Text = "Edit";

                UpdateVoteNumberDisplay();

                listBox.SelectedIndex = selectionId;
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

        private void neutralVotes_Click(object sender, EventArgs e)
        {

        }
    }
}
