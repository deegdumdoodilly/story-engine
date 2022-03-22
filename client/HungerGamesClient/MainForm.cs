using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HungerGamesClient
{   
    public struct Ballot
    {
        public int id;
        public Performance performance;
        public User voter;
        public Outcome chosenOutcome;
        public bool hasChosenOutcome;
        public bool inProgress;

        public Ballot(int id, Performance performance, int voterId, Outcome chosenOutcome, bool inProgress)
        {
            this.id = id;
            this.performance = performance;
            this.voter = MainForm.userList.Find(x => x.id == voterId);
            this.chosenOutcome = chosenOutcome;
            this.inProgress = inProgress;
            hasChosenOutcome = (chosenOutcome.id == -1);
        }

        public Ballot(JsonObject json)
        {
            id = json.GetInt("id");
            performance = MainForm.performanceList.Find(x => x.id == json.GetInt("performanceId"));
            voter = MainForm.userList.Find(x => x.id == json.GetInt("voterId"));
            inProgress = json.GetBool("inProgress");
            if (json.GetBool("hasChosenOutcome")) {
                hasChosenOutcome = true;
                int chosenOutcomeId = json.GetInt("chosenOutcomeId");
                chosenOutcome = new Outcome();
                foreach (Outcome outcome in performance.scene.outcomes)
                {
                    if(outcome.id == chosenOutcomeId)
                    {
                        chosenOutcome = outcome;
                        break;
                    }
                }
            }
            else
            {
                hasChosenOutcome = false;
                chosenOutcome = new Outcome();
            }
        }

        public override string ToString()
        {
            string output = performance.scene.sceneName + " (";
            foreach (Actor actor in performance.participants)
            {
                output += actor.name + ", ";
            }
            return output.Substring(0, output.Length - 2) + ")";
        }
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static MainForm reference;
        public static List<Scene> sceneList;
        public static List<Performance> performanceList;
        public static List<Actor> actorList;
        public static List<Ballot> ballotList;
        public static List<User> userList;

        public static User currentUser;
        private VotingBooth votingBooth;
        private RosterForm rosterForm;
        private UserLogin userLogin;

        public static string GetConnectionString()
        {
            return "server=hungergames-db.cwbqbtsmrk8y.us-east-1.rds.amazonaws.com;" +
                                                   "uid=admin;" +
                                                   "pwd=Password1!;" +
                                                   "database=" + Properties.Settings.Default.db_name;
        }
        
        public static void Log(string s)
        {

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshLog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reference = this;
            UpdateUserLabel();
            RefreshLog();
            if (userLogin is null)
            {
                userLogin = new UserLogin();
            }
            userLogin.UpdateOptions();
            Dropdown.SelectedIndex = 0;
        }

        public void RefreshLog()
        {
            SyncData();
            logPanel.Controls.Clear();
            foreach (Performance s in performanceList)
            {
                if (!s.inProgress)
                {
                    AddPortraits(s, logPanel);
                    AddDescription(s);
                }
            }
        }

        public void SyncData()
        {
            try
            {
                sceneList = new List<Scene>();
                FetchActors();
                FetchPerformances();
                FetchBallots();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed refresh.\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void FetchActors()
        {
            List<JsonObject> jsonList = JsonObject.GetJsonsFromRequest("/actors");
            actorList = new List<Actor>();
            foreach (JsonObject j in jsonList)
            {
                Actor newActor = new Actor(j);
                actorList.Add(newActor);
            }
        }

        public static void FetchPerformances()
        {
            performanceList = new List<Performance>();

            List<JsonObject> performanceJsons = JsonObject.GetJsonsFromRequest("/performances");

            foreach(JsonObject json in performanceJsons)
            {
                performanceList.Add(new Performance(json));
            }
        }

        public static void FetchBallots()
        {
            List<JsonObject> voteJSONs = JsonObject.GetJsonsFromRequest("/votes");

            ballotList = new List<Ballot>();

            foreach(JsonObject json in voteJSONs)
            {
                ballotList.Add(new Ballot(json));
            }
        }

        public static Scene GetScene(int id)
        {
            foreach(Scene s in sceneList){
                if(s.sceneId == id)
                {
                    return s;
                }
            }
            JsonObject sceneJSON = JsonObject.GetJsonFromRequest("/scenes?id=" + id);
            Scene newScene = new Scene(sceneJSON);
            sceneList.Add(newScene);
            return newScene;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshLog();
            if(!(votingBooth is null))
            {
                votingBooth.RefreshBooth();
            }
        }

        private void AddDescription(Performance p)
        {
            string text = p.flavor;
            if(text == "")
            {
                text = p.GetDescription() + " " + p.GetChosenOutcome().GetDescription(p);
            }

            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Font = new System.Drawing.Font("Baskerville Old Face", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Text = text;
            logPanel.Controls.Add(newLabel);
        }

        /*private string FormatDescription(Performance s)
        {
            string description = s.scene.description;
            for (int role = 1; role <= s.participants.Length; role++)
            {
                string name = s.participants[role - 1].name;
                description = description.Replace("{" + role + "}", name);
            }
            description = description.Replace("}", "");
            description = description.Replace("{", "");


            return description;

        }*/

        public static void AddPortraits(Performance stackScene, FlowLayoutPanel parentPanel)
        {
            Actor[] participants = stackScene.participants;
            TableLayoutPanel panel = new TableLayoutPanel();

            panel.AutoSize = true;
            panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            panel.ColumnCount = participants.Length;
            foreach (Actor i in participants)
                panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / participants.Length));
            panel.Dock = DockStyle.Top;
            panel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            panel.RowCount = 1;
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            panel.Size = new Size(994, 80);
            panel.MinimumSize = new Size(0, 80);
            panel.TabIndex = 18;

            for (int i = 0; i < participants.Length; i++)
            {
                string actorName = participants[i].name;

                PictureBox picture1 = new PictureBox();
                picture1.Image = Image.FromFile("Images/" + actorName.ToLower() + "_icon.png");
                picture1.Anchor = AnchorStyles.Right & AnchorStyles.Left;
                picture1.Dock = DockStyle.Fill;
                picture1.SizeMode = PictureBoxSizeMode.Zoom;

                panel.Controls.Add(picture1, i, 0);
            }

            parentPanel.Controls.Add(panel);
        }

        private void votingBoothButton_Click(object sender, EventArgs e)
        {
            if (votingBooth is null)
            {
                votingBooth = new VotingBooth();
            }
            else
            {
                votingBooth.RefreshBooth();
            }
            votingBooth.Show();
        }

        private void loginLabel_Click(object sender, EventArgs e)
        {
            if (userLogin is null)
            {
                userLogin = new UserLogin();
            }
            userLogin.UpdateOptions();
            userLogin.ShowDialog();
        }

        public void UpdateUserLabel()
        {
            loginLabel.Text = "User: " + currentUser.username;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(!(rosterForm is null))
            {
                rosterForm.Close();
            }
            rosterForm = new RosterForm();
            rosterForm.Show();
        }
    }

    

}
