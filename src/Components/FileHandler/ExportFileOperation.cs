using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Handles exporting an image to a file
    /// </summary>
    public class ExportFileOperation : IFileOperation
    {
        /// <summary>
        /// Execute the export file operation
        /// </summary>
        /// <param name="currentImage">The current image being edited</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the export status</returns>
        public FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null)
        {
            if (currentImage == null)
            {
                FileHandlerUtilities.ShowInfo("No image to export.");
                return FileOperationResult.Failed();
            }
            
            try
            {
                // Always prompt for export location
                string filePath = ShowExportFileDialog(FileHandlerUtilities.PNG_EXTENSION);
                if (string.IsNullOrEmpty(filePath))
                {
                    // User canceled
                    return FileOperationResult.Failed();
                }
                
                // Export the image
                if (ExportImage(currentImage, filePath))
                {
                    statusCallback?.Invoke($"Exported: {Path.GetFileName(filePath)}");
                    
                    // Export doesn't change the current file path or unsaved status
                    return FileOperationResult.Successful(currentImage, currentFilePath);
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error exporting image", ex);
            }
            
            return FileOperationResult.Failed();
        }
        
        /// <summary>
        /// Shows a save file dialog to select an export location
        /// </summary>
        /// <param name="defaultExtension">Default file extension</param>
        /// <returns>Path where the file should be exported, or null if canceled</returns>
        protected virtual string ShowExportFileDialog(string defaultExtension = FileHandlerUtilities.PNG_EXTENSION)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Use export-specific filter (PNG and JPG only)
                saveFileDialog.Filter = FileHandlerUtilities.EXPORT_FILTER;
                saveFileDialog.Title = "Export Image";
                saveFileDialog.DefaultExt = defaultExtension;
                
                // Set appropriate filter index for export
                saveFileDialog.FilterIndex = defaultExtension == FileHandlerUtilities.PNG_EXTENSION ? 1 : 2;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// Exports an image to a file
        /// </summary>
        /// <param name="image">The image to export</param>
        /// <param name="filePath">Path where to export the image</param>
        /// <returns>True if successful, false otherwise</returns>
        protected virtual bool ExportImage(Image image, string filePath)
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
                        
                    default:
                        FileHandlerUtilities.ShowError($"Unsupported file format for export: {extension}");
                        return false;
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error exporting image", ex);
                return false;
            }
        }
    }
}