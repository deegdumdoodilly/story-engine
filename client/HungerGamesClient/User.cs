using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGamesClient
{
    public class User
    {
        public int id;
        public string username;
        public string passhash;

        public int votingChances;
        public int positiveVotes;
        public int negativeVotes;
        public int neutralVotes;

        public bool validVoter;
        public User(int id, string username, int votingChances, int positiveVotes, int negativeVotes, int neutralVotes, bool validVoter)
        {
            this.id = id;
            this.username = username;
            this.passhash = "";

            this.votingChances = votingChances;
            this.positiveVotes = positiveVotes;
            this.negativeVotes = negativeVotes;
            this.neutralVotes = neutralVotes;

            this.validVoter = validVoter;
        }

        public User(JsonObject obj)
        {
            this.id = obj.GetInt("id");
            this.username = obj.GetString("username");
            this.passhash = obj.GetString("passhash");

            this.votingChances = obj.GetInt("votingChances");
            this.positiveVotes = obj.GetInt("positiveVotes");
            this.negativeVotes = obj.GetInt("negativeVotes");
            this.neutralVotes = obj.GetInt("neutralVotes");

            this.validVoter = obj.GetBool("validVoter");
        }

        public bool HasPassword()
        {
            return passhash.Length != 0;
        }

        public bool CanCastNegativeVote()
        {
            return ((positiveVotes * 3 + neutralVotes) - negativeVotes * 3) >= 3;
        }

        public override string ToString()
        {
            return username;
        }

        public string ToJson()
        {
            return "{\"id\":" + id
                + ",\"username\":\"" + username
                + ",\"passhash\":\"" + passhash
                + ",\"votingChances\":" + votingChances
                + ",\"positiveVotes\":" + positiveVotes
                + ",\"neutralVotes\":" + neutralVotes
                + ",\"negativeVotes\":" + negativeVotes
                + ",\"validVoter\":" + validVoter.ToString().ToLower() + "}";
        }
    }
}
