using System;
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
    public partial class RunScene : Form
    {
        private List<ComboBox> participantDropdowns;
        private List<Label> labels;
        private List<PictureBox> pictureBoxes;
        private List<string> names;

        private Scene scene;

        public RunScene()
        {
            InitializeComponent();
        }

        private void RunScene_Load(object sender, EventArgs e)
        {
            testButton.Enabled = false;
            executeButton.Enabled = false;
            scene = SceneEditor.selectedScene;

            label1.Text = scene.sceneName;

            participantDropdowns = new List<ComboBox>() { participant1, participant2, participant3, participant4, participant5, participant6 };
            labels = new List<Label>() { label2, label3, label4, label5, label6, label7 };
            pictureBoxes = new List<PictureBox>() { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };
            names = new List<string>();

            for(int i = 0; i < scene.numParticipants; i++)
            {
                participantDropdowns[i].Visible = true;
                foreach(Actor a in MainForm.actorList)
                    participantDropdowns[i].Items.Add(a);
                labels[i].Visible = true;
                pictureBoxes[i].Visible = true;
                names.Add("{" + (i + 1) + "}");
            }

            descriptionBox.Text = scene.DescribeWithNames(names);
        }

        private void DropdownChanged(int index)
        {
            string name = ((Actor)participantDropdowns[index].SelectedItem).name;
            pictureBoxes[index].Image = Image.FromFile("Images/" + name.ToLower() + "_icon.png");
            names[index] = name;

            descriptionBox.Text = scene.DescribeWithNames(names);

            testButton.Enabled = false;
            executeButton.Enabled = false;
            for (int i = 0; i < scene.numParticipants; i++)
            {
                if (participantDropdowns[i].SelectedIndex == -1)
                    return;
                for (int j = i + 1; j < scene.numParticipants; j++)
                {
                    if (participantDropdowns[i].SelectedIndex == participantDropdowns[j].SelectedIndex)
                        return;
                }
            }
            testButton.Enabled = true;
            executeButton.Enabled = true;
        }

        private void participant1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(0);
        }

        private void participant2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(1);
        }

        private void participant3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(2);
        }

        private void participant4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(3);
        }

        private void participant5_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(4);
        }

        private void participant6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownChanged(5);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            string cast = "";
            for (int i = 0; i < scene.numParticipants; i++)
                cast += ((Actor)participantDropdowns[i].SelectedItem).id + ",";
            Performance testPerformance = new Performance(-1, scene, cast.Trim(','), MainForm.actorList);

            JsonObject response = JsonObject.PostWithJson("/performances/validate", testPerformance.toJSON());

            if(response.GetString("result").Equals("valid"))
            {
                MessageBox.Show("Test passed! Scene is ready to be executed.");
            }
            else
            {
                MessageBox.Show("Test failed for one or more reasons. Make sure the involved characters meet all requirements.");
            }
        }
    }
}
