using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools.FileUtils
{
    public class DirectoryUtils
    {
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private string _environmentsTxt = "\\environments.txt";
        private string _toolsTxt = "\\tools.txt";
        private string _generalText = "\\general.txt";
        
        public void CreateTextIfItDoesntExist(string file)
        {
            if (file == _environmentsTxt && !File.Exists(_currentDirectory + _environmentsTxt))
            {
                var guid1 = Guid.NewGuid();
                var guid2 = Guid.NewGuid();
                var guid3 = Guid.NewGuid();
                var guid4 = Guid.NewGuid();
                var guid5 = Guid.NewGuid();

                using (var sw = new StreamWriter(_currentDirectory + _environmentsTxt))
                {
                    sw.WriteLine(guid1.ToString() + "|Development|Data\\devDB.xml|D|dev|Blue");
                    sw.WriteLine(guid2.ToString() + "|sb1|Data\\sb1DB.xml|1|sb1|Blue");
                    sw.WriteLine(guid3.ToString() + "|sb2|Data\\sb2DB.xml|2|sb2|Blue");
                    sw.WriteLine(guid4.ToString() + "|sb3|Data\\sb3DB.xml|3|sb3|Blue");
                    sw.WriteLine(guid5.ToString() + "|Production|Data\\prdDB.xml|P|prd|Red");
                }
            }
            else if (file == _toolsTxt && !File.Exists(_currentDirectory + _toolsTxt))
            {
                var guidOne = Guid.NewGuid();

                using (var sw = new StreamWriter(_currentDirectory + _toolsTxt))
                {
                    sw.WriteLine(guidOne.ToString() + "|Set Window Positions|C:\\Users\\ggross\\Documents\\AutoIT Scripts\\SetWindowPositions.exe|W");
                }
            }
            else if (file == _generalText && !File.Exists(_currentDirectory + _generalText))
            {
                using (var sw = new StreamWriter(_currentDirectory + _generalText))
                {
                    sw.WriteLine("|");
                }
            }
        }

        #region Populate GeneralStruct

        public GeneralStruct ReadFileandPopulateGeneralStruct()
        {
            if (!File.Exists(_currentDirectory + _generalText))
                CreateTextIfItDoesntExist(_generalText);

            return MakeGeneralStruct();
        }

        private GeneralStruct MakeGeneralStruct()
        {
            using (StreamReader sr = new StreamReader(_generalText.Replace("\\", "")))
            {
                var line = sr.ReadLine();

                var readLine = line.Split('|');

                return new GeneralStruct
                {
                    RegistryRoot = readLine[0],
                    RegistryField = readLine[1]
                };
            }
        } 

        #endregion Populate GeneralStruct

        #region Populate FileStruct List
        public List<FileStruct> ReadFileAndPopulateList(string file)
        {
            var listToReturn = new List<FileStruct>();

            if (file == _environmentsTxt && !File.Exists(_currentDirectory + _environmentsTxt))
            {
                CreateTextIfItDoesntExist(file);
                listToReturn = MakeEnvironmentsList(file);
            }
            else if (file == _environmentsTxt && File.Exists(_currentDirectory + _environmentsTxt))
            {
                listToReturn = MakeEnvironmentsList(file);
            }
            else if (file == _toolsTxt && !File.Exists(_currentDirectory + _toolsTxt))
            {
                CreateTextIfItDoesntExist(file);
                listToReturn = MakeToolsList(file);
            }
            else if (file == _toolsTxt && File.Exists(_currentDirectory + _toolsTxt))
            {
                listToReturn = MakeToolsList(file);
            }
            
            return listToReturn;
        }

        private List<FileStruct> MakeEnvironmentsList(string file)
        {
            var listToReturn = new List<FileStruct>();
            using (StreamReader sr = new StreamReader(file.Replace("\\", "")))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var readLine = line.Split('|');
                    Brush colorOfIcon = new SolidBrush(Color.FromName(readLine[5]));

                    var readLineStruct = new FileStruct
                    {
                        ID = Guid.Parse(readLine[0]),
                        Name = readLine[1],
                        ValueKey = readLine[2],
                        HotKey = readLine[3],
                        IconLabel = readLine[4],
                        IconColor = colorOfIcon
                    };

                    listToReturn.Add(readLineStruct);
                }
            }
            return listToReturn;
        } 

        private List<FileStruct> MakeToolsList(string file)
        {
            var listToReturn = new List<FileStruct>();
            using (StreamReader sr = new StreamReader(file.Replace("\\", "")))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var readLine = line.Split('|');
                    
                    var readLineStruct = new FileStruct
                    {
                        ID = Guid.Parse(readLine[0]),
                        Name = readLine[1],
                        ValueKey = readLine[2],
                        HotKey = readLine[3]
                    };

                    listToReturn.Add(readLineStruct);
                }
            }
            return listToReturn;
        }

        public void AddNameToFile(string name, bool isEnv, IEnumerable<FileStruct> currentList)
        {
            if (isEnv)
                AddEnvironmentToFile(name, currentList);
            else
                AddToolToFile(name, currentList);
        }

        private void AddEnvironmentToFile(string name, IEnumerable<FileStruct> currentList)
        {
            var guid1 = Guid.NewGuid();
            var iconLabel = name[0].ToString().ToLower() +
                            name[1].ToString().ToLower() +
                            name[2].ToString().ToLower();
            using (var sw = new StreamWriter(_currentDirectory + _environmentsTxt))
            {
                var builder = new StringBuilder();
                foreach (var line in currentList)
                {
                    builder.Append(line.HotKey.ToUpperInvariant());
                    builder.Append(line.HotKey.ToLowerInvariant());
                    sw.WriteLine(line.ID + "|" + 
                                 line.Name + "|" + 
                                 line.ValueKey + "|" + 
                                 line.HotKey + "|" + 
                                 line.IconLabel + "|" + 
                                 ((SolidBrush)line.IconColor).Color.Name);
                }
                var newUniqueHotkey = GetFirstUniqueHotkey(name, builder.ToString().ToCharArray());
                sw.WriteLine(guid1.ToString() + "|" + name + 
                            "|Data\\DB.xml|" + newUniqueHotkey + "|" +
                            iconLabel + "|Blue");
            }
        }

        private void AddToolToFile(string name, IEnumerable<FileStruct> currentList)
        {
            var guidOne = Guid.NewGuid();

            using (var sw = new StreamWriter(_currentDirectory + _toolsTxt))
            {
                var builder = new StringBuilder();
                foreach (var line in currentList)
                {
                    builder.Append(line.HotKey.ToUpperInvariant());
                    builder.Append(line.HotKey.ToLowerInvariant());
                    sw.WriteLine(line.ID + "|" + 
                                 line.Name + "|" + 
                                 line.ValueKey + "|" + 
                                 line.HotKey);
                }

                var newUniqueHotkey = GetFirstUniqueHotkey(name, builder.ToString().ToCharArray());
                sw.WriteLine(guidOne.ToString() + "|" + name + "|" + _currentDirectory + "|" + newUniqueHotkey);
            }
        }

        private char GetFirstUniqueHotkey(string name, params char[] charset)
        {
            return name.TrimStart(charset)[0];
        }
        #endregion Populate FileStruct List

        #region Write FileStruct to file
        public void SaveListToFile(List<FileStruct> listToSave, string file)
        {
            if (file == _environmentsTxt && File.Exists(_currentDirectory + _environmentsTxt))
            {
                WriteEnvironmentsToFile(listToSave, file);
            }
            else if (file == _toolsTxt && File.Exists(_currentDirectory + _toolsTxt))
            {
                WriteToolsToFile(listToSave, file);
            }

            
        }

        private void WriteEnvironmentsToFile(List<FileStruct> listToSave, string file)
        {
            using (StreamWriter sw = new StreamWriter(file.Replace("\\", "")))
            {
                foreach (var line in listToSave)
                {
                    sw.WriteLine(line.ID + "|" + 
                                 line.Name + "|" + 
                                 line.ValueKey + "|" + 
                                 line.HotKey + "|" + 
                                 line.IconLabel + "|" + 
                                 ((SolidBrush)line.IconColor).Color.Name);
                }
            }
        }

        private void WriteToolsToFile(List<FileStruct> listToSave, string file)
        {
            using (StreamWriter sw = new StreamWriter(file.Replace("\\", "")))
            {
                foreach (var line in listToSave)
                {
                    sw.WriteLine(line.ID + "|" + 
                                 line.Name + "|" + 
                                 line.ValueKey + "|" + 
                                 line.HotKey);
                }
            }
        }
        #endregion Write FileStruct to file

        #region Write GeneralStruct to file
        public void SaveGeneralStructToFile(GeneralStruct structToSave)
        {
            if (File.Exists(_currentDirectory + _generalText))
            {
                WriteGeneralStructToFile(structToSave);
            }
        }

        private void WriteGeneralStructToFile(GeneralStruct structToSave)
        {
            using (StreamWriter sw = new StreamWriter(_generalText.Replace("\\", "")))
            {
                sw.WriteLine(structToSave.RegistryRoot + "|" + structToSave.RegistryField);
            }
        }

        #endregion Write GeneralStruct to file
    }
}
