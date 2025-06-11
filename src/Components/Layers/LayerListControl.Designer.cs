namespace Image_Markup_Tool.Components.Layers
{
    partial class LayerListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layerListBox = new System.Windows.Forms.ListBox();
            this.layerToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.layerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerButton = new System.Windows.Forms.ToolStripButton();
            this.moveUpButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownButton = new System.Windows.Forms.ToolStripButton();
            this.layerToolStrip.SuspendLayout();
            this.layerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // layerListBox
            // 
            this.layerListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.layerListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.layerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerListBox.ForeColor = System.Drawing.Color.White;
            this.layerListBox.FormattingEnabled = true;
            this.layerListBox.Location = new System.Drawing.Point(0, 25);
            this.layerListBox.Name = "layerListBox";
            this.layerListBox.Size = new System.Drawing.Size(132, 690);
            this.layerListBox.TabIndex = 0;
            this.layerListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LayerListBox_MouseClick);
            // 
            // layerToolStrip
            // 
            this.layerToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.layerToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.layerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeLayerButton,
            this.toolStripSeparator1,
            this.moveUpButton,
            this.moveDownButton});
            this.layerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.layerToolStrip.Name = "layerToolStrip";
            this.layerToolStrip.Size = new System.Drawing.Size(132, 25);
            this.layerToolStrip.TabIndex = 1;
            this.layerToolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // layerContextMenu
            // 
            this.layerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.propertiesToolStripMenuItem});
            this.layerContextMenu.Name = "layerContextMenu";
            this.layerContextMenu.Size = new System.Drawing.Size(128, 98);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // removeLayerButton
            // 
            this.removeLayerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeLayerButton.ForeColor = System.Drawing.Color.White;
            this.removeLayerButton.Image = global::Image_Markup_Tool.Properties.Resources._32_X;
            this.removeLayerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeLayerButton.Name = "removeLayerButton";
            this.removeLayerButton.Size = new System.Drawing.Size(23, 22);
            this.removeLayerButton.Text = "Remove Layer";
            this.removeLayerButton.Click += new System.EventHandler(this.removeLayerButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpButton.ForeColor = System.Drawing.Color.White;
            this.moveUpButton.Image = global::Image_Markup_Tool.Properties.Resources._32_up;
            this.moveUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(23, 22);
            this.moveUpButton.Text = "Move Up";
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownButton.ForeColor = System.Drawing.Color.White;
            this.moveDownButton.Image = global::Image_Markup_Tool.Properties.Resources._32_down;
            this.moveDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(23, 22);
            this.moveDownButton.Text = "Move Down";
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // LayerListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.layerListBox);
            this.Controls.Add(this.layerToolStrip);
            this.Name = "LayerListControl";
            this.Size = new System.Drawing.Size(132, 715);
            this.layerToolStrip.ResumeLayout(false);
            this.layerToolStrip.PerformLayout();
            this.layerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox layerListBox;
        private System.Windows.Forms.ToolStrip layerToolStrip;
        private System.Windows.Forms.ToolStripButton removeLayerButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton moveUpButton;
        private System.Windows.Forms.ToolStripButton moveDownButton;
        private System.Windows.Forms.ContextMenuStrip layerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    }
}