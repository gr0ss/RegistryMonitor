using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.FileUtils;
using RegistryMonitor.Properties;
using RegistryMonitor.Structs;
using RegistryMonitor.Utils;

namespace RegistryMonitor
{
    /// <summary>
    /// All settings for RegistryMonitor.
    /// </summary>
    public partial class Settings : Form
    {
        private readonly LoadedSettings _loadedSettings;
        private bool _loadingValues;

        /// <summary>
        /// All settings for RegistryMonitor.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// All settings for RegistryMonitor.
        /// </summary>
        /// <param name="loadedSettings"></param>
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

        private void SampleIcon_Changed(object sender, EventArgs e)
        {
            IconUtils.UpdateSampleIcon(comboGeneralIconFont, comboGeneralIconColor, comboGeneralIconTextColor, 
                upDownGeneralIconSize.Text, txtGeneralIconSampleText.Text, picGeneralIconSample);
        }

        private void LoadedSampleIcon_Changed(object sender, EventArgs e)
        {
            IconUtils.UpdateLoadedSampleIcon(txtEnvIconFileLocation.Text, picEnvSampleIcon);
        }

        private void RootCombo_Changed(object sender, EventArgs e)
        {
            comboGeneralRegistryKeyRoot2.Items.Clear();
            comboGeneralRegistryKeyRoot2.Text = "";
            comboGeneralRegistryKeyRoot3.Items.Clear();
            comboGeneralRegistryKeyRoot3.Text = "";
            RegistryKeyUtils.PopulateRootCombo2(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2);
        }

        private void RootCombo2_Changed(object sender, EventArgs e)
        {
            comboGeneralRegistryKeyRoot3.Items.Clear();
            comboGeneralRegistryKeyRoot3.Text = "";

            RegistryKeyUtils.PopulateRootCombo3(comboGeneralRegistryKeyRoot, comboGeneralRegistryKeyRoot2, comboGeneralRegistryKeyRoot3);
        }

        private void TabControl_Changed(object sender, EventArgs e)
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
            RegistryKeyUtils.PopulateComboBoxesBasedOnCurrentRegistryKey(_loadedSettings.MonitoredRegistryKey, CreateRegistryKeyObjectStruct());
            GlobalHotkeyUtils.PopulateGlobalHotkeyCombos(_loadedSettings.General.LoadedGlobalHotkey, CreateGlobalHotkeyObjectStruct());
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
            RepopulateSelectedTabsListbox();
            if (tabControl.SelectedTab == tabEnvironments)
                radioEnvDynamicIcon.Checked = true;
        }

        private void LstEnvAllEnvironments_Changed(object sender, EventArgs e)
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
                    
                    MiscUtils.PopulateHotkeyCombo(comboEnvHotkey, txtEnvName.Text);
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

