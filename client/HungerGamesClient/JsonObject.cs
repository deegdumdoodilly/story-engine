using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HungerGamesClient
{
    public class JsonObject
    {
        public Dictionary<string, string> pairings;

        public JsonObject(string body)
        {
            pairings = new Dictionary<string, string>();

            char[] characters = body.ToCharArray();

            string[] fieldAndValue = new string[] { "", "" };
            int fieldAndValueIndex = 0;

            bool inQuote = false;

            for (int i = 0; i < characters.Length; i++)
            {
                char c = characters[i];
                if (inQuote)
                {
                    if (c == '\"')
                    {
                        inQuote = false;
                    }
                    else
                    {
                        fieldAndValue[fieldAndValueIndex] += c;
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '\"':
                            inQuote = true;
                            break;
                        case ':':
                            fieldAndValueIndex = 1;
                            break;
                        case ',':
                            pairings.Add(fieldAndValue[0], fieldAndValue[1]);
                            fieldAndValue[0] = "";
                            fieldAndValue[1] = "";
                            fieldAndValueIndex = 0;
                            break;
                        default:
                            fieldAndValue[fieldAndValueIndex] += c;
                            break;
                    }
                }
            }
            pairings.Add(fieldAndValue[0], fieldAndValue[1]);
        }

        public static JsonObject GetJsonFromRequest(string endpoint)
        {
            string url = Properties.Settings.Default.api_url  + endpoint;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
            char[] trimChars = new char[] { '[', ']', '{', '}' };
            string responseString = responseReader.ReadLine().Trim(trimChars);
            return new JsonObject(responseString);
        }

        public static string GetStringFromRequest(string endpoint)
        {
            string url = Properties.Settings.Default.api_url + endpoint;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
            char[] trimChars = new char[] { '[', ']', '{', '}' };
            string responseString = responseReader.ReadLine().Trim(trimChars);
            return responseString;
        }

        public static List<JsonObject> GetJsonsFromRequest(string endpoint)
        {
            string url = Properties.Settings.Default.api_url + endpoint;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
            char[] trimChars = new char[] { '[', ']', '{', '}' };
            string[] responseString = responseReader.ReadLine().Trim(trimChars).Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

            List<JsonObject> result = new List<JsonObject>();
            foreach (string s in responseString)
            {
                result.Add(new JsonObject(s));
            }
            return result;
        }

        public static JsonObject PostWithJson(string endpoint, string json)
        {
            string url = Properties.Settings.Default.api_url + "/" + endpoint;
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            Stream stream = request.GetResponse().GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                return new JsonObject(reader.ReadToEnd());
            }
        }

        public string GetString(string key)
        {
            if (!pairings.ContainsKey(key) || pairings[key] == "null")
            {
                return "";
            }
            return pairings[key];
        }

        public int GetInt(string key)
        {
            if (!pairings.ContainsKey(key) || pairings[key] == "null")
            {
                return -1;
            }
            return int.Parse(pairings[key]);
        }

        public bool GetBool(string key)
        {
            if (!pairings.ContainsKey(key) || pairings[key] == "null")
            {
                return false;
            }
            return pairings[key].ToLower() == "true";
        }
    }
}
