using System;
using System.Drawing;
using System.Windows.Forms;

namespace Image_Markup_Tool
{
    namespace Styles
    {
        /// <summary>
        /// Provides dark theme styling for the application
        /// </summary>
        public static class DarkTheme
        {
            // Dark theme colors
            public static readonly Color DarkBackground = Color.FromArgb(30, 30, 30);
            public static readonly Color DarkPanelBackground = Color.FromArgb(45, 45, 48);
            public static readonly Color DarkText = Color.FromArgb(240, 240, 240);
            public static readonly Color MenuHighlightColor = Color.FromArgb(70, 70, 70);
            public static readonly Color MenuBorderColor = Color.FromArgb(60, 60, 60);

            /// <summary>
            /// Applies dark theme to a form
            /// </summary>
            public static void ApplyToForm(Form form)
            {
                if (form == null) return;

                form.BackColor = DarkBackground;
                form.ForeColor = DarkText;

                // Apply to all controls recursively
                foreach (Control control in form.Controls)
                {
                    ApplyToControl(control);
                }
            }

            /// <summary>
            /// Applies dark theme to a control and its children
            /// </summary>
            public static void ApplyToControl(Control control)
            {
                if (control == null) return;

                // Handle specific control types
                if (control is Panel panel)
                {
                    panel.BackColor = DarkPanelBackground;
                    panel.ForeColor = DarkText;
                }
                else if (control is Label label)
                {
                    label.ForeColor = DarkText;
                }
                else
                {
                    // Default styling for other controls
                    control.BackColor = DarkBackground;
                    control.ForeColor = DarkText;
                }

                // Apply to child controls recursively
                foreach (Control child in control.Controls)
                {
                    ApplyToControl(child);
                }
            }

            /// <summary>
            /// Applies dark theme to a menu strip
            /// </summary>
            public static void ApplyToMenuStrip(MenuStrip menuStrip)
            {
                if (menuStrip == null) return;

                menuStrip.BackColor = DarkPanelBackground;
                menuStrip.ForeColor = DarkText;
                menuStrip.Renderer = new DarkMenuRenderer();

                // Apply to all menu items
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        ApplyToMenuItem(menuItem);
                    }
                }
            }

            /// <summary>
            /// Applies dark theme to a status strip
            /// </summary>
            public static void ApplyToStatusStrip(StatusStrip statusStrip)
            {
                if (statusStrip == null) return;

                statusStrip.BackColor = DarkPanelBackground;
                statusStrip.ForeColor = DarkText;

                // Apply to all status strip items
                foreach (ToolStripItem item in statusStrip.Items)
                {
                    item.BackColor = DarkPanelBackground;
                    item.ForeColor = DarkText;
                }
            }

            /// <summary>
            /// Recursively applies dark theme to menu items
            /// </summary>
            private static void ApplyToMenuItem(ToolStripMenuItem item)
            {
                if (item == null) return;

                item.BackColor = DarkPanelBackground;
                item.ForeColor = DarkText;

                foreach (ToolStripItem subItem in item.DropDownItems)
                {
                    if (subItem is ToolStripMenuItem menuSubItem)
                    {
                        ApplyToMenuItem(menuSubItem);
                    }
                    else
                    {
                        subItem.BackColor = DarkPanelBackground;
                        subItem.ForeColor = DarkText;
                    }
                }
            }
        }

        /// <summary>
        /// Custom menu renderer to provide grey highlight for menu items
        /// </summary>
        public class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkColorTable())
            {
            }
        }

        /// <summary>
        /// Custom color table for menu rendering with grey highlight
        /// </summary>
        public class DarkColorTable : ProfessionalColorTable
        {
            // Grey highlight color
            
            public override Color MenuItemSelected => DarkTheme.MenuHighlightColor;
            public override Color MenuItemSelectedGradientBegin => DarkTheme.MenuHighlightColor;
            public override Color MenuItemSelectedGradientEnd => DarkTheme.MenuHighlightColor;
            
            public override Color MenuItemPressedGradientBegin => DarkTheme.MenuHighlightColor;
            public override Color MenuItemPressedGradientEnd => DarkTheme.MenuHighlightColor;
            
            public override Color MenuItemBorder => DarkTheme.MenuBorderColor;
            
            public override Color MenuBorder => DarkTheme.DarkPanelBackground;
            
            public override Color ToolStripDropDownBackground => DarkTheme.DarkPanelBackground;
            
            public override Color ImageMarginGradientBegin => DarkTheme.DarkPanelBackground;
            public override Color ImageMarginGradientMiddle => DarkTheme.DarkPanelBackground;
            public override Color ImageMarginGradientEnd => DarkTheme.DarkPanelBackground;
        }
    }
}