using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Markup_Tool
{
    public partial class MainForm : Form
    {
        // Panel sizes from PRD
        private const int TOOL_PANEL_WIDTH = 70;
        private const int LAYER_PANEL_WIDTH = 132;

        public MainForm()
        {
            InitializeComponent();
            
            // Apply dark mode theme
            ApplyDarkTheme();
            
            // Set up menu renderer for grey highlight
            menuStrip.Renderer = new CustomMenuRenderer();
        }

        /// <summary>
        /// Applies dark theme to the application
        /// </summary>
        private void ApplyDarkTheme()
        {
            // Dark theme colors
            Color darkBackground = Color.FromArgb(30, 30, 30);
            Color darkPanelBackground = Color.FromArgb(45, 45, 48);
            Color darkText = Color.FromArgb(240, 240, 240);
            
            // Apply dark theme to form
            this.BackColor = darkBackground;
            this.ForeColor = darkText;
            
            // Apply to menu
            menuStrip.BackColor = darkPanelBackground;
            menuStrip.ForeColor = darkText;
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                ApplyDarkThemeToMenuItem(item);
            }
            
            // Apply to panels
            toolPanel.BackColor = darkPanelBackground;
            editorPanel.BackColor = darkBackground;
            layerPanel.BackColor = darkPanelBackground;
            
            // Apply to status bar
            statusStrip.BackColor = darkPanelBackground;
            statusStrip.ForeColor = darkText;
        }
        
        /// <summary>
        /// Recursively applies dark theme to menu items
        /// </summary>
        private void ApplyDarkThemeToMenuItem(ToolStripMenuItem item)
        {
            // Dark theme colors
            Color darkDropdownBackground = Color.FromArgb(45, 45, 48);
            Color darkText = Color.FromArgb(240, 240, 240);
            
            item.BackColor = darkDropdownBackground;
            item.ForeColor = darkText;
            
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem menuSubItem)
                {
                    ApplyDarkThemeToMenuItem(menuSubItem);
                }
                else
                {
                    subItem.BackColor = darkDropdownBackground;
                    subItem.ForeColor = darkText;
                }
            }
        }

        /// <summary>
        /// Handles window resizing to maintain panel proportions
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }

        /// <summary>
        /// Resizes panels according to the PRD specifications
        /// </summary>
        private void ResizePanels()
        {
            // Tool panel is fixed width
            toolPanel.Width = TOOL_PANEL_WIDTH;
            
            // Layer panel is fixed width and aligned to right
            layerPanel.Width = LAYER_PANEL_WIDTH;
            layerPanel.Left = this.ClientSize.Width - layerPanel.Width;
            
            // Editor panel fills the remaining space
            editorPanel.Left = toolPanel.Right;
            editorPanel.Width = layerPanel.Left - toolPanel.Right;
        }

        /// <summary>
        /// Handles the Open menu item click event
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.svg|PNG Files (*.png)|*.png|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|SVG Files (*.svg)|*.svg";
                openFileDialog.Title = "Open Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // TODO: Implement file opening logic
                        string filePath = openFileDialog.FileName;
                        string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();
                        
                        // Update status bar
                        statusLabel.Text = $"Opened: {System.IO.Path.GetFileName(filePath)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Custom menu renderer to provide grey highlight for menu items
    /// </summary>
    public class CustomMenuRenderer : ToolStripProfessionalRenderer
    {
        public CustomMenuRenderer() : base(new CustomColorTable())
        {
        }
    }

    /// <summary>
    /// Custom color table for menu rendering with grey highlight
    /// </summary>
    public class CustomColorTable : ProfessionalColorTable
    {
        // Grey highlight color
        private Color menuHighlightColor = Color.FromArgb(70, 70, 70);
        
        public override Color MenuItemSelected => menuHighlightColor;
        public override Color MenuItemSelectedGradientBegin => menuHighlightColor;
        public override Color MenuItemSelectedGradientEnd => menuHighlightColor;
        
        public override Color MenuItemPressedGradientBegin => menuHighlightColor;
        public override Color MenuItemPressedGradientEnd => menuHighlightColor;
        
        public override Color MenuItemBorder => Color.FromArgb(60, 60, 60);
        
        public override Color MenuBorder => Color.FromArgb(45, 45, 48);
        
        public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
        
        public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 48);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 48);
        public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 48);
    }
}