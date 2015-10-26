using System.IO;
using gr0ssSysTools.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gr0ssSysTools.FileUtils
{
    public class RegistryKeyUtils
    {
        private const string REGISTRYKEY_JSON_FILE_NAME = "registrykey.json";

        public static void WriteRegistryKeySettingsJson(RegistryKey registryKey, bool firstLoad = false)
        {
            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), REGISTRYKEY_JSON_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(environmnentJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                var jsonRegistryKey = JsonConvert.SerializeObject(firstLoad ? GetInitialRegistryKey() : registryKey);
                writer.WriteRaw(jsonRegistryKey);
            }
        }

        public static RegistryKey ReadJsonRegistryKeySettings()
        {
            string registryKeyJsonFile = Path.Combine(Directory.GetCurrentDirectory(), REGISTRYKEY_JSON_FILE_NAME);

            var registryKey = new RegistryKey();

            using (StreamReader file = File.OpenText(registryKeyJsonFile))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                while (reader.Read())
                {
                    JObject o3 = (JObject) JToken.ReadFrom(reader);
                    foreach (var child in o3.Children())
                    {
                        AddPropertyToRegistryKey(registryKey, child.Path, child.First.ToString());
                    }
                }
            }

            return registryKey;
        }

        private static RegistryKey AddPropertyToRegistryKey(RegistryKey registryKey, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case Constants.RegistryKey.ROOT:
                    registryKey.Root = propertyValue;
                    break;
                case Constants.RegistryKey.SUBKEY:
                    registryKey.Subkey = propertyValue;
                    break;
            }
            return registryKey;
        }

        private static RegistryKey GetInitialRegistryKey()
        {
            return new RegistryKey {Root = "", Subkey = ""};
        } 
    }
}
