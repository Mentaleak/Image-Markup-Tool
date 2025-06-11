using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Image_Markup_Tool.Components.Layers
{
    /// <summary>
    /// Custom control for displaying and managing image layers
    /// </summary>
    public partial class LayerListControl : UserControl
    {
        // Event for when a layer is selected
        public event EventHandler<LayerEventArgs> LayerSelected;
        
        // Event for when a layer visibility is toggled
        public event EventHandler<LayerEventArgs> LayerVisibilityChanged;
        
        // Event for when layers are reordered
        public event EventHandler LayersReordered;
        
        // Collection of layers
        private List<Layer> _layers = new List<Layer>();
        
        // Currently selected layer index
        private int _selectedLayerIndex = -1;
        
        public LayerListControl()
        {
            InitializeComponent();
            
            // Set up the list box
            layerListBox.DrawMode = DrawMode.OwnerDrawVariable;
            layerListBox.MeasureItem += LayerListBox_MeasureItem;
            layerListBox.DrawItem += LayerListBox_DrawItem;
            layerListBox.SelectedIndexChanged += LayerListBox_SelectedIndexChanged;
            
            // Set up the context menu
            layerListBox.ContextMenuStrip = layerContextMenu;
        }
        
        /// <summary>
        /// Gets or sets the currently selected layer
        /// </summary>
        public Layer SelectedLayer
        {
            get
            {
                if (_selectedLayerIndex >= 0 && _selectedLayerIndex < _layers.Count)
                    return _layers[_selectedLayerIndex];
                return null;
            }
        }
        
        /// <summary>
        /// Gets the list of layers
        /// </summary>
        public IReadOnlyList<Layer> Layers => _layers.AsReadOnly();
        
        /// <summary>
        /// Adds a new layer to the list
        /// </summary>
        /// <param name="layer">The layer to add</param>
        public void AddLayer(Layer layer)
        {
            if (layer == null)
                throw new ArgumentNullException(nameof(layer));
                
            _layers.Add(layer);
            RefreshLayerList();
            
            // Select the newly added layer
            SelectLayer(_layers.Count - 1);
        }
        
        /// <summary>
        /// Removes a layer from the list
        /// </summary>
        /// <param name="index">The index of the layer to remove</param>
        public void RemoveLayer(int index)
        {
            if (index >= 0 && index < _layers.Count)
            {
                _layers.RemoveAt(index);
                RefreshLayerList();
                
                // Update selected layer
                if (_selectedLayerIndex == index)
                {
                    // Select the next available layer
                    if (_layers.Count > 0)
                    {
                        int newIndex = Math.Min(index, _layers.Count - 1);
                        SelectLayer(newIndex);
                    }
                    else
                    {
                        _selectedLayerIndex = -1;
                        OnLayerSelected(null);
                    }
                }
                else if (_selectedLayerIndex > index)
                {
                    // Adjust selected index if a layer before it was removed
                    _selectedLayerIndex--;
                }
            }
        }
        
        /// <summary>
        /// Clears all layers from the list
        /// </summary>
        public void ClearLayers()
        {
            _layers.Clear();
            _selectedLayerIndex = -1;
            RefreshLayerList();
            OnLayerSelected(null);
        }
        
        /// <summary>
        /// Moves a layer up in the stack (visually down in the list)
        /// </summary>
        /// <param name="index">The index of the layer to move</param>
        public void MoveLayerUp(int index)
        {
            if (index > 0 && index < _layers.Count)
            {
                Layer layer = _layers[index];
                _layers.RemoveAt(index);
                _layers.Insert(index - 1, layer);
                
                RefreshLayerList();
                
                // Update selected layer
                if (_selectedLayerIndex == index)
                    _selectedLayerIndex--;
                else if (_selectedLayerIndex == index - 1)
                    _selectedLayerIndex++;
                    
                layerListBox.SelectedIndex = _selectedLayerIndex;
                
                OnLayersReordered();
            }
        }
        
        /// <summary>
        /// Moves a layer down in the stack (visually up in the list)
        /// </summary>
        /// <param name="index">The index of the layer to move</param>
        public void MoveLayerDown(int index)
        {
            if (index >= 0 && index < _layers.Count - 1)
            {
                Layer layer = _layers[index];
                _layers.RemoveAt(index);
                _layers.Insert(index + 1, layer);
                
                RefreshLayerList();
                
                // Update selected layer
                if (_selectedLayerIndex == index)
                    _selectedLayerIndex++;
                else if (_selectedLayerIndex == index + 1)
                    _selectedLayerIndex--;
                    
                layerListBox.SelectedIndex = _selectedLayerIndex;
                
                OnLayersReordered();
            }
        }
        
        /// <summary>
        /// Selects a layer by index
        /// </summary>
        /// <param name="index">The index of the layer to select</param>
        public void SelectLayer(int index)
        {
            if (index >= 0 && index < _layers.Count)
            {
                _selectedLayerIndex = index;
                layerListBox.SelectedIndex = index;
                OnLayerSelected(_layers[index]);
            }
        }
        
        /// <summary>
        /// Refreshes the layer list display
        /// </summary>
        private void RefreshLayerList()
        {
            layerListBox.BeginUpdate();
            layerListBox.Items.Clear();
            
            // Add items in reverse order (top layer first visually)
            for (int i = 0; i < _layers.Count; i++)
            {
                layerListBox.Items.Add(_layers[i]);
            }
            
            layerListBox.EndUpdate();
        }
        
        /// <summary>
        /// Handles the MeasureItem event for the layer list box
        /// </summary>
        private void LayerListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Set the height of each item
            e.ItemHeight = 50; // Adjust as needed
        }
        
        /// <summary>
        /// Handles the DrawItem event for the layer list box
        /// </summary>
        private void LayerListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _layers.Count)
                return;
                
            e.DrawBackground();
            
            // Get the layer
            Layer layer = _layers[e.Index];
            
            // Draw selection rectangle if selected
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var brush = new SolidBrush(Color.FromArgb(80, 120, 180)))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            
            // Draw layer thumbnail
            if (layer.Thumbnail != null)
            {
                // Calculate thumbnail rectangle
                Rectangle thumbRect = new Rectangle(
                    e.Bounds.X + 30, // Leave space for visibility icon
                    e.Bounds.Y + 5,
                    40, // Thumbnail width
                    40  // Thumbnail height
                );
                
                e.Graphics.DrawImage(layer.Thumbnail, thumbRect);
            }
            
            // Draw layer name
            using (var brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(
                    layer.Name,
                    e.Font,
                    brush,
                    e.Bounds.X + 80, // Position after thumbnail
                    e.Bounds.Y + (e.Bounds.Height - e.Font.Height) / 2 // Vertically centered
                );
            }
            
            // Draw visibility icon
            Rectangle visibilityRect = new Rectangle(
                e.Bounds.X + 5,
                e.Bounds.Y + (e.Bounds.Height - 16) / 2, // Centered
                16, // Icon width
                16  // Icon height
            );
            
            // Draw eye icon based on visibility
            using (var pen = new Pen(e.ForeColor, 1.5f))
            {
                if (layer.Visible)
                {
                    // Draw open eye
                    e.Graphics.DrawEllipse(pen, visibilityRect.X + 3, visibilityRect.Y + 4, 10, 8);
                    e.Graphics.DrawEllipse(pen, visibilityRect.X + 6, visibilityRect.Y + 6, 4, 4);
                }
                else
                {
                    // Draw closed eye (line through eye)
                    e.Graphics.DrawEllipse(pen, visibilityRect.X + 3, visibilityRect.Y + 4, 10, 8);
                    e.Graphics.DrawLine(pen, 
                        visibilityRect.X + 2, visibilityRect.Y + 12,
                        visibilityRect.X + 14, visibilityRect.Y + 4);
                }
            }
            
            e.DrawFocusRectangle();
        }
        
        /// <summary>
        /// Handles the SelectedIndexChanged event for the layer list box
        /// </summary>
        private void LayerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = layerListBox.SelectedIndex;
            if (index >= 0 && index < _layers.Count)
            {
                _selectedLayerIndex = index;
                OnLayerSelected(_layers[index]);
            }
        }
        
        /// <summary>
        /// Handles the click event for the visibility icon
        /// </summary>
        private void LayerListBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Get the index of the item that was clicked
            int index = layerListBox.IndexFromPoint(e.Location);
            if (index >= 0 && index < _layers.Count)
            {
                // Check if click was on the visibility icon area
                Rectangle itemRect = layerListBox.GetItemRectangle(index);
                Rectangle visibilityRect = new Rectangle(
                    itemRect.X + 5,
                    itemRect.Y + (itemRect.Height - 16) / 2,
                    16,
                    16
                );
                
                if (visibilityRect.Contains(e.Location))
                {
                    // Toggle visibility
                    _layers[index].Visible = !_layers[index].Visible;
                    
                    // Refresh the display
                    layerListBox.Invalidate(itemRect);
                    
                    // Raise the event
                    OnLayerVisibilityChanged(_layers[index]);
                }
            }
        }
        
        /// <summary>
        /// Raises the LayerSelected event
        /// </summary>
        protected virtual void OnLayerSelected(Layer layer)
        {
            LayerSelected?.Invoke(this, new LayerEventArgs(layer));
        }
        
        /// <summary>
        /// Raises the LayerVisibilityChanged event
        /// </summary>
        protected virtual void OnLayerVisibilityChanged(Layer layer)
        {
            LayerVisibilityChanged?.Invoke(this, new LayerEventArgs(layer));
        }
        
        /// <summary>
        /// Raises the LayersReordered event
        /// </summary>
        protected virtual void OnLayersReordered()
        {
            LayersReordered?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// Handles the Add Layer button click
        /// </summary>
        private void addLayerButton_Click(object sender, EventArgs e)
        {
            // This would typically be handled by the parent form
            // which would create a new layer and add it
        }
        
        /// <summary>
        /// Handles the Remove Layer button click
        /// </summary>
        private void removeLayerButton_Click(object sender, EventArgs e)
        {
            if (_selectedLayerIndex >= 0)
            {
                RemoveLayer(_selectedLayerIndex);
            }
        }
        
        /// <summary>
        /// Handles the Move Up button click
        /// </summary>
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (_selectedLayerIndex > 0)
            {
                MoveLayerUp(_selectedLayerIndex);
            }
        }
        
        /// <summary>
        /// Handles the Move Down button click
        /// </summary>
        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (_selectedLayerIndex >= 0 && _selectedLayerIndex < _layers.Count - 1)
            {
                MoveLayerDown(_selectedLayerIndex);
            }
        }
    }
    
    /// <summary>
    /// Event arguments for layer events
    /// </summary>
    public class LayerEventArgs : EventArgs
    {
        public Layer Layer { get; }
        
        public LayerEventArgs(Layer layer)
        {
            Layer = layer;
        }
    }
}