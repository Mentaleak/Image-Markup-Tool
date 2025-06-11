using System;
using System.Drawing;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.FileHandler
{
    /// <summary>
    /// Handles creating a new image from clipboard content
    /// </summary>
    public class NewFromClipboardOperation : IFileOperation
    {
        /// <summary>
        /// Execute the new from clipboard operation
        /// </summary>
        /// <param name="currentImage">The current image being edited, if any</param>
        /// <param name="currentFilePath">The current file path, if any</param>
        /// <param name="statusCallback">Callback to update status after operation</param>
        /// <returns>Operation result containing the new image from clipboard</returns>
        public FileOperationResult Execute(Image currentImage = null, string currentFilePath = null, Action<string> statusCallback = null)
        {
            try
            {
                // Check if clipboard contains an image
                if (Clipboard.ContainsImage())
                {
                    // Get image from clipboard
                    Image clipboardImage = Clipboard.GetImage();
                    
                    if (clipboardImage != null)
                    {
                        // Update status
                        statusCallback?.Invoke("New image created from clipboard");
                        
                        // Return successful result with the new image, no file path, and unsaved changes
                        return FileOperationResult.Successful(clipboardImage, null, true);
                    }
                }
                else
                {
                    FileHandlerUtilities.ShowInfo("Clipboard does not contain an image.");
                }
            }
            catch (Exception ex)
            {
                FileHandlerUtilities.ShowError("Error creating image from clipboard", ex);
            }
            
            return FileOperationResult.Failed();
        }
    }
}