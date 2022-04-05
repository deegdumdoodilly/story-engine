using System;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class RequirementEditor : Form
    {
        ComboBox[] typeDropdown;
        TextBox[] rawBox;
        ComboBox[] attributeDropdown;

        private Scene scene;
        private Requirement requirement;

        public RequirementEditor()
        {
            InitializeComponent();
        }

        private void RequirementEditor_Load(object sender, EventArgs e)
        {
            scene = SceneEditor.selectedScene;

            typeDropdown = new ComboBox[2] {TypeDropdown1, TypeDropdown2 };
            rawBox = new TextBox[2] { RawBox1, RawBox2 };
            attributeDropdown = new ComboBox[2] { attributeDropdown1, attributeDropdown2 };

            rawBox[0].Visible = false;
            rawBox[1].Visible = false;
            attributeDropdown[0].Visible = false;
            attributeDropdown[1].Visible = false;
            timeOfDayDropdown.Visible = false;

            for(int i = 1; i <= scene.numParticipants; i++)
            {
                typeDropdown[0].Items.Add("Character " + i.ToString());
                typeDropdown[1].Items.Add("Character " + i.ToString());
            }
            typeDropdown[0].Items.Add("Time of day");
            typeDropdown[1].Items.Add("[Raw value]");

            requirement = SceneEditor.selectedRequirement;

            textBox1.Text = requirement.requirement;

            ParseTokensFromRequirement(requirement.requirement);

            UpdateTextBox();
        }

        private void ParseTokensFromRequirement(string requirement)
        {
            // time [comparator] morning/afternoon/...
            // character 1 name [comparator] "value"

            string buffer = "";
            string comparator = "";

            bool inQuote = false;
            bool escapeNextChar = false;
            bool expectingCharacterField = false;
            bool expectingCharacterNumber = false;
            bool expectingTime = false;
            bool expectingRawValue = false;

            char[] charArray = requirement.ToCharArray();

            int expressionIndex = 0;

            for (int i = 0; i <= charArray.Length; i++)
            {
                if (i < charArray.Length)
                {
                    char character = charArray[i];
                    if (escapeNextChar)
                    {
                        buffer += character;
                        escapeNextChar = false;
                    }
                    else if (character == '\\')
                    {
                        escapeNextChar = true;
                    }
                    else if (character == '\"')
                    {
                        inQuote = !inQuote;
                    }
                    else if (inQuote || character != ' ')
                    {
                        buffer += character;
                        continue;
                    }
                }
                if (buffer.Length > 0)
                {
                    if (expectingCharacterNumber)
                    {
                        TypeDropdown1.SelectedItem = "Character " + buffer;
                        expectingCharacterField = true;
                        expectingCharacterNumber = false;
                    }
                    else if (expectingCharacterField)
                    {
                        switch (buffer)
                        {
                            case "status":
                                attributeDropdown1.SelectedIndex = 0;
                                break;
                            case "environment":
                                attributeDropdown1.SelectedIndex = 1;
                                break;
                            case "name":
                                attributeDropdown1.SelectedIndex = 2;
                                break;
                        }
                        expectingCharacterField = false;
                        expectingRawValue = true;
                    }
                    else if (buffer == "is" || buffer == "not" || buffer == "contains" || buffer == "greater" || buffer == "less" || buffer == "than")
                    {
                        comparator += buffer + " ";
                        expressionIndex = 1;
                    }
                    else if (buffer == "time")
                    {
                        TypeDropdown1.SelectedIndex = TypeDropdown1.Items.Count - 1;
                        expectingTime = true;
                    }
                    else if (buffer == "character")
                    {
                        // Buffer indicates the start of a character field
                        attributeDropdown[expressionIndex].Visible = true;
                        expectingCharacterNumber = true;
                    }
                    else if (expectingRawValue)
                    {
                        RawBox2.Text = buffer.Trim('\"');
                    }
                    else if (expectingTime)
                    {
                        for (int j = 0; j < timeOfDayDropdown.Items.Count; j++)
                        {
                            string item = (string) timeOfDayDropdown.Items[j];
                            if(item.ToLower().Equals(buffer))
                            {
                                timeOfDayDropdown.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                    buffer = "";
                }
            }
            if (comparator.Contains("greater"))
            {
                comparisionBox.SelectedIndex = 4;
            }
            else if (comparator.Contains("less"))
            {
                comparisionBox.SelectedIndex = 5;
            }
            else if (comparator.Contains("contain"))
            {
                if (comparator.Contains("not"))
                {
                    comparisionBox.SelectedIndex = 3;
                }
                else
                {
                    comparisionBox.SelectedIndex = 2;
                }
            }
            else if (comparator.Contains("is"))
            {
                if (comparator.Contains("not"))
                {
                    comparisionBox.SelectedIndex = 1;
                }
                else
                {
                    comparisionBox.SelectedIndex = 0;
                }
            }
        }

        private string UpdateTextBox()
        {
            textBox1.Text = "";
            string result = "";

            if (comparisionBox.SelectedIndex == -1)
            {
                textBox1.Text = "";
                return "";
            }

            string comparator = comparisionBox.Text.ToLower();
            int i = TypeDropdown1.SelectedIndex;
            if (i == -1)
            {
                return result;
            }
            else if (i == TypeDropdown1.Items.Count - 1)
            {
                // Time of day
                result += "time ";

                if(timeOfDayDropdown.SelectedIndex == -1)
                {
                    return "";
                }

                result += comparator + " ";
                result += timeOfDayDropdown.SelectedItem.ToString().ToLower();
            }
            else
            {
                // Character
                result += TypeDropdown1.SelectedItem.ToString().ToLower() + " ";

                if (attributeDropdown1.SelectedIndex == -1)
                {
                    return "";
                }

                string field = attributeDropdown1.SelectedItem.ToString().ToLower();

                result += field + " ";

                if(field == "flag")
                {
                    if (RawBox1.Text == "")
                    {
                        return "";
                    }
                    result += "\"" + RawBox1.Text + "\" ";
                }

                if (RawBox2.Text == "")
                {
                    return "";
                }

                result += comparator + " ";
                result += "\"" + RawBox2.Text + "\"";
            }

            textBox1.Text = result;
            return result;
        }

        private void TypeDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = TypeDropdown1.SelectedIndex;
            if (i == -1) {
                TypeDropdown2.Visible = false;
            }else if(i == TypeDropdown1.Items.Count - 1) {
                // Time of day
                attributeDropdown1.Visible = false;
                RawBox1.Visible = false;
                TypeDropdown2.Visible = false;
                attributeDropdown2.Visible = false;
                RawBox2.Visible = false;
                timeOfDayDropdown.Visible = true;
            }
            else
            {
                // Character
                attributeDropdown1.Visible = true;
                RawBox1.Visible = false;
                TypeDropdown2.Visible = false;
                attributeDropdown2.Visible = false;
                RawBox2.Visible = true;
                timeOfDayDropdown.Visible = false;
            }
            UpdateTextBox();
        }

        private void TypeDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = (string)TypeDropdown2.SelectedItem;
            if (selected.Contains("Character"))
            {
                rawBox[1].Visible = false;
                attributeDropdown[1].Visible = true;
            }
            else if (selected == "[Raw value]")
            {
                rawBox[1].Visible = true;
                attributeDropdown[1].Visible = false;
            }
            else if (selected == "Time")
            {
                rawBox[1].Visible = false;
                attributeDropdown[1].Visible = false;
            }
            UpdateTextBox();
        }

        private void RawBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

        private void RawBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

        private void comparisionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

        private void attributeDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Status, Environment, Name, Flag
            RawBox1.Visible = false;
            TypeDropdown2.Visible = false;
            attributeDropdown2.Visible = false;
            RawBox2.Visible = true;
            timeOfDayDropdown.Visible = false;
            switch (attributeDropdown1.SelectedIndex)
            {
                case -1:
                    RawBox2.Visible = false;
                    timeOfDayDropdown.Visible = TypeDropdown1.SelectedIndex == TypeDropdown1.Items.Count - 1;
                    break;
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    RawBox1.Visible = true;
                    break;
            }

            UpdateTextBox();
        }

        private void attributeDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBox();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != requirement.requirement)
            {
                requirement.requirement = textBox1.Text;
            }
            SceneEditor.requirementBox.DrawMode = DrawMode.OwnerDrawFixed;
            SceneEditor.requirementBox.DrawMode = DrawMode.Normal;

            SceneEditor.unsavedChanges = true;
            SceneEditor.saveButtonRef.Enabled = true;

            this.Close();
        }

        private void timeOfDayDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBox();
        }
    }
}
