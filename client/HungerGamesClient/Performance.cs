using System;
using System.Collections.Generic;

namespace HungerGamesClient
{
    public struct Performance
    {
        public int id;
        public Scene scene;
        public bool inProgress;
        public int winningVoteId;
        public string flavor;
        public Actor[] participants;

        public Performance(int id, Scene scene, string participants, List<Actor> characterListReference)
        {
            this.id = id;
            this.scene = scene;
            inProgress = true;
            winningVoteId = -1;
            flavor = "";
            string[] participantsSplit = participants.Split(',');
            this.participants = new Actor[participantsSplit.Length];
            for (int i = 0; i < participantsSplit.Length; i++)
            {
                int participantId = int.Parse(participantsSplit[i]);
                this.participants[i] = characterListReference.Find(character => character.id == participantId);
            }
        }

        public Performance(int id, Scene scene, string participants, List<Actor> characterListReference, bool inProgress, int winningVoteId)
            : this(id, scene, participants, characterListReference)
        {
            this.inProgress = inProgress;
            this.winningVoteId = winningVoteId;
            flavor = "";
        }

        public Performance(int id, Scene scene, string participants, List<Actor> characterListReference, bool inProgress, int winningVoteId, string flavor)
            : this(id, scene, participants, characterListReference, inProgress, winningVoteId)
        {
            this.flavor = flavor;
        }

        public Performance(JsonObject json)
        {
            id = json.GetInt("id");
            scene = MainForm.GetScene(json.GetInt("sceneId"));

            String participantsString = json.GetString("participants");

            string[] participantsSplit = participantsString.Split(',');
            this.participants = new Actor[participantsSplit.Length];
            for (int i = 0; i < participantsSplit.Length; i++)
            {
                int participantId = int.Parse(participantsSplit[i]);
                this.participants[i] = MainForm.actorList.Find(actor => actor.id == participantId);
            }

            if (json.GetBool("inProgress"))
            {
                inProgress = true;
                winningVoteId = -1;
            }
            else
            {
                inProgress = false;
                winningVoteId = json.GetInt("winningVote");
            }
            flavor = json.GetString("flavor");
        }

        public string ToMySQLString()
        {
            string participantString = "";
            foreach (Actor c in participants)
            {
                participantString += (c.id + ",");
            }
            participantString = participantString.Substring(0, participantString.Length - 1);
            return String.Format("({0},{1},{2},\"{3}\",\"{4}\")", scene.sceneId, inProgress ? 1 : 0, winningVoteId, flavor, participantString);
        }

        public override string ToString()
        {
            string output = scene.sceneName + " (";
            foreach (Actor actor in participants)
            {
                output += actor.name + ", ";
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

        public Outcome GetChosenOutcome()
        {
            int winningVoteId = this.winningVoteId;
            return MainForm.ballotList.Find(x => x.id == winningVoteId).chosenOutcome;
        }
    }
}
