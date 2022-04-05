using System;
using System.Windows.Forms;
using System.Collections;

using System.Collections.Generic;

namespace HungerGamesClient
{
    public partial class OutcomeEditor : Form
    {
        private Outcome selectedOutcome;
        private Scene selectedScene;

        private bool lockList = false;
        private bool lockListbox = false;
        public OutcomeEditor()
        {
            InitializeComponent();
        }

        private void OutcomeEditor_Load(object sender, EventArgs e)
        {
            selectedScene = SceneEditor.selectedScene;
            selectedOutcome = SceneEditor.selectedOutcome;

            functionDropdown.Enabled = false;
            inputBox.Enabled = false;
            button1.Enabled = false;
            characterDropdown.Enabled = false;

            functionDropdown.Items.Add("set environment");
            functionDropdown.Items.Add("add status");
            functionDropdown.Items.Add("remove status");
            functionDropdown.Items.Add("set flag");
            functionDropdown.Items.Add("increase flag");
            functionDropdown.Items.Add("decrease flag");
            functionDropdown.Items.Add("remove flag");

            for (int i = 1; i <= selectedScene.numParticipants; i++)
            {
                characterDropdown.Items.Add("character " + i);
            }

            foreach (string subOutcome in selectedOutcome.effect.Split(','))
            {
                int sum = 0;
                foreach (char c in subOutcome)
                {
                    if (c == ' ')
                    {
                        sum++;
                        if (sum >= 3)
                            break;
                    }
                }
                if(sum >= 3)
                    listBox1.Items.Add("character " + subOutcome);
            }

            typeDropdown.SelectedIndex = 1 - selectedOutcome.GetOutcomeInt();

            descriptionBox.Text = selectedOutcome.description;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockListbox)
                return;
            lockListbox = true;
            if (listBox1.SelectedIndex == -1)
            {
                functionDropdown.SelectedIndex = -1;
                functionDropdown.Enabled = false;
                inputBox.Text = "";
                characterDropdown.Enabled = false;
                inputBox.Enabled = false;
                button1.Enabled = false;
                lockListbox = false;
                return;
            }

            List<String> terms = new List<String>();
            bool inQuote = false;
            string buffer = "";

            string effect = listBox1.SelectedItem.ToString().Trim();
            foreach (char c in effect)
            {
                if (c == ' ' && !inQuote)
                {
                    terms.Add(buffer);
                    buffer = "";
                }
                else if(c == '\"')
                {
                    inQuote = !inQuote;
                }
                else
                {
                    buffer += c;
                }
            }
            if (buffer.Length > 0)
                terms.Add(buffer);

            if (terms.Count < 4)
            {
                functionDropdown.SelectedIndex = -1;
                inputBox.Text = "";
                functionDropdown.Enabled = false;
                inputBox.Enabled = false;
                characterDropdown.Enabled = true;
                button1.Enabled = true;
                lockListbox = false;
                return;
            }

            functionDropdown.Enabled = true;
            inputBox.Enabled = true;
            button1.Enabled = true;
            characterDropdown.Enabled = true;

            int characterNumber = int.Parse(terms[1]) - 1;
            string functionPhrase = terms[2] + " " + terms[3];

            characterDropdown.SelectedIndex = characterNumber;

            /*
            functionDropdown.Items.Add("set environment");
            functionDropdown.Items.Add("add status");
            functionDropdown.Items.Add("remove status");
            functionDropdown.Items.Add("set flag");
            functionDropdown.Items.Add("increase flag");
            functionDropdown.Items.Add("decrease flag");
            functionDropdown.Items.Add("remove flag");*/

            prepositionLabel.Visible = false;
            inputBox2.Visible = false;

            lockListbox = false;
            switch (functionPhrase)
            {
                case "set status":
                    functionDropdown.SelectedIndex = 0;
                    break;
                case "add status":
                    functionDropdown.SelectedIndex = 1;
                    break;
                case "remove status":
                    functionDropdown.SelectedIndex = 2;
                    break;
                case "set flag":
                    if (terms.Count < 6)
                        return;
                    functionDropdown.SelectedIndex = 3;
                    prepositionLabel.Visible = true;
                    prepositionLabel.Text = "to";
                    inputBox2.Visible = true;
                    inputBox2.Text = terms[5];
                    break;
                case "increase flag":
                    if (terms.Count < 6)
                        return;
                    functionDropdown.SelectedIndex = 4;
                    prepositionLabel.Visible = true;
                    prepositionLabel.Text = "by";
                    inputBox2.Visible = true;
                    inputBox2.Text = terms[5];
                    break;
                case "decrease flag":
                    if (terms.Count < 6)
                        return;
                    functionDropdown.SelectedIndex = 5;
                    prepositionLabel.Visible = true;
                    prepositionLabel.Text = "by";
                    inputBox2.Visible = true;
                    inputBox2.Text = terms[5];
                    break;
                case "remove flag":
                    functionDropdown.SelectedIndex = 6;
                    break;
            }
            inputBox.Text = terms[4].Trim(new char[] {'\"'});
        }

        private void UpdateEffectInListBox(object sender, EventArgs e)
        {
            if (!lockList && listBox1.SelectedIndex != -1)
            {
                lockList = true;
                lockListbox = true;
                listBox1.Items[listBox1.SelectedIndex] = characterDropdown.Text + " " + functionDropdown.Text + " \"" + inputBox.Text + "\"";
                if (prepositionLabel.Visible)
                    listBox1.Items[listBox1.SelectedIndex] += " \"" + inputBox2.Text + "\"";
                lockList = false;
                lockListbox = false;
            }
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            UpdateEffectInListBox(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Delete
            functionDropdown.SelectedIndex = -1;
            inputBox.Text = "";
            functionDropdown.Enabled = false;
            characterDropdown.Enabled = false;
            inputBox.Enabled = false;
            button1.Enabled = false;

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("character 1 add status \"hungry\"");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string effectText = "";

            foreach(string s in listBox1.Items)
            {
                effectText += s.Substring(s.IndexOf(' ') + 1) + ",";
            }
            effectText = effectText.Trim(',');

            selectedOutcome.effect = effectText;
            selectedOutcome.description = descriptionBox.Text.Replace('\n', ' ');
            switch (typeDropdown.SelectedIndex)
            {
                case 0:
                    selectedOutcome.outcomeType = OutcomeType.Positive;
                    break;
                case 1:
                    selectedOutcome.outcomeType = OutcomeType.Neutral;
                    break;
                case 2:
                    selectedOutcome.outcomeType = OutcomeType.Harmful;
                    break;
            }

            SceneEditor.outcomeBox.DrawMode = DrawMode.OwnerDrawFixed;
            SceneEditor.outcomeBox.DrawMode = DrawMode.Normal;

            SceneEditor.unsavedChanges = true;
            SceneEditor.saveButtonRef.Enabled = true;

            Cursor = Cursors.Default;
            this.Close();
        }

        private void functionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (functionDropdown.SelectedIndex >= 3 && functionDropdown.SelectedIndex <= 5)
            {
                if(functionDropdown.SelectedIndex == 3)
                {
                    prepositionLabel.Text = "to";
                }
                else
                {
                    prepositionLabel.Text = "by";
                }
                inputBox2.Visible = true;
                prepositionLabel.Visible = true;
            }
            else
            {
                inputBox2.Visible = false;
                prepositionLabel.Visible = false;
            }
            this.UpdateEffectInListBox(sender, e);
        }
    }
}
