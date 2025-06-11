using System;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Utility methods for file handling operations
    /// </summary>
    public static class FileHandlerUtilities
    {
        // File extension constants (with dots)
        public const string PNG_EXTENSION = ".png";
        public const string JPG_EXTENSION = ".jpg";
        public const string JPEG_EXTENSION = ".jpeg";
        public const string BMP_EXTENSION = ".bmp";
        public const string GIF_EXTENSION = ".gif";
        public const string SVG_EXTENSION = ".svg";
        
        // Filter strings for file dialogs
        public const string IMAGE_FILTER = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        public const string SVG_FILTER = "SVG Files|*.svg";
        public const string ALL_FILES_FILTER = "All Files|*.*";
        public const string COMBINED_FILTER = "All Supported Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.svg|PNG Files|*.png|JPEG Files|*.jpg;*.jpeg|SVG Files|*.svg|All Files|*.*";
        
        // Individual format filters
        public const string PNG_FILTER = "PNG Files|*.png";
        public const string JPG_FILTER = "JPEG Files|*.jpg;*.jpeg";
        public const string BMP_FILTER = "BMP Files|*.bmp";
        public const string GIF_FILTER = "GIF Files|*.gif";
        
        // Export-specific filter (PNG and JPG only)
        public const string EXPORT_FILTER = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg";
        
        /// <summary>
        /// Shows an information message to the user
        /// </summary>
        /// <param name="message">The message to display</param>
        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// Shows an error message to the user
        /// </summary>
        /// <param name="message">The error message to display</param>
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        /// <summary>
        /// Shows an error message to the user with exception details
        /// </summary>
        /// <param name="message">The error message to display</param>
        /// <param name="ex">The exception that occurred</param>
        public static void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        /// <summary>
        /// Shows a warning message to the user
        /// </summary>
        /// <param name="message">The warning message to display</param>
        /// <returns>The dialog result (OK/Cancel)</returns>
        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        
        /// <summary>
        /// Gets the appropriate file filter string for Open/Save dialogs based on operation type
        /// </summary>
        /// <param name="includeAllFiles">Whether to include the "All Files" filter</param>
        /// <param name="operationType">The type of file operation</param>
        /// <returns>A filter string for file dialogs</returns>
        public static string GetFileFilter(bool includeAllFiles = true, FileOperationType operationType = FileOperationType.Open)
        {
            string filter = "";
            
            switch (operationType)
            {
                case FileOperationType.Open:
                case FileOperationType.Import:
                    filter = IMAGE_FILTER;
                    break;
                    
                case FileOperationType.Save:
                case FileOperationType.SaveAs:
                    filter = SVG_FILTER;
                    break;
                    
                case FileOperationType.Export:
                    filter = EXPORT_FILTER;  // Now using the constant instead of hardcoded string
                    break;
            }
            
            if (includeAllFiles)
            {
                filter += "|" + ALL_FILES_FILTER;
            }
            
            return filter;
        }
        
        /// <summary>
        /// Gets the default extension for a file operation
        /// </summary>
        /// <param name="operationType">The type of file operation</param>
        /// <returns>The default file extension (with dot)</returns>
        public static string GetDefaultExtension(FileOperationType operationType)
        {
            switch (operationType)
            {
                case FileOperationType.Save:
                case FileOperationType.SaveAs:
                    return SVG_EXTENSION;
                    
                case FileOperationType.Export:
                    return PNG_EXTENSION;
                    
                default:
                    return "";
            }
        }
    }
    
    /// <summary>
    /// Defines the types of file operations
    /// </summary>
    public enum FileOperationType
    {
        Open,
        Import,
        Save,
        SaveAs,
        Export
    }
}