using System;
using System.Drawing;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Interface for file operations in the Image Markup Tool
    /// </summary>
    public interface IFileOperation
    {
        /// <summary>
        /// Execute the file operation
        /// </summary>
        /// <param name="currentImage">The current image being edited, if any</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing updated image, file path, and success status</returns>
        FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null);
    }

    /// <summary>
    /// Result of a file operation
    /// </summary>
    public class FileOperationResult
    {
        /// <summary>
        /// The image resulting from the operation, if any
        /// </summary>
        public Image Image { get; set; }
        
        /// <summary>
        /// The file path resulting from the operation, if any
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Whether the operation was successful
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Whether the operation resulted in unsaved changes
        /// </summary>
        public bool HasUnsavedChanges { get; set; }

        /// <summary>
        /// Create a successful result
        /// </summary>
        public static FileOperationResult Successful(Image image = null, string filePath = null, bool hasUnsavedChanges = false)
        {
            return new FileOperationResult
            {
                Image = image,
                FilePath = filePath,
                Success = true,
                HasUnsavedChanges = hasUnsavedChanges
            };
        }

        /// <summary>
        /// Create a failed result
        /// </summary>
        public static FileOperationResult Failed()
        {
            return new FileOperationResult
            {
                Success = false
            };
        }
    }
}