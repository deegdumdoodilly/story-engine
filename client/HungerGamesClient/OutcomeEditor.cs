using System;
using System.Windows.Forms;

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

            dropdown.Enabled = false;
            inputBox.Enabled = false;
            button1.Enabled = false;
            characterDropdown.Enabled = false;

            dropdown.Items.Add("set environment");
            dropdown.Items.Add("add status");
            dropdown.Items.Add("remove status");

            for(int i = 1; i <= selectedScene.numParticipants; i++)
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
            if (listBox1.SelectedIndex == -1)
            {
                dropdown.SelectedIndex = -1;
                dropdown.Enabled = false;
                inputBox.Text = "";
                characterDropdown.Enabled = false;
                inputBox.Enabled = false;
                button1.Enabled = false;
                return;
            }

            string effect = listBox1.SelectedItem.ToString().Trim();
            effect = effect.Substring(effect.IndexOf(' ') + 1);
            int sum = 0;
            foreach (char c in effect)
            {
                if (c == ' ')
                {
                    sum++;
                    if (sum >= 3)
                        break;
                }
            }

            if (sum < 3)
            {
                dropdown.SelectedIndex = -1;
                inputBox.Text = "";
                dropdown.Enabled = false;
                inputBox.Enabled = false;
                characterDropdown.Enabled = true;
                button1.Enabled = true;
                return;
            }

            dropdown.Enabled = true;
            inputBox.Enabled = true;
            button1.Enabled = true;
            characterDropdown.Enabled = true;

            string numberPhrase = effect.Substring(0, effect.IndexOf(' '));
            effect = effect.Substring(effect.IndexOf(' ') + 1);

            string firstPhrase = effect.Substring(0, effect.IndexOf(' '));
            effect = effect.Substring(effect.IndexOf(' ') + 1);

            string secondPhrase = effect.Substring(0, effect.IndexOf(' '));
            string thirdPhrase = effect.Substring(effect.IndexOf(' ') + 1);

            characterDropdown.SelectedIndex = int.Parse(numberPhrase) - 1;

            switch (firstPhrase)
            {
                case "set":
                    if(secondPhrase == "environment")
                    {
                        dropdown.SelectedIndex = 0;
                    }
                    break;
                case "add":
                    if (secondPhrase == "status")
                    {
                        dropdown.SelectedIndex = 1;
                    }
                    break;
                case "remove":
                    if (secondPhrase == "status")
                    {
                        dropdown.SelectedIndex = 2;
                    }
                    break;
            }
            inputBox.Text = thirdPhrase.Trim(new char[] {'\"'});
        }

        private void dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lockList && listBox1.SelectedIndex != -1 && dropdown.ContainsFocus)
            {
                listBox1.Items[listBox1.SelectedIndex] = characterDropdown.Text + " " + dropdown.Text + " \"" + inputBox.Text + "\"";
            }
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                lockList = true;
                listBox1.Items[listBox1.SelectedIndex] = characterDropdown.Text + " " + dropdown.Text + " \"" + inputBox.Text + "\"";
                lockList = false;
                //inputBox.Focus();
            }
        }

        private void inputBox_TextChangedEnter(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*if (listBox1.SelectedIndex != -1 && e.KeyCode == Keys.Enter)
            {
                lockList = true;
                listBox1.Items[listBox1.SelectedIndex] = characterDropdown.Text + " " + dropdown.Text + " \"" + inputBox.Text + "\"";
                lockList = false;
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Delete
            dropdown.SelectedIndex = -1;
            inputBox.Text = "";
            dropdown.Enabled = false;
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
                    selectedOutcome.outcomeType = OutcomeType.Negative;
                    break;
            }

            SceneEditor.outcomeBox.DrawMode = DrawMode.OwnerDrawFixed;
            SceneEditor.outcomeBox.DrawMode = DrawMode.Normal;

            SceneEditor.unsavedChanges = true;
            SceneEditor.saveButtonRef.Enabled = true;

            this.Close();
        }

        private void inputBox_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
