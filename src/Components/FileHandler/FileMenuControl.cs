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
        
        // Method to add this control's menu items to a menu strip
        public void AddToMenuStrip(MenuStrip menuStrip)
        {
            if (menuStrip == null) throw new ArgumentNullException(nameof(menuStrip));
            menuStrip.Items.Add(fileMenuItem);
        }
        
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