using System.Windows.Forms;

namespace gr0ssSysTools.Utils
{
    public static class OpenFileDialogUtils
    {
        public static void FindFile(TextBox currentTextBox, string filter)
        {
            var openFile = new OpenFileDialog {Filter = filter};

            if (openFile.ShowDialog() != DialogResult.OK) return;

            if (openFile.CheckFileExists)
                currentTextBox.Text = openFile.FileName;
        }
    }
}
