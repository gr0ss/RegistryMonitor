using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using gr0ssSysTools.Files;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace gr0ssSysTools.FileUtils
{
    public class ToolsUtils
    {
        private const string TOOLS_FILE_NAME = "tools.txt";
        private const string TOOLS_JSON_FILE_NAME = "tools.json";

        public static void WriteToolsSettingsJson(IEnumerable<Tools> tools)
        {
            string toolJsonFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_JSON_FILE_NAME);
            
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

        public static List<Tools> ReadToolsSettingsJson()
        {
            var tools = new List<Tools>();

            string toolJsonFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_JSON_FILE_NAME);


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

        public static List<Tools> GetGenericToolsJson()
        {
            var fileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Documents\\AutoIT Scripts\\SetWindowPositions.exe");
            return new List<Tools>
            {
                new Tools {ID = Guid.NewGuid(), Name = "Set Windows Positions", FileLocation = fileLocation, HotKey = "W"}
            };
        } 

        public static List<FileStruct> ReadToolsSettings()
        {
            string toolsFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_FILE_NAME);
            if (!File.Exists(toolsFile))
                return new List<FileStruct>();
            
            var listToReturn = new List<FileStruct>();
            using (StreamReader sr = new StreamReader(toolsFile))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var result = ParseToolsSetting(line);
                    if (result != null)
                        listToReturn.Add(result);
                }
            }

            return listToReturn;
        } 

        private static FileStruct ParseToolsSetting(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var readLine = input.Split('|');

            return new FileStruct
            {
                ID = Guid.Parse(readLine[0]),
                Name = readLine[1],
                ValueKey = readLine[2],
                HotKey = readLine[3]
            };
        }

        public static void AddNewToolSetting(string name, IEnumerable<FileStruct> currentList)
        {
            string toolsFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_FILE_NAME);
            using (var sw = new StreamWriter(toolsFile))
            {
                var builder = new StringBuilder();
                foreach (var line in currentList)
                {
                    builder.Append(line.HotKey.ToUpperInvariant());
                    builder.Append(line.HotKey.ToLowerInvariant());
                    sw.WriteLine($"{line.ID}|{line.Name}|{line.ValueKey}|{line.HotKey}");
                }

                var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(name, builder.ToString().ToCharArray());
                sw.WriteLine($"{Guid.NewGuid()}|{name}|{Directory.GetCurrentDirectory()}|{newUniqueHotkey}");
            }
        }
        
        public static void SaveToolsSettings(List<FileStruct> listToSave)
        {
            string toolsFile = Path.Combine(Directory.GetCurrentDirectory(), TOOLS_FILE_NAME);
            using (StreamWriter sw = new StreamWriter(toolsFile))
            {
                foreach (var line in listToSave)
                {
                    sw.WriteLine($"{line.ID}|{line.Name}|{line.ValueKey}|{line.HotKey}");
                }
            }
        }
    }
}
