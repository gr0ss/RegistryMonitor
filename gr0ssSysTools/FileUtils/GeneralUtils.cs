using System.IO;

namespace gr0ssSysTools.FileUtils
{
    public class GeneralUtils
    {
        private const string GENERAL_FILE_NAME = "general.txt";
        
        public static GeneralStruct ReadGeneralStructSettings()
        {
            string generalFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);
            if (!File.Exists(generalFile))
                return new GeneralStruct();

            using (StreamReader sr = new StreamReader(generalFile))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var result = ParseGeneralSetting(line);
                    if (result != null)
                        return result;
                }
            }

            return new GeneralStruct();
        }

        private static GeneralStruct ParseGeneralSetting(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var readLine = input.Split('|');
            
            return new GeneralStruct
            {
                RegistryRoot = readLine[0],
                RegistryField = readLine[1]
            };
        }
        
        public static void SaveGeneralSettings(GeneralStruct generalStruct)
        {
            string generalFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);

            using (StreamWriter sw = new StreamWriter(generalFile))
            {
                sw.WriteLine($"{generalStruct.RegistryRoot}|{generalStruct.RegistryField}");
            }
        }
    }
}
