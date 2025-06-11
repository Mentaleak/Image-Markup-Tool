using System.Windows.Forms;

namespace Image_Markup_Tool.Components.Dialogs
{
    /// <summary>
    /// Handles dialog prompts for unsaved changes
    /// </summary>
    public class UnsavedChangesDialog
    {
        /// <summary>
        /// Checks if there are unsaved changes and prompts the user to save if needed
        /// </summary>
        /// <param name="hasUnsavedChanges">Flag indicating if there are unsaved changes</param>
        /// <param name="saveAction">Action to perform if user chooses to save</param>
        /// <returns>True if it's safe to proceed with a new operation, false if the operation should be canceled</returns>
        public static bool CheckUnsavedChanges(bool hasUnsavedChanges, System.Action saveAction)
        {
            if (hasUnsavedChanges)
            {
                string message = "The current image has unsaved changes. Do you want to save before proceeding?";
                DialogResult result = MessageBox.Show(message, "Unsaved Changes", 
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
                switch (result)
                {
                    case DialogResult.Yes:
                        // User wants to save changes
                        saveAction?.Invoke();
                        // The save action should update the hasUnsavedChanges flag
                        // We'll need to check the updated status from the caller
                        return true;
                        
                    case DialogResult.No:
                        // User doesn't want to save changes, proceed
                        return true;
                        
                    case DialogResult.Cancel:
                    default:
                        // User canceled, don't proceed
                        return false;
                }
            }
            
            // No unsaved changes, safe to proceed
            return true;
        }
    }
}