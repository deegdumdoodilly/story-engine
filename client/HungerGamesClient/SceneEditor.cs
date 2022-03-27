using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HungerGamesClient
{
    public partial class SceneEditor : Form
    {
        public static ListBox outcomeBox;
        public static ListBox requirementBox;

        public static Requirement selectedRequirement;
        public static Outcome selectedOutcome;
        public static Scene selectedScene;
        private RequirementEditor reqEditor;
        private OutcomeEditor outEditor;
        private RunScene runScene;

        List<Scene> scenes;

        private bool lockSceneList = false;

        private int sceneListLastIndex = -1;

        public static bool unsavedChanges = false;
        public static Button saveButtonRef;
        private bool updatingTextFields;

        public SceneEditor()
        {
            InitializeComponent();
        }

        private void SceneEditor_Load(object sender, EventArgs e)
        {
            outcomeBox = this.outcomesBox;
            requirementBox = this.requirementsBox;
            saveButtonRef = this.saveButton;

            saveButton.Enabled = false;

            requirementsBox.Items.Clear();
            outcomesBox.Items.Clear();
            parentSceneDropdown.Items.Clear();
            parentSceneDropdown.Items.Add("None");

            List<JsonObject> sceneJsons = JsonObject.GetJsonsFromRequest("/scenes");

            MainForm.FetchScenes();
            scenes = MainForm.sceneList;

            foreach (Scene scene in scenes)
            {
                sceneListBox.Items.Add(scene);
            }

            MainForm.reference.Cursor = Cursors.Default;
            MainForm.reference.editScenesButtonRef.Enabled = true;

            UpdateTextFields();
        }

        private void UpdateTextFields(int index = -2)
        {
            updatingTextFields = true;
            if (index == -2)
                index = sceneListBox.SelectedIndex;

            editReq.Enabled = false;
            removeReq.Enabled = false;
            editOut.Enabled = false;
            removeOut.Enabled = false;

            requirementsBox.Items.Clear();
            outcomesBox.Items.Clear();
            parentSceneDropdown.Items.Clear();

            if (index == -1)
            {
                nameBox.Text = "";
                descriptionBox.Text = "";
                briefBox.Text = "";
                priorityBox.Value = 0;
                participantsBox.Value = 1;

                parentSceneDropdown.Enabled = false;
                nameBox.Enabled = false;
                descriptionBox.Enabled = false;
                briefBox.Enabled = false;
                priorityBox.Enabled = false;
                participantsBox.Enabled = false;
                requirementBox.Enabled = false;
                outcomeBox.Enabled = false;

                addReq.Enabled = false;
                addOut.Enabled = false;
            }
            else
            {
                parentSceneDropdown.Enabled = true;
                nameBox.Enabled = true;
                descriptionBox.Enabled = true;
                briefBox.Enabled = true;
                priorityBox.Enabled = true;
                participantsBox.Enabled = true;
                requirementBox.Enabled = true;
                outcomeBox.Enabled = true;

                addReq.Enabled = true;
                addOut.Enabled = true;

                selectedScene = (Scene)sceneListBox.Items[index];

                Scene s = (Scene)sceneListBox.SelectedItem;
                nameBox.Text = s.sceneName;
                descriptionBox.Text = s.description;
                briefBox.Text = s.briefDescription;
                participantsBox.Value = s.numParticipants;
                priorityBox.Value = s.priority;

                parentSceneDropdown.Items.Add("None");
                foreach (Scene scene in scenes)
                {
                    if (scene != s)
                        parentSceneDropdown.Items.Add(scene.sceneName);
                }
                foreach (Requirement r in s.requirements)
                    requirementsBox.Items.Add(r);
                foreach (Outcome o in s.outcomes)
                    outcomesBox.Items.Add(o);

                if (s.parentSceneId >= 0)
                {
                    Scene parentScene = MainForm.GetScene(s.parentSceneId);
                    parentSceneDropdown.SelectedIndex = parentSceneDropdown.Items.IndexOf(parentScene.sceneName);
                }
                else
                {
                    parentSceneDropdown.SelectedIndex = 0;
                }
            }
            updatingTextFields = false;
        }

        private bool ConfirmDiscardChanges()
        {
            return MessageBox.Show("You have unsaved changes. Discard them?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockSceneList || sceneListBox.SelectedIndex == sceneListLastIndex)
                return;
            if (unsavedChanges)
            {
                int targetIndex = sceneListBox.SelectedIndex;
                lockSceneList = true;
                sceneListBox.SelectedIndex = sceneListLastIndex;
                if (!ConfirmDiscardChanges())
                {
                    lockSceneList = false;
                    return;
                }
                sceneListBox.SelectedIndex = targetIndex;
                lockSceneList = false;
            }

            sceneListLastIndex = sceneListBox.SelectedIndex;

            unsavedChanges = false;
            saveButton.Enabled = false;
            UpdateTextFields();
        }

        private void requirementsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (requirementBox.SelectedIndex >= 0)
            {
                editReq.Enabled = true;
                removeReq.Enabled = true;
                selectedRequirement = (Requirement)requirementsBox.Items[requirementsBox.SelectedIndex];
            }
        }

        private void outcomesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outcomesBox.SelectedIndex > -1)
            {
                editOut.Enabled = true;
                removeOut.Enabled = true;
                selectedOutcome = (Outcome)outcomesBox.Items[outcomesBox.SelectedIndex];
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            unsavedChanges = false;
            this.Cursor = Cursors.WaitCursor;

            string sceneJSON = AssembleSceneJSON();
            string issue = "";
            try
            {
                issue = "the scene";
                JsonObject.PostWithJson("/scenes/validate", sceneJSON);

                foreach (Requirement r in selectedScene.requirements)
                {
                    issue = "requirement " + (selectedScene.requirements.IndexOf(r) + 1);
                    JsonObject.PostWithJson("/requirements/validate", r.toJSON());
                }

                foreach (Outcome o in selectedScene.outcomes)
                {
                    issue = "outcome " + (selectedScene.outcomes.IndexOf(o) + 1);
                    JsonObject.PostWithJson("/outcomes/validate", o.toJSON());
                }
            }
            catch
            {
                MessageBox.Show("Error, the formatting of " + issue + " was not accepted by the server. Make sure you have not used any invalid characters (including line breaks) in your text fields.\n Your changes have been kept locally for you to reformat and try again.", "Invalid format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sceneListBox.Text = selectedScene.sceneName;

                sceneListBox.DrawMode = DrawMode.OwnerDrawFixed;
                sceneListBox.DrawMode = DrawMode.Normal;
                UpdateTextFields();

                this.Cursor = Cursors.Default;
                sceneListBox.SelectedIndex = sceneListLastIndex;
                return;
            }


            JsonObject.Post("/requirements/remove?sceneId=" + selectedScene.sceneId);
            JsonObject.Post("/outcomes/remove?sceneId=" + selectedScene.sceneId);

            JsonObject response = JsonObject.PostWithJson("/scenes/add", sceneJSON);
            Scene newScene = new Scene(response);

            foreach(Requirement r in selectedScene.requirements)
            {
                r.sceneId = newScene.sceneId;
                JsonObject.PostWithJson("/requirements/add", r.toJSON());
            }

            foreach (Outcome o in selectedScene.outcomes)
            {
                o.sceneId = newScene.sceneId;
                JsonObject.PostWithJson("/outcomes/add", o.toJSON());
            }

            scenes[sceneListBox.SelectedIndex] = newScene;
            sceneListBox.Text = newScene.sceneName;


            sceneListBox.DrawMode = DrawMode.OwnerDrawFixed;
            sceneListBox.DrawMode = DrawMode.Normal;
            UpdateTextFields();

            this.Cursor = Cursors.Default;
            sceneListBox.SelectedIndex = sceneListLastIndex;
        }

        private string AssembleSceneJSON()
        {
            selectedScene.sceneName = nameBox.Text;
            selectedScene.description = descriptionBox.Text.Replace('\n', ' ');
            selectedScene.briefDescription = briefBox.Text;
            selectedScene.numParticipants = (int) participantsBox.Value;
            if (parentSceneDropdown.Text != "None") {
                int parentSceneId = scenes.Find(scene => scene.sceneName == parentSceneDropdown.Text).sceneId;
                selectedScene.parentSceneId = parentSceneId;
            }
            else
            {
                selectedScene.parentSceneId = -1;
            }
            selectedScene.priority = (int) priorityBox.Value;
            return selectedScene.toJSON();
        }

        private void addReq_Click(object sender, EventArgs e)
        {
            Requirement r = new Requirement(selectedScene.sceneId, "character 1 status is \"hungry\"");
            selectedScene.requirements.Add(r);
            requirementsBox.Items.Add(r);
            requirementBox.SelectedIndex = requirementBox.Items.IndexOf(r);
            selectedRequirement = r;

            if (reqEditor != null && !reqEditor.IsDisposed)
            {
                reqEditor.Close();
            }
            reqEditor = new RequirementEditor();
            reqEditor.Show();
        }
        private void addOut_Click(object sender, EventArgs e)
        {
            Outcome o = new Outcome(-1, selectedScene.sceneId, 0, "", "");
            selectedScene.outcomes.Add(o);
            outcomesBox.Items.Add(o);
            outcomeBox.SelectedIndex = outcomeBox.Items.IndexOf(o);
            selectedOutcome = o;

            if (outEditor != null && !outEditor.IsDisposed)
            {
                outEditor.Close();
            }
            
            outEditor = new OutcomeEditor();
            outEditor.Show();
        }

        private void removeReq_Click(object sender, EventArgs e)
        {
            selectedScene.requirements.Remove(selectedRequirement);
            requirementsBox.Items.Remove(selectedRequirement);

            editReq.Enabled = false;
            removeReq.Enabled = false;

            saveButton.Enabled = true;
            unsavedChanges = true;
        }

        private void removeOut_Click(object sender, EventArgs e)
        {
            selectedScene.outcomes.Remove(selectedOutcome);
            outcomesBox.Items.Remove(selectedOutcome);

            editOut.Enabled = false;
            removeOut.Enabled = false;

            saveButton.Enabled = true;
            unsavedChanges = true;
        }

        private void editReq_Click(object sender, EventArgs e)
        {
            if (requirementBox.SelectedIndex == -1)
            {
                MessageBox.Show("No requirement selected.");
                return;
            }
            if (reqEditor == null || reqEditor.IsDisposed)
            {
                reqEditor = new RequirementEditor();
            }
            else
            {
                reqEditor.Close();
            }
            reqEditor.Show();
        }

        private void editOut_Click(object sender, EventArgs e)
        {
            if (outcomesBox.SelectedIndex == -1)
            {
                MessageBox.Show("No outcome selected.");
                return;
            }
            if (outEditor == null || outEditor.IsDisposed)
            {
                outEditor = new OutcomeEditor();
            }
            else
            {
                outEditor.Close();
            }
            outEditor.Show();
        }

        private void addScene_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            addScene.Enabled = false;
            Scene newScene = new Scene(-1, -1, 1, 1, "New Scene", new List<Requirement>(), new List<Outcome>(), "", "");

            JsonObject newSceneJSON = JsonObject.PostWithJson("/scenes/add", newScene.toJSON());

            newScene = new Scene(newSceneJSON);

            scenes.Add(newScene);

            lockSceneList = true;

            sceneListBox.Items.Add(newScene);

            UpdateTextFields();

            sceneListBox.SelectedItem = newScene;
            selectedScene = newScene;

            lockSceneList = false;

            Cursor = Cursors.Default;
            addScene.Enabled = true;
        }

        private void removeScene_Click(object sender, EventArgs e)
        {
            if(sceneListBox.SelectedIndex != -1)
            {
                if(MessageBox.Show("Are you sure you want to delete the scene \"" + selectedScene + "\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                    return;
                }
                Cursor = Cursors.WaitCursor;
                removeScene.Enabled = false;

                scenes.Remove(selectedScene);
                JsonObject.Post("/scenes/remove?id=" + selectedScene.sceneId);

                int index = sceneListBox.SelectedIndex;

                lockSceneList = true;

                sceneListBox.Items.RemoveAt(index);

                sceneListBox.SelectedIndex = -1;

                lockSceneList = false;

                UpdateTextFields();

                Cursor = Cursors.Default;
                removeScene.Enabled = true;
            }
        }

        private void MarkUnsavedChanges(object sender, EventArgs e)
        {
            if (!updatingTextFields)
            {
                unsavedChanges = true;
                saveButton.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (runScene == null || runScene.IsDisposed)
            {
                runScene = new RunScene();
            }
            else
            {
                runScene.Close();
            }
            runScene.Show();
        }
    }
}
