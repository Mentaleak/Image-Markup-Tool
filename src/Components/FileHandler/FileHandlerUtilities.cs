using System;
using System.IO;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Utilities and constants for file handling operations
    /// </summary>
    public static class FileHandlerUtilities
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
        
        // Export filter - only PNG and JPG
        public const string EXPORT_FILTER = PNG_FILTER + "|" + JPG_FILTER;

        /// <summary>
        /// Shows an error message dialog
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="ex">Optional exception for detailed error information</param>
        public static void ShowError(string message, Exception ex = null)
        {
            string detailedMessage = ex != null ? $"{message}: {ex.Message}" : message;
            MessageBox.Show(detailedMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an information message dialog
        /// </summary>
        /// <param name="message">The information message</param>
        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Gets the appropriate image format based on file extension
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>The System.Drawing.Imaging.ImageFormat for the file</returns>
        public static System.Drawing.Imaging.ImageFormat GetImageFormat(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            
            switch (extension)
            {
                case PNG_EXTENSION:
                    return System.Drawing.Imaging.ImageFormat.Png;
                    
                case JPG_EXTENSION:
                case JPEG_EXTENSION:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                    
                default:
                    // Default to PNG if unknown
                    return System.Drawing.Imaging.ImageFormat.Png;
            }
        }
    }
}