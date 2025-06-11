using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Handles saving an image to a new file location
    /// </summary>
    public class SaveAsFileOperation : IFileOperation
    {
        /// <summary>
        /// Execute the save as file operation
        /// </summary>
        /// <param name="currentImage">The current image being edited</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the saved image and new file path</returns>
        public FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null)
        {
            if (currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to save.");
                return FileOperationResult.Failed();
            }
            
            try
            {
                // Prompt for a new file location
                string filePath = ShowSaveFileDialog(FileHandlerUtilities.PNG_EXTENSION);
                if (string.IsNullOrEmpty(filePath))
                {
                    // User canceled
                    return FileOperationResult.Failed();
                }
                
                // Save the image to the new file path
                if (SaveImage(currentImage, filePath))
                {
                    statusCallback?.Invoke($"Saved: {Path.GetFileName(filePath)}");
                    return FileOperationResult.Successful(currentImage, filePath);
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error saving image", ex);
            }
            
            return FileOperationResult.Failed();
        }
        
        /// <summary>
        /// Shows a save file dialog to select a save location
        /// </summary>
        /// <param name="defaultExtension">Default file extension</param>
        /// <returns>Path where the file should be saved, or null if canceled</returns>
        protected virtual string ShowSaveFileDialog(string defaultExtension = FileHandlerUtilities.PNG_EXTENSION)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = FileHandlerUtilities.COMBINED_FILTER;
                saveFileDialog.Title = "Save Image As";
                saveFileDialog.DefaultExt = defaultExtension;
                
                // Set appropriate filter index
                if (defaultExtension == FileHandlerUtilities.PNG_EXTENSION)
                    saveFileDialog.FilterIndex = 2; // PNG filter index
                else if (defaultExtension == FileHandlerUtilities.JPG_EXTENSION || defaultExtension == FileHandlerUtilities.JPEG_EXTENSION)
                    saveFileDialog.FilterIndex = 3; // JPG filter index
                else if (defaultExtension == FileHandlerUtilities.SVG_EXTENSION)
                    saveFileDialog.FilterIndex = 4; // SVG filter index

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// Saves an image to a file
        /// </summary>
        /// <param name="image">The image to save</param>
        /// <param name="filePath">Path where to save the image</param>
        /// <returns>True if successful, false otherwise</returns>
        protected virtual bool SaveImage(Image image, string filePath)
        {
            if (image == null || string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                
                // Handle different file types
                switch (extension)
                {
                    case FileHandlerUtilities.PNG_EXTENSION:
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        return true;
                        
                    case FileHandlerUtilities.JPG_EXTENSION:
                    case FileHandlerUtilities.JPEG_EXTENSION:
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return true;
                        
                    case FileHandlerUtilities.SVG_EXTENSION:
                        // TODO: Implement SVG saving
                        FileHandlerUtilities.ShowInfo("SVG saving not yet implemented");
                        return false;
                        
                    default:
                        FileHandlerUtilities.ShowError($"Unsupported file format for saving: {extension}");
                        return false;
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error saving image", ex);
                return false;
            }
        }
    }
}