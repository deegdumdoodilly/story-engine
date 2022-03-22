using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerGamesClient
{
    public struct Actor
    {
        public int id;
        public string name;
        public int lastAte;
        public string environment;

        public Actor(int id, string name, int lastAte, string environment)
        {
            this.id = id;
            this.name = name;
            this.lastAte = lastAte;
            this.environment = environment;
        }

        public Actor(JsonObject json)
        {
            id = json.GetInt("id");
            name = json.GetString("name");
            lastAte = json.GetInt("lastAte");
            environment = json.GetString("environment");
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
    }
}
