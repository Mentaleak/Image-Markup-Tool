namespace Image_Markup_Tool.Components.FileHandler
{
    partial class FileMenuControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFromClipboardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFromClipboardMenuItem,
            this.openMenuItem,
            this.importMenuItem,
            this.separatorMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.exportMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // newFromClipboardMenuItem
            // 
            this.newFromClipboardMenuItem.Name = "newFromClipboardMenuItem";
            this.newFromClipboardMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFromClipboardMenuItem.Size = new System.Drawing.Size(227, 22);
            this.newFromClipboardMenuItem.Text = "New From Clipboard";
            this.newFromClipboardMenuItem.Click += new System.EventHandler(this.newFromClipboardMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(227, 22);
            this.openMenuItem.Text = "Open SVG";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // importMenuItem
            // 
            this.importMenuItem.Name = "importMenuItem";
            this.importMenuItem.Size = new System.Drawing.Size(227, 22);
            this.importMenuItem.Text = "Import";
            this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);
            // 
            // separatorMenuItem
            // 
            this.separatorMenuItem.Name = "separatorMenuItem";
            this.separatorMenuItem.Size = new System.Drawing.Size(224, 6);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(227, 22);
            this.saveMenuItem.Text = "Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsMenuItem.Size = new System.Drawing.Size(227, 22);
            this.saveAsMenuItem.Text = "Save As...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportMenuItem.Size = new System.Drawing.Size(227, 22);
            this.exportMenuItem.Text = "Export";
            this.exportMenuItem.Click += new System.EventHandler(this.exportMenuItem_Click);
            // 
            // FileMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FileMenuControl";
            this.Size = new System.Drawing.Size(0, 0);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFromClipboardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMenuItem;
        private System.Windows.Forms.ToolStripSeparator separatorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
    }
}