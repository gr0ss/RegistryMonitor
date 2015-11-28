using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.FileUtils;
using RegistryMonitor.Properties;
using RegistryMonitor.Utils;

namespace RegistryMonitor
{
    public partial class Settings : Form
    {
        private LoadedSettings _loadedSettings;
        private bool _loadingValues;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(LoadedSettings loadedSettings)
        {
            InitializeComponent();
            
            _loadedSettings = loadedSettings;

            tabControl.SelectedTab = tabGeneral;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            LoadButtonImages();
            
            LoadGeneralTab();

            LoadToolTips();
        }

        private void LoadToolTips()
        {
            ToolTipUtils.AddToolTip(checkGeneralShowBalloonTips, Constants.BalloonTips.CheckGeneralShowBalloonTipsCaption);
            ToolTipUtils.AddToolTip(btnToolFileLocation, Constants.BalloonTips.BtnToolFileLocationCaption);
            ToolTipUtils.AddToolTip(groupGeneralGlobalHotkey, Constants.BalloonTips.GroupGeneralGlobalHotkeyCaption);
            ToolTipUtils.AddToolTip(checkEnvDisplayOnMenu, Constants.BalloonTips.CheckEnvDisplayOnMenuCaption);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                LoadGeneralTab();
            }
            else
            {
                LoadEnvironmentsOrToolsTab();
            }
        }

        private void LoadGeneralTab()
        {
            if (tabControl.SelectedTab != tabGeneral) return;

            SetupButtonEnabled(false);
            RegistryKeyUtils.PopulateComboBoxesBasedOnCurrentRegistryKey(_loadedSettings.MonitoredRegistryKey, comboGeneralRegistryKeyRoot, 
                                                                         comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3, 
                                                                         txtGeneralRegistryKeyField);
            GlobalHotkeyUtils.PopulateGlobalHotkeyCombos(_loadedSettings.General.LoadedGlobalHotkey, comboGeneralGlobalHotkey, 
                                                         comboGeneralFirstModifierKey, comboGeneralSecondModifierKey);
            checkGeneralShowBalloonTips.Checked = _loadedSettings.General.ShowBalloonTips;
            GeneralUtils.PopulateIconProperties(_loadedSettings.General, comboGeneralIconFont, comboGeneralIconColor, comboGeneralIconTextColor);
            upDownGeneralIconSize.Text = _loadedSettings.General.IconFontSize.ToString(CultureInfo.InvariantCulture);
        }

        private void LoadEnvironmentsOrToolsTab()
        {
            if (tabControl.SelectedTab == tabGeneral) return;

            ClearEnvironmentFields();
            ClearToolFields();
            SetupButtonEnabled(true);
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
            if (tabControl.SelectedTab == tabEnvironments)
                radioEnvDynamicIcon.Checked = true;
        }

        private void UpdateSample(object sender, EventArgs e)
        {
            IconUtils.UpdateSampleIcon(comboGeneralIconFont, comboGeneralIconColor, comboGeneralIconTextColor, 
                upDownGeneralIconSize.Text, txtGeneralIconSampleText.Text, picGeneralIconSample);
        }

        private void UpdateLoadedSampleIcon(object sender, EventArgs e)
        {
            IconUtils.UpdateLoadedSampleIcon(txtEnvIconFileLocation.Text, picEnvSampleIcon);
        }

#region Setup and Populate

        private void RepopulateSelectedTabsListbox(bool env)
        {
            ListboxUtils.RepopulateListBox(env, 
                                           env ? lstEnvAllEnvironments : lstToolAllTools, 
                                           _loadedSettings, 
                                           env ? lblEnvCurrentEnvironmentGuid.Text : lblToolCurrentToolGuid.Text);
        }
        
        private void SetupButtonEnabled(bool enabled)
        {
            /* Save button is always enabled. All other *
             * buttons are dependent on the current tab */
            addButton.Enabled = enabled;
            removeButton.Enabled = enabled;
            moveUpButton.Enabled = enabled;
            moveDownButton.Enabled = enabled;
        }
        
