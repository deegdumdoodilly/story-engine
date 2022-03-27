using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace HungerGamesClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Logging

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool establishedConnection = false;

            if (Properties.Settings.Default.api_url.Length == 0)
            {
                SetURL setURL = new SetURL();
                setURL.ShowDialog();
            }

            while (!establishedConnection)
            {
                try
                {
                    JsonObject.GetJsonFromRequest("/health");
                    establishedConnection = true;
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error, unable to connect at \"" + Properties.Settings.Default.api_url + "\". " + e.Message);
                    SetURL setURL = new SetURL();
                    setURL.ShowDialog();
                }
            }

            string passhash = "";
            User user;

            string storedUsername = Properties.Settings.Default.username;
            string storedPassword = Properties.Settings.Default.password;
            if (storedUsername.Length > 0)
            {
                try
                {
                    JsonObject userJson = JsonObject.GetJsonFromRequest("/users?username=" + storedUsername);
                    user = new User(userJson);
                }
                catch
                {
                    user = new User();
                }
                passhash = user.passhash;
                if (user.username is null)
                {
                    // Found no matching user for saved login
                    MessageBox.Show("Failed automatic login, you will now be asked to log in normally (username not accepted).");
                    Application.Run(new UserLogin());
                }
                else
                {
                    // Found user, compare passwords

                    if (!user.HasPassword())
                    {
                        // No password required, login successful
                        MainForm.currentUser = user;
                        UserLogin.FetchUsers();
                        Application.Run(new MainForm());
                    }
                    else if(storedPassword.Length == 0)
                    {
                        // Password required, no local password given
                        MessageBox.Show("Failed automatic login, you will now be asked to log in normally (missing password).");
                        Application.Run(new UserLogin());
                    }
                    else
                    {
                        SHA256 hashAlgo = SHA256.Create();
                        byte[] output = hashAlgo.ComputeHash(Encoding.ASCII.GetBytes(storedPassword));
                        string localPasshash = BitConverter.ToString(output);
                        if (passhash == localPasshash)
                        {
                            //login successful
                            MainForm.currentUser = user;
                            Application.Run(new MainForm());
                        }
                        else
                        {
                            //local/DB password mismatch
                            MessageBox.Show("Failed automatic login, you will now be asked to log in normally (password not accepted).");
                            Application.Run(new UserLogin());
                        }
                    }
                }
            }
            else
            {
                Application.Run(new UserLogin());
            }
        }
    }
}
