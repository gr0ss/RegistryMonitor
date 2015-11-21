using System.Windows.Forms;

namespace gr0ssSysTools.Utils
{
    public static class OpenFileDialogUtils
    {
        public static string FindFileInFolder(string filter)
        {
            var openFile = new OpenFileDialog {Filter = filter};

            if (openFile.ShowDialog() != DialogResult.OK) return "";

            return openFile.CheckFileExists ? openFile.FileName : "";
        }
    }
}
