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

        public static void WriteEnvironmentSettings(IEnumerable<LoadedEnvironments> environments)
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

        public static List<LoadedEnvironments> ReadEnvironmentsSettings()
        {
            var environments = new List<LoadedEnvironments>();

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
                    var env = new LoadedEnvironments();

                    reader.SupportMultipleContent = true;

                    while (reader.Read())
                    {
                        JObject o3 = (JObject) JToken.ReadFrom(reader);
                        foreach (var child in o3.Children())
                        {
                            AddPropertyToEnvironment(env, child.Path, child.First.ToString());
                        }
                        environments.Add(env);
                        env = new LoadedEnvironments();
                    }
                }
            }
            return environments;
        }

        private static LoadedEnvironments AddPropertyToEnvironment(LoadedEnvironments loadedEnvironment, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case Constants.Environments.ID:
                    loadedEnvironment.ID = Guid.Parse(propertyValue);
                    break;
                case Constants.Environments.NAME:
                    loadedEnvironment.Name = propertyValue;
                    break;
                case Constants.Environments.SUBKEY_VALUE:
                    loadedEnvironment.SubkeyValue = propertyValue;
                    break;
                case Constants.Environments.HOTKEY:
                    loadedEnvironment.HotKey = propertyValue;
                    break;
                case Constants.Environments.ICON_LABEL:
                    loadedEnvironment.IconLabel = propertyValue;
                    break;
                case Constants.Environments.ICON_COLOR:
                    loadedEnvironment.IconColor = propertyValue;
                    break;
            }
            return loadedEnvironment;
        }

        public static List<LoadedEnvironments> GetGenericEnvironments()
        {
            return new List<LoadedEnvironments>
            {
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "Development", SubkeyValue = "Data\\devDB.xml", HotKey = "D", IconLabel = "dev", IconColor = "Blue"},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb1", SubkeyValue = "Data\\sb1DB.xml", HotKey = "1", IconLabel = "sb1", IconColor = "Blue"},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb2", SubkeyValue = "Data\\sb2DB.xml", HotKey = "2", IconLabel = "sb2", IconColor = "Blue"},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb3", SubkeyValue = "Data\\sb3DB.xml", HotKey = "3", IconLabel = "sb3", IconColor = "Blue"},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "Production", SubkeyValue = "Data\\prdDB.xml", HotKey = "P", IconLabel = "prd", IconColor = "Red"}
            };
        }
    }
}
