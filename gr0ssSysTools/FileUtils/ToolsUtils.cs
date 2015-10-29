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

        public static void WriteToolsSettings(IEnumerable<Tools> tools)
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

        public static List<Tools> ReadToolsSettings()
        {
            var tools = new List<Tools>();

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
                    var tool = new Tools();

                    reader.SupportMultipleContent = true;

                    while (reader.Read())
                    {
                        JObject o3 = (JObject) JToken.ReadFrom(reader);
                        foreach (var child in o3.Children())
                        {
                            AddPropertyToTool(tool, child.Path, child.First.ToString());
                        }
                        tools.Add(tool);
                        tool = new Tools();
                    }
                }
            }
            return tools;
        }

        private static Tools AddPropertyToTool(Tools tool, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case Constants.Tools.ID:
                    tool.ID = Guid.Parse(propertyValue);
                    break;
                case Constants.Tools.NAME:
                    tool.Name = propertyValue;
                    break;
                case Constants.Tools.FILE_LOCATION:
                    tool.FileLocation = propertyValue;
                    break;
                case Constants.Tools.HOTKEY:
                    tool.HotKey = propertyValue;
                    break;
            }
            return tool;
        }

        public static List<Tools> GetGenericTools()
        {
            var fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Documents\\AutoIT Scripts\\SetWindowPositions.exe");
            return new List<Tools>
            {
                new Tools {ID = Guid.NewGuid(), Name = "Set Windows Positions", FileLocation = fileLocation, HotKey = "S"}
            };
        }
    }
}