        private void LstToolAllTools_Changed(object sender, EventArgs e)
        {
            if (lstToolAllTools.SelectedIndex != -1)
            {
                _loadingValues = true;
                var curItem = lstToolAllTools.SelectedItem.ToString();
                var itemToLoad = _loadedSettings.Tools.First(tool => tool.Name == curItem);

                if (itemToLoad != null)
                {
                    lblToolCurrentToolGuid.Text = itemToLoad.ID.ToString();
                    txtToolName.Text = curItem;
                    txtToolFileLocation.Text = itemToLoad.FileLocation;

                    MiscUtils.PopulateHotkeyCombo(comboToolHotkey, txtToolName.Text);
                    comboToolHotkey.SelectedIndex = MiscUtils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);
                }
            }
            _loadingValues = false;
        }

        private void TurnOffListEventHandlers()
        {
            lstEnvAllEnvironments.SelectedIndexChanged -= LstEnvAllEnvironments_Changed;
            lstToolAllTools.SelectedIndexChanged -= LstToolAllTools_Changed;
        }

        private void TurnOnListEventHandlers()
        {
            lstEnvAllEnvironments.SelectedIndexChanged += LstEnvAllEnvironments_Changed;
            lstToolAllTools.SelectedIndexChanged += LstToolAllTools_Changed;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var addName = new AddName(tabControl.SelectedTab == tabEnvironments, _loadedSettings);
            addName.Closing += NameAdded_EventHandler;
            addName.Show();
        }

        private void NameAdded_EventHandler(object sender, CancelEventArgs e)
        {
            RepopulateSelectedTabsListbox();

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
            RepopulateSelectedTabsListbox();
        }

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

        private void checkButton_Click(object sender, EventArgs e)
        {
            RegistryKeyUtils.CheckCurrentKeyValue(CreateRegistryKeyStruct());
        }
        
        private void toolsDirectoryButton_Click(object sender, EventArgs e)
        {
            if (lstToolAllTools.SelectedIndex == -1) return;

            OpenFileDialogUtils.FindFile(txtToolFileLocation, Constants.FileDialogFilters.ExecutableFilesOnly);
        }

        private void btnEnvIconFileLocation_Clicked(object sender, EventArgs e)
        {
            if (lstEnvAllEnvironments.SelectedIndex == -1) return;

            OpenFileDialogUtils.FindFile(txtEnvIconFileLocation, Constants.FileDialogFilters.IconFilesOnly);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                GeneralUtils.SaveGeneralSettings(_loadedSettings, CreateGeneralStruct());
            }
            else
            {
                if (tabControl.SelectedTab == tabEnvironments)
                {
                    EnvironmentUtils.SaveCurrentEnvironment(_loadedSettings, lstEnvAllEnvironments, CreateEnvironmentStruct());
                }
                else if (tabControl.SelectedTab == tabTools)
                {
                    ToolsUtils.SaveCurrentTool(_loadedSettings, lstToolAllTools, CreateToolStruct());
                }
            }
        }

        private void NameTextbox_Leave(object sender, EventArgs e)
        {
            var env = tabControl.SelectedTab == tabEnvironments;
            MiscUtils.PopulateHotkeyCombo(env ? comboEnvHotkey : comboToolHotkey, 
                                          env ? txtEnvName.Text : txtToolName.Text);
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

        private void RepopulateSelectedTabsListbox()
        {
            var env = tabControl.SelectedTab == tabEnvironments;
            var envGuid = string.IsNullOrEmpty(lblEnvCurrentEnvironmentGuid.Text)
                        ? Guid.Empty
                        : Guid.Parse(lblEnvCurrentEnvironmentGuid.Text);
            var toolGuid = string.IsNullOrEmpty(lblToolCurrentToolGuid.Text) 
                         ? Guid.Empty 
                         : Guid.Parse(lblToolCurrentToolGuid.Text);
            ListboxUtils.RepopulateListBox(env, 
                                           env ? lstEnvAllEnvironments : lstToolAllTools, 
                                           _loadedSettings, 
                                           env ? envGuid : toolGuid);
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
            txtToolFileLocation.Text = "";
            _loadingValues = false;
        }

        private EnvironmentStruct CreateEnvironmentStruct()
        {
            return new EnvironmentStruct
            {
                ID = string.IsNullOrEmpty(lblEnvCurrentEnvironmentGuid.Text) 
                    ? Guid.Empty 
                    : Guid.Parse(lblEnvCurrentEnvironmentGuid.Text),
                Name = txtEnvName.Text,
                SubkeyValue = txtEnvRegistryValue.Text,
                HotKey = comboEnvHotkey.Text,
                IconLabel = txtEnvIconDisplayText.Text,
                IconTextColor = comboEnvIconTextColor.SelectedItem.ToString(),
                IconBackgroundColor = comboEnvIconBackgroundColor.SelectedItem.ToString(),
                LoadIcon = radioEnvIconFromFile.Checked,
                IconFileLocation = txtEnvIconFileLocation.Text,
                DisplayOnMenu = checkEnvDisplayOnMenu.Checked
            };
        }

        private ToolStruct CreateToolStruct()
        {
            return new ToolStruct
            {
                ID = string.IsNullOrEmpty(lblToolCurrentToolGuid.Text)
                   ? Guid.Empty 
                   : Guid.Parse(lblToolCurrentToolGuid.Text),
                Name = txtToolName.Text,
                HotKey = comboToolHotkey.Text,
                FileLocation = txtToolFileLocation.Text
            };
        }

        private GeneralStruct CreateGeneralStruct()
        {
            return new GeneralStruct
            {
                IconFont = comboGeneralIconFont.SelectedItem.ToString(),
                IconFontSize = upDownGeneralIconSize.Text.ToFloat(),
                ShowBalloonTips = checkGeneralShowBalloonTips.Checked,
                RegistryKey = CreateRegistryKeyStruct(),
                GlobalHotkey = CreateGlobalHotkeyStruct()
            };
        }

        private RegistryKeyStruct CreateRegistryKeyStruct()
        {
            return new RegistryKeyStruct
            {
                Root = comboGeneralRegistryKeyRoot,
                Root2 = comboGeneralRegistryKeyRoot2,
                Root3 = comboGeneralRegistryKeyRoot3,
                Subkey = txtGeneralRegistryKeyField.Text
            };
        }

        private GlobalHotkeyStruct CreateGlobalHotkeyStruct()
        {
            return new GlobalHotkeyStruct
            {
                Hotkey = comboGeneralGlobalHotkey.Text,
                FirstModifierKey = comboGeneralFirstModifierKey.Text,
                SecondModifierKey = comboGeneralSecondModifierKey.Text
            };
        }

        private RegistryKeyObjectStruct CreateRegistryKeyObjectStruct()
        {
            return new RegistryKeyObjectStruct
            {
                Root = comboGeneralRegistryKeyRoot,
                Root2 = comboGeneralRegistryKeyRoot2,
                Root3 = comboGeneralRegistryKeyRoot3,
                Subkey = txtGeneralRegistryKeyField
            };
        }

        private GlobalHotkeyObjectStruct CreateGlobalHotkeyObjectStruct()
        {
            return new GlobalHotkeyObjectStruct
            {
                Hotkey = comboGeneralGlobalHotkey,
                FirstModifierKey = comboGeneralFirstModifierKey,
                SecondModifierKey = comboGeneralSecondModifierKey
            };
        }
    }
}
