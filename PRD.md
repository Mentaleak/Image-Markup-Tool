# Product Requirements Document (PRD)
## Image Editor Application

### Overview
This document outlines the requirements for a vector-based image editing application with layer support, multiple tools, and various export options. The application is designed with a modular architecture to facilitate component reuse and maintainability.
The Solution is located "src\Image-Markup-Tool.sln"

### Architecture Principles
1. **Modular Design**: Components will be separated into a hierarchy of folders for easy reuse and modification.
2. **Layer-Based Editing**: All tools create new layers when used, enabling non-destructive editing.
3. **Vector-First Approach**: All editing operations are vector-driven, not raster-based.

### User Interface Layout
- **Dark Mode**: The entire application will use a dark theme by default.
- **Menu Bar**: Located at the top of the application.
- **Three-Column Layout**:
  - **Left Column (Tool Panel)**: 70px wide, containing tools in 2 columns
  - **Middle Column (Editor Area)**: Fills remaining space, main editing canvas
  - **Right Column (Layer Panel)**: 132px wide, displaying and managing layers
- **Status Bar**: Located at the bottom of the application

### Menu Structure
#### File Menu
- Open
- New From Clipboard
- Save
- Export
- Help

### Export Options
- JPEG (.jpg)
- PNG (.png)
- SVG (.svg)
  - Special handling: First layer preserved as embedded PNG
  - All other layers maintained as SVG elements for future editing

### Color Management
- **Primary Color Picker**:
  - With alpha channel support
  - Default: #F23CCE (Solid, no alpha)
- **Secondary Color Picker**:
  - With alpha channel support
  - Default: Yellow (Highlighted, translucent)
- **Tertiary Color Picker**:
  - With alpha channel support
  - Default: Black (Solid)

### Tools
#### Selection Tools
- **Select Tool**:
  - Click on a drawn entity to highlight it
  - Shows resize points for manipulation
  - Delete key removes the selected entity and its layer

#### Drawing Tools
- **Arrow Tool**:
  - Left click drag: Draw using Primary Color
  - Right click drag: Draw using Secondary Color
  - Endpoints can be repositioned after creation

- **Line Tool**:
  - Left click drag: Draw using Primary Color
  - Right click drag: Draw using Secondary Color
  - Endpoints can be repositioned after creation

- **Box Tools**:
  - **Solid Box Tool**:
    - Left click drag: Draw using Primary Color
    - Right click drag: Draw using Secondary Color
    - Corner points can be repositioned after creation
  
  - **SemiSolid Box Tool**:
    - Left click drag: Primary Color outline with Secondary Color fill
    - Right click drag: Secondary Color outline with Primary Color fill
    - Corner points can be repositioned after creation
  
  - **Frame Box Tool**:
    - Left click drag: Draw frame using Primary Color
    - Right click drag: Draw frame using Secondary Color
    - Corner points can be repositioned after creation

- **Ellipse Tools**:
  - **Solid Ellipse Tool**:
    - Left click drag: Draw using Primary Color
    - Right click drag: Draw using Secondary Color
    - Control points can be repositioned after creation
  
  - **SemiSolid Ellipse Tool**:
    - Left click drag: Primary Color outline with Secondary Color fill
    - Right click drag: Secondary Color outline with Primary Color fill
    - Control points can be repositioned after creation
  
  - **Frame Ellipse Tool**:
    - Left click drag: Draw frame using Primary Color
    - Right click drag: Draw frame using Secondary Color
    - Control points can be repositioned after creation

#### Special Tools
- **Blur Tool**:
  - Left click drag: Draw box that applies blur effect
  - Right click drag: Draw box that applies pixelation effect
  - Box dimensions can be adjusted after creation

- **TextOutline Tool**:
  - Opens text editor window
  - Custom color settings:
    - Primary color (text): Default #F23CCE
    - Secondary color (outline): Default Black
  - Text can be positioned after creation

- **Cursor Tool**:
  - Left click: Apply cursor stamp
  - Right click: Apply cursor stamp with Primary Color outline

- **Steps Tool**:
  - Creates numbered circles with Primary Color background and Tertiary Color numbers
  - Can create bubble pointers by drag and drop
  - Grab points can be adjusted after creation
  - Auto-renumbers when steps are deleted

### Layer Management
- Each tool operation creates a new layer
- Layers can be selected, hidden, reordered
- Layers preserve all vector properties for future editing
- Deleting a layer removes the associated graphic element

### Technical Requirements
- Vector-based rendering engine
- Layer compositing system
- File format handlers for JPG, PNG, and SVG
- Undo/Redo functionality

### Folder Structure
The application will be organized into a hierarchical folder structure:
- `/components`: Reusable UI components
  - `/tools`: Individual tool implementations
  - `/panels`: Panel components (tool panel, layer panel)
  - `/dialogs`: Dialog components (color picker, text editor)
- `/models`: Data models for application entities
- `/services`: Application services (file handling, rendering)
- `/utils`: Utility functions and helpers
- `/styles`: Styling definitions and themes