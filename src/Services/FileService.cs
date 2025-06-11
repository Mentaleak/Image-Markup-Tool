using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool
{
    namespace Services
    {
        /// <summary>
        /// Handles file operations for the application
        /// </summary>
        public class FileService
        {
            // Supported file formats
            public const string PNG_EXTENSION = ".png";
            public const string JPG_EXTENSION = ".jpg";
            public const string JPEG_EXTENSION = ".jpeg";
            public const string SVG_EXTENSION = ".svg";

            // File dialog filter strings
            public const string ALL_SUPPORTED_FILTER = "Image Files|*.png;*.jpg;*.jpeg;*.svg";
            public const string PNG_FILTER = "PNG Files (*.png)|*.png";
            public const string JPG_FILTER = "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg";
            public const string SVG_FILTER = "SVG Files (*.svg)|*.svg";
            public const string ALL_FILTER = "All Files (*.*)|*.*";

            // Combined filter for open/save dialogs
            public const string COMBINED_FILTER = ALL_SUPPORTED_FILTER + "|" + PNG_FILTER + "|" + JPG_FILTER + "|" + SVG_FILTER + "|" + ALL_FILTER;

            /// <summary>
            /// Opens a file dialog to select an image file
            /// </summary>
            /// <param name="statusCallback">Callback to update status after file is opened</param>
            /// <returns>Path to the selected file, or null if canceled</returns>
            public string OpenFile(Action<string> statusCallback = null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = COMBINED_FILTER;
                    openFileDialog.Title = "Open Image File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string filePath = openFileDialog.FileName;
                            
                            // Call the status callback if provided
                            statusCallback?.Invoke($"Opened: {Path.GetFileName(filePath)}");
                            
                            return filePath;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
                return null;
            }

            /// <summary>
            /// Loads an image from a file
            /// </summary>
            /// <param name="filePath">Path to the image file</param>
            /// <returns>The loaded image or null if failed</returns>
            public Image LoadImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return null;

                try
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    
                    // Handle different file types
                    switch (extension)
                    {
                        case PNG_EXTENSION:
                        case JPG_EXTENSION:
                        case JPEG_EXTENSION:
                            return Image.FromFile(filePath);
                            
                        case SVG_EXTENSION:
                            // TODO: Implement SVG loading
                            MessageBox.Show("SVG loading not yet implemented", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return null;
                            
                        default:
                            MessageBox.Show($"Unsupported file format: {extension}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            /// <summary>
            /// Opens a save file dialog to save an image
            /// </summary>
            /// <param name="defaultExtension">Default file extension</param>
            /// <param name="statusCallback">Callback to update status after file is saved</param>
            /// <returns>Path where the file should be saved, or null if canceled</returns>
            public string SaveFile(string defaultExtension = PNG_EXTENSION, Action<string> statusCallback = null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = COMBINED_FILTER;
                    saveFileDialog.Title = "Save Image";
                    saveFileDialog.DefaultExt = defaultExtension;
                    
                    if (defaultExtension == PNG_EXTENSION)
                        saveFileDialog.FilterIndex = 2; // PNG filter index
                    else if (defaultExtension == JPG_EXTENSION || defaultExtension == JPEG_EXTENSION)
                        saveFileDialog.FilterIndex = 3; // JPG filter index
                    else if (defaultExtension == SVG_EXTENSION)
                        saveFileDialog.FilterIndex = 4; // SVG filter index

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string filePath = saveFileDialog.FileName;
                            
                            // Call the status callback if provided
                            statusCallback?.Invoke($"Saved: {Path.GetFileName(filePath)}");
                            
                            return filePath;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error preparing to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
                return null;
            }

            /// <summary>
            /// Exports an image to a file
            /// </summary>
            /// <param name="image">The image to export</param>
            /// <param name="filePath">Path where to save the image</param>
            /// <returns>True if successful, false otherwise</returns>
            public bool ExportImage(Image image, string filePath)
            {
                if (image == null || string.IsNullOrEmpty(filePath))
                    return false;

                try
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    
                    // Handle different file types
                    switch (extension)
                    {
                        case PNG_EXTENSION:
                            image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                            return true;
                            
                        case JPG_EXTENSION:
                        case JPEG_EXTENSION:
                            image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            return true;
                            
                        case SVG_EXTENSION:
                            // TODO: Implement SVG export
                            MessageBox.Show("SVG export not yet implemented", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                            
                        default:
                            MessageBox.Show($"Unsupported file format for export: {extension}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}