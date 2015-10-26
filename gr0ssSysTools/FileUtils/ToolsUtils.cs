using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gr0ssSysTools.FileUtils
{
    public class ToolsUtils
    {
        private const string TOOLS_FILE_NAME = "tools.txt";

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
