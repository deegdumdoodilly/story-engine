using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.db_name.Length == 0)
            {
                SetDBName dbName = new SetDBName();
                dbName.ShowDialog();
            }

            string passhash = "";
            User user = new User();

            string username = Properties.Settings.Default.username;
            string password = Properties.Settings.Default.password;
            if (username.Length > 0)
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {

                        cnn.ConnectionString = MainForm.GetConnectionString();

                        UserLogin.FetchUsers(cnn);
                        cnn.Close();
                        cnn.Open();
                        user = MainForm.userList.Find(x => x.username == username);

                        string query = "SELECT passhash FROM hungergames.users WHERE id = " + user.id + ";";
                        MySqlCommand command = new MySqlCommand(query, cnn);
                        MySqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if (!reader.IsDBNull(0))
                            passhash = reader.GetString(0);
                        cnn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, could not fetch users.\n" + ex.GetType() + "\n" + ex.Message + "\n" + ex.StackTrace);
                }
                if (user.username is null)
                {
                    // Found no matching user for saved login
                    MessageBox.Show("Failed automatic login, you will now be asked to log in normally (username not accepted).");
                    Application.Run(new UserLogin());
                }
                else
                {
                    // Found user, compare passwords

                    if (!user.hasPassword)
                    {
                        // No password required, login successful
                        MainForm.currentUser = user;
                        Application.Run(new MainForm());
                    }
                    else if(password.Length == 0)
                    {
                        // Password required, no local password given
                        MessageBox.Show("Failed automatic login, you will now be asked to log in normally (missing password).");
                        Application.Run(new UserLogin());
                    }
                    else
                    {
                        SHA256 hashAlgo = SHA256.Create();
                        byte[] output = hashAlgo.ComputeHash(Encoding.ASCII.GetBytes(password));
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
