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

        /// <summary>
        /// Handles the Open menu item click event
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding using the dialog component
            if (!UnsavedChangesDialog.CheckUnsavedChanges(_hasUnsavedChanges, () => saveToolStripMenuItem_Click(this, EventArgs.Empty)))
                return;
                
            // Use the FileHandler API to open a file
            var result = _fileHandler.OpenFile(status => statusLabel.Text = status);
            
            if (result.Success && result.Image != null)
            {
                // Display the image in the editor
                DisplayImage(result.Image);
                
                // Update window title with filename
                this.Text = $"Image Markup Tool - {Path.GetFileName(result.FilePath)}";
                
                // Store the current file path
                _currentFilePath = result.FilePath;
                
                // Reset unsaved changes flag
                _hasUnsavedChanges = false;
            }
        }
        
        /// <summary>
        /// Handles the New From Clipboard menu item click event
        /// </summary>
        private void newFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding using the dialog component
            if (!UnsavedChangesDialog.CheckUnsavedChanges(_hasUnsavedChanges, () => saveToolStripMenuItem_Click(this, EventArgs.Empty)))
                return;
                
            // Use the FileHandler API to create a new image from clipboard
            var result = _fileHandler.NewFromClipboard(status => statusLabel.Text = status);
            
            if (result.Success && result.Image != null)
            {
                // Display the image in the editor
                DisplayImage(result.Image);
                
                // Update window title
                this.Text = "Image Markup Tool - New Image from Clipboard";
                
                // Clear current file path since this is a new image
                _currentFilePath = null;
                
                // Set unsaved changes flag
                _hasUnsavedChanges = true;
            }
        }
        
        /// <summary>
        /// Handles the Save menu item click event
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to save.");
                return;
            }
            
            // Use the FileHandler API to save the file
            var result = _fileHandler.SaveFile(_currentImage, _currentFilePath, status => statusLabel.Text = status);
            
            if (result.Success)
            {
                // Update file path if it was a new file
                _currentFilePath = result.FilePath;
                
                // Update window title with filename
                this.Text = $"Image Markup Tool - {Path.GetFileName(_currentFilePath)}";
                
                // Clear unsaved changes flag
                _hasUnsavedChanges = false;
            }
        }
        
        /// <summary>
        /// Handles the Save As menu item click event
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to save.");
                return;
            }
            
            // Use the FileHandler API to save the file with a new name
            var result = _fileHandler.SaveFileAs(_currentImage, status => statusLabel.Text = status);
            
            if (result.Success)
            {
                // Update to the new file path
                _currentFilePath = result.FilePath;
                
                // Update window title with filename
                this.Text = $"Image Markup Tool - {Path.GetFileName(_currentFilePath)}";
                
                // Clear unsaved changes flag
                _hasUnsavedChanges = false;
            }
        }
        
        /// <summary>
        /// Handles the Export menu item click event
        /// </summary>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to export.");
                return;
            }
            
            // Use the FileHandler API to export the file
            var result = _fileHandler.ExportFile(_currentImage, _currentFilePath, status => statusLabel.Text = status);
            
            // Note: Export doesn't change the current file path or unsaved status
        }
    }
}