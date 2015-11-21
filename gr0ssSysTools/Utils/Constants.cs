
namespace gr0ssSysTools.Utils
{
    public class Constants
    {
        public static class FileExtensions
        {
            public const string IconExtension = ".ico";
        }

        public static class FileDialogFilters
        {
            public const string IconFilesOnly = "Icon Files|*.ico";
            public const string ExecutableFilesOnly = "Executable Files|*.exe";
        }

        public static class EnvironmentExitCodes
        {
            public const int Success = 0;
            public const int NoRegistryKey = 1;
            public const int FailedToFindRegistryKey = 2;
        }

        public static class BalloonTips
        {
            public const string IconTitle = "Environment Has Changed";
            public const string IconCaption = "The environment has been changed to";
            public const string ShowBalloonTipsCheckBoxCaption = "Toggle balloon tips when registry key changes.";
            public const string ToolsDirectoryButtonCaption = "Search for tool.";
            public const string GlobalHotkeyGroupBoxCaption = "";
        }

        public static class IconMessages
        {
            public const string ErrorLoadingIcon = "There was an error loading your icon\r\nException:\r\n";
            public const string ErrorLoadingIconCaption = "Error Loading Icon";
        }
        
        public static class HotkeyMessages
        {
            public const string SelectGlobalHotkeyToSave = "You must first select a valid modifier key.";
            public const string SelectGlobalHotkeyToSaveCaption = "Error Finding Modifier Key";
        }

        public static class RegistryKeyMessages
        {
            public const string SelectRegistryKey = "You must first select a valid registry key.";
            public const string SelectRegistryKeyCaption = "Error Saving Registry Key";

            public const string OverrideRegistryKey = "Are you sure this is what you want to override the currently monitored registry key with?";
            public const string OverrideRegistryKeyCaption = "Continue With Save";

            public const string SelectRegistryKeyToMonitor = "As this is your first time running the program, we need you to select the registry key you would like to monitor.";
            public const string SelectRegistryKeyToMonitorCaption = "New User Registry Monitoring Setup";

            public const string CurrentSelectedKey = "The current registry key selected is:\n";
            public const string CurrentValueOfKey = "\n\nIt has a value of:\n";
            public const string CurrentValueOfKeyCaption = "Current Value of Key";
        }

        public static class ToolMessages
        {
            public const string ErrorRetrievingTool = "There was an error retrieving the tool you selected.";
            public const string ErrorRetrievingToolCaption = "Error Retrieving Tool";

            public const string SelectToolToDelete = "You need to select a tool before you can delete one...";
            public const string SelectToolToDeleteCaption = "Please Select Tool";

            public const string ToolDoesntExist = "The file you selected doesn't appear to exist, please select a file that does.";
            public const string ToolDoesntExistCaption = "Invalid File";

            public const string CouldntFindTool = "Couldn't find anything to run at ";
            public const string CouldntFindToolCaption = "Cant Find Tool";
        }

        public static class EnvironmentMessages
        {
            public const string ErrorRetrievingEnvironment = "There was an error retrieving the environment you selected.";
            public const string ErrorRetrievingEnvironmentCaption = "Error Retrieving Environment";

            public const string SelectEnvironmentToDelete = "You need to select an environment before you can delete one...";
            public const string SelectEnvironmentToDeleteCaption = "Please Select Environment";
        }

        public static class Messages
        {
            public const string Error = "Error: ";
            public const string ErrorCaption = "Error";
        }
    }
}
