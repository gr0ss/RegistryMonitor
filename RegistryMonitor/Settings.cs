using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FlimFlan.IconEncoder;
using RegistryMonitor.ExtensionMethods;
using Microsoft.Win32;
using RegistryMonitor.FileUtils;
using RegistryMonitor.Parsers;
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
            // Make sure all properties are filled in
            if (comboGeneralIconFont.SelectedIndex == -1 ||
                comboGeneralIconColor.SelectedIndex == -1 ||
                comboGeneralIconTextColor.SelectedIndex == -1 ||
                string.IsNullOrEmpty(upDownGeneralIconSize.Text) ||
                string.IsNullOrEmpty(txtGeneralIconSampleText.Text)) return;

            var size = upDownGeneralIconSize.Text.ToFloat();
            if (size <= float.Epsilon) return;

            Font font = new Font(comboGeneralIconFont.SelectedItem.ToString(), size);
            Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
                Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			    g.FillEllipse(comboGeneralIconColor.SelectedItem.ToString().ToSolidBrush(), rectangle);
                g.DrawString(txtGeneralIconSampleText.Text, font, comboGeneralIconTextColor.SelectedItem.ToString().ToSolidBrush(), 0, 2);
			}

            picGeneralIconSample.Image = Converter.BitmapToIcon(bmp).ToBitmap();
        }

