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
    public partial class CreateAndResolve : Form
    {
        private Scene scene;
        public CreateAndResolve()
        {
            InitializeComponent();
        }

        private void CreateAndResolve_Load(object sender, EventArgs e)
        {
            scene = RunScene.scene;

            int yLocation = 33;
            foreach (Outcome outcome in scene.outcomes)
            {
                Button newButton = new Button();
                newButton.Location = new Point(16, yLocation);
                newButton.Name = "" + outcome.id;
                newButton.Size = new Size(411, 61);
                newButton.TabIndex = 1 + (yLocation-33)/67;
                newButton.Text = outcome.GetDescription(RunScene.performance);

                newButton.UseVisualStyleBackColor = true;
                newButton.Click += new EventHandler(button_Click);

                this.Controls.Add(newButton);

                yLocation += 67;

                this.Size = new Size(this.Size.Width, this.Size.Height + 67);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Outcome chosenOutcome = null;
            foreach (Outcome outcome in scene.outcomes)
            {
                if(outcome.id.ToString() == ((Button)sender).Name)
                {
                    chosenOutcome = outcome;
                    break;
                }
            }


            JsonObject response = JsonObject.PostWithJson("/performances/add", RunScene.performance.toJSON());
            string time = JsonObject.GetStringFromRequest("/time");
            Performance newPerformance = new Performance(response);
            MainForm.performanceList.Add(newPerformance);

            Ballot vote = new Ballot(-1, newPerformance, int.Parse(time), -1, chosenOutcome, true);

            vote = new Ballot(JsonObject.PostWithJson("/votes/add", vote.ToJSON()));

            // update winningballot id
            newPerformance.winningVoteId = vote.id;
            JsonObject.PostWithJson("/performances/add", newPerformance.toJSON());

            JsonObject.Post("/simulation/execute?performanceId=" + newPerformance.id);


            Cursor = Cursors.Default;
            MessageBox.Show("Performance successfully created and resolved!");

            Close();
        }
    }
}
