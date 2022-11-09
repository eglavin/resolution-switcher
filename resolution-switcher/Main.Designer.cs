namespace ResolutionSwitcher
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.ResolutionListBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(30, 30);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(197, 32);
            this.ResolutionLabel.TabIndex = 1;
            this.ResolutionLabel.Text = "Select Resolution";
            // 
            // ResolutionListBox
            // 
            this.ResolutionListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResolutionListBox.FormattingEnabled = true;
            this.ResolutionListBox.ItemHeight = 32;
            this.ResolutionListBox.Items.AddRange(new object[] {
            "1280x720",
            "1920x1080"});
            this.ResolutionListBox.Location = new System.Drawing.Point(30, 101);
            this.ResolutionListBox.Name = "ResolutionListBox";
            this.ResolutionListBox.Size = new System.Drawing.Size(515, 548);
            this.ResolutionListBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(30, 695);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(515, 46);
            this.button1.TabIndex = 3;
            this.button1.Text = "Set Resolution";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetResolution_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 779);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ResolutionListBox);
            this.Controls.Add(this.ResolutionLabel);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resolution Switcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label ResolutionLabel;
        private ListBox ResolutionListBox;
        private Button button1;
    }
}