using System;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    public partial class FileMenuControl : UserControl
    {
        // Events for menu actions
        public event EventHandler NewFromClipboardRequested;
        public event EventHandler OpenRequested;
        public event EventHandler ImportRequested;
        public event EventHandler SaveRequested;
        public event EventHandler SaveAsRequested;
        public event EventHandler ExportRequested;
        
        public FileMenuControl()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Adds this control's menu items to a menu strip at the specified index
        /// </summary>
        /// <param name="menuStrip">The menu strip to add to</param>
        /// <param name="index">The index at which to insert the menu item (default is 0, which is the first position)</param>
        public void AddToMenuStrip(MenuStrip menuStrip, int index = 0)
        {
            if (menuStrip == null) throw new ArgumentNullException(nameof(menuStrip));
            
            // Insert at the specified index
            if (index >= 0 && index <= menuStrip.Items.Count)
            {
                menuStrip.Items.Insert(index, fileMenuItem);
            }
            else
            {
                // If index is out of range, just add to the end
                menuStrip.Items.Add(fileMenuItem);
            }
        }
        
        /// <summary>
        /// Gets the File menu item for direct manipulation
        /// </summary>
        public ToolStripMenuItem FileMenuItem => fileMenuItem;
        
        // Properties to enable/disable menu items
        public bool SaveEnabled
        {
            get => saveMenuItem.Enabled;
            set => saveMenuItem.Enabled = value;
        }
        
        public bool SaveAsEnabled
        {
            get => saveAsMenuItem.Enabled;
            set => saveAsMenuItem.Enabled = value;
        }
        
        public bool ExportEnabled
        {
            get => exportMenuItem.Enabled;
            set => exportMenuItem.Enabled = value;
        }
        
        // Event handlers
        private void newFromClipboardMenuItem_Click(object sender, EventArgs e)
        {
            NewFromClipboardRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void importMenuItem_Click(object sender, EventArgs e)
        {
            ImportRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            SaveRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void exportMenuItem_Click(object sender, EventArgs e)
        {
            ExportRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}