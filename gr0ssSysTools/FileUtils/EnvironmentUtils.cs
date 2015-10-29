using System;
using System.Collections.Generic;
using System.IO;
using gr0ssSysTools.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gr0ssSysTools.FileUtils
{
    public class EnvironmentUtils
    {
        private const string ENVIRONMENT_FILE_NAME = "environments.json";

        public static void WriteEnvironmentSettings(IEnumerable<Environments> environments)
        {
            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(environmnentJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                foreach (var environment in environments)
                {
                    var jsonEnvironment = JsonConvert.SerializeObject(environment);
                    writer.WriteRaw(jsonEnvironment);
                }
            }
        }

        public static List<Environments> ReadEnvironmentsSettings()
        {
            var environments = new List<Environments>();

            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);

            if (!File.Exists(environmnentJsonFile))
            {
                environments = GetGenericEnvironments();
                WriteEnvironmentSettings(environments);
            }
            else
            {
                using (StreamReader file = File.OpenText(environmnentJsonFile))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var env = new Environments();

                    reader.SupportMultipleContent = true;

                    while (reader.Read())
                    {
                        JObject o3 = (JObject) JToken.ReadFrom(reader);
                        foreach (var child in o3.Children())
                        {
                            AddPropertyToEnvironment(env, child.Path, child.First.ToString());
                        }
                        environments.Add(env);
                        env = new Environments();
                    }
                }
            }
            return environments;
        }

        private static Environments AddPropertyToEnvironment(Environments environment, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case Constants.Environments.ID:
                    environment.ID = Guid.Parse(propertyValue);
                    break;
                case Constants.Environments.NAME:
                    environment.Name = propertyValue;
                    break;
                case Constants.Environments.SUBKEY_VALUE:
                    environment.SubkeyValue = propertyValue;
                    break;
                case Constants.Environments.HOTKEY:
                    environment.HotKey = propertyValue;
                    break;
                case Constants.Environments.ICON_LABEL:
                    environment.IconLabel = propertyValue;
                    break;
                case Constants.Environments.ICON_COLOR:
                    environment.IconColor = propertyValue;
                    break;
            }
            return environment;
        }

        public static List<Environments> GetGenericEnvironments()
        {
            return new List<Environments>
            {
                new Environments {ID = Guid.NewGuid(), Name = "Development", SubkeyValue = "Data\\devDB.xml", HotKey = "D", IconLabel = "dev", IconColor = "Blue"},
                new Environments {ID = Guid.NewGuid(), Name = "sb1", SubkeyValue = "Data\\sb1DB.xml", HotKey = "1", IconLabel = "sb1", IconColor = "Blue"},
                new Environments {ID = Guid.NewGuid(), Name = "sb2", SubkeyValue = "Data\\sb2DB.xml", HotKey = "2", IconLabel = "sb2", IconColor = "Blue"},
                new Environments {ID = Guid.NewGuid(), Name = "sb3", SubkeyValue = "Data\\sb3DB.xml", HotKey = "3", IconLabel = "sb3", IconColor = "Blue"},
                new Environments {ID = Guid.NewGuid(), Name = "Production", SubkeyValue = "Data\\prdDB.xml", HotKey = "P", IconLabel = "prd", IconColor = "Red"}
            };
        }
    }
}
