using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace HungerGamesClient
{
    public struct StackScene
    {
        public int stackId;
        public Scene scene;
        public bool inProgress;
        public Outcome outcome;
        public int winningVoteId;
        public string flavor;
        public Character[] participants;

        public StackScene(int id, Scene scene, string participants, List<Character> characterListReference)
        {
            this.stackId = id;
            this.scene = scene;
            inProgress = true;
            winningVoteId = -1;
            flavor = "";
            string[] participantsSplit = participants.Split(',');
            this.participants = new Character[participantsSplit.Length];
            for(int i = 0; i < participantsSplit.Length; i++)
            {
                int participantId = int.Parse(participantsSplit[i]);
                this.participants[i] = characterListReference.Find(character => character.id == participantId);
            }
            outcome = new Outcome();
        }

        public StackScene(int id, Scene scene, string participants, List<Character> characterListReference, bool inProgress, int winningVoteId)
            : this(id, scene, participants, characterListReference)
        {
            this.inProgress = inProgress;
            this.winningVoteId = winningVoteId;
            flavor = "";
        }

        public StackScene(int id, Scene scene, string participants, List<Character> characterListReference, bool inProgress, int winningVoteId, string flavor)
            : this(id, scene, participants, characterListReference, inProgress, winningVoteId)
        {
            this.flavor = flavor;
        }

        public string ToMySQLString()
        {
            string participantString = "";
            foreach(Character c in participants)
            {
                participantString += (c.id + ",");
            }
            participantString = participantString.Substring(0, participantString.Length - 1);
            return String.Format("({0},{1},{2},\"{3}\",\"{4}\")", scene.sceneId, inProgress ? 1 : 0, winningVoteId, flavor, participantString);
        }

        public override string ToString()
        {
            string output = scene.sceneName + " (";
            foreach(Character character in participants)
            {
                output += character.name + ", ";
            }
            return output.Substring(0, output.Length - 2) + ")";
        }

        public string GetDescription()
        {
            string description = scene.description;
            for (int role = 1; role <= participants.Length; role++)
            {
                string name = participants[role - 1].name;
                description = description.Replace("{" + role + "}", name);
            }
            description = description.Replace("}", "");
            description = description.Replace("{", "");


            return description;
        }
    }

    public struct Scene
    {
        public int sceneId;
        public string sceneName;
        public List<Requirement> requirements;
        public List<Outcome> outcomes;
        public string description;

        public Scene(int sceneId, string sceneName, List<Requirement> requirements, List<Outcome> outcomes, string description)
        {
            this.sceneId = sceneId;
            this.sceneName = sceneName;
            this.requirements = requirements;
            this.outcomes = outcomes;
            this.description = description;
        }
    }

    public struct Requirement
    {
        public int id;
        public int sceneId;
        public string requirement;
        public int role;

        public Requirement(int id, int sceneId, string requirement, int role)
        {
            this.id = id;
            this.sceneId = sceneId;
            this.requirement = requirement;
            this.role = role;
        }
    }

    public enum OutcomeType { Positive, Negative, Neutral };

    public struct Outcome
    {

        public int id;
        public int sceneId;
        public OutcomeType outcomeType;
        public string effect;
        public string description;

        public Outcome(int id, int sceneId, int outcomeInt, string effect, string description)
        {
            this.id = id;
            this.sceneId = sceneId;
            this.effect = effect;
            this.description = description;
            if (outcomeInt < 0)
                this.outcomeType = OutcomeType.Negative;
            else if (outcomeInt > 0)
                this.outcomeType = OutcomeType.Positive;
            else
                this.outcomeType = OutcomeType.Neutral;
        }

        public string GetDescription(StackScene parentScene)
        {
            string result = description;
            for (int role = 1; role <= parentScene.participants.Length; role++)
            {
                string name = parentScene.participants[role - 1].name;
                result = result.Replace("{" + role + "}", name);
            }
            result = result.Replace("}", "");
            result = result.Replace("{", "");

            return result;
        }
    }

    public struct Character
    {
        public int id;
        public string name;
        public int lastAte;
        public string environment;

        public Character(int id, string name, int lastAte, string environment)
        {
            this.id = id;
            this.name = name;
            this.lastAte = lastAte;
            this.environment = environment;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            Character other = (Character)obj;
            return (this.id == other.id &&
                    this.name == other.name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
    public struct User
    {
        public int id;
        public string username;
        public bool hasPassword;

        public int votingChances;
        public int positiveVotes;
        public int negativeVotes;
        public int neutralVotes;

        public bool validVoter;
        public User(int id, string username, bool hasPassword, int votingChances, int positiveVotes, int negativeVotes, int neutralVotes, bool validVoter)
        {
            this.id = id;
            this.username = username;
            this.hasPassword = hasPassword;

            this.votingChances = votingChances;
            this.positiveVotes = positiveVotes;
            this.negativeVotes = negativeVotes;
            this.neutralVotes = neutralVotes;

            this.validVoter = validVoter;
        }

        public bool CanCastNegativeVote()
        {
            return ((positiveVotes * 3 + neutralVotes) - negativeVotes * 3) >= 3;
        }

        public override string ToString()
        {
            return username;
        }
    }
   
    public struct Ballot
    {
        public int id;
        public StackScene stackScene;
        public User voter;
        public Outcome chosenOutcome;
        public bool hasChosenOutcome;
        public bool inProgress;

        public Ballot(int id, StackScene stackScene, int voterId, Outcome chosenOutcome, bool inProgress)
        {
            this.id = id;
            this.stackScene = stackScene;
            this.voter = MainForm.userList.Find(x => x.id == voterId);
            this.chosenOutcome = chosenOutcome;
            this.inProgress = inProgress;
            hasChosenOutcome = (chosenOutcome.id == -1);
        }

        public override string ToString()
        {
            string output = stackScene.scene.sceneName + " (";
            foreach (Character character in stackScene.participants)
            {
                output += character.name + ", ";
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
        public static List<StackScene> stackSceneList;
        public static List<Character> characterList;
        public static List<Ballot> ballotList;
        public static List<User> userList;

        public static User currentUser;
        private VotingBooth votingBooth;
        private UserLogin userLogin;

        public static string GetConnectionString()
        {
            return "server=hungergames-db.cwbqbtsmrk8y.us-east-1.rds.amazonaws.com;" +
                                                   "uid=admin;" +
                                                   "pwd=Password1!;" +
                                                   "database=" + Properties.Settings.Default.db_name;
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
            foreach (StackScene s in stackSceneList)
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
                using (MySqlConnection cnn = new MySqlConnection())
                {

                    cnn.ConnectionString = GetConnectionString();
                    FetchCharacters(cnn);
                    FetchSceneStack(cnn);
                    FetchBallots(cnn, new int[] { 1, 2, 3, 4, 5, 6, 7});
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed refresh.\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void FetchCharacters(MySqlConnection cnn)
        {
            cnn.Open();
            string query = "SELECT id, name, last_ate, environment FROM hungergames.characters;";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            characterList = new List<Character>();
            while (reader.Read())
            {
                // int id, string name, int lastAte, string environment
                Character newCharacter = new Character(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                characterList.Add(newCharacter);
            }
            cnn.Close();
        }

        public static void FetchSceneStack(MySqlConnection cnn)
        {
            cnn.Open();
            string query = "SELECT id, scene_id, participants, in_progress, winning_vote, flavor FROM hungergames.scene_stack;";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            stackSceneList = new List<StackScene>();

            // Need to do these seperately to maintain the one connection per client policy
            List<int> id = new List<int>();
            List<int> sceneId = new List<int>();
            List<string> participants = new List<string>();
            List<bool> inProgress = new List<bool>();
            List<int> winningVote = new List<int>();
            List<string> flavor = new List<string>();
            while (reader.Read())
            {
                id.Add(reader.GetInt32(0));
                sceneId.Add(reader.GetInt32(1));
                participants.Add(reader.GetString(2));
                inProgress.Add(reader.GetBoolean(3));
                if (!reader.IsDBNull(4))
                {
                    winningVote.Add(reader.GetInt32(4));
                }
                else
                {
                    winningVote.Add(-1);
                }
                flavor.Add(reader.GetString(5));
            }
            cnn.Close();


            for (int i = 0; i < id.Count; i++)
            {
                StackScene newStackScene = new StackScene(id[i], FetchScene(cnn, sceneId[i]), participants[i], characterList, inProgress[i], winningVote[i], flavor[i]);
                if (!inProgress[i])
                {
                    cnn.Open();
                    query = "SELECT id, chosen_outcome FROM hungergames.votes WHERE scene_stack_id = " + newStackScene.stackId + ";";
                    command = new MySqlCommand(query, cnn);

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if(reader.GetInt32(0) == newStackScene.winningVoteId)
                        {
                            newStackScene.outcome = newStackScene.scene.outcomes.Find(x => x.id == reader.GetInt32(1));
                        }
                    }
                    cnn.Close();
                }
                stackSceneList.Add(newStackScene);
            }
        }

        public static void FetchBallots(MySqlConnection cnn, int[] voter_ids)
        {
            cnn.Open();

            string permittedIds = "(";
            foreach (int i in voter_ids)
                permittedIds += i + ",";
            permittedIds = permittedIds.Substring(0, permittedIds.Length - 1) + ")";

            string query = "SELECT id, scene_stack_id, voter_id, chosen_outcome, in_progress, has_chosen_outcome FROM hungergames.votes WHERE voter_id IN " + permittedIds + " OR in_progress = 0;";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            ballotList = new List<Ballot>();
            while (reader.Read())
            {
                int ballotId = reader.GetInt32(0);
                int sceneStackId = reader.GetInt32(1);
                StackScene stackScene = MainForm.stackSceneList.Find(x => x.stackId == sceneStackId);
                Outcome chosenOutcome = new Outcome();
                int voterId = -1;
                if (!reader.IsDBNull(2))
                    voterId = reader.GetInt32(2);
                if (reader.GetBoolean(5))
                {
                    int outcomeId = reader.GetInt32(3);
                    chosenOutcome = stackScene.scene.outcomes.Find(x => x.id == outcomeId);
                }
                Ballot newBallot = new Ballot(ballotId, stackScene, voterId, chosenOutcome, reader.GetBoolean(5));
                ballotList.Add(newBallot);
            }
        }

        public static Scene FetchScene(MySqlConnection cnn, int id)
        {
            cnn.Open();
            string query = "SELECT * FROM hungergames.scenes where id=" + id + ";";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();
            int sceneId = reader.GetInt32(0);
            string name = reader.GetString(1);
            string desc = reader.GetString(6);
            cnn.Close();

            List<Requirement> requirements = FetchRequirements(cnn, sceneId);
            List<Outcome> outcomes = FetchOutcomes(cnn, sceneId);
            return new Scene(sceneId, name, requirements, outcomes, desc);
        }

        public static List<Requirement> FetchRequirements(MySqlConnection cnn, int sceneId)
        {
            cnn.Open();
            string query = "SELECT * FROM hungergames.requirements WHERE requirement_scene_id = " + sceneId + ";";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            List<Requirement> requirementsList = new List<Requirement>();
            while (reader.Read())
            {
                Requirement newRequirement = new Requirement(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                requirementsList.Add(newRequirement);
            }
            cnn.Close();
            return requirementsList;
        }

        public static List<Outcome> FetchOutcomes(MySqlConnection cnn, int sceneId)
        {
            cnn.Open();
            string query = "SELECT * FROM hungergames.outcomes WHERE outcome_scene_id=" + sceneId + ";";
            MySqlCommand command = new MySqlCommand(query, cnn);

            MySqlDataReader reader = command.ExecuteReader();

            List<Outcome> outcomeList = new List<Outcome>();
            while (reader.Read())
            {
                Outcome outcome = new Outcome(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                outcomeList.Add(outcome);
            }
            cnn.Close();
            return outcomeList;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Text = DateTime.Now.ToString("T") + "Here's some more text that will make the message longer. This will help test things out.";
            logPanel.Controls.Add(newLabel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Text = "short";
            logPanel.Controls.Add(newLabel);
        }

        private void AddDescription(StackScene s)
        {
            string text = s.flavor;
            if(text == "")
            {
                text = s.GetDescription() + " " + s.outcome.GetDescription(s);
            }

            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Font = new System.Drawing.Font("Baskerville Old Face", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Text = text;
            logPanel.Controls.Add(newLabel);
        }

        private string FormatDescription(StackScene s)
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

        }

        public static void AddPortraits(StackScene stackScene, FlowLayoutPanel parentPanel)
        {
            Character[] participants = stackScene.participants;
            TableLayoutPanel panel = new TableLayoutPanel();

            panel.AutoSize = true;
            panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            panel.ColumnCount = participants.Length;
            foreach (Character i in participants)
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
                string characterName = participants[i].name;

                PictureBox picture1 = new PictureBox();
                picture1.Image = Image.FromFile("Images/" + characterName + "_icon.png");
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

    }
}
