using System;
using System.Drawing;
using System.Windows.Forms;

namespace Image_Markup_Tool
{
    public partial class MainForm : Form
    {
        // Panel sizes from PRD
        private const int TOOL_PANEL_WIDTH = 70;
        private const int LAYER_PANEL_WIDTH = 132;
        
        // Services
        private readonly Services.FileService _fileService;
        
        // Current loaded image
        private Image _currentImage;

        public MainForm()
        {
            InitializeComponent();
            
            // Initialize services
            _fileService = new Services.FileService();
            
            // Apply dark mode theme
            Styles.DarkTheme.ApplyToForm(this);
            Styles.DarkTheme.ApplyToMenuStrip(menuStrip);
            Styles.DarkTheme.ApplyToStatusStrip(statusStrip);
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
        /// Handles the Open menu item click event
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = _fileService.OpenFile(status => statusLabel.Text = status);
            
            if (!string.IsNullOrEmpty(filePath))
            {
                // Load the image into the editor
                Image image = _fileService.LoadImage(filePath);
                if (image != null)
                {
                    // Display the image in the editor
                    DisplayImage(image);
                    
                    // Update window title with filename
                    this.Text = $"Image Markup Tool - {System.IO.Path.GetFileName(filePath)}";
                }
            }
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