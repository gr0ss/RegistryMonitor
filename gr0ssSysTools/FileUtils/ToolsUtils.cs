using System;
using System.Collections.Generic;
using System.IO;
using gr0ssSysTools.Files;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace gr0ssSysTools.FileUtils
{
    public class ToolsUtils
    {
        private const string TOOLS_FILE_NAME = "tools.json";

        public static void WriteToolsSettings(IEnumerable<LoadedTools> tools)
        {
            string toolJsonFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(toolJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                foreach (var tool in tools)
                {
                    var jsonTool = JsonConvert.SerializeObject(tool);
                    writer.WriteRaw(jsonTool);
                }
            }
        }

        public static List<LoadedTools> ReadToolsSettings()
        {
            var tools = new List<LoadedTools>();

            string toolJsonFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_FILE_NAME);

            if (!File.Exists(toolJsonFile))
            {
                tools = GetGenericTools();
                WriteToolsSettings(tools);
            }
            else
            {
                using (StreamReader file = File.OpenText(toolJsonFile))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var tool = new LoadedTools();

                    reader.SupportMultipleContent = true;

                    while (reader.Read())
                    {
                        JObject o3 = (JObject) JToken.ReadFrom(reader);
                        foreach (var child in o3.Children())
                        {
                            AddPropertyToTool(tool, child.Path, child.First.ToString());
                        }
                        tools.Add(tool);
                        tool = new LoadedTools();
                    }
                }
            }
            return tools;
        }

        private static LoadedTools AddPropertyToTool(LoadedTools loadedTool, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case Constants.Tools.ID:
                    loadedTool.ID = Guid.Parse(propertyValue);
                    break;
                case Constants.Tools.NAME:
                    loadedTool.Name = propertyValue;
                    break;
                case Constants.Tools.FILE_LOCATION:
                    loadedTool.FileLocation = propertyValue;
                    break;
                case Constants.Tools.HOTKEY:
                    loadedTool.HotKey = propertyValue;
                    break;
            }
            return loadedTool;
        }

        public static List<LoadedTools> GetGenericTools()
        {
            var fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Documents\\AutoIT Scripts\\SetWindowPositions.exe");
            return new List<LoadedTools>
            {
                new LoadedTools {ID = Guid.NewGuid(), Name = "Set Windows Positions", FileLocation = fileLocation, HotKey = "W"}
            };
        }
    }
}
