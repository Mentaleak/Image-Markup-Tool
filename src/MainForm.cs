using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Image_Markup_Tool.Components.Dialogs;
using Image_Markup_Tool.Components.FileHandler;

namespace Image_Markup_Tool
{
    public partial class MainForm : Form
    {
        // Panel sizes from PRD
        private const int TOOL_PANEL_WIDTH = 70;
        private const int LAYER_PANEL_WIDTH = 132;
        
        // Components
        private readonly FileHandler _fileHandler;
        
        // Current loaded image and file path
        private Image _currentImage;
        private string _currentFilePath;
        
        // Track whether the current image has unsaved changes
        private bool _hasUnsavedChanges = false;

        public MainForm()
        {
            InitializeComponent();
            
            // Initialize components
            _fileHandler = new FileHandler();
            
            // Apply dark mode theme
            Styles.DarkTheme.ApplyToForm(this);
            Styles.DarkTheme.ApplyToMenuStrip(menuStrip);
            Styles.DarkTheme.ApplyToStatusStrip(statusStrip);
            
            // Wire up the import menu item click event handler
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
        }

        /// <summary>
        /// Handles window resizing to maintain panel proportions
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }

        /// <summary>
        /// Resizes panels according to the PRD specifications
        /// </summary>
        private void ResizePanels()
        {
            // Tool panel is fixed width
            toolPanel.Width = TOOL_PANEL_WIDTH;
            
            // Layer panel is fixed width and aligned to right
            layerPanel.Width = LAYER_PANEL_WIDTH;
            layerPanel.Left = this.ClientSize.Width - layerPanel.Width;
            
            // Editor panel fills the remaining space
            editorPanel.Left = toolPanel.Right;
            editorPanel.Width = layerPanel.Left - toolPanel.Right;
        }
        
        /// <summary>
        /// Displays an image in the editor
        /// </summary>
        /// <param name="image">The image to display</param>
        private void DisplayImage(Image image)
        {
            try
            {
                // Clean up previous image if exists
                if (_currentImage != null)
                {
                    editorPictureBox.Image = null;
                    _currentImage.Dispose();
                    _currentImage = null;
                }
                
                // Set the new image
                _currentImage = image;
                editorPictureBox.Image = _currentImage;
                
                // Adjust PictureBox settings for best display
                AdjustPictureBoxSettings();
                
                // TODO: Create initial layer for the image
                // This would be implemented when layer management is added
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Adjusts PictureBox settings based on the loaded image
        /// </summary>
        private void AdjustPictureBoxSettings()
        {
            if (_currentImage == null)
                return;
                
            // Choose the appropriate SizeMode based on image size vs. container size
            if (_currentImage.Width > editorPanel.Width || _currentImage.Height > editorPanel.Height)
            {
                // Image is larger than the panel, use Zoom to fit it
                editorPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Image is smaller than the panel, center it
                editorPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            
            // Update status bar with image dimensions
            statusLabel.Text = $"Image dimensions: {_currentImage.Width} x {_currentImage.Height} pixels";
        }

        
    }
}