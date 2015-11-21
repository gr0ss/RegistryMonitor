namespace gr0ssSysTools.Utils
{
    public class Constants
    {
        public static class FileExtensions
        {
            public const string IconExtension = ".ico";
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
    }
}
