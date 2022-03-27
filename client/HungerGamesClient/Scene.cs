using System;
using System.Collections.Generic;
using System.Linq;

namespace HungerGamesClient
{
    public enum OutcomeType { Positive, Negative, Neutral };
    public class Scene
    {
        public int sceneId;
        public int parentSceneId;
        public int priority;
        public int numParticipants;
        public string sceneName;
        public List<Requirement> requirements;
        public List<Outcome> outcomes;
        public string description;
        public string briefDescription;

        public Scene(int sceneId, int parentSceneId, int priority, int numParticipants, string sceneName, List<Requirement> requirements, List<Outcome> outcomes, string description, string briefDescription)
        {
            this.sceneId = sceneId;
            this.parentSceneId = parentSceneId;
            this.priority = priority;
            this.numParticipants = numParticipants;
            this.sceneName = sceneName;
            this.requirements = requirements;
            this.outcomes = outcomes;
            this.description = description;
            this.briefDescription = briefDescription;
        }

        public Scene(JsonObject json)
        {
            sceneId = json.GetInt("id");
            parentSceneId = json.GetInt("parentSceneId");
            priority = json.GetInt("priority");
            numParticipants = json.GetInt("numParticipants");
            sceneName = json.GetString("sceneName");
            description = json.GetString("description");
            briefDescription = json.GetString("briefDescription");

            List<JsonObject> requirementsJson = JsonObject.GetJsonsFromRequest("/requirements?sceneId=" + sceneId);
            requirements = new List<Requirement>();
            foreach (JsonObject j in requirementsJson)
            {
                Requirement r = new Requirement(j);
                requirements.Add(r);
            }

            List<JsonObject> outcomesJson = JsonObject.GetJsonsFromRequest("/outcomes?sceneId=" + sceneId);
            outcomes = new List<Outcome>();
            foreach (JsonObject j in outcomesJson)
            {
                Outcome o = new Outcome(j);
                outcomes.Add(o);
            }
        }

        public String toJSON()
        {
            return "{\n"
                + "\"id\": \"" + sceneId + "\",\n"
                + "\"parentSceneId\": \"" + parentSceneId + "\",\n"
                + "\"sceneName\": \"" + sceneName + "\",\n"
                + "\"numRequirements\": \"" + requirements.Count() + "\",\n"
                + "\"numOutcomes\": \"" + outcomes.Count() + "\",\n"
                + "\"occurrences\": \"" + 0 + "\",\n"
                + "\"numParticipants\": \"" + numParticipants + "\",\n"
                + "\"description\": \"" + description + "\",\n"
                + "\"briefDescription\": \"" + briefDescription + "\",\n"
                + "\"priority\": \"" + priority + "\""
              + "}";
        }

        public string DescribeWithNames(List<String> names)
        {
            string result = description;
            for (int role = 1; role <= names.Count(); role++)
            {
                result = result.Replace("{" + role + "}", names[role - 1]);
            }

            return result;
        }

        public override string ToString()
        {
            /*string append = "";
            if (numParticipants > 1)
            {
                append += numParticipants + "-player";
            }
            if(priority != 1)
            {
                if (append != "")
                    append += ", ";
                append += "priority " + priority;
            }
            if(parentSceneId != -1) 
            {
                Scene parentScene = MainForm.sceneList.Find(scene => scene.sceneId == parentSceneId);
                if (append != "")
                    append += ", ";
                append += "child of " + parentScene.sceneName;
            }

            if (append != "")
                append = " (" + append + ")";
            return sceneName + append;*/
            return sceneName;
        }
    }

    public class Requirement
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

        public Requirement(int sceneId, string requirement)
        {
            this.id = -1;
            this.sceneId = sceneId;
            this.requirement = requirement;
        }

        public Requirement(JsonObject json)
        {
            id = json.GetInt("id");
            sceneId = json.GetInt("sceneId");
            requirement = json.GetString("requirement");
        }

        public string toJSON()
        {
            if(id < 1)
            {
                return "{"
                + "\"sceneId\": \"" + sceneId + "\","
                + "\"requirement\": \"" + requirement.Replace("\"", "\\\"") + "\""
                + "}";
            }
            return "{"
                + "\"id\": \"" + id + "\","
                + "\"sceneId\": \"" + sceneId + "\","
                + "\"requirement\": \"" + requirement.Replace("\"", "\\\"") + "\""
                + "}";
        }

        public override string ToString()
        {
            if (requirement == "")
                return "Empty requirement";
            return requirement;
        }
    }

    public class Outcome
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

        public Outcome(JsonObject json) : this(json.GetInt("id"), json.GetInt("sceneId"), json.GetInt("type"), json.GetString("effect"), json.GetString("description")) { }

        public string GetDescription(Performance performance)
        {
            string result = description;
            for (int role = 1; role <= performance.participants.Length; role++)
            {
                string name = performance.participants[role - 1].name;
                result = result.Replace("{" + role + "}", name);
            }
            result = result.Replace("}", "");
            result = result.Replace("{", "");

            return result;
        }

        public int GetOutcomeInt()
        {
            switch (outcomeType)
            {
                case OutcomeType.Negative:
                    return -1;
                case OutcomeType.Positive:
                    return 1;
                default:
                    return 0;
            }
        }

        public override string ToString()
        {
            /*if (description == "")
            {
                return "Empty outcome";
            }
            else
            {
                int sum = 0;
                foreach(char c in effect.ToCharArray())
                {
                    if(c == ' ')
                        sum++;
                }
                if(sum < 3)
                {
                    return "Incomplete outcome definition";
                }
            }
            return "character " + description + " (" + outcomeType.ToString() + ")";*/
            return description;
        }

        public string toJSON()
        {
            if(id < 1)
            {
                return "{"
                + "\"sceneId\": \"" + sceneId + "\","
                + "\"type\": \"" + GetOutcomeInt() + "\","
                + "\"effect\": \"" + effect.Replace("\"", "\\\"") + "\","
                + "\"description\": \"" + description + "\""
                + "}";
            }
            return "{"
                + "\"id\": \"" + id + "\","
                + "\"sceneId\": \"" + sceneId + "\","
                + "\"type\": \"" + GetOutcomeInt() + "\","
                + "\"effect\": \"" + effect.Replace("\"", "\\\"") + "\","
                + "\"description\": \"" + description + "\""
                + "}";
        }
    }
}
