using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Handles importing various image file types (excluding SVG)
    /// </summary>
    public class ImportFileOperation : IFileOperation
    {
        /// <summary>
        /// Execute the import file operation
        /// </summary>
        /// <param name="currentImage">The current image being edited, if any</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the imported image and file path</returns>
        public FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Create a filter for raster image formats only (no SVG)
                // Start with "All Supported Images" as the default option
                string allRasterImagesFilter = "All Supported Images|*.png;*.jpg;*.jpeg";
                string rasterFilter = allRasterImagesFilter + "|" + 
                                     FileHandlerUtilities.PNG_FILTER + "|" + 
                                     FileHandlerUtilities.JPG_FILTER;
                
                openFileDialog.Filter = rasterFilter;
                openFileDialog.Title = "Import Image File";
                openFileDialog.FilterIndex = 1; // Default to "All Supported Images"

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        Image image = LoadImage(filePath);
                        
                        if (image != null)
                        {
                            // Call the status callback if provided
                            statusCallback?.Invoke($"Imported: {Path.GetFileName(filePath)}");
                            
                            return FileOperationResult.Successful(image, filePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        FileHandlerUtilities.ShowError("Error importing file", ex);
                    }
                }
            }
            
            return FileOperationResult.Failed();
        }

        /// <summary>
        /// Loads an image from a file
        /// </summary>
        /// <param name="filePath">Path to the image file</param>
        /// <returns>The loaded image or null if failed</returns>
        private Image LoadImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return null;

            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                
                // Handle different file types
                switch (extension)
                {
                    case FileHandlerUtilities.PNG_EXTENSION:
                    case FileHandlerUtilities.JPG_EXTENSION:
                    case FileHandlerUtilities.JPEG_EXTENSION:
                        return Image.FromFile(filePath);
                        
                    default:
                        FileHandlerUtilities.ShowError($"Unsupported file format: {extension}");
                        return null;
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error loading image", ex);
                return null;
            }
        }
    }
}