#region Setup and Populate

        private void RepopulateSelectedTabsListbox(bool env)
        {
            if (env)
            {
                lstEnvAllEnvironments.Items.Clear();

                foreach (var key in _loadedSettings.Environments)
                {
                    lstEnvAllEnvironments.Items.Add(key.Name);
                }

                if (string.IsNullOrEmpty(lblEnvCurrentEnvironmentGuid.Text)) return;

                var currentEnvironment = _loadedSettings.Environments
                                                        .First(environment => environment.ID == Guid.Parse(lblEnvCurrentEnvironmentGuid.Text));
                lstEnvAllEnvironments.SelectedIndex = lstEnvAllEnvironments.Items.IndexOf(currentEnvironment.Name);
            }
            else
            {
                lstToolAllTools.Items.Clear();

                foreach (var key in _loadedSettings.Tools)
                {
                    lstToolAllTools.Items.Add(key.Name);
                }

                if (string.IsNullOrEmpty(lblToolCurrentToolGuid.Text)) return;

                var currentTool = _loadedSettings.Tools.First(tool => tool.ID == Guid.Parse(lblToolCurrentToolGuid.Text));
                lstToolAllTools.SelectedIndex = lstToolAllTools.Items.IndexOf(currentTool.Name);
            }
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
            if (tabControl.SelectedTab == tabEnvironments)
            {
                if (lstEnvAllEnvironments.SelectedIndex != -1)
                {
                    var allEnvironments = _loadedSettings.Environments;
                    var environmentToRemove = allEnvironments.FirstOrDefault(env => env.ID == Guid.Parse(lblEnvCurrentEnvironmentGuid.Text));
                    if (environmentToRemove?.ID != Guid.Empty)
                    {
                        allEnvironments.Remove(environmentToRemove);
                        _loadedSettings.Environments = allEnvironments;
                        ClearEnvironmentFields();
                    }
                    else
                    {
                        MessageBox.Show(Constants.EnvironmentMessages.ErrorRetrievingEnvironment,
                                        Constants.EnvironmentMessages.ErrorRetrievingEnvironmentCaption, 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(Constants.EnvironmentMessages.SelectEnvironmentToDelete, 
                                    Constants.EnvironmentMessages.SelectEnvironmentToDeleteCaption, 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                if (lstToolAllTools.SelectedIndex != -1)
                {
                    var allTools = _loadedSettings.Tools;
                    var toolToRemove = allTools.FirstOrDefault(tool => tool.ID == Guid.Parse(lblToolCurrentToolGuid.Text));
                    if (toolToRemove != null && toolToRemove.ID != Guid.Empty)
                    {
                        allTools.Remove(toolToRemove);
                        _loadedSettings.Tools = allTools;
                        ClearToolFields();
                    }
                    else
                    {
                        MessageBox.Show(Constants.ToolMessages.ErrorRetrievingTool,
                            Constants.ToolMessages.ErrorRetrievingToolCaption,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(Constants.ToolMessages.SelectToolToDelete, 
                                    Constants.ToolMessages.SelectToolToDeleteCaption, 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
        }

#region Clear Methods
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
            _loadingValues = false;
            radioEnvDynamicIcon.Checked = true;
            txtEnvIconFileLocation.Text = "";
            checkEnvDisplayOnMenu.Checked = false;
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
#endregion Clear Methods

#region Save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                SaveNewRegistryKey();
                SaveNewGlobalHotkey();
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

        private void SaveNewGlobalHotkey()
        {
            if (comboGeneralFirstModifierKey.Text == System.Windows.Input.ModifierKeys.None.ToString())
            {
                MessageBox.Show(Constants.HotkeyMessages.SelectGlobalHotkeyToSave, 
                                Constants.HotkeyMessages.SelectGlobalHotkeyToSaveCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else if (CurrentHotkeyEqualsSavedHotkey())
            {
                return;
            }
            else
            {
                _loadedSettings.General.LoadedGlobalHotkey.Hotkey =
                    GlobalHotkeyParser.ConvertStringToKey(comboGeneralGlobalHotkey.Text);
                _loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(comboGeneralFirstModifierKey.Text);
                _loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(comboGeneralSecondModifierKey.Text);
            }
        }

        private bool CurrentHotkeyEqualsSavedHotkey()
        {
            return _loadedSettings.General.LoadedGlobalHotkey.Hotkey.ToString() ==
                    comboGeneralGlobalHotkey.Text &&
                   _loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey.ToString() ==
                    comboGeneralFirstModifierKey.Text &&
                   _loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey.ToString() ==
                    comboGeneralSecondModifierKey.Text;
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
            SetCurrentOrderOfEnvironmentsAndSave();
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
            SetCurrentOrderOfToolsAndSave();
        }

        private void SetCurrentOrderOfEnvironmentsAndSave()
        {
            var environmentsOrdered = (from object item 
                                       in lstEnvAllEnvironments.Items
                                       select _loadedSettings.Environments.FirstOrDefault(env => env.Name == item.ToString()))
                                       .ToList();
            _loadedSettings.Environments = environmentsOrdered;
        }

        private void SetCurrentOrderOfToolsAndSave()
        {
            var toolsOrdered = (from object item 
                                in lstToolAllTools.Items
                                select _loadedSettings.Tools.FirstOrDefault(tool => tool.Name == item.ToString()))
                                .ToList();
            _loadedSettings.Tools = toolsOrdered;
        }
#endregion Save button

#region Move buttons
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            TurnOffListEventHandlers();
            MoveItem(-1);
            TurnOnListEventHandlers();
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            TurnOffListEventHandlers();
            MoveItem(1);
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

        private void MoveItem(int direction)
         {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                // Checking selected item
                if (lstEnvAllEnvironments.SelectedItem == null || lstEnvAllEnvironments.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = lstEnvAllEnvironments.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= lstEnvAllEnvironments.Items.Count)
                    return; // Index out of range - nothing to do

                object selected = lstEnvAllEnvironments.SelectedItem;

                // Removing removable element
                lstEnvAllEnvironments.Items.Remove(selected);
                // Insert it in new position
                lstEnvAllEnvironments.Items.Insert(newIndex, selected);
                // Restore selection
                lstEnvAllEnvironments.SetSelected(newIndex, true);
                // Save the new order
                SetCurrentOrderOfEnvironmentsAndSave();
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                // Checking selected item
                if (lstToolAllTools.SelectedItem == null || lstToolAllTools.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = lstToolAllTools.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= lstToolAllTools.Items.Count)
                    return; // Index out of range - nothing to do

                object selected = lstToolAllTools.SelectedItem;

                // Removing removable element
                lstToolAllTools.Items.Remove(selected);
                // Insert it in new position
                lstToolAllTools.Items.Insert(newIndex, selected);
                // Restore selection
                lstToolAllTools.SetSelected(newIndex, true);
                // Save the new order
                SetCurrentOrderOfToolsAndSave();
            }
        }
#endregion Move buttons

#region Registry Key Methods
        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = RegistryKeyUtils.GetCurrentRoot(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3);

            MessageBox.Show($"{Constants.RegistryKeyMessages.CurrentSelectedKey}" +
                            $"{rootValue}\\{txtGeneralRegistryKeyField.Text}" +
                            $"{Constants.RegistryKeyMessages.CurrentValueOfKey}" +
                            $"{GetCurrentKeyValue()}", 
                            Constants.RegistryKeyMessages.CurrentValueOfKeyCaption, 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveNewRegistryKey()
        {
            if (GetCurrentKeyValue() == string.Empty) // New Key is invalid.
            {
                MessageBox.Show(Constants.RegistryKeyMessages.SelectRegistryKey, 
                                Constants.RegistryKeyMessages.SelectRegistryKeyCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CurrentKeyEqualsSavedKey()) // New Key is Old Key.
            {
                return;
            }
            else // Save New Key.
            {
                var confirmMessage = MessageBox.Show(Constants.RegistryKeyMessages.OverrideRegistryKey, 
                                                     Constants.RegistryKeyMessages.OverrideRegistryKeyCaption, 
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                if (confirmMessage != DialogResult.Yes) return;

                var newRegistryKey = new Files.MonitoredRegistryKey
                {
                    Root = RegistryKeyUtils.GetCurrentRoot(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3),
                    Subkey = txtGeneralRegistryKeyField.Text
                };
                _loadedSettings.MonitoredRegistryKey = newRegistryKey;
            }
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

        private bool CurrentKeyEqualsSavedKey()
        {
            var currentRoot = RegistryKeyUtils.GetCurrentRoot(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3);

            return currentRoot == _loadedSettings.MonitoredRegistryKey.Root &&
                   txtGeneralRegistryKeyField.Text == _loadedSettings.MonitoredRegistryKey.Subkey;
        }

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(RegistryKeyUtils.GetCurrentRoot(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3).ToString(), txtGeneralRegistryKeyField.Text, "");
        }

        #endregion Registry Key Methods

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

        private void UpdateLoadedSampleIcon(object sender, EventArgs e)
        {
            if (!File.Exists(txtEnvIconFileLocation.Text) ||
                !txtEnvIconFileLocation.Text.Contains(Constants.FileExtensions.IconExtension, StringComparison.OrdinalIgnoreCase))
            {
                picEnvSampleIcon.Image = null;
                return;
            }
            
            try
            {
                var iconFromFile = new Icon(txtEnvIconFileLocation.Text, 16, 16);
                picEnvSampleIcon.Image = iconFromFile.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.IconMessages.ErrorLoadingIcon + ex, 
                                Constants.IconMessages.ErrorLoadingIconCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                picEnvSampleIcon.Image = Resources.Exit_16.ToBitmap();
            }
        }
    }
}