        private void LoadButtonImages()
        {
            addButton.Image = Resources.Add;
            removeButton.Image = Resources.Delete;
            saveButton.Image = Resources.Save;

            var arrowUpPicture = Resources.Move_Arrow;
            arrowUpPicture.RotateFlip(RotateFlipType.Rotate90FlipY);
            moveUpButton.Image = arrowUpPicture;

            var arrowDownPicture = Resources.Move_Arrow;
            arrowDownPicture.RotateFlip(RotateFlipType.Rotate270FlipY);
            moveDownButton.Image = arrowDownPicture;
        }

        private void PopulateHotkeyCombo()
        {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                comboEnvHotkey.Items.Clear();
                foreach (var c in txtEnvName.Text.Where(c => c.ToString() != " "))
                    comboEnvHotkey.Items.Add(c);
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                comboToolHotkey.Items.Clear();
                foreach (var c in txtToolName.Text.Where(c => c.ToString() != " "))
                    comboToolHotkey.Items.Add(c);
            }
        }

        private void NameTextbox_Leave(object sender, EventArgs e)
        {
            PopulateHotkeyCombo();
        }
#endregion Setup and Populate

        private void addButton_Click(object sender, EventArgs e)
        {
            var addName = new AddName(tabControl.SelectedTab == tabEnvironments, _loadedSettings);
            addName.Closing += NameAdded_EventHandler;
            addName.Show();
        }

        private void NameAdded_EventHandler(object sender, CancelEventArgs e)
        {
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);

            if (tabControl.SelectedTab == tabEnvironments)
            {
                var index = lstEnvAllEnvironments.Items.Count - 1;
                lstEnvAllEnvironments.SelectedIndex = index;
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                var toolIndex = lstToolAllTools.Items.Count - 1;
                lstToolAllTools.SelectedIndex = toolIndex;
            }
        }
        
        private void removeButton_Click(object sender, EventArgs e)
        {
            var env = tabControl.SelectedTab == tabEnvironments;
            ListboxUtils.RemoveListBoxItem(env, 
                                           env ? lstEnvAllEnvironments : lstToolAllTools, 
                                           _loadedSettings, 
                                           env ? lblEnvCurrentEnvironmentGuid.Text : lblToolCurrentToolGuid.Text);
            if (env)
            {
                ClearEnvironmentFields();
            }
            else
            {
                ClearToolFields();
            }
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
        }
        
        private void ClearEnvironmentFields()
        {
            _loadingValues = true;
            txtEnvName.Text = "";
            txtEnvRegistryValue.Text = "";
            comboEnvHotkey.Items.Clear();
            comboEnvHotkey.Text = "";
            txtEnvIconDisplayText.Text = "";
            comboEnvIconTextColor.SelectedIndex = -1;
            comboEnvIconBackgroundColor.SelectedIndex = -1;
            lblEnvCurrentEnvironmentGuid.Text = "";
            radioEnvDynamicIcon.Checked = true;
            txtEnvIconFileLocation.Text = "";
            checkEnvDisplayOnMenu.Checked = false;
            _loadingValues = false;
        }

        private void ClearToolFields()
        {
            _loadingValues = true;
            txtToolName.Text = "";
            txtToolFileLocation.Text = "";
            comboToolHotkey.Items.Clear();
            comboToolHotkey.Text = "";
            lblToolCurrentToolGuid.Text = "";
            _loadingValues = false;
        }

