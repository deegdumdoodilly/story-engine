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
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace HungerGamesClient
{
    public partial class UserLogin : Form
    {
        private int passwordPanelHeight;
        private int checkBoxHeight;
        public UserLogin()
        {
            InitializeComponent();
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            TopMost = true;
            TopLevel = true;
            passwordPanelHeight = passwordPanel.Height;
            passwordPanel.Height -= passwordPanelHeight;
            this.Height -= passwordPanelHeight;
            checkBoxHeight = saveCheckBox.Height;
            if (saveCheckBox.Visible)
            {
                saveCheckBox.Visible = false;
                this.Height -= checkBoxHeight;
            }
            UpdateOptions();
        }

        public static void FetchUsers()
        {
            // Add users to MainForm.userList
            List<JsonObject> jsonList = JsonObject.GetJsonsFromRequest("/users");
            MainForm.userList = new List<User>();
            foreach (JsonObject j in jsonList)
            {
                User newUser = new User(j);
                MainForm.userList.Add(newUser);
            }
        }

        public void UpdateOptions()
        {
            userDropdown.Items.Clear();
            try
            {
                FetchUsers();

                foreach(User user in MainForm.userList)
                    userDropdown.Items.Add(user);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error reading from users.\nError: " + ex.GetType().Name + "\n" + ex.ToString());
            }
        }

        private void ShowMainForm()
        {
            if (MainForm.reference is null)
            {
                MainForm newMainForm = new MainForm();
                newMainForm.UpdateUserLabel();
                newMainForm.Show();
            }
            else
            {
                MainForm.reference.UpdateUserLabel();
                MainForm.reference.Show();
            }
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if(userDropdown.SelectedIndex == -1){
                MessageBox.Show("Please select a user.");
                return;
            }
            User selectedUser = (User) userDropdown.SelectedItem;
            if (selectedUser.HasPassword())
            {
                // Requires password
                try
                {
                    JsonObject userJson = null;
                    try
                    {
                        // Retrieve record with matching username
                        userJson = JsonObject.GetJsonFromRequest("/users?username=" + selectedUser.username);
                    }catch(WebException ex)
                    {
                        MessageBox.Show("Username not found. Restart the application or DM deeg if problem persists.");
                    }
                    // Found user, compare passwords
                    string passhash = userJson.GetString("passhash");
                    int userId = userJson.GetInt("id");
                    string username = userJson.GetString("username");


                    SHA256 hashAlgo = SHA256.Create();
                    byte[] output = hashAlgo.ComputeHash(Encoding.ASCII.GetBytes(passwordField.Text));
                    string localPasshash = BitConverter.ToString(output);
                    if (passhash == localPasshash)
                    {
                        //login successful
                        MainForm.currentUser = selectedUser;
                        SaveOrDeleteLoginInfo(selectedUser);
                        if (! (MainForm.reference is null))
                            MainForm.reference.UpdateUserLabel();
                        ShowMainForm();
                    }
                    else
                    {
                        //local/DB password mismatch
                        MessageBox.Show("Error, incorrect password. DM deeg to get it reset at the price of one RP per reset.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error encountered .\n\"" + ex.Message + "\"\n" + ex.GetType().Name + "\n" + ex.StackTrace);
                    ShowMainForm();
                }
            }
            else
            {
                //login successful
                MainForm.currentUser = selectedUser;

                SaveOrDeleteLoginInfo(selectedUser);

                ShowMainForm();
            }
        }

        private void SaveOrDeleteLoginInfo(User selectedUser)
        {
            if (saveCheckBox.Checked)
            {
                Properties.Settings.Default.username = ((User)userDropdown.SelectedItem).username;
                Properties.Settings.Default.password = passwordField.Text;
            }
            else if (Properties.Settings.Default.username == selectedUser.username)
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
            }
            Properties.Settings.Default.Save();
        }

        private void userDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string username = Properties.Settings.Default.username;
            string password = Properties.Settings.Default.password;
            saveCheckBox.Checked = false;

            if (username.Length >= 1 && username == ((User)userDropdown.SelectedItem).username)
            {
                saveCheckBox.Checked = true;
            }

            if (((User)userDropdown.SelectedItem).HasPassword())
            {
                if(passwordPanel.Height == 0)
                {
                    passwordField.Text = password;
                    passwordPanel.Height = passwordPanelHeight;
                    this.Height += passwordPanelHeight;
                }
            }
            else if(passwordPanel.Height != 0)
            {
                passwordPanel.Height = 0;
                this.Height -= passwordPanelHeight;
            }

            if(userDropdown.SelectedIndex >= 0)
            {
                if (!saveCheckBox.Visible)
                {
                    this.Height += checkBoxHeight;
                    saveCheckBox.Visible = true;
                }
                loginButton.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(MainForm.reference == null)
            {
                base.OnFormClosing(e);
                Application.Exit();
            }
            else
            {
                this.Hide();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            userDropdown.SelectedIndex = -1;
            saveCheckBox.Checked = false;
            if (saveCheckBox.Visible)
            {
                saveCheckBox.Visible = false;
                this.Height -= checkBoxHeight;
            }
            if (passwordPanel.Height != 0)
            {
                passwordPanel.Height = 0;
                this.Height -= passwordPanelHeight;
            }
            base.OnShown(e);
        }
    }
}
