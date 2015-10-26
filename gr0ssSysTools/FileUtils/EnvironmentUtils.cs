using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace gr0ssSysTools.FileUtils
{
    public class EnvironmentUtils
    {
        private const string ENVIRONMENT_FILE_NAME = "environments.txt";

        public static List<FileStruct> ReadEnvironmentSettings()
        {
            string environmnentFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);
            if (!File.Exists(environmnentFile))
                return new List<FileStruct>();
            
            var listToReturn = new List<FileStruct>();
            using (StreamReader sr = new StreamReader(environmnentFile))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var result = ParseEnvironmentSetting(line);
                    if (result != null)
                        listToReturn.Add(result);
                }
            }

            return listToReturn;
        } 

        private static FileStruct ParseEnvironmentSetting(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var readLine = input.Split('|');
            Brush colorOfIcon = new SolidBrush(Color.FromName(readLine[5]));

            return new FileStruct
            {
                ID = Guid.Parse(readLine[0]),
                Name = readLine[1],
                ValueKey = readLine[2],
                HotKey = readLine[3],
                IconLabel = readLine[4],
                IconColor = colorOfIcon
            };
        }

        public static List<FileStruct> GetSampleEnvironmentSettings()
        {
            return new List<FileStruct>
            {
                ParseEnvironmentSetting($"{Guid.NewGuid()}|Development|Data\\devDB.xml|D|dev|Blue"),
                ParseEnvironmentSetting($"{Guid.NewGuid()}|sb1|Data\\sb1DB.xml|1|sb1|Blue"),
                ParseEnvironmentSetting($"{Guid.NewGuid()}|sb2|Data\\sb2DB.xml|2|sb2|Blue"),
                ParseEnvironmentSetting($"{Guid.NewGuid()}|sb3|Data\\sb3DB.xml|3|sb3|Blue"),
                ParseEnvironmentSetting($"{Guid.NewGuid()}|Production|Data\\prdDB.xml|P|prd|Red")
            };
        }

        public static void AddNewEnvironmentSetting(string name, IEnumerable<FileStruct> currentList)
        {
            string environmnentFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);
            var iconLabel = name[0].ToString().ToLower() +
                            name[1].ToString().ToLower() +
                            name[2].ToString().ToLower();
            using (var sw = new StreamWriter(environmnentFile))
            {
                var builder = new StringBuilder();
                foreach (var line in currentList)
                {
                    builder.Append(line.HotKey.ToUpperInvariant());
                    builder.Append(line.HotKey.ToLowerInvariant());
                    sw.WriteLine($"{line.ID}|{line.Name}|{line.ValueKey}|{line.HotKey}|{line.IconLabel}|{((SolidBrush)line.IconColor).Color.Name}");
                }
                var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(name, builder.ToString().ToCharArray());
                sw.WriteLine($"{Guid.NewGuid()}|{name}|Data\\DB.xml|{newUniqueHotkey}|{iconLabel}|Blue");
            }
        }

        public static void SaveEnvironmentsSettings(List<FileStruct> listToSave)
        {
            string environmnentFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);

            using (StreamWriter sw = new StreamWriter(environmnentFile))
            {
                foreach (var line in listToSave)
                {
                    sw.WriteLine($"{line.ID}|{line.Name}|{line.ValueKey}|{line.HotKey}|{line.IconLabel}|{((SolidBrush)line.IconColor).Color.Name}");
                }
            }
        }
    }
}
