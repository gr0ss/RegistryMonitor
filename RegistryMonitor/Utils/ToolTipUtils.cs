using System.Windows.Forms;

namespace RegistryMonitor.Utils
{
    public static class ToolTipUtils
    {
        public static void AddToolTip(Control control, string caption)
        {
            var balloonToolTip = new ToolTip();
            SetToolTipValues(balloonToolTip);
            balloonToolTip.SetToolTip(control, caption);
        }

        private static void SetToolTipValues(ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 6000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
        }
    }
}
