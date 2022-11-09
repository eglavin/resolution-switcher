namespace resolution_switcher
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
            this.AddNameAction = new System.Windows.Forms.Button();
            this.NamesLabel = new System.Windows.Forms.Label();
            this.NamesList = new System.Windows.Forms.ListBox();
            this.NameInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AddNameAction
            // 
            this.AddNameAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNameAction.Location = new System.Drawing.Point(815, 171);
            this.AddNameAction.Name = "AddNameAction";
            this.AddNameAction.Size = new System.Drawing.Size(569, 46);
            this.AddNameAction.TabIndex = 0;
            this.AddNameAction.Text = "Add Name";
            this.AddNameAction.UseVisualStyleBackColor = true;
            this.AddNameAction.Click += new System.EventHandler(this.AddName_Click);
            // 
            // NamesLabel
            // 
            this.NamesLabel.AutoSize = true;
            this.NamesLabel.Location = new System.Drawing.Point(60, 33);
            this.NamesLabel.Name = "NamesLabel";
            this.NamesLabel.Size = new System.Drawing.Size(88, 32);
            this.NamesLabel.TabIndex = 1;
            this.NamesLabel.Text = "Names";
            // 
            // NamesList
            // 
            this.NamesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NamesList.FormattingEnabled = true;
            this.NamesList.ItemHeight = 32;
            this.NamesList.Location = new System.Drawing.Point(60, 90);
            this.NamesList.Name = "NamesList";
            this.NamesList.Size = new System.Drawing.Size(698, 644);
            this.NamesList.TabIndex = 2;
            // 
            // NameInput
            // 
            this.NameInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NameInput.Location = new System.Drawing.Point(815, 90);
            this.NameInput.Name = "NameInput";
            this.NameInput.Size = new System.Drawing.Size(569, 39);
            this.NameInput.TabIndex = 3;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 766);
            this.Controls.Add(this.NameInput);
            this.Controls.Add(this.NamesList);
            this.Controls.Add(this.NamesLabel);
            this.Controls.Add(this.AddNameAction);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The best window you\'ve ever seen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button AddNameAction;
        private Label NamesLabel;
        private ListBox NamesList;
        private TextBox NameInput;
    }
}