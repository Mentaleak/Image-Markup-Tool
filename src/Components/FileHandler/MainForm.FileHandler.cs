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
        // FileMenuControl instance - declared in MainForm.Designer.cs
        // private Image_Markup_Tool.Components.FileHandler.FileMenuControl fileMenuControl;
        
        /// <summary>
        /// Initializes the file menu control and connects event handlers
        /// </summary>
        private void InitializeFileMenu()
        {
            // Connect file menu control events to our handlers
            fileMenuControl.NewFromClipboardRequested += newFromClipboardToolStripMenuItem_Click;
            fileMenuControl.OpenRequested += openToolStripMenuItem_Click;
            fileMenuControl.ImportRequested += importToolStripMenuItem_Click;
            fileMenuControl.SaveRequested += saveToolStripMenuItem_Click;
            fileMenuControl.SaveAsRequested += saveAsToolStripMenuItem_Click;
            fileMenuControl.ExportRequested += exportToolStripMenuItem_Click;
            
            // Update the initial state of menu items
            UpdateFileMenuState();
        }
        
        /// <summary>
        /// Updates the enabled state of file menu items based on current application state
        /// </summary>
        private void UpdateFileMenuState()
        {
            bool hasImage = _currentImage != null;
            
            // Enable/disable menu items based on whether we have an image
            fileMenuControl.SaveEnabled = hasImage;
            fileMenuControl.SaveAsEnabled = hasImage;
            fileMenuControl.ExportEnabled = hasImage;
        }

        /// <summary>
        /// Handles the Open menu item click event (SVG files only)
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding using the dialog component
            if (!UnsavedChangesDialog.CheckUnsavedChanges(_hasUnsavedChanges, () => saveToolStripMenuItem_Click(this, EventArgs.Empty)))
                return;
                
            // Use the FileHandler API to open an SVG file
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
                
                // Update menu item states
                UpdateFileMenuState();
            }
        }
        
        /// <summary>
        /// Handles the Import menu item click event (raster image files)
        /// </summary>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding using the dialog component
            if (!UnsavedChangesDialog.CheckUnsavedChanges(_hasUnsavedChanges, () => saveToolStripMenuItem_Click(this, EventArgs.Empty)))
                return;
                
            // Use the FileHandler API to import a raster image file
            var result = _fileHandler.ImportFile(status => statusLabel.Text = status);
            
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
                
                // Update menu item states
                UpdateFileMenuState();
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
                
                // Update menu item states
                UpdateFileMenuState();
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