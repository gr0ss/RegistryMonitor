using RegistryMonitor.Utils;

namespace RegistryMonitor
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.saveButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.btnToolFileLocation = new System.Windows.Forms.Button();
            this.lblToolCurrentToolGuid = new System.Windows.Forms.Label();
            this.comboToolHotkey = new System.Windows.Forms.ComboBox();
            this.lblToolHotkey = new System.Windows.Forms.Label();
            this.lstToolAllTools = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToolFileLocation = new System.Windows.Forms.TextBox();
            this.txtToolName = new System.Windows.Forms.TextBox();
            this.directoryPathLbl = new System.Windows.Forms.Label();
            this.LblToolName = new System.Windows.Forms.Label();
            this.LblEnvName = new System.Windows.Forms.Label();
            this.txtEnvRegistryValue = new System.Windows.Forms.TextBox();
            this.txtEnvName = new System.Windows.Forms.TextBox();
            this.lstEnvAllEnvironments = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.checkGeneralShowBalloonTips = new System.Windows.Forms.CheckBox();
            this.groupGeneralIconProperties = new System.Windows.Forms.GroupBox();
            this.upDownGeneralIconSize = new System.Windows.Forms.NumericUpDown();
            this.comboGeneralIconTextColor = new RegistryMonitor.Utils.ColorComboBox();
            this.comboGeneralIconColor = new RegistryMonitor.Utils.ColorComboBox();
            this.lblGeneralIconTextColor = new System.Windows.Forms.Label();
            this.picGeneralIconSample = new System.Windows.Forms.PictureBox();
            this.lblGeneralIconSample = new System.Windows.Forms.Label();
            this.lblGeneralIconSampleText = new System.Windows.Forms.Label();
            this.txtGeneralIconSampleText = new System.Windows.Forms.TextBox();
            this.lblGeneralIconColor = new System.Windows.Forms.Label();
            this.lblGeneralIconSize = new System.Windows.Forms.Label();
            this.lblGeneralIconFont = new System.Windows.Forms.Label();
            this.comboGeneralIconFont = new System.Windows.Forms.ComboBox();
            this.groupGeneralGlobalHotkey = new System.Windows.Forms.GroupBox();
            this.lblGeneralModifierKeys = new System.Windows.Forms.Label();
            this.lblGeneralGlobalHotkey = new System.Windows.Forms.Label();
            this.comboGeneralSecondModifierKey = new System.Windows.Forms.ComboBox();
            this.comboGeneralFirstModifierKey = new System.Windows.Forms.ComboBox();
            this.comboGeneralGlobalHotkey = new System.Windows.Forms.ComboBox();
            this.groupGeneralUpdateRegistryKey = new System.Windows.Forms.GroupBox();
            this.lblGeneralRegistryKeyField = new System.Windows.Forms.Label();
            this.lblGeneralRegistryKeyRoots = new System.Windows.Forms.Label();
            this.lblGeneralRegistryKeyRoot = new System.Windows.Forms.Label();
            this.btnGeneralCheckRegistryKey = new System.Windows.Forms.Button();
            this.txtGeneralRegistryKeyField = new System.Windows.Forms.TextBox();
            this.comboGeneralRegistryKeyRoot3 = new System.Windows.Forms.ComboBox();
            this.comboGeneralRegistryKeyRoot2 = new System.Windows.Forms.ComboBox();
            this.comboGeneralRegistryKeyRoot = new System.Windows.Forms.ComboBox();
            this.tabEnvironments = new System.Windows.Forms.TabPage();
            this.checkEnvDisplayOnMenu = new System.Windows.Forms.CheckBox();
            this.pnlEnvIcon = new System.Windows.Forms.Panel();
            this.pnlEnvIconFileLocation = new System.Windows.Forms.Panel();
            this.picEnvSampleIcon = new System.Windows.Forms.PictureBox();
            this.lblEnvSampleIcon = new System.Windows.Forms.Label();
            this.btnEnvIconFileLocation = new System.Windows.Forms.Button();
            this.txtEnvIconFileLocation = new System.Windows.Forms.TextBox();
            this.lblEnvIconFileLocation = new System.Windows.Forms.Label();
            this.pnlEnvDynamicIcon = new System.Windows.Forms.Panel();
            this.iconTextLabel = new System.Windows.Forms.Label();
            this.comboEnvIconTextColor = new RegistryMonitor.Utils.ColorComboBox();
            this.txtEnvIconDisplayText = new System.Windows.Forms.TextBox();
            this.iconTextColorLabel = new System.Windows.Forms.Label();
            this.iconColorLabel = new System.Windows.Forms.Label();
            this.comboEnvIconBackgroundColor = new RegistryMonitor.Utils.ColorComboBox();
            this.pnlEnvIconType = new System.Windows.Forms.Panel();
            this.radioEnvIconFromFile = new System.Windows.Forms.RadioButton();
            this.radioEnvDynamicIcon = new System.Windows.Forms.RadioButton();
            this.comboEnvHotkey = new System.Windows.Forms.ComboBox();
            this.hotkeyLabel = new System.Windows.Forms.Label();
            this.lblEnvCurrentEnvironmentGuid = new System.Windows.Forms.Label();
            this.registryValueLbl = new System.Windows.Forms.Label();
            this.tabTools.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupGeneralIconProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownGeneralIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneralIconSample)).BeginInit();
            this.groupGeneralGlobalHotkey.SuspendLayout();
            this.groupGeneralUpdateRegistryKey.SuspendLayout();
            this.tabEnvironments.SuspendLayout();
            this.pnlEnvIcon.SuspendLayout();
            this.pnlEnvIconFileLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnvSampleIcon)).BeginInit();
            this.pnlEnvDynamicIcon.SuspendLayout();
            this.pnlEnvIconType.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(70, 268);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(26, 26);
            this.saveButton.TabIndex = 17;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(134, 268);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(26, 26);
            this.moveDownButton.TabIndex = 16;
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(102, 268);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(26, 26);
            this.moveUpButton.TabIndex = 15;
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(6, 268);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(26, 26);
            this.addButton.TabIndex = 13;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(38, 268);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(26, 26);
            this.removeButton.TabIndex = 14;
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.btnToolFileLocation);
            this.tabTools.Controls.Add(this.lblToolCurrentToolGuid);
            this.tabTools.Controls.Add(this.comboToolHotkey);
            this.tabTools.Controls.Add(this.lblToolHotkey);
            this.tabTools.Controls.Add(this.lstToolAllTools);
            this.tabTools.Controls.Add(this.label2);
            this.tabTools.Controls.Add(this.txtToolFileLocation);
            this.tabTools.Controls.Add(this.txtToolName);
            this.tabTools.Controls.Add(this.directoryPathLbl);
            this.tabTools.Controls.Add(this.LblToolName);
            this.tabTools.Location = new System.Drawing.Point(4, 22);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(517, 239);
            this.tabTools.TabIndex = 1;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // btnToolFileLocation
            // 
            this.btnToolFileLocation.Image = global::RegistryMonitor.Properties.Resources.folder_add16;
            this.btnToolFileLocation.Location = new System.Drawing.Point(485, 60);
            this.btnToolFileLocation.Name = "btnToolFileLocation";
            this.btnToolFileLocation.Size = new System.Drawing.Size(23, 20);
            this.btnToolFileLocation.TabIndex = 22;
            this.btnToolFileLocation.UseVisualStyleBackColor = true;
            this.btnToolFileLocation.Click += new System.EventHandler(this.toolsDirectoryButton_Click);
            // 
            // lblToolCurrentToolGuid
            // 
            this.lblToolCurrentToolGuid.AutoSize = true;
            this.lblToolCurrentToolGuid.Location = new System.Drawing.Point(214, 5);
            this.lblToolCurrentToolGuid.Name = "lblToolCurrentToolGuid";
            this.lblToolCurrentToolGuid.Size = new System.Drawing.Size(18, 13);
            this.lblToolCurrentToolGuid.TabIndex = 21;
            this.lblToolCurrentToolGuid.Text = "ID";
            this.lblToolCurrentToolGuid.Visible = false;
            // 
            // comboToolHotkey
            // 
            this.comboToolHotkey.FormattingEnabled = true;
            this.comboToolHotkey.Location = new System.Drawing.Point(167, 99);
            this.comboToolHotkey.Name = "comboToolHotkey";
            this.comboToolHotkey.Size = new System.Drawing.Size(341, 21);
            this.comboToolHotkey.TabIndex = 20;
            // 
            // lblToolHotkey
            // 
            this.lblToolHotkey.AutoSize = true;
            this.lblToolHotkey.Location = new System.Drawing.Point(167, 83);
            this.lblToolHotkey.Name = "lblToolHotkey";
            this.lblToolHotkey.Size = new System.Drawing.Size(47, 13);
            this.lblToolHotkey.TabIndex = 19;
            this.lblToolHotkey.Text = "Hotkey: ";
            // 
            // lstToolAllTools
            // 
            this.lstToolAllTools.FormattingEnabled = true;
            this.lstToolAllTools.Location = new System.Drawing.Point(7, 21);
            this.lstToolAllTools.Name = "lstToolAllTools";
            this.lstToolAllTools.Size = new System.Drawing.Size(154, 212);
            this.lstToolAllTools.TabIndex = 12;
            this.lstToolAllTools.SelectedIndexChanged += new System.EventHandler(this.LstToolAllToolsSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tools";
            // 
            // txtToolFileLocation
            // 
            this.txtToolFileLocation.Location = new System.Drawing.Point(167, 60);
            this.txtToolFileLocation.Name = "txtToolFileLocation";
            this.txtToolFileLocation.Size = new System.Drawing.Size(315, 20);
            this.txtToolFileLocation.TabIndex = 10;
            // 
            // txtToolName
            // 
            this.txtToolName.Location = new System.Drawing.Point(167, 21);
            this.txtToolName.Name = "txtToolName";
            this.txtToolName.Size = new System.Drawing.Size(341, 20);
            this.txtToolName.TabIndex = 9;
            this.txtToolName.Leave += new System.EventHandler(this.NameTextbox_Leave);
            // 
            // directoryPathLbl
            // 
            this.directoryPathLbl.AutoSize = true;
            this.directoryPathLbl.Location = new System.Drawing.Point(167, 44);
            this.directoryPathLbl.Name = "directoryPathLbl";
            this.directoryPathLbl.Size = new System.Drawing.Size(73, 13);
            this.directoryPathLbl.TabIndex = 8;
            this.directoryPathLbl.Text = "File Location: ";
            // 
            // LblToolName
            // 
            this.LblToolName.AutoSize = true;
            this.LblToolName.Location = new System.Drawing.Point(167, 5);
            this.LblToolName.Name = "LblToolName";
            this.LblToolName.Size = new System.Drawing.Size(41, 13);
            this.LblToolName.TabIndex = 7;
            this.LblToolName.Text = "Name: ";
            // 
            // LblEnvName
            // 
            this.LblEnvName.AutoSize = true;
            this.LblEnvName.Location = new System.Drawing.Point(167, 5);
            this.LblEnvName.Name = "LblEnvName";
            this.LblEnvName.Size = new System.Drawing.Size(41, 13);
            this.LblEnvName.TabIndex = 1;
            this.LblEnvName.Text = "Name: ";
            // 
            // txtEnvRegistryValue
            // 
            this.txtEnvRegistryValue.Location = new System.Drawing.Point(167, 60);
            this.txtEnvRegistryValue.Name = "txtEnvRegistryValue";
            this.txtEnvRegistryValue.Size = new System.Drawing.Size(266, 20);
            this.txtEnvRegistryValue.TabIndex = 4;
            // 
            // txtEnvName
            // 
            this.txtEnvName.Location = new System.Drawing.Point(167, 21);
            this.txtEnvName.Name = "txtEnvName";
            this.txtEnvName.Size = new System.Drawing.Size(341, 20);
            this.txtEnvName.TabIndex = 3;
            this.txtEnvName.Leave += new System.EventHandler(this.NameTextbox_Leave);
            // 
            // lstEnvAllEnvironments
            // 
            this.lstEnvAllEnvironments.FormattingEnabled = true;
            this.lstEnvAllEnvironments.Location = new System.Drawing.Point(7, 21);
            this.lstEnvAllEnvironments.Name = "lstEnvAllEnvironments";
            this.lstEnvAllEnvironments.Size = new System.Drawing.Size(154, 212);
            this.lstEnvAllEnvironments.TabIndex = 6;
            this.lstEnvAllEnvironments.SelectedIndexChanged += new System.EventHandler(this.LstEnvAllEnvironmentsSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Environments:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabEnvironments);
            this.tabControl.Controls.Add(this.tabTools);
            this.tabControl.ItemSize = new System.Drawing.Size(76, 18);
            this.tabControl.Location = new System.Drawing.Point(2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(525, 265);
            this.tabControl.TabIndex = 12;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.checkGeneralShowBalloonTips);
            this.tabGeneral.Controls.Add(this.groupGeneralIconProperties);
            this.tabGeneral.Controls.Add(this.groupGeneralGlobalHotkey);
            this.tabGeneral.Controls.Add(this.groupGeneralUpdateRegistryKey);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(517, 239);
            this.tabGeneral.TabIndex = 2;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // checkGeneralShowBalloonTips
            // 
            this.checkGeneralShowBalloonTips.AutoSize = true;
            this.checkGeneralShowBalloonTips.Location = new System.Drawing.Point(355, 138);
            this.checkGeneralShowBalloonTips.Name = "checkGeneralShowBalloonTips";
            this.checkGeneralShowBalloonTips.Size = new System.Drawing.Size(114, 17);
            this.checkGeneralShowBalloonTips.TabIndex = 2;
            this.checkGeneralShowBalloonTips.Text = "Show Balloon Tips";
            this.checkGeneralShowBalloonTips.UseVisualStyleBackColor = true;
            // 
            // groupGeneralIconProperties
            // 
            this.groupGeneralIconProperties.Controls.Add(this.upDownGeneralIconSize);
            this.groupGeneralIconProperties.Controls.Add(this.comboGeneralIconTextColor);
            this.groupGeneralIconProperties.Controls.Add(this.comboGeneralIconColor);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconTextColor);
            this.groupGeneralIconProperties.Controls.Add(this.picGeneralIconSample);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconSample);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconSampleText);
            this.groupGeneralIconProperties.Controls.Add(this.txtGeneralIconSampleText);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconColor);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconSize);
            this.groupGeneralIconProperties.Controls.Add(this.lblGeneralIconFont);
            this.groupGeneralIconProperties.Controls.Add(this.comboGeneralIconFont);
            this.groupGeneralIconProperties.Location = new System.Drawing.Point(6, 3);
            this.groupGeneralIconProperties.Name = "groupGeneralIconProperties";
            this.groupGeneralIconProperties.Size = new System.Drawing.Size(164, 230);
            this.groupGeneralIconProperties.TabIndex = 19;
            this.groupGeneralIconProperties.TabStop = false;
            this.groupGeneralIconProperties.Text = "Update Icon Properties";
            // 
            // upDownGeneralIconSize
            // 
            this.upDownGeneralIconSize.DecimalPlaces = 2;
            this.upDownGeneralIconSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.upDownGeneralIconSize.Location = new System.Drawing.Point(6, 72);
            this.upDownGeneralIconSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.upDownGeneralIconSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownGeneralIconSize.Name = "upDownGeneralIconSize";
            this.upDownGeneralIconSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.upDownGeneralIconSize.Size = new System.Drawing.Size(78, 20);
            this.upDownGeneralIconSize.TabIndex = 18;
            this.upDownGeneralIconSize.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.upDownGeneralIconSize.ValueChanged += new System.EventHandler(this.UpdateSample);
            // 
            // comboGeneralIconTextColor
            // 
            this.comboGeneralIconTextColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboGeneralIconTextColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGeneralIconTextColor.FormattingEnabled = true;
            this.comboGeneralIconTextColor.Location = new System.Drawing.Point(6, 111);
            this.comboGeneralIconTextColor.Name = "comboGeneralIconTextColor";
            this.comboGeneralIconTextColor.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralIconTextColor.TabIndex = 30;
            this.comboGeneralIconTextColor.SelectedIndexChanged += new System.EventHandler(this.UpdateSample);
            // 
            // comboGeneralIconColor
            // 
            this.comboGeneralIconColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboGeneralIconColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGeneralIconColor.FormattingEnabled = true;
            this.comboGeneralIconColor.Location = new System.Drawing.Point(6, 151);
            this.comboGeneralIconColor.Name = "comboGeneralIconColor";
            this.comboGeneralIconColor.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralIconColor.TabIndex = 29;
            this.comboGeneralIconColor.SelectedIndexChanged += new System.EventHandler(this.UpdateSample);
            // 
            // lblGeneralIconTextColor
            // 
            this.lblGeneralIconTextColor.AutoSize = true;
            this.lblGeneralIconTextColor.Location = new System.Drawing.Point(6, 95);
            this.lblGeneralIconTextColor.Name = "lblGeneralIconTextColor";
            this.lblGeneralIconTextColor.Size = new System.Drawing.Size(96, 13);
            this.lblGeneralIconTextColor.TabIndex = 28;
            this.lblGeneralIconTextColor.Text = "Sample Text Color:";
            this.lblGeneralIconTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picGeneralIconSample
            // 
            this.picGeneralIconSample.Location = new System.Drawing.Point(20, 191);
            this.picGeneralIconSample.Name = "picGeneralIconSample";
            this.picGeneralIconSample.Size = new System.Drawing.Size(32, 32);
            this.picGeneralIconSample.TabIndex = 23;
            this.picGeneralIconSample.TabStop = false;
            // 
            // lblGeneralIconSample
            // 
            this.lblGeneralIconSample.AutoSize = true;
            this.lblGeneralIconSample.Location = new System.Drawing.Point(6, 175);
            this.lblGeneralIconSample.Name = "lblGeneralIconSample";
            this.lblGeneralIconSample.Size = new System.Drawing.Size(69, 13);
            this.lblGeneralIconSample.TabIndex = 22;
            this.lblGeneralIconSample.Text = "Sample Icon:";
            // 
            // lblGeneralIconSampleText
            // 
            this.lblGeneralIconSampleText.AutoSize = true;
            this.lblGeneralIconSampleText.Location = new System.Drawing.Point(89, 56);
            this.lblGeneralIconSampleText.Name = "lblGeneralIconSampleText";
            this.lblGeneralIconSampleText.Size = new System.Drawing.Size(69, 13);
            this.lblGeneralIconSampleText.TabIndex = 25;
            this.lblGeneralIconSampleText.Text = "Sample Text:";
            // 
            // txtGeneralIconSampleText
            // 
            this.txtGeneralIconSampleText.Location = new System.Drawing.Point(92, 72);
            this.txtGeneralIconSampleText.Name = "txtGeneralIconSampleText";
            this.txtGeneralIconSampleText.Size = new System.Drawing.Size(66, 20);
            this.txtGeneralIconSampleText.TabIndex = 24;
            this.txtGeneralIconSampleText.Text = "abc";
            this.txtGeneralIconSampleText.TextChanged += new System.EventHandler(this.UpdateSample);
            // 
            // lblGeneralIconColor
            // 
            this.lblGeneralIconColor.AutoSize = true;
            this.lblGeneralIconColor.Location = new System.Drawing.Point(6, 135);
            this.lblGeneralIconColor.Name = "lblGeneralIconColor";
            this.lblGeneralIconColor.Size = new System.Drawing.Size(133, 13);
            this.lblGeneralIconColor.TabIndex = 21;
            this.lblGeneralIconColor.Text = "Sample Background Color:";
            this.lblGeneralIconColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGeneralIconSize
            // 
            this.lblGeneralIconSize.AutoSize = true;
            this.lblGeneralIconSize.Location = new System.Drawing.Point(6, 56);
            this.lblGeneralIconSize.Name = "lblGeneralIconSize";
            this.lblGeneralIconSize.Size = new System.Drawing.Size(78, 13);
            this.lblGeneralIconSize.TabIndex = 20;
            this.lblGeneralIconSize.Text = "Icon Text Size:";
            // 
            // lblGeneralIconFont
            // 
            this.lblGeneralIconFont.AutoSize = true;
            this.lblGeneralIconFont.Location = new System.Drawing.Point(6, 16);
            this.lblGeneralIconFont.Name = "lblGeneralIconFont";
            this.lblGeneralIconFont.Size = new System.Drawing.Size(79, 13);
            this.lblGeneralIconFont.TabIndex = 19;
            this.lblGeneralIconFont.Text = "Icon Text Font:";
            // 
            // comboGeneralIconFont
            // 
            this.comboGeneralIconFont.FormattingEnabled = true;
            this.comboGeneralIconFont.Location = new System.Drawing.Point(6, 32);
            this.comboGeneralIconFont.Name = "comboGeneralIconFont";
            this.comboGeneralIconFont.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralIconFont.TabIndex = 16;
            this.comboGeneralIconFont.SelectedIndexChanged += new System.EventHandler(this.UpdateSample);
            // 
            // groupGeneralGlobalHotkey
            // 
            this.groupGeneralGlobalHotkey.Controls.Add(this.lblGeneralModifierKeys);
            this.groupGeneralGlobalHotkey.Controls.Add(this.lblGeneralGlobalHotkey);
            this.groupGeneralGlobalHotkey.Controls.Add(this.comboGeneralSecondModifierKey);
            this.groupGeneralGlobalHotkey.Controls.Add(this.comboGeneralFirstModifierKey);
            this.groupGeneralGlobalHotkey.Controls.Add(this.comboGeneralGlobalHotkey);
            this.groupGeneralGlobalHotkey.Location = new System.Drawing.Point(346, 3);
            this.groupGeneralGlobalHotkey.Name = "groupGeneralGlobalHotkey";
            this.groupGeneralGlobalHotkey.Size = new System.Drawing.Size(164, 127);
            this.groupGeneralGlobalHotkey.TabIndex = 1;
            this.groupGeneralGlobalHotkey.TabStop = false;
            this.groupGeneralGlobalHotkey.Text = "Update Global Hotkey";
            // 
            // lblGeneralModifierKeys
            // 
            this.lblGeneralModifierKeys.AutoSize = true;
            this.lblGeneralModifierKeys.Location = new System.Drawing.Point(6, 56);
            this.lblGeneralModifierKeys.Name = "lblGeneralModifierKeys";
            this.lblGeneralModifierKeys.Size = new System.Drawing.Size(121, 13);
            this.lblGeneralModifierKeys.TabIndex = 22;
            this.lblGeneralModifierKeys.Text = "Optional Modifier Key(s):";
            // 
            // lblGeneralGlobalHotkey
            // 
            this.lblGeneralGlobalHotkey.AutoSize = true;
            this.lblGeneralGlobalHotkey.Location = new System.Drawing.Point(6, 16);
            this.lblGeneralGlobalHotkey.Name = "lblGeneralGlobalHotkey";
            this.lblGeneralGlobalHotkey.Size = new System.Drawing.Size(44, 13);
            this.lblGeneralGlobalHotkey.TabIndex = 21;
            this.lblGeneralGlobalHotkey.Text = "Hotkey:";
            // 
            // comboGeneralSecondModifierKey
            // 
            this.comboGeneralSecondModifierKey.FormattingEnabled = true;
            this.comboGeneralSecondModifierKey.Location = new System.Drawing.Point(6, 99);
            this.comboGeneralSecondModifierKey.Name = "comboGeneralSecondModifierKey";
            this.comboGeneralSecondModifierKey.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralSecondModifierKey.TabIndex = 18;
            // 
            // comboGeneralFirstModifierKey
            // 
            this.comboGeneralFirstModifierKey.FormattingEnabled = true;
            this.comboGeneralFirstModifierKey.Location = new System.Drawing.Point(6, 72);
            this.comboGeneralFirstModifierKey.Name = "comboGeneralFirstModifierKey";
            this.comboGeneralFirstModifierKey.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralFirstModifierKey.TabIndex = 17;
            // 
            // comboGeneralGlobalHotkey
            // 
            this.comboGeneralGlobalHotkey.FormattingEnabled = true;
            this.comboGeneralGlobalHotkey.Location = new System.Drawing.Point(6, 32);
            this.comboGeneralGlobalHotkey.Name = "comboGeneralGlobalHotkey";
            this.comboGeneralGlobalHotkey.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralGlobalHotkey.TabIndex = 16;
            // 
            // groupGeneralUpdateRegistryKey
            // 
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.lblGeneralRegistryKeyField);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.lblGeneralRegistryKeyRoots);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.lblGeneralRegistryKeyRoot);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.btnGeneralCheckRegistryKey);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.txtGeneralRegistryKeyField);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.comboGeneralRegistryKeyRoot3);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.comboGeneralRegistryKeyRoot2);
            this.groupGeneralUpdateRegistryKey.Controls.Add(this.comboGeneralRegistryKeyRoot);
            this.groupGeneralUpdateRegistryKey.Location = new System.Drawing.Point(176, 3);
            this.groupGeneralUpdateRegistryKey.Name = "groupGeneralUpdateRegistryKey";
            this.groupGeneralUpdateRegistryKey.Size = new System.Drawing.Size(164, 194);
            this.groupGeneralUpdateRegistryKey.TabIndex = 0;
            this.groupGeneralUpdateRegistryKey.TabStop = false;
            this.groupGeneralUpdateRegistryKey.Text = "Update Registry Key";
            // 
            // lblGeneralRegistryKeyField
            // 
            this.lblGeneralRegistryKeyField.AutoSize = true;
            this.lblGeneralRegistryKeyField.Location = new System.Drawing.Point(6, 123);
            this.lblGeneralRegistryKeyField.Name = "lblGeneralRegistryKeyField";
            this.lblGeneralRegistryKeyField.Size = new System.Drawing.Size(100, 13);
            this.lblGeneralRegistryKeyField.TabIndex = 22;
            this.lblGeneralRegistryKeyField.Text = "Registry Key Name:";
            // 
            // lblGeneralRegistryKeyRoots
            // 
            this.lblGeneralRegistryKeyRoots.AutoSize = true;
            this.lblGeneralRegistryKeyRoots.Location = new System.Drawing.Point(6, 56);
            this.lblGeneralRegistryKeyRoots.Name = "lblGeneralRegistryKeyRoots";
            this.lblGeneralRegistryKeyRoots.Size = new System.Drawing.Size(107, 13);
            this.lblGeneralRegistryKeyRoots.TabIndex = 21;
            this.lblGeneralRegistryKeyRoots.Text = "Registry Sub Root(s):";
            // 
            // lblGeneralRegistryKeyRoot
            // 
            this.lblGeneralRegistryKeyRoot.AutoSize = true;
            this.lblGeneralRegistryKeyRoot.Location = new System.Drawing.Point(6, 16);
            this.lblGeneralRegistryKeyRoot.Name = "lblGeneralRegistryKeyRoot";
            this.lblGeneralRegistryKeyRoot.Size = new System.Drawing.Size(74, 13);
            this.lblGeneralRegistryKeyRoot.TabIndex = 20;
            this.lblGeneralRegistryKeyRoot.Text = "Registry Root:";
            // 
            // btnGeneralCheckRegistryKey
            // 
            this.btnGeneralCheckRegistryKey.Location = new System.Drawing.Point(6, 165);
            this.btnGeneralCheckRegistryKey.Name = "btnGeneralCheckRegistryKey";
            this.btnGeneralCheckRegistryKey.Size = new System.Drawing.Size(152, 23);
            this.btnGeneralCheckRegistryKey.TabIndex = 17;
            this.btnGeneralCheckRegistryKey.Text = "Check Key";
            this.btnGeneralCheckRegistryKey.UseVisualStyleBackColor = true;
            this.btnGeneralCheckRegistryKey.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // txtGeneralRegistryKeyField
            // 
            this.txtGeneralRegistryKeyField.Location = new System.Drawing.Point(6, 139);
            this.txtGeneralRegistryKeyField.Name = "txtGeneralRegistryKeyField";
            this.txtGeneralRegistryKeyField.Size = new System.Drawing.Size(152, 20);
            this.txtGeneralRegistryKeyField.TabIndex = 16;
            // 
            // comboGeneralRegistryKeyRoot3
            // 
            this.comboGeneralRegistryKeyRoot3.FormattingEnabled = true;
            this.comboGeneralRegistryKeyRoot3.Location = new System.Drawing.Point(6, 99);
            this.comboGeneralRegistryKeyRoot3.Name = "comboGeneralRegistryKeyRoot3";
            this.comboGeneralRegistryKeyRoot3.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralRegistryKeyRoot3.TabIndex = 15;
            // 
            // comboGeneralRegistryKeyRoot2
            // 
            this.comboGeneralRegistryKeyRoot2.FormattingEnabled = true;
            this.comboGeneralRegistryKeyRoot2.Location = new System.Drawing.Point(6, 72);
            this.comboGeneralRegistryKeyRoot2.Name = "comboGeneralRegistryKeyRoot2";
            this.comboGeneralRegistryKeyRoot2.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralRegistryKeyRoot2.TabIndex = 14;
            this.comboGeneralRegistryKeyRoot2.SelectedIndexChanged += new System.EventHandler(this.RootCombo2_SelectedIndexChanged);
            // 
            // comboGeneralRegistryKeyRoot
            // 
            this.comboGeneralRegistryKeyRoot.FormattingEnabled = true;
            this.comboGeneralRegistryKeyRoot.Location = new System.Drawing.Point(6, 32);
            this.comboGeneralRegistryKeyRoot.Name = "comboGeneralRegistryKeyRoot";
            this.comboGeneralRegistryKeyRoot.Size = new System.Drawing.Size(152, 21);
            this.comboGeneralRegistryKeyRoot.TabIndex = 13;
            this.comboGeneralRegistryKeyRoot.SelectedIndexChanged += new System.EventHandler(this.RootCombo_SelectedIndexChanged);
            // 
            // tabEnvironments
            // 
            this.tabEnvironments.Controls.Add(this.checkEnvDisplayOnMenu);
            this.tabEnvironments.Controls.Add(this.pnlEnvIcon);
            this.tabEnvironments.Controls.Add(this.comboEnvHotkey);
            this.tabEnvironments.Controls.Add(this.hotkeyLabel);
            this.tabEnvironments.Controls.Add(this.lblEnvCurrentEnvironmentGuid);
            this.tabEnvironments.Controls.Add(this.lstEnvAllEnvironments);
            this.tabEnvironments.Controls.Add(this.label1);
            this.tabEnvironments.Controls.Add(this.txtEnvRegistryValue);
            this.tabEnvironments.Controls.Add(this.txtEnvName);
            this.tabEnvironments.Controls.Add(this.registryValueLbl);
            this.tabEnvironments.Controls.Add(this.LblEnvName);
            this.tabEnvironments.Location = new System.Drawing.Point(4, 22);
            this.tabEnvironments.Name = "tabEnvironments";
            this.tabEnvironments.Padding = new System.Windows.Forms.Padding(3);
            this.tabEnvironments.Size = new System.Drawing.Size(517, 239);
            this.tabEnvironments.TabIndex = 0;
            this.tabEnvironments.Text = "Environments";
            this.tabEnvironments.UseVisualStyleBackColor = true;
            // 
            // checkEnvDisplayOnMenu
            // 
            this.checkEnvDisplayOnMenu.AutoSize = true;
            this.checkEnvDisplayOnMenu.Location = new System.Drawing.Point(167, 86);
            this.checkEnvDisplayOnMenu.Name = "checkEnvDisplayOnMenu";
            this.checkEnvDisplayOnMenu.Size = new System.Drawing.Size(169, 17);
            this.checkEnvDisplayOnMenu.TabIndex = 22;
            this.checkEnvDisplayOnMenu.Text = "Display Environment On Menu";
            this.checkEnvDisplayOnMenu.UseVisualStyleBackColor = true;
            // 
            // pnlEnvIcon
            // 
            this.pnlEnvIcon.Controls.Add(this.pnlEnvIconFileLocation);
            this.pnlEnvIcon.Controls.Add(this.pnlEnvDynamicIcon);
            this.pnlEnvIcon.Controls.Add(this.pnlEnvIconType);
            this.pnlEnvIcon.Location = new System.Drawing.Point(163, 104);
            this.pnlEnvIcon.Name = "pnlEnvIcon";
            this.pnlEnvIcon.Size = new System.Drawing.Size(350, 212);
            this.pnlEnvIcon.TabIndex = 21;
            // 
            // pnlEnvIconFileLocation
            // 
            this.pnlEnvIconFileLocation.Controls.Add(this.picEnvSampleIcon);
            this.pnlEnvIconFileLocation.Controls.Add(this.lblEnvSampleIcon);
            this.pnlEnvIconFileLocation.Controls.Add(this.btnEnvIconFileLocation);
            this.pnlEnvIconFileLocation.Controls.Add(this.txtEnvIconFileLocation);
            this.pnlEnvIconFileLocation.Controls.Add(this.lblEnvIconFileLocation);
            this.pnlEnvIconFileLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEnvIconFileLocation.Location = new System.Drawing.Point(0, 106);
            this.pnlEnvIconFileLocation.Name = "pnlEnvIconFileLocation";
            this.pnlEnvIconFileLocation.Size = new System.Drawing.Size(350, 83);
            this.pnlEnvIconFileLocation.TabIndex = 2;
            // 
            // picEnvSampleIcon
            // 
            this.picEnvSampleIcon.Location = new System.Drawing.Point(78, 45);
            this.picEnvSampleIcon.Name = "picEnvSampleIcon";
            this.picEnvSampleIcon.Size = new System.Drawing.Size(32, 32);
            this.picEnvSampleIcon.TabIndex = 27;
            this.picEnvSampleIcon.TabStop = false;
            // 
            // lblEnvSampleIcon
            // 
            this.lblEnvSampleIcon.AutoSize = true;
            this.lblEnvSampleIcon.Location = new System.Drawing.Point(3, 53);
            this.lblEnvSampleIcon.Name = "lblEnvSampleIcon";
            this.lblEnvSampleIcon.Size = new System.Drawing.Size(69, 13);
            this.lblEnvSampleIcon.TabIndex = 26;
            this.lblEnvSampleIcon.Text = "Sample Icon:";
            // 
            // btnEnvIconFileLocation
            // 
            this.btnEnvIconFileLocation.Image = global::RegistryMonitor.Properties.Resources.folder_add16;
            this.btnEnvIconFileLocation.Location = new System.Drawing.Point(321, 19);
            this.btnEnvIconFileLocation.Name = "btnEnvIconFileLocation";
            this.btnEnvIconFileLocation.Size = new System.Drawing.Size(23, 20);
            this.btnEnvIconFileLocation.TabIndex = 25;
            this.btnEnvIconFileLocation.UseVisualStyleBackColor = true;
            this.btnEnvIconFileLocation.Click += new System.EventHandler(this.btnEnvIconFileLocation_Clicked);
            // 
            // txtEnvIconFileLocation
            // 
            this.txtEnvIconFileLocation.Location = new System.Drawing.Point(3, 19);
            this.txtEnvIconFileLocation.Name = "txtEnvIconFileLocation";
            this.txtEnvIconFileLocation.Size = new System.Drawing.Size(315, 20);
            this.txtEnvIconFileLocation.TabIndex = 24;
            this.txtEnvIconFileLocation.TextChanged += new System.EventHandler(this.UpdateLoadedSampleIcon);
            // 
            // lblEnvIconFileLocation
            // 
            this.lblEnvIconFileLocation.AutoSize = true;
            this.lblEnvIconFileLocation.Location = new System.Drawing.Point(3, 3);
            this.lblEnvIconFileLocation.Name = "lblEnvIconFileLocation";
            this.lblEnvIconFileLocation.Size = new System.Drawing.Size(97, 13);
            this.lblEnvIconFileLocation.TabIndex = 23;
            this.lblEnvIconFileLocation.Text = "Icon File Location: ";
            // 
            // pnlEnvDynamicIcon
            // 
            this.pnlEnvDynamicIcon.Controls.Add(this.iconTextLabel);
            this.pnlEnvDynamicIcon.Controls.Add(this.comboEnvIconTextColor);
            this.pnlEnvDynamicIcon.Controls.Add(this.txtEnvIconDisplayText);
            this.pnlEnvDynamicIcon.Controls.Add(this.iconTextColorLabel);
            this.pnlEnvDynamicIcon.Controls.Add(this.iconColorLabel);
            this.pnlEnvDynamicIcon.Controls.Add(this.comboEnvIconBackgroundColor);
            this.pnlEnvDynamicIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEnvDynamicIcon.Location = new System.Drawing.Point(0, 23);
            this.pnlEnvDynamicIcon.Name = "pnlEnvDynamicIcon";
            this.pnlEnvDynamicIcon.Size = new System.Drawing.Size(350, 83);
            this.pnlEnvDynamicIcon.TabIndex = 1;
            // 
            // iconTextLabel
            // 
            this.iconTextLabel.AutoSize = true;
            this.iconTextLabel.Location = new System.Drawing.Point(3, 3);
            this.iconTextLabel.Name = "iconTextLabel";
            this.iconTextLabel.Size = new System.Drawing.Size(95, 13);
            this.iconTextLabel.TabIndex = 10;
            this.iconTextLabel.Text = "Icon Display Text: ";
            // 
            // comboEnvIconTextColor
            // 
            this.comboEnvIconTextColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboEnvIconTextColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEnvIconTextColor.FormattingEnabled = true;
            this.comboEnvIconTextColor.Location = new System.Drawing.Point(3, 58);
            this.comboEnvIconTextColor.Name = "comboEnvIconTextColor";
            this.comboEnvIconTextColor.Size = new System.Drawing.Size(168, 21);
            this.comboEnvIconTextColor.TabIndex = 20;
            // 
            // txtEnvIconDisplayText
            // 
            this.txtEnvIconDisplayText.Location = new System.Drawing.Point(3, 19);
            this.txtEnvIconDisplayText.MaxLength = 3;
            this.txtEnvIconDisplayText.Name = "txtEnvIconDisplayText";
            this.txtEnvIconDisplayText.Size = new System.Drawing.Size(341, 20);
            this.txtEnvIconDisplayText.TabIndex = 11;
            // 
            // iconTextColorLabel
            // 
            this.iconTextColorLabel.AutoSize = true;
            this.iconTextColorLabel.Location = new System.Drawing.Point(3, 42);
            this.iconTextColorLabel.Name = "iconTextColorLabel";
            this.iconTextColorLabel.Size = new System.Drawing.Size(85, 13);
            this.iconTextColorLabel.TabIndex = 19;
            this.iconTextColorLabel.Text = "Icon Text Color: ";
            // 
            // iconColorLabel
            // 
            this.iconColorLabel.AutoSize = true;
            this.iconColorLabel.Location = new System.Drawing.Point(176, 42);
            this.iconColorLabel.Name = "iconColorLabel";
            this.iconColorLabel.Size = new System.Drawing.Size(122, 13);
            this.iconColorLabel.TabIndex = 12;
            this.iconColorLabel.Text = "Icon Background Color: ";
            // 
            // comboEnvIconBackgroundColor
            // 
            this.comboEnvIconBackgroundColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboEnvIconBackgroundColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEnvIconBackgroundColor.FormattingEnabled = true;
            this.comboEnvIconBackgroundColor.Location = new System.Drawing.Point(176, 58);
            this.comboEnvIconBackgroundColor.Name = "comboEnvIconBackgroundColor";
            this.comboEnvIconBackgroundColor.Size = new System.Drawing.Size(168, 21);
            this.comboEnvIconBackgroundColor.TabIndex = 18;
            // 
            // pnlEnvIconType
            // 
            this.pnlEnvIconType.Controls.Add(this.radioEnvIconFromFile);
            this.pnlEnvIconType.Controls.Add(this.radioEnvDynamicIcon);
            this.pnlEnvIconType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEnvIconType.Location = new System.Drawing.Point(0, 0);
            this.pnlEnvIconType.Name = "pnlEnvIconType";
            this.pnlEnvIconType.Size = new System.Drawing.Size(350, 23);
            this.pnlEnvIconType.TabIndex = 0;
            // 
            // radioEnvIconFromFile
            // 
            this.radioEnvIconFromFile.AutoSize = true;
            this.radioEnvIconFromFile.Location = new System.Drawing.Point(135, 3);
            this.radioEnvIconFromFile.Name = "radioEnvIconFromFile";
            this.radioEnvIconFromFile.Size = new System.Drawing.Size(118, 17);
            this.radioEnvIconFromFile.TabIndex = 1;
            this.radioEnvIconFromFile.TabStop = true;
            this.radioEnvIconFromFile.Text = "Load Icon From File";
            this.radioEnvIconFromFile.UseVisualStyleBackColor = true;
            this.radioEnvIconFromFile.CheckedChanged += new System.EventHandler(this.iconRadioButtons_CheckChanged);
            // 
            // radioEnvDynamicIcon
            // 
            this.radioEnvDynamicIcon.AutoSize = true;
            this.radioEnvDynamicIcon.Location = new System.Drawing.Point(5, 3);
            this.radioEnvDynamicIcon.Name = "radioEnvDynamicIcon";
            this.radioEnvDynamicIcon.Size = new System.Drawing.Size(124, 17);
            this.radioEnvDynamicIcon.TabIndex = 0;
            this.radioEnvDynamicIcon.TabStop = true;
            this.radioEnvDynamicIcon.Text = "Create Dynamic Icon";
            this.radioEnvDynamicIcon.UseVisualStyleBackColor = true;
            this.radioEnvDynamicIcon.CheckedChanged += new System.EventHandler(this.iconRadioButtons_CheckChanged);
            // 
            // comboEnvHotkey
            // 
            this.comboEnvHotkey.FormattingEnabled = true;
            this.comboEnvHotkey.Location = new System.Drawing.Point(439, 60);
            this.comboEnvHotkey.Name = "comboEnvHotkey";
            this.comboEnvHotkey.Size = new System.Drawing.Size(68, 21);
            this.comboEnvHotkey.TabIndex = 18;
            // 
            // hotkeyLabel
            // 
            this.hotkeyLabel.AutoSize = true;
            this.hotkeyLabel.Location = new System.Drawing.Point(439, 44);
            this.hotkeyLabel.Name = "hotkeyLabel";
            this.hotkeyLabel.Size = new System.Drawing.Size(47, 13);
            this.hotkeyLabel.TabIndex = 8;
            this.hotkeyLabel.Text = "Hotkey: ";
            // 
            // lblEnvCurrentEnvironmentGuid
            // 
            this.lblEnvCurrentEnvironmentGuid.AutoSize = true;
            this.lblEnvCurrentEnvironmentGuid.Location = new System.Drawing.Point(214, 5);
            this.lblEnvCurrentEnvironmentGuid.Name = "lblEnvCurrentEnvironmentGuid";
            this.lblEnvCurrentEnvironmentGuid.Size = new System.Drawing.Size(18, 13);
            this.lblEnvCurrentEnvironmentGuid.TabIndex = 7;
            this.lblEnvCurrentEnvironmentGuid.Text = "ID";
            this.lblEnvCurrentEnvironmentGuid.Visible = false;
            // 
            // registryValueLbl
            // 
            this.registryValueLbl.AutoSize = true;
            this.registryValueLbl.Location = new System.Drawing.Point(167, 44);
            this.registryValueLbl.Name = "registryValueLbl";
            this.registryValueLbl.Size = new System.Drawing.Size(81, 13);
            this.registryValueLbl.TabIndex = 2;
            this.registryValueLbl.Text = "Registry Value: ";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 297);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabTools.ResumeLayout(false);
            this.tabTools.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupGeneralIconProperties.ResumeLayout(false);
            this.groupGeneralIconProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownGeneralIconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneralIconSample)).EndInit();
            this.groupGeneralGlobalHotkey.ResumeLayout(false);
            this.groupGeneralGlobalHotkey.PerformLayout();
            this.groupGeneralUpdateRegistryKey.ResumeLayout(false);
            this.groupGeneralUpdateRegistryKey.PerformLayout();
            this.tabEnvironments.ResumeLayout(false);
            this.tabEnvironments.PerformLayout();
            this.pnlEnvIcon.ResumeLayout(false);
            this.pnlEnvIconFileLocation.ResumeLayout(false);
            this.pnlEnvIconFileLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnvSampleIcon)).EndInit();
            this.pnlEnvDynamicIcon.ResumeLayout(false);
            this.pnlEnvDynamicIcon.PerformLayout();
            this.pnlEnvIconType.ResumeLayout(false);
            this.pnlEnvIconType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.ListBox lstToolAllTools;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToolFileLocation;
        private System.Windows.Forms.TextBox txtToolName;
        private System.Windows.Forms.Label directoryPathLbl;
        private System.Windows.Forms.Label LblToolName;
        private System.Windows.Forms.Label LblEnvName;
        private System.Windows.Forms.TextBox txtEnvRegistryValue;
        private System.Windows.Forms.TextBox txtEnvName;
        private System.Windows.Forms.ListBox lstEnvAllEnvironments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEnvironments;
        private System.Windows.Forms.Label registryValueLbl;
        private System.Windows.Forms.Label lblEnvCurrentEnvironmentGuid;
        private System.Windows.Forms.ComboBox comboEnvHotkey;
        private System.Windows.Forms.Label iconColorLabel;
        private System.Windows.Forms.TextBox txtEnvIconDisplayText;
        private System.Windows.Forms.Label iconTextLabel;
        private System.Windows.Forms.Label hotkeyLabel;
        private ColorComboBox comboEnvIconBackgroundColor;
        private System.Windows.Forms.Label lblToolCurrentToolGuid;
        private System.Windows.Forms.ComboBox comboToolHotkey;
        private System.Windows.Forms.Label lblToolHotkey;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupGeneralUpdateRegistryKey;
        private System.Windows.Forms.Button btnGeneralCheckRegistryKey;
        private System.Windows.Forms.TextBox txtGeneralRegistryKeyField;
        private System.Windows.Forms.ComboBox comboGeneralRegistryKeyRoot3;
        private System.Windows.Forms.ComboBox comboGeneralRegistryKeyRoot2;
        private System.Windows.Forms.ComboBox comboGeneralRegistryKeyRoot;
        private System.Windows.Forms.GroupBox groupGeneralGlobalHotkey;
        private System.Windows.Forms.ComboBox comboGeneralSecondModifierKey;
        private System.Windows.Forms.ComboBox comboGeneralFirstModifierKey;
        private System.Windows.Forms.ComboBox comboGeneralGlobalHotkey;
        private System.Windows.Forms.CheckBox checkGeneralShowBalloonTips;
        private System.Windows.Forms.GroupBox groupGeneralIconProperties;
        private System.Windows.Forms.PictureBox picGeneralIconSample;
        private System.Windows.Forms.Label lblGeneralIconSample;
        private System.Windows.Forms.Label lblGeneralIconColor;
        private System.Windows.Forms.Label lblGeneralIconSize;
        private System.Windows.Forms.Label lblGeneralIconFont;
        private System.Windows.Forms.ComboBox comboGeneralIconFont;
        private System.Windows.Forms.Label lblGeneralIconSampleText;
        private System.Windows.Forms.TextBox txtGeneralIconSampleText;
        private System.Windows.Forms.Label lblGeneralIconTextColor;
        private ColorComboBox comboGeneralIconTextColor;
        private ColorComboBox comboGeneralIconColor;
        private System.Windows.Forms.NumericUpDown upDownGeneralIconSize;
        private System.Windows.Forms.Label lblGeneralRegistryKeyField;
        private System.Windows.Forms.Label lblGeneralRegistryKeyRoots;
        private System.Windows.Forms.Label lblGeneralRegistryKeyRoot;
        private System.Windows.Forms.Label lblGeneralModifierKeys;
        private System.Windows.Forms.Label lblGeneralGlobalHotkey;
        private ColorComboBox comboEnvIconTextColor;
        private System.Windows.Forms.Label iconTextColorLabel;
        private System.Windows.Forms.Button btnToolFileLocation;
        private System.Windows.Forms.Panel pnlEnvIcon;
        private System.Windows.Forms.Panel pnlEnvDynamicIcon;
        private System.Windows.Forms.Panel pnlEnvIconType;
        private System.Windows.Forms.RadioButton radioEnvIconFromFile;
        private System.Windows.Forms.RadioButton radioEnvDynamicIcon;
        private System.Windows.Forms.Panel pnlEnvIconFileLocation;
        private System.Windows.Forms.Button btnEnvIconFileLocation;
        private System.Windows.Forms.TextBox txtEnvIconFileLocation;
        private System.Windows.Forms.Label lblEnvIconFileLocation;
        private System.Windows.Forms.PictureBox picEnvSampleIcon;
        private System.Windows.Forms.Label lblEnvSampleIcon;
        private System.Windows.Forms.CheckBox checkEnvDisplayOnMenu;
    }
}