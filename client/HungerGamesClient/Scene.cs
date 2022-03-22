using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGamesClient
{
    public enum OutcomeType { Positive, Negative, Neutral };
    public struct Scene
    {
        public int sceneId;
        public string sceneName;
        public List<Requirement> requirements;
        public List<Outcome> outcomes;
        public string description;

        public Scene(int sceneId, string sceneName, List<Requirement> requirements, List<Outcome> outcomes, string description)
        {
            this.sceneId = sceneId;
            this.sceneName = sceneName;
            this.requirements = requirements;
            this.outcomes = outcomes;
            this.description = description;
        }

        public Scene(JsonObject json)
        {
            sceneId = json.GetInt("id");
            sceneName = json.GetString("sceneName");
            description = json.GetString("description");

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
    }

    public struct Requirement
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

        public Requirement(JsonObject json)
        {
            id = json.GetInt("id");
            sceneId = json.GetInt("sceneId");
            requirement = json.GetString("requirement");
            role = json.GetInt("role");
        }
    }

    public struct Outcome
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

        public string GetDescription(Performance parentScene)
        {
            string result = description;
            for (int role = 1; role <= parentScene.participants.Length; role++)
            {
                string name = parentScene.participants[role - 1].name;
                result = result.Replace("{" + role + "}", name);
            }
            result = result.Replace("}", "");
            result = result.Replace("{", "");

            return result;
        }
    }
}