#region Save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                RegistryKeyUtils.SaveNewRegistryKey(_loadedSettings, comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3, txtGeneralRegistryKeyField.Text);
                GlobalHotkeyUtils.SaveNewGlobalHotkey(_loadedSettings, comboGeneralGlobalHotkey.Text, comboGeneralFirstModifierKey.Text, comboGeneralSecondModifierKey.Text);
                _loadedSettings.General.ShowBalloonTips = checkGeneralShowBalloonTips.Checked;
                _loadedSettings.General.IconFont = comboGeneralIconFont.SelectedItem.ToString();
                _loadedSettings.General.IconFontSize = upDownGeneralIconSize.Text.ToFloat();
                _loadedSettings.General = _loadedSettings.General;
            }
            else
            {
                if (tabControl.SelectedTab == tabEnvironments)
                {
                    SaveCurrentEnvironment();
                }
                else if (tabControl.SelectedTab == tabTools)
                {
                    SaveCurrentTool();
                }
            }
        }
        
        private void SaveCurrentEnvironment()
        {
            var currentEnvironment = _loadedSettings.Environments.First(env => env.ID == Guid.Parse(lblEnvCurrentEnvironmentGuid.Text));

            if (currentEnvironment.Name != txtEnvName.Text)
                currentEnvironment.Name = txtEnvName.Text;
            if (currentEnvironment.SubkeyValue != txtEnvRegistryValue.Text)
                currentEnvironment.SubkeyValue = txtEnvRegistryValue.Text;
            if (currentEnvironment.HotKey != comboEnvHotkey.Text)
                currentEnvironment.HotKey = comboEnvHotkey.Text;
            if (currentEnvironment.IconLabel != txtEnvIconDisplayText.Text)
                currentEnvironment.IconLabel = txtEnvIconDisplayText.Text;
            if (currentEnvironment.IconTextColor != comboEnvIconTextColor.SelectedItem.ToString())
                currentEnvironment.IconTextColor = comboEnvIconTextColor.SelectedItem.ToString();
            if (currentEnvironment.IconBackgroundColor != comboEnvIconBackgroundColor.SelectedItem.ToString())
                currentEnvironment.IconBackgroundColor = comboEnvIconBackgroundColor.SelectedItem.ToString();
            if (currentEnvironment.LoadIcon != radioEnvIconFromFile.Checked)
                currentEnvironment.LoadIcon = radioEnvIconFromFile.Checked;
            if (currentEnvironment.IconFileLocation != txtEnvIconFileLocation.Text)
                currentEnvironment.IconFileLocation = txtEnvIconFileLocation.Text;
            if (currentEnvironment.DisplayOnMenu != checkEnvDisplayOnMenu.Checked)
                currentEnvironment.DisplayOnMenu = checkEnvDisplayOnMenu.Checked;

            RepopulateSelectedTabsListbox(true);
            ListboxUtils.SetCurrentOrderFromListBoxAndSave(true, lstEnvAllEnvironments, _loadedSettings);
        }

        private void SaveCurrentTool()
        {
            var currentTool = _loadedSettings.Tools.First(tool => tool.ID == Guid.Parse(lblToolCurrentToolGuid.Text));

            if (currentTool.Name != txtToolName.Text)
                currentTool.Name = txtToolName.Text;
            if (currentTool.FileLocation != txtToolFileLocation.Text)
                currentTool.FileLocation = txtToolFileLocation.Text;
            if (currentTool.HotKey != comboToolHotkey.Text)
                currentTool.HotKey = comboToolHotkey.Text;

            RepopulateSelectedTabsListbox(false);
            ListboxUtils.SetCurrentOrderFromListBoxAndSave(false, lstToolAllTools, _loadedSettings);
        }
#endregion Save button

