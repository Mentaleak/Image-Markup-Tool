using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Image_Markup_Tool.Components.Dialogs;
using Image_Markup_Tool.Components.FileHandler;
using Image_Markup_Tool.Components.Layers; // Add this namespace for LayerListControl

namespace Image_Markup_Tool
{
    public partial class MainForm : Form
    {
        // Panel sizes from PRD
        private const int TOOL_PANEL_WIDTH = 70;
        private const int LAYER_PANEL_WIDTH = 132;
        
        // Components
        private readonly FileHandler _fileHandler;
        
        // Layer control
        private LayerListControl layerListControl; // Add this field declaration
        
        // Current loaded image and file path
        private Image _currentImage;
        private string _currentFilePath;
        
        // Track whether the current image has unsaved changes
        private bool _hasUnsavedChanges = false;

        public MainForm()
        {
            InitializeComponent();
            
            // Initialize components
            _fileHandler = new FileHandler();

            // Initialize layer control
            InitializeLayerControl();
    
            // Apply dark mode theme
            Styles.DarkTheme.ApplyToForm(this);
            Styles.DarkTheme.ApplyToMenuStrip(menuStrip);
            Styles.DarkTheme.ApplyToStatusStrip(statusStrip);
            
            // Update menu state
            UpdateFileMenuState();
        }

        /// <summary>
        /// Initializes the layer control and sets up event handlers
        /// </summary>
        private void InitializeLayerControl()
        {
            // Create the layer list control
            layerListControl = new LayerListControl();
            layerListControl.Dock = DockStyle.Fill;
            
            // Add event handlers
            layerListControl.LayerSelected += LayerListControl_LayerSelected;
            layerListControl.LayerVisibilityChanged += LayerListControl_LayerVisibilityChanged;
            layerListControl.LayersReordered += LayerListControl_LayersReordered;
            
            // Add to the layer panel
            layerPanel.Controls.Add(layerListControl);
        }

        private void UpdateFileMenuState()
        {
            bool hasImage = _currentImage != null;
            
            // Enable/disable menu items based on whether we have an image
            saveToolStripMenuItem.Enabled = hasImage;
            saveAsToolStripMenuItem.Enabled = hasImage;
            exportToolStripMenuItem.Enabled = hasImage;
            
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
        /// Handles window resizing to maintain panel proportions
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }
        
        /// <summary>
        /// Displays an image in the editor
        /// </summary>
        /// <param name="image">The image to display</param>
        private void DisplayImage(Image image)
        {
            try
            {
                // Clear existing layers
                layerListControl.ClearLayers();
                
                // Create a new layer with the image
                CreateLayerFromImage(image, "Background");
                
                // The composite image will be created by RedrawCompositeImage
                RedrawCompositeImage();
                
                // Adjust PictureBox settings for best display
                AdjustPictureBoxSettings();
                
                // Update file menu state
                UpdateFileMenuState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Adjusts PictureBox settings based on the loaded image
        /// </summary>
        private void AdjustPictureBoxSettings()
        {
            if (_currentImage == null)
                return;
                
            // Choose the appropriate SizeMode based on image size vs. container size
            if (_currentImage.Width > editorPanel.Width || _currentImage.Height > editorPanel.Height)
            {
                // Image is larger than the panel, use Zoom to fit it
                editorPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Image is smaller than the panel, center it
                editorPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            
            // Update status bar with image dimensions
            statusLabel.Text = $"Image dimensions: {_currentImage.Width} x {_currentImage.Height} pixels";
        }



        /// <summary>
        /// Handles the LayerSelected event
        /// </summary>
        private void LayerListControl_LayerSelected(object sender, Components.Layers.LayerEventArgs e)
        {
            // Handle layer selection
            // For example, update tools or properties based on the selected layer
        }

        /// <summary>
        /// Handles the LayerVisibilityChanged event
        /// </summary>
        private void LayerListControl_LayerVisibilityChanged(object sender, Components.Layers.LayerEventArgs e)
        {
            // Redraw the composite image when layer visibility changes
            RedrawCompositeImage();
        }

        /// <summary>
        /// Handles the LayersReordered event
        /// </summary>
        private void LayerListControl_LayersReordered(object sender, EventArgs e)
        {
            // Redraw the composite image when layers are reordered
            RedrawCompositeImage();
        }

        /// <summary>
        /// Creates a new layer from an image
        /// </summary>
        private void CreateLayerFromImage(Image image, string name = null)
        {
            if (image == null)
                return;
                
            // Create a default name if none provided
            if (string.IsNullOrEmpty(name))
            {
                name = $"Layer {layerListControl.Layers.Count + 1}";
            }
            
            // Create the layer
            var layer = new Components.Layers.Layer(name, image);
            
            // Add to the layer list
            layerListControl.AddLayer(layer);
            
            // Mark as having unsaved changes
            _hasUnsavedChanges = true;
        }

        /// <summary>
        /// Redraws the composite image from all visible layers
        /// </summary>
        private void RedrawCompositeImage()
        {
            // Get all layers
            var layers = layerListControl.Layers;
            
            if (layers.Count == 0)
            {
                // No layers, clear the display
                if (_currentImage != null)
                {
                    _currentImage.Dispose();
                    _currentImage = null;
                }
                
                editorPictureBox.Image = null;
                return;
            }
            
            // Find the first visible layer to determine dimensions
            Components.Layers.Layer baseLayer = null;
            foreach (var layer in layers)
            {
                if (layer.Visible)
                {
                    baseLayer = layer;
                    break;
                }
            }
            
            if (baseLayer == null)
            {
                // No visible layers
                editorPictureBox.Image = null;
                return;
            }
            
            // Create a new composite image
            int width = baseLayer.Image.Width;
            int height = baseLayer.Image.Height;
            
            // Dispose of existing image
            if (_currentImage != null)
            {
                _currentImage.Dispose();
            }
            
            // Create new composite image
            _currentImage = new Bitmap(width, height);
            
            using (Graphics g = Graphics.FromImage(_currentImage))
            {
                g.Clear(Color.Transparent);
                
                // Draw each visible layer from bottom to top
                for (int i = layers.Count - 1; i >= 0; i--)
                {
                    var layer = layers[i];
                    if (layer.Visible && layer.Image != null)
                    {
                        // Create color matrix with opacity
                        System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix();
                        matrix.Matrix33 = layer.Opacity; // Set opacity
                        
                        System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
                        attributes.SetColorMatrix(matrix);
                        
                        // Draw the layer with opacity
                        g.DrawImage(
                            layer.Image,
                            new Rectangle(0, 0, width, height),
                            0, 0, layer.Image.Width, layer.Image.Height,
                            GraphicsUnit.Pixel,
                            attributes
                        );
                    }
                }
            }
            
            // Update the display
            editorPictureBox.Image = _currentImage;
            
            // Mark as having unsaved changes
            _hasUnsavedChanges = true;
        }
    }
}