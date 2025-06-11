using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Handles saving an image to a file
    /// </summary>
    public class SaveFileOperation : IFileOperation
    {
        /// <summary>
        /// Execute the save file operation
        /// </summary>
        /// <param name="currentImage">The current image being edited</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the saved image and file path</returns>
        public FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null)
        {
            if (currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to save.");
                return FileOperationResult.Failed();
            }
            
            try
            {
                // If we already have a file path, use it; otherwise, prompt for one
                string filePath = currentFilePath;
                if (string.IsNullOrEmpty(filePath))
                {
                    // No existing file path, delegate to SaveAsFileOperation
                    var saveAsOperation = new SaveAsFileOperation();
                    return saveAsOperation.Execute(currentImage, null, statusCallback);
                }
                
                // Save the image to the existing file path
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