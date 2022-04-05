using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class RunScene : Form
    {
        private List<ComboBox> participantDropdowns;
        private List<Label> labels;
        private List<PictureBox> pictureBoxes;
        private List<string> names;

        public static Scene scene;
        public static Performance performance;

        private CreateAndResolve createAndResolve;

        public RunScene()
        {
            InitializeComponent();
        }

        private void RunScene_Load(object sender, EventArgs e)
        {
            testButton.Enabled = false;
            executeButton.Enabled = false;
            resolveButton.Enabled = false;
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
            resolveButton.Enabled = false;
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
            resolveButton.Enabled = true;
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

        private bool TestScene()
        {
            string cast = "";
            for (int i = 0; i < scene.numParticipants; i++)
                cast += ((Actor)participantDropdowns[i].SelectedItem).id + ",";
            cast = cast.Trim(',');
            Performance testPerformance = new Performance(-1, scene, cast.Trim(','), MainForm.actorList);

            JsonObject response = JsonObject.PostWithJson("/performances/validate", testPerformance.toJSON());

            return response.GetString("result").Equals("valid");
        }

        private Performance CreatePerformance()
        {
            string cast = "";
            for (int i = 0; i < scene.numParticipants; i++)
                cast += ((Actor)participantDropdowns[i].SelectedItem).id + ",";
            cast = cast.Trim(',');
            Performance newPerformance = new Performance(-1, scene, cast.Trim(','), MainForm.actorList);
            return newPerformance;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if(TestScene())
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Test passed! Scene is ready to be executed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Test failed for one or more reasons. Make sure the involved characters meet all requirements.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!TestScene())
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Performance creation failed for one or more reasons. Make sure the involved characters meet all requirements.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Performance newPerformance = CreatePerformance();

            JsonObject response = JsonObject.PostWithJson("/performances/add", newPerformance.toJSON());
            performance = new Performance(response);

            Cursor = Cursors.Default;
            MessageBox.Show("Performance created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // /simulation/execute
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(createAndResolve != null && !createAndResolve.IsDisposed)
            {
                createAndResolve.Focus();
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                if (!TestScene())
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Performance creation failed for one or more reasons. Make sure the involved characters meet all requirements.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                performance = CreatePerformance();

                Cursor = Cursors.Default;

                createAndResolve = new CreateAndResolve();
                createAndResolve.Show();
            }
        }
    }
}
