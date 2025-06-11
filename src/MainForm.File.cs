using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Image_Markup_Tool.Components.FileHandler;

namespace Image_Markup_Tool
{
    /// <summary>
    /// Partial class containing file-related functionality for MainForm
    /// </summary>
    public partial class MainForm
    {
        // Track whether the current image has unsaved changes
        private bool _hasUnsavedChanges = false;

        /// <summary>
        /// Checks if there are unsaved changes and prompts the user to save if needed
        /// </summary>
        /// <returns>True if it's safe to proceed with a new operation, false if the operation should be canceled</returns>
        private bool CheckUnsavedChanges()
        {
            if (_currentImage != null && _hasUnsavedChanges)
            {
                string message = "The current image has unsaved changes. Do you want to save before proceeding?";
                DialogResult result = MessageBox.Show(message, "Unsaved Changes", 
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
                switch (result)
                {
                    case DialogResult.Yes:
                        // User wants to save changes
                        saveToolStripMenuItem_Click(this, EventArgs.Empty);
                        // If _hasUnsavedChanges is still true, the save operation failed or was canceled
                        return !_hasUnsavedChanges;
                        
                    case DialogResult.No:
                        // User doesn't want to save changes, proceed
                        return true;
                        
                    case DialogResult.Cancel:
                    default:
                        // User canceled, don't proceed
                        return false;
                }
            }
            
            // No unsaved changes, safe to proceed
            return true;
        }

        /// <summary>
        /// Handles the Open menu item click event
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding
            if (!CheckUnsavedChanges())
                return;
                
            string filePath = _fileHandler.OpenFile(status => statusLabel.Text = status);
            
            if (!string.IsNullOrEmpty(filePath))
            {
                // Load the image into the editor
                Image image = _fileHandler.LoadImage(filePath);
                if (image != null)
                {
                    // Display the image in the editor
                    DisplayImage(image);
                    
                    // Update window title with filename
                    this.Text = $"Image Markup Tool - {Path.GetFileName(filePath)}";
                    
                    // Store the current file path
                    _currentFilePath = filePath;
                    
                    // Reset unsaved changes flag
                    _hasUnsavedChanges = false;
                }
            }
        }
        
        /// <summary>
        /// Handles the New From Clipboard menu item click event
        /// </summary>
        private void newFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for unsaved changes before proceeding
            if (!CheckUnsavedChanges())
                return;
                
            try
            {
                // Check if clipboard contains an image
                if (Clipboard.ContainsImage())
                {
                    // Get image from clipboard
                    Image clipboardImage = Clipboard.GetImage();
                    
                    if (clipboardImage != null)
                    {
                        // Display the image in the editor
                        DisplayImage(clipboardImage);
                        
                        // Update window title
                        this.Text = "Image Markup Tool - New Image from Clipboard";
                        
                        // Clear current file path since this is a new image
                        _currentFilePath = null;
                        
                        // Update status
                        statusLabel.Text = "New image created from clipboard";
                        
                        // Set unsaved changes flag
                        _hasUnsavedChanges = true;
                    }
                }
                else
                {
                    MessageBox.Show("Clipboard does not contain an image.", "Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating image from clipboard: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Handles the Save menu item click event
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentImage == null)
            {
                MessageBox.Show("No image to save.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // If we already have a file path, use it; otherwise, prompt for one
            string filePath = _currentFilePath;
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = _fileHandler.SaveFile(FileHandler.PNG_EXTENSION, status => statusLabel.Text = status);
                if (string.IsNullOrEmpty(filePath))
                    return; // User canceled
                    
                _currentFilePath = filePath;
            }
            
            // Save the image
            if (SaveImageToFile(filePath))
            {
                // Update window title with filename
                this.Text = $"Image Markup Tool - {Path.GetFileName(filePath)}";
                
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
                MessageBox.Show("No image to export.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Always prompt for export location
            string filePath = _fileHandler.SaveFile(FileHandler.PNG_EXTENSION, status => statusLabel.Text = status);
            if (string.IsNullOrEmpty(filePath))
                return; // User canceled
                
            // Export the image
            ExportImageToFile(filePath);
        }
        
        /// <summary>
        /// Saves the current image to a file
        /// </summary>
        /// <param name="filePath">Path where to save the image</param>
        /// <returns>True if save was successful, false otherwise</returns>
        private bool SaveImageToFile(string filePath)
        {
            try
            {
                if (_currentImage == null || string.IsNullOrEmpty(filePath))
                    return false;
                
                bool success = _fileHandler.ExportImage(_currentImage, filePath);
                if (success)
                {
                    statusLabel.Text = $"Saved: {Path.GetFileName(filePath)}";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        /// <summary>
        /// Exports the current image to a file
        /// </summary>
        /// <param name="filePath">Path where to export the image</param>
        private void ExportImageToFile(string filePath)
        {
            try
            {
                if (_currentImage == null || string.IsNullOrEmpty(filePath))
                    return;
                
                bool success = _fileHandler.ExportImage(_currentImage, filePath);
                if (success)
                {
                    statusLabel.Text = $"Exported: {Path.GetFileName(filePath)}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting image: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}