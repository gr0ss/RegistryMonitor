using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools.FileUtils
{
    public class DirectoryUtils
    {
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private string _environmentsTxt = "\\environments.txt";
        private string _toolsTxt = "\\tools.txt";
        
        public void CreateTextIfItDoesntExist(string file)
        {
            if (file == _environmentsTxt && !File.Exists(_currentDirectory + _environmentsTxt))
            {
                var guid1 = new Guid();
                var guid2 = new Guid();
                var guid3 = new Guid();
                var guid4 = new Guid();
                var guid5 = new Guid();

                using (var sw = new StreamWriter(_currentDirectory + _environmentsTxt))
                {
                    sw.WriteLine(guid1.ToString() + "|Development|Data\\devDB.xml|D|dev|Blue");
                    sw.WriteLine(guid2.ToString() + "|sb1|Data\\sb1DB.xml|1|sb1|Blue");
                    sw.WriteLine(guid3.ToString() + "|sb2|Data\\sb2DB.xml|2|sb2|Blue");
                    sw.WriteLine(guid4.ToString() + "|sb3|Data\\sb3DB.xml|3|sb3|Blue");
                    sw.WriteLine(guid5.ToString() + "|Production|Data\\prdDB.xml|P|prd|Red");
                }
            }
            else if (file == _toolsTxt && !File.Exists(_currentDirectory + _environmentsTxt))
            {
                var guidOne = new Guid();

                using (var sw = new StreamWriter(_currentDirectory + _toolsTxt))
                {
                    sw.WriteLine(guidOne.ToString() + "|Set Window Positions|C:\\Users\\ggross\\Documents\\AutoIT Scripts\\SetWindowPositions.exe|W");
                }
            }
        }

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
                listToReturn = MakeEnvironmentsList(file);
            }
            else if (file == _toolsTxt && File.Exists(_currentDirectory + _toolsTxt))
            {
                listToReturn = MakeEnvironmentsList(file);
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
                                 line.IconColor);
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
    }
}
