using System;
using System.Drawing;

namespace Image_Markup_Tool.Components.Layers
{
    /// <summary>
    /// Represents a layer in the image editor
    /// </summary>
    public class Layer
    {
        private string _name;
        private Image _image;
        private Image _thumbnail;
        private bool _visible = true;
        private float _opacity = 1.0f;
        
        /// <summary>
        /// Creates a new layer with the specified name and image
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <param name="image">The image for the layer</param>
        public Layer(string name, Image image)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _image = image ?? throw new ArgumentNullException(nameof(image));
            
            // Generate thumbnail
            GenerateThumbnail();
        }
        
        /// <summary>
        /// Gets or sets the name of the layer
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        /// <summary>
        /// Gets or sets the image for the layer
        /// </summary>
        public Image Image
        {
            get => _image;
            set
            {
                _image = value ?? throw new ArgumentNullException(nameof(value));
                GenerateThumbnail();
            }
        }
        
        /// <summary>
        /// Gets the thumbnail for the layer
        /// </summary>
        public Image Thumbnail => _thumbnail;
        
        /// <summary>
        /// Gets or sets whether the layer is visible
        /// </summary>
        public bool Visible
        {
            get => _visible;
            set => _visible = value;
        }
        
        /// <summary>
        /// Gets or sets the opacity of the layer (0.0 to 1.0)
        /// </summary>
        public float Opacity
        {
            get => _opacity;
            set => _opacity = Math.Max(0, Math.Min(1, value));
        }
        
        /// <summary>
        /// Generates a thumbnail for the layer
        /// </summary>
        private void GenerateThumbnail()
        {
            if (_image == null)
                return;
                
            // Dispose of existing thumbnail
            if (_thumbnail != null)
            {
                _thumbnail.Dispose();
                _thumbnail = null;
            }
            
            // Create a new thumbnail
            const int maxSize = 40;
            int width = _image.Width;
            int height = _image.Height;
            
            // Calculate thumbnail dimensions
            if (width > height)
            {
                height = (int)((float)height * maxSize / width);
                width = maxSize;
            }
            else
            {
                width = (int)((float)width * maxSize / height);
                height = maxSize;
            }
            
            // Create the thumbnail
            _thumbnail = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(_thumbnail))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(_image, 0, 0, width, height);
            }
        }
    }
}