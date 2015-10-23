namespace gr0ssSysTools
{
    partial class AddRegistryKey
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.textLabel = new System.Windows.Forms.Label();
            this.rootCombo = new System.Windows.Forms.ComboBox();
            this.rootCombo2 = new System.Windows.Forms.ComboBox();
            this.rootCombo3 = new System.Windows.Forms.ComboBox();
            this.fieldTextBox = new System.Windows.Forms.TextBox();
            this.checkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(83, 164);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 164);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(3, 9);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(107, 13);
            this.textLabel.TabIndex = 4;
            this.textLabel.Text = "Registry Key To Use:";
            // 
            // rootCombo
            // 
            this.rootCombo.FormattingEnabled = true;
            this.rootCombo.Location = new System.Drawing.Point(6, 28);
            this.rootCombo.Name = "rootCombo";
            this.rootCombo.Size = new System.Drawing.Size(152, 21);
            this.rootCombo.TabIndex = 8;
            this.rootCombo.SelectedIndexChanged += new System.EventHandler(this.RootCombo_SelectedIndexChanged);
            // 
            // rootCombo2
            // 
            this.rootCombo2.FormattingEnabled = true;
            this.rootCombo2.Location = new System.Drawing.Point(6, 55);
            this.rootCombo2.Name = "rootCombo2";
            this.rootCombo2.Size = new System.Drawing.Size(152, 21);
            this.rootCombo2.TabIndex = 9;
            this.rootCombo2.SelectedIndexChanged += new System.EventHandler(this.RootCombo2_SelectedIndexChanged);
            // 
            // rootCombo3
            // 
            this.rootCombo3.FormattingEnabled = true;
            this.rootCombo3.Location = new System.Drawing.Point(6, 82);
            this.rootCombo3.Name = "rootCombo3";
            this.rootCombo3.Size = new System.Drawing.Size(152, 21);
            this.rootCombo3.TabIndex = 10;
            // 
            // fieldTextBox
            // 
            this.fieldTextBox.Location = new System.Drawing.Point(6, 109);
            this.fieldTextBox.Name = "fieldTextBox";
            this.fieldTextBox.Size = new System.Drawing.Size(152, 20);
            this.fieldTextBox.TabIndex = 11;
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(6, 135);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(152, 23);
            this.checkButton.TabIndex = 12;
            this.checkButton.Text = "Check Key";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // AddRegistryKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(164, 197);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.fieldTextBox);
            this.Controls.Add(this.rootCombo3);
            this.Controls.Add(this.rootCombo2);
            this.Controls.Add(this.rootCombo);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.textLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRegistryKey";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddRegistryKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.ComboBox rootCombo;
        private System.Windows.Forms.ComboBox rootCombo2;
        private System.Windows.Forms.ComboBox rootCombo3;
        private System.Windows.Forms.TextBox fieldTextBox;
        private System.Windows.Forms.Button checkButton;
    }
}