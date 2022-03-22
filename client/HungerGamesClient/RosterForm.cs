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
    public partial class RosterForm : Form
    {
        public RosterForm()
        {
            InitializeComponent();
        }

        private void RosterForm_Load(object sender, EventArgs e)
        {
            dateLabel.Visible = false;
            panel1.Visible = false;

            int time = int.Parse(JsonObject.GetStringFromRequest("/time"));

            int day = 1 + time / 4;
            string timeOfDay = "";
            switch (time % 4)
            {
                case 0:
                    timeOfDay = "Morning";
                    break;
                case 1:
                    timeOfDay = "Afternoon";
                    break;
                case 2:
                    timeOfDay = "Evening";
                    break;
                default:
                    timeOfDay = "Night";
                    break;
            }
            dateLabel.Text = "" + time;


            List<Actor> actors = MainForm.actorList;

            RowStyle rowStyle = rosterTable.RowStyles[0];
            rosterTable.RowCount = actors.Count / 4 + 1;
            for(int i = 1; i < rosterTable.RowCount; i++)
            {
                rosterTable.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));
            }

            List<JsonObject> statuses = JsonObject.GetJsonsFromRequest("/statuses");

            foreach (Actor a in actors)
            {
                Label nameplate = new Label();
                nameplate.Anchor = (AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right;
                nameplate.AutoSize = true;
                nameplate.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);

                nameplate.Text = a.name;
                nameplate.TextAlign = ContentAlignment.MiddleCenter;

                PictureBox pictureBox = new PictureBox();

                pictureBox.BackgroundImage = Image.FromFile("Images/" + a.name + "_icon.png");
                pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox.Size = new Size(261, 115);

                Label environmentLabel = new Label();
                environmentLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                environmentLabel.AutoSize = true;
                environmentLabel.Font = new Font("Baskerville Old Face", 12F, FontStyle.Regular, GraphicsUnit.Point,0);
                environmentLabel.Text = "Environment: " + a.environment.Replace("\\\\", "");
                environmentLabel.TextAlign = ContentAlignment.MiddleCenter;

                Label statusLabel = new Label();
                statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                statusLabel.AutoSize = true;
                statusLabel.Font = new Font("Baskerville Old Face", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);


                List<JsonObject> relevantStatuses = new List<JsonObject>();
                foreach( JsonObject s in statuses)
                {
                    if(s.GetInt("id") == + a.id)
                    {
                        relevantStatuses.Add(s);
                    }
                }
                if (relevantStatuses.Count == 0)
                {
                    statusLabel.Text = "Status: None";
                }
                else
                {
                    statusLabel.Text = "Status:";
                    foreach (JsonObject s in relevantStatuses)
                    {
                        statusLabel.Text += " " + s.GetString("status") + ",";
                    }
                    statusLabel.Text = statusLabel.Text.TrimEnd(',');
                }

                statusLabel.TextAlign = ContentAlignment.MiddleCenter;

                FlowLayoutPanel layoutPanel = new FlowLayoutPanel();

                layoutPanel.AutoSize = true;
                layoutPanel.Controls.Add(nameplate);
                layoutPanel.Controls.Add(pictureBox);
                layoutPanel.Controls.Add(environmentLabel);
                layoutPanel.Controls.Add(statusLabel);
                layoutPanel.FlowDirection = FlowDirection.TopDown;
                layoutPanel.Location = new Point(150, 4);


                rosterTable.Controls.Add(layoutPanel);
            }
            dateLabel.Visible = true;
            panel1.Visible = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
