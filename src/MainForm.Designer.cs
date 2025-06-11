namespace Image_Markup_Tool
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.editorPictureBox = new System.Windows.Forms.PictureBox();
            this.layerPanel = new System.Windows.Forms.Panel();
            this.layerPanelLabel = new System.Windows.Forms.Label();
            
            // Create the file menu control
            this.fileMenuControl = new Image_Markup_Tool.Components.FileHandler.FileMenuControl();
            
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.editorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorPictureBox)).BeginInit();
            this.layerPanel.SuspendLayout();
            this.SuspendLayout();
            
          // 
// menuStrip
// 
this.menuStrip.Location = new System.Drawing.Point(0, 0);
this.menuStrip.Name = "menuStrip";
this.menuStrip.Size = new System.Drawing.Size(1024, 24);
this.menuStrip.TabIndex = 0;
this.menuStrip.Text = "menuStrip1";

// Clear any existing items
this.menuStrip.Items.Clear();

// Add the file menu first
this.fileMenuControl.AddToMenuStrip(this.menuStrip);

// Then add the help menu
this.menuStrip.Items.Add(this.helpToolStripMenuItem);
            
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // toolPanel
            // 
            this.toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.toolPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolPanel.Location = new System.Drawing.Point(0, 24);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(70, 715);
            this.toolPanel.TabIndex = 2;
            // 
            // editorPanel
            // 
            this.editorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.editorPanel.Controls.Add(this.editorPictureBox);
            this.editorPanel.Location = new System.Drawing.Point(70, 24);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Size = new System.Drawing.Size(822, 715);
            this.editorPanel.TabIndex = 3;
            // 
            // editorPictureBox
            // 
            this.editorPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.editorPictureBox.Location = new System.Drawing.Point(0, 0);
            this.editorPictureBox.Name = "editorPictureBox";
            this.editorPictureBox.Size = new System.Drawing.Size(822, 715);
            this.editorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.editorPictureBox.TabIndex = 0;
            this.editorPictureBox.TabStop = false;
            // 
            // layerPanel
            // 
            this.layerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.layerPanel.Controls.Add(this.layerPanelLabel);
            this.layerPanel.Location = new System.Drawing.Point(892, 24);
            this.layerPanel.Name = "layerPanel";
            this.layerPanel.Size = new System.Drawing.Size(132, 715);
            this.layerPanel.TabIndex = 4;
            // 
            // layerPanelLabel
            // 
            this.layerPanelLabel.AutoSize = true;
            this.layerPanelLabel.ForeColor = System.Drawing.Color.White;
            this.layerPanelLabel.Location = new System.Drawing.Point(3, 10);
            this.layerPanelLabel.Name = "layerPanelLabel";
            this.layerPanelLabel.Size = new System.Drawing.Size(38, 13);
            this.layerPanelLabel.TabIndex = 0;
            this.layerPanelLabel.Text = "Layers";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1024, 761);
            this.Controls.Add(this.layerPanel);
            this.Controls.Add(this.editorPanel);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Image Markup Tool";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            
            // Add the file menu to the menu strip
            this.fileMenuControl.AddToMenuStrip(this.menuStrip);
            
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.editorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorPictureBox)).EndInit();
            this.layerPanel.ResumeLayout(false);
            this.layerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.Panel layerPanel;
        private System.Windows.Forms.Label layerPanelLabel;
        private System.Windows.Forms.PictureBox editorPictureBox;
        
        // File menu control
        private Image_Markup_Tool.Components.FileHandler.FileMenuControl fileMenuControl;
    }
}