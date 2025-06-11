using System;
using System.Drawing;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Main facade for file handling operations in the Image Markup Tool
    /// </summary>
    public class FileHandler
    {
        // File operation instances
        private readonly OpenFileOperation _openFileOperation;
        private readonly ImportFileOperation _importFileOperation;
        private readonly NewFromClipboardOperation _newFromClipboardOperation;
        private readonly SaveFileOperation _saveFileOperation;
        private readonly SaveAsFileOperation _saveAsFileOperation;
        private readonly ExportFileOperation _exportFileOperation;

        /// <summary>
        /// Initializes a new instance of the FileHandler class
        /// </summary>
        public FileHandler()
        {
            _openFileOperation = new OpenFileOperation();
            _importFileOperation = new ImportFileOperation();
            _newFromClipboardOperation = new NewFromClipboardOperation();
            _saveFileOperation = new SaveFileOperation();
            _saveAsFileOperation = new SaveAsFileOperation();
            _exportFileOperation = new ExportFileOperation();
        }

        /// <summary>
        /// Opens an SVG file
        /// </summary>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the opened SVG image and file path</returns>
        public FileOperationResult OpenFile(Action<string> statusCallback = null)
        {
            return _openFileOperation.Execute(null, null, statusCallback);
        }

        /// <summary>
        /// Imports a raster image file (PNG, JPG, JPEG)
        /// </summary>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the imported image and file path</returns>
        public FileOperationResult ImportFile(Action<string> statusCallback = null)
        {
            return _importFileOperation.Execute(null, null, statusCallback);
        }

        /// <summary>
        /// Creates a new image from clipboard content
        /// </summary>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the new image from clipboard</returns>
        public FileOperationResult NewFromClipboard(Action<string> statusCallback = null)
        {
            return _newFromClipboardOperation.Execute(null, null, statusCallback);
        }

        /// <summary>
        /// Saves an image to its current file path or prompts for a new one if none exists
        /// </summary>
        /// <param name="image">The image to save</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the saved image and file path</returns>
        public FileOperationResult SaveFile(Image image, string currentFilePath = null, Action<string> statusCallback = null)
        {
            return _saveFileOperation.Execute(image, currentFilePath, statusCallback);
        }

        /// <summary>
        /// Saves an image to a new file location
        /// </summary>
        /// <param name="image">The image to save</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the saved image and new file path</returns>
        public FileOperationResult SaveFileAs(Image image, Action<string> statusCallback = null)
        {
            return _saveAsFileOperation.Execute(image, null, statusCallback);
        }

        /// <summary>
        /// Exports an image to a file (PNG or JPG only)
        /// </summary>
        /// <param name="image">The image to export</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the export status</returns>
        public FileOperationResult ExportFile(Image image, string currentFilePath = null, Action<string> statusCallback = null)
        {
            return _exportFileOperation.Execute(image, currentFilePath, statusCallback);
        }

        /// <summary>
        /// Convenience method for accessing file extension constants
        /// </summary>
        public static class Extensions
        {
            public static string PNG => FileHandlerUtilities.PNG_EXTENSION;
            public static string JPG => FileHandlerUtilities.JPG_EXTENSION;
            public static string JPEG => FileHandlerUtilities.JPEG_EXTENSION;
            public static string SVG => FileHandlerUtilities.SVG_EXTENSION;
        }
    }
}