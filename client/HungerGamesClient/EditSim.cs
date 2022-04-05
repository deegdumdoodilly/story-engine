using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class EditSim : Form
    {
        public Actor selectedActor;
        public string seedText;
        public static ListBox statusListBoxRef;
        public static ListBox flagListBoxRef;
        public static int time;

        private bool actorListLocked = false;
        private bool unsavedChanges = false;
        private bool saveButtonLocked = false;

        private int previousActorBoxIndex;

        private bool locked = false;

        public EditSim()
        {
            InitializeComponent();
        }

        private void EditSim_Load(object sender, EventArgs e)
        {
            locked = true;

            selectedActor = null;
            seedText = "";
            removeStatus.Enabled = false;
            addStatus.Enabled = false;
            statusListBoxRef = statusListBox;
            flagListBoxRef = flagBox;

            MainForm.reference.Cursor = Cursors.Default;

            actorListBox.DataSource = MainForm.actorList;

            UpdateTimeDisplay();
            MainForm.reference.editSimButtonRef.Enabled = true;

            locked = false;
        }

        private void UpdateTimeDisplay()
        {
            locked = true;
            time = int.Parse(JsonObject.GetStringFromRequest("/time"));
            dayInput.Value = 1 + (time / 4);
            timeInput.SelectedIndex = time % 4;
            locked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            JsonObject.Post("/users/reset");
            this.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            JsonObject.Post("/simulation/reset");
            MainForm.FetchBallots();
            MainForm.FetchPerformances();
            MainForm.FetchActors();
            MainForm.FetchScenes();
            UpdateTimeDisplay();

            actorListBox.DataSource = null;
            actorListBox.DataSource = MainForm.actorList;
            this.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void assignScenesButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            string append = "";
            if (seedText != "")
                append = "?seed=" + seedText;
            JsonObject.GetJsonsFromPost("/simulation/assign-performances" + append);
            MainForm.FetchPerformances();
            MainForm.FetchBallots();
            this.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void resolvePerformanceButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            string append = "";
            if (seedText != "")
                append = "?seed=" + seedText;
            JsonObject.GetJsonsFromPost("/simulation/execute" + append);
            JsonObject.GetJsonsFromPost("/time/advance" + append);
            UpdateTimeDisplay();
            MainForm.FetchPerformances();
            MainForm.FetchBallots();
            this.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void actorListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actorListLocked)
                return;

            if (unsavedChanges && MessageBox.Show("Warning, you have unsaved changes. Discard?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                actorListLocked = true;
                actorListBox.SelectedIndex = previousActorBoxIndex;
                actorListLocked = false;
                unsavedChanges = false;
                return;
            }

            saveButton.Enabled = false;

            saveButtonLocked = true;


            if (actorListBox.SelectedIndex == -1)
            {
                addStatus.Enabled = false;
                return;
            }
            previousActorBoxIndex = actorListBox.SelectedIndex;

            addStatus.Enabled = true;
            selectedActor = (Actor)actorListBox.SelectedItem;

            nameTextBox.Text = selectedActor.name;
            environmentBox.Text = selectedActor.environment;

            UpdateListBoxes();

            saveButtonLocked = false;
            unsavedChanges = false;
        }

        private void UpdateListBoxes()
        {
            statusListBox.Items.Clear();
            foreach (string status in selectedActor.statuses)
            {
                statusListBox.Items.Add(status);
            }

            flagBox.Items.Clear();
            foreach (KeyValuePair<string, string> flag in selectedActor.flags)
            {
                flagBox.Items.Add("" + flag.Key + ":" + flag.Value);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (locked)
                return;
            int output = -1;
            if(textBox1.Text == "" || int.TryParse(textBox1.Text, out output))
            {
                seedText = textBox1.Text;
            }
            else
            {
                textBox1.Text = seedText;
            }
        }

        private void dayInput_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
                return;
            this.Enabled = false;
            Cursor = Cursors.WaitCursor;
            JsonObject.PostWithJson("/time/set", ((dayInput.Value-1) * 4 + timeInput.SelectedIndex).ToString());
            this.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void statusListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
                return;
            removeStatus.Enabled = statusListBox.SelectedIndex != -1;
        }

        private void removeStatus_Click(object sender, EventArgs e)
        {
            if (statusListBox.SelectedIndex != -1)
            {
                int index = statusListBox.SelectedIndex;
                statusListBox.Items.RemoveAt(statusListBox.SelectedIndex);
                if (index >= statusListBox.Items.Count)
                    index--;
                statusListBox.SelectedIndex = index;
            }
        }

        private void addStatus_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            StatusDialog dialogBox = new StatusDialog();
            dialogBox.ShowDialog();

            saveButton.Enabled = true;
            unsavedChanges = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (actorListBox.SelectedIndex == -1)
                return;

            int originalIndex = actorListBox.SelectedIndex;
            Cursor = Cursors.WaitCursor;
            saveButton.Enabled = false;

            selectedActor.environment = environmentBox.Text;

            MainForm.actorList.Remove(selectedActor);

            JsonObject response = JsonObject.PostWithJson("/actors/add", selectedActor.ToJson());

            selectedActor = new Actor(response);
            selectedActor.statuses = new List<string>();
            selectedActor.flags = new Dictionary<string, string>();

            JsonObject.Post("/statuses/remove?actorId=" + selectedActor.id);

            foreach(string status in statusListBox.Items)
            {
                string json = "{\"id\":-1"
                            + ",\"actorId\":" + selectedActor.id
                            + ",\"status\":\"" + status + "\""
                            + "}";

                JsonObject.PostWithJson("/statuses/add", json);
                selectedActor.statuses.Add(status);
            }

            JsonObject.Post("/flags/remove?actorId=" + selectedActor.id);

            foreach (string flag in flagBox.Items)
            {
                string[] split = flag.Split(':');
                string json = "{\"id\":-1"
                            + ",\"actorId\":" + selectedActor.id
                            + ",\"key\":\"" + split[0] + "\""
                            + ",\"value\":\"" + split[1] + "\""
                            + "}";

                JsonObject.PostWithJson("/flags/add", json);
                selectedActor.flags.Add(split[0], split[1]);
            }
            MainForm.reference.SyncData();

            actorListLocked = true;
            actorListBox.DataSource = null;
            actorListBox.DataSource = MainForm.actorList;
            actorListLocked = false;

            UpdateListBoxes();
            unsavedChanges = false;
            Cursor = Cursors.Default;

            actorListBox.SelectedIndex = originalIndex;
        }

        private void multi_TextChanged(object sender, EventArgs e)
        {
            if (!saveButtonLocked && !locked)
            {
                unsavedChanges = true;
                saveButton.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            Actor newActor = new Actor(-1, "New actor", 0, "Woods");
            JsonObject response = JsonObject.PostWithJson("/actors/add", newActor.ToJson());
            newActor = new Actor(response);

            actorListLocked = true;

            MainForm.actorList.Add(newActor);
            actorListBox.DataSource = null;
            actorListBox.DataSource = MainForm.actorList;

            actorListLocked = false;

            Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MainForm.performanceList.Count > 0 && MessageBox.Show("Warning: deleting an Actor while the game is in progress can lead to fatal errors.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            Cursor = Cursors.WaitCursor;

            JsonObject.Post("/actors/remove?id=" + selectedActor.id);
            JsonObject.Post("/statuses/remove?actorId=" + selectedActor.id);
            JsonObject.Post("/flags/remove?actorId=" + selectedActor.id);

            actorListLocked = true;

            MainForm.actorList.Remove(selectedActor);

            actorListBox.DataSource = null;
            actorListBox.DataSource = MainForm.actorList;

            if (previousActorBoxIndex >= actorListBox.Items.Count)
                previousActorBoxIndex--;

            actorListLocked = false;

            actorListBox.SelectedIndex = previousActorBoxIndex;

            Cursor = Cursors.Default;
        }

        private void flagBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
                return;
            removeFlag.Enabled = flagBox.SelectedIndex != -1;
        }

        private void addFlag_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            FlagDialog dialogBox = new FlagDialog();
            dialogBox.ShowDialog();

            saveButton.Enabled = true;
            unsavedChanges = true;
        }

        private void removeFlag_Click(object sender, EventArgs e)
        {
            if (flagBox.SelectedIndex != -1)
            {
                int index = flagBox.SelectedIndex;
                flagBox.Items.RemoveAt(flagBox.SelectedIndex);
                if (index >= flagBox.Items.Count)
                    index--;
                flagBox.SelectedIndex = index;
            }
        }
    }
}
