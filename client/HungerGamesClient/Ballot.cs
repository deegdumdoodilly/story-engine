using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace HungerGamesClient
{
    public class Ballot : IComparable
    {
        public int id;
        public Performance performance;
        public User voter;
        public int timeOfSubmission;
        public Outcome chosenOutcome;
        public bool hasChosenOutcome;
        public bool inProgress;

        public Ballot(int id, Performance performance, int voterId, int timeOfSubmission, Outcome chosenOutcome, bool inProgress)
        {
            this.id = id;
            this.performance = performance;
            this.timeOfSubmission = timeOfSubmission;
            if (voterId != -1)
            {
                this.voter = MainForm.userList.Find(x => x.id == voterId);
            }
            else
            {
                this.voter = null;
            }
            this.chosenOutcome = chosenOutcome;
            this.inProgress = inProgress;
            hasChosenOutcome = (chosenOutcome.id != -1);
        }

        public Ballot(JsonObject json)
        {
            id = json.GetInt("id");
            performance = MainForm.performanceList.Find(x => x.id == json.GetInt("performanceId"));
            timeOfSubmission = json.GetInt("timeOfSubmission");
            if(performance is null)
            {
                MessageBox.Show("Warning, could not find performance with id " + json.GetInt("performanceId"));
            }
            voter = MainForm.userList.Find(x => x.id == json.GetInt("voterId"));
            inProgress = json.GetBool("inProgress");
            if (json.GetBool("hasChosenOutcome"))
            {
                hasChosenOutcome = true;
                int chosenOutcomeId = json.GetInt("chosenOutcomeId");
                chosenOutcome = new Outcome(-1, -1, 0, "", "MISSING OUTCOME");
                foreach (Outcome outcome in performance.scene.outcomes)
                {
                    if (outcome.id == chosenOutcomeId)
                    {
                        chosenOutcome = outcome;
                        break;
                    }
                }
            }
            else
            {
                hasChosenOutcome = false;
                chosenOutcome = new Outcome(-1, -1, 0, "", "MISSING OUTCOME");
            }
        }

        public string ToJSON()
        {
            int voterId = -1;
            if (voter != null)
                voterId = voter.id;

            int chosenOutcomeId = -1;
            if (chosenOutcome != null)
                chosenOutcomeId = chosenOutcome.id;

            return "{"
                + "\"id\":\"" + id + "\""
                + ",\"performanceId\":\"" + performance.id + "\""
                + ",\"voterId\":\"" + voterId + "\""
                + ",\"timeOfSubmission\":\"" + timeOfSubmission + "\""
                + ",\"chosenOutcomeId\":\"" + chosenOutcomeId + "\""
                + ",\"inProgress\":\"" + inProgress + "\""
                + ",\"hasChosenOutcome\":\"" + hasChosenOutcome + "\""
                + "}";
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

        public int CompareTo(object other)
        {
            Ballot otherBallot = (Ballot)other;
            if (timeOfSubmission > otherBallot.timeOfSubmission)
                return -1;
            if (timeOfSubmission < otherBallot.timeOfSubmission)
                return 1;
            return 0;
        }
    }
}
