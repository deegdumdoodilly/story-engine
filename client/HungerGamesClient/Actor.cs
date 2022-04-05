using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGamesClient
{
    public class Actor : IComparable
    {
        public int id;
        public string name;
        public int lastAte;
        public string environment;

        public List<string> statuses;
        public Dictionary<string, string> flags;

        public Actor(int id, string name, int lastAte, string environment)
        {
            this.id = id;
            this.name = name;
            this.lastAte = lastAte;
            this.environment = environment;
            PullStatuses();
            PullFlags();
        }

        public Actor(JsonObject json)
        {
            id = json.GetInt("id");
            name = json.GetString("name");
            lastAte = json.GetInt("lastAte");
            environment = json.GetString("environment");
            PullStatuses();
            PullFlags();
        }

        private void PullStatuses()
        {
            statuses = new List<string>();
            List<JsonObject> statusJsons = JsonObject.GetJsonsFromRequest("/statuses?actorId=" + id);
            foreach(JsonObject json in statusJsons)
            {
                statuses.Add(json.GetString("status"));
            }
        }

        private void PullFlags()
        {
            flags = new Dictionary<string, string>();
            List<JsonObject> flagJsons = JsonObject.GetJsonsFromRequest("/flags?actorId=" + id);
            foreach(JsonObject json in flagJsons)
            {
                flags.Add(json.GetString("key"), json.GetString("value"));
            }
        }

        public string ToJson()
        {
            return "{"
                + "\"id\":\"" + id + "\""
                + ",\"name\":\"" + name + "\""
                + ",\"lastAte\":\"" + lastAte + "\""
                + ",\"environment\":\"" + environment + "\""
                + "}";
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            Actor other = (Actor)obj;
            return (this.id == other.id &&
                    this.name == other.name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(Object other)
        {
            Actor otherActor = (Actor)other;
            return name.CompareTo(otherActor.name);
        }
    }
}