#region Move buttons
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            var env = tabControl.SelectedTab == tabEnvironments;
            TurnOffListEventHandlers();
            ListboxUtils.MoveItem(-1, env, env ? lstEnvAllEnvironments : lstToolAllTools, _loadedSettings);
            TurnOnListEventHandlers();
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            var env = tabControl.SelectedTab == tabEnvironments;
            TurnOffListEventHandlers();
            ListboxUtils.MoveItem(1, env, env ? lstEnvAllEnvironments : lstToolAllTools, _loadedSettings);
            TurnOnListEventHandlers();
        }

        private void LstEnvAllEnvironmentsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEnvAllEnvironments.SelectedIndex != -1)
            {
                _loadingValues = true;
                var curItem = lstEnvAllEnvironments.SelectedItem.ToString();
                var itemToLoad = _loadedSettings.Environments.FirstOrDefault(env => env.Name == curItem);

                if (itemToLoad != null)
                {
                    lblEnvCurrentEnvironmentGuid.Text = itemToLoad.ID.ToString();
                    txtEnvName.Text = curItem;
                    txtEnvRegistryValue.Text = itemToLoad.SubkeyValue;

                    PopulateHotkeyCombo();
                    comboEnvHotkey.SelectedIndex = MiscUtils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);

                    txtEnvIconDisplayText.Text = itemToLoad.IconLabel;
                    ColorUtils.PopulateColorComboBox(itemToLoad.IconTextColor, comboEnvIconTextColor);
                    ColorUtils.PopulateColorComboBox(itemToLoad.IconBackgroundColor, comboEnvIconBackgroundColor);

                    radioEnvIconFromFile.Checked = itemToLoad.LoadIcon;
                    radioEnvDynamicIcon.Checked = !itemToLoad.LoadIcon;

                    txtEnvIconFileLocation.Text = itemToLoad.IconFileLocation;

                    checkEnvDisplayOnMenu.Checked = itemToLoad.DisplayOnMenu;
                }
            }
            _loadingValues = false;
        }

        private void LstToolAllToolsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstToolAllTools.SelectedIndex != -1)
            {
                _loadingValues = true;
                var curItem = lstToolAllTools.SelectedItem.ToString();
                var itemToLoad = _loadedSettings.Tools.First(tool => tool.Name == curItem);

                lblToolCurrentToolGuid.Text = itemToLoad.ID.ToString();
                txtToolName.Text = curItem;
                txtToolFileLocation.Text = itemToLoad.FileLocation;

                PopulateHotkeyCombo();
                comboToolHotkey.SelectedIndex = MiscUtils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);
            }
            _loadingValues = false;
        }

        private void TurnOffListEventHandlers()
        {
            lstEnvAllEnvironments.SelectedIndexChanged -= LstEnvAllEnvironmentsSelectedIndexChanged;
            lstToolAllTools.SelectedIndexChanged -= LstToolAllToolsSelectedIndexChanged;
        }

        private void TurnOnListEventHandlers()
        {
            lstEnvAllEnvironments.SelectedIndexChanged += LstEnvAllEnvironmentsSelectedIndexChanged;
            lstToolAllTools.SelectedIndexChanged += LstToolAllToolsSelectedIndexChanged;
        }
#endregion Move buttons
        
        private void checkButton_Click(object sender, EventArgs e)
        {
            RegistryKeyUtils.CheckCurrentKeyValue(comboGeneralRegistryKeyRoot,
                                                  comboGeneralRegistryKeyRoot2, 
                                                  comboGeneralRegistryKeyRoot3, 
                                                  txtGeneralRegistryKeyField.Text);
        }
        
        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboGeneralRegistryKeyRoot2.Items.Clear();
            comboGeneralRegistryKeyRoot2.Text = "";
            comboGeneralRegistryKeyRoot3.Items.Clear();
            comboGeneralRegistryKeyRoot3.Text = "";
            RegistryKeyUtils.PopulateRootCombo2(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2);
        }

        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboGeneralRegistryKeyRoot3.Items.Clear();
            comboGeneralRegistryKeyRoot3.Text = "";

            RegistryKeyUtils.PopulateRootCombo3(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3);
        }

        private void toolsDirectoryButton_Click(object sender, EventArgs e)
        {
            if (lstToolAllTools.SelectedIndex == -1) return;

            OpenFileDialogUtils.FindFile(txtToolFileLocation, Constants.FileDialogFilters.ExecutableFilesOnly);
        }

        private void iconRadioButtons_CheckChanged(object sender, EventArgs e)
        {
            if (radioEnvIconFromFile.Checked)
            {
                pnlEnvIconFileLocation.Visible = true;
                pnlEnvDynamicIcon.Visible = false;
            }
            else if (radioEnvDynamicIcon.Checked)
            {
                pnlEnvIconFileLocation.Visible = false;
                pnlEnvDynamicIcon.Visible = true;
            }
        }

        private void btnEnvIconFileLocation_Clicked(object sender, EventArgs e)
        {
            if (lstEnvAllEnvironments.SelectedIndex == -1) return;

            OpenFileDialogUtils.FindFile(txtEnvIconFileLocation, Constants.FileDialogFilters.IconFilesOnly);
        }
    }
}
