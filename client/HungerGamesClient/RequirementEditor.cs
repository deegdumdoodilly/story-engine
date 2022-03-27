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

            for(int i = 1; i <= scene.numParticipants; i++)
            {
                typeDropdown[0].Items.Add("Character " + i.ToString());
                typeDropdown[1].Items.Add("Character " + i.ToString());
            }
            typeDropdown[0].Items.Add("[Raw value]");
            typeDropdown[0].Items.Add("Time");
            typeDropdown[1].Items.Add("[Raw value]");
            typeDropdown[1].Items.Add("Time");

            requirement = SceneEditor.selectedRequirement;

            textBox1.Text = requirement.requirement;

            ParseTokensFromRequirement(requirement.requirement);

            UpdateTextBox();
        }

        private void ParseTokensFromRequirement(string requirement)
        {
            string buffer = "";
            string comparator = "";

            bool inQuote = false;
            bool escapeNextChar = false;
            bool expectingCharacterField = false;
            bool expectingCharacterNumber = false;

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
                        typeDropdown[expressionIndex].SelectedItem = "Character " + buffer;
                        expectingCharacterField = true;
                        expectingCharacterNumber = false;
                    }
                    else if (expectingCharacterField)
                    {
                        switch (buffer)
                        {
                            case "status":
                                attributeDropdown[expressionIndex].SelectedIndex = 0;
                                break;
                            case "environment":
                                attributeDropdown[expressionIndex].SelectedIndex = 1;
                                break;
                            case "name":
                                attributeDropdown[expressionIndex].SelectedIndex = 2;
                                break;
                        }
                        expectingCharacterField = false;
                    }
                    else if (buffer == "is" || buffer == "not" || buffer == "contains" || buffer == "greater" || buffer == "less" || buffer == "than")
                    {
                        comparator += buffer;
                        expressionIndex = 1;
                    }
                    else if (buffer == "time")
                    {
                        typeDropdown[expressionIndex].SelectedIndex = 2;
                    }
                    else if (buffer == "character")
                    {
                        // Buffer indicates the start of a character field
                        attributeDropdown[expressionIndex].Visible = true;
                        expectingCharacterNumber = true;
                    }
                    else
                    {
                        // Buffer contains a literal value.
                        typeDropdown[expressionIndex].SelectedIndex = 1;
                        rawBox[expressionIndex].Visible = true;
                        rawBox[expressionIndex].Text = buffer;
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
            if(TypeDropdown1.SelectedItem is null || TypeDropdown2.SelectedItem is null || comparisionBox.SelectedItem is null)
            {
                return "";
            }
            string result = "";
            string type1 = ((string)TypeDropdown1.SelectedItem).ToLower();
            if(type1 == "raw value")
            {
                result += "\"" + rawBox[0].Text + "\" ";
            }
            else
            {
                result += type1 + " ";

                if(type1 != "time")
                {
                    if(attributeDropdown[0].SelectedItem is null)
                    {
                        return "";
                    }
                    result += attributeDropdown[0].SelectedItem.ToString().ToLower() + " ";
                }
            }

            result += comparisionBox.Text.ToLower() + " ";

            string type2 = ((string)TypeDropdown2.SelectedItem).ToLower();
            if (type2 == "raw value")
            {
                result += "\"" + rawBox[1].Text + "\"";
            }
            else
            {
                result += type2;

                if (type2 != "time")
                {
                    if (attributeDropdown[1].SelectedItem is null)
                    {
                        return "";
                    }
                    result += " " + attributeDropdown[1].SelectedItem.ToString().ToLower();
                }
            }

            textBox1.Text = result;
            return result;
        }

        private void TypeDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = (string)TypeDropdown1.SelectedItem;
            if (selected.Contains("Character"))
            {
                rawBox[0].Visible = false;
                attributeDropdown[0].Visible = true;
            }
            else if(selected == "[Raw value]")
            {
                rawBox[0].Visible = true;
                attributeDropdown[0].Visible = false;
            }
            else if(selected == "Time")
            {
                rawBox[0].Visible = false;
                attributeDropdown[0].Visible = false;
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
    }
}
