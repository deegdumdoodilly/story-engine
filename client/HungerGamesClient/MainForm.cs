using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HungerGamesClient
{   
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
        private EditSim editSim;
        private SceneEditor sceneEditor;

        public Button editSimButtonRef;
        public Button editScenesButtonRef;

        private int logPanelWidthDifference = 0;

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
            editScenesButtonRef = editScenesButton;
            editSimButtonRef = editSimButton;

            logPanelWidthDifference = logPanel.Width - panel1.Width;

            reference = this;

            SyncData();
            UpdateUserLabel();
            RefreshLog();
            if (userLogin is null)
            {
                userLogin = new UserLogin();
            }
            userLogin.UpdateOptions();
            Dropdown.SelectedIndex = 0;

            RefreshButton.Focus();
        }

        public void RefreshLog()
        {
            SyncData();
            Control preservePanel = logPanel.Controls[0];
            logPanel.Controls.Clear();
            logPanel.Controls.Add(preservePanel);
            foreach (Performance s in performanceList)
            {
                if (!s.inProgress)
                {
                    AddPortraits(s, logPanel);
                    AddDescription(s);
                }
            }
            ResizeLog();
        }

        public void SyncData()
        {
            try
            {
                FetchScenes();
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
            actorList.Sort();
        }

        public static void FetchScenes()
        {
            sceneList = new List<Scene>();

            List<JsonObject> sceneJsons = JsonObject.GetJsonsFromRequest("/scenes");

            foreach(JsonObject json in sceneJsons)
            {
                sceneList.Add(new Scene(json));
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            RefreshLog();
            if(!(votingBooth is null))
            {
                votingBooth.RefreshBooth();
            }
            RefreshButton.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void AddDescription(Performance p)
        {
            string text = p.flavor;
            if(text == "")
            {
                text = p.GetDescription() + "\n" + p.GetChosenOutcome().GetDescription(p);
            }

            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Font = new Font("Baskerville Old Face", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Text = text;
            logPanel.Controls.Add(newLabel);
        }

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
                try
                {
                    picture1.Image = Image.FromFile("Images/" + actorName.ToLower() + "_icon.png");
                }
                catch
                {
                    picture1.Image = Image.FromFile("Images/default_icon.png");
                }
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
                votingBooth.Initialize();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (sceneEditor is null || sceneEditor.IsDisposed)
            {
                editScenesButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                sceneEditor = new SceneEditor();
                sceneEditor.Show();
            }
            else
            {
                sceneEditor.Focus();
            }
        }

        private void editSimButton_Click(object sender, EventArgs e)
        {
            SyncData();
            editSimButton.Enabled = false;
            if (editSim is null || editSim.IsDisposed)
            {
                Cursor = Cursors.WaitCursor;
                editSim = new EditSim();
                editSim.Show();
            }
            else
            {
                editSim.Focus();
                editSimButton.Enabled = true;
            }
        }

        private void logPanel_ChangeSize(object sender, System.EventArgs e)
        {
            ResizeLog();
        }

        private void ResizeLog()
        {
            panel1.Width = logPanel.Width - 50;
        }
    }

    

}
