//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.1.1144.0 using the following command:
// xscgen -n BcfToolkit.Model.Bcf21 --nullable visinfo.xsd
namespace BcfToolkit.Model.Bcf21
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("OrthogonalCamera", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OrthogonalCamera
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraViewPoint")]
        public Point CameraViewPoint { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraDirection")]
        public Direction CameraDirection { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraUpVector")]
        public Direction CameraUpVector { get; set; }
        
        /// <summary>
        /// <para>view's visible size in meters</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("view\'s visible size in meters")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("ViewToWorldScale")]
        public double ViewToWorldScale { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Point", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Point
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("X")]
        public double X { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Y")]
        public double Y { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Z")]
        public double Z { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Direction", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Direction
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("X")]
        public double X { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Y")]
        public double Y { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Z")]
        public double Z { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("PerspectiveCamera", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PerspectiveCamera
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraViewPoint")]
        public Point CameraViewPoint { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraDirection")]
        public Direction CameraDirection { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("CameraUpVector")]
        public Direction CameraUpVector { get; set; }
        
        /// <summary>
        /// <para>It is currently limited to a value between 45 and 60 degrees.
        ///						This limitation will be dropped in the next release and viewers
        ///						should be expect values outside this range in current implementations.</para>
        /// <para xml:lang="en">Minimum inclusive value: 45.</para>
        /// <para xml:lang="en">Maximum inclusive value: 60.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("It is currently limited to a value between 45 and 60 degrees. This limitation wil" +
            "l be dropped in the next release and viewers should be expect values outside thi" +
            "s range in current implementations.")]
        [System.ComponentModel.DataAnnotations.RangeAttribute(typeof(double), "45", "60")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("FieldOfView")]
        public double FieldOfView { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Components", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Components
    {
        
        [System.Xml.Serialization.XmlElementAttribute("ViewSetupHints")]
        public ViewSetupHints ViewSetupHints { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Component> _selection;
        
        [System.Xml.Serialization.XmlArrayAttribute("Selection")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Component")]
        public System.Collections.ObjectModel.Collection<Component> Selection
        {
            get
            {
                return _selection;
            }
            private set
            {
                _selection = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Selection collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SelectionSpecified
        {
            get
            {
                return (this.Selection.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Components" /> class.</para>
        /// </summary>
        public Components()
        {
            this._selection = new System.Collections.ObjectModel.Collection<Component>();
            this._coloring = new System.Collections.ObjectModel.Collection<ComponentColoringColor>();
        }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Visibility")]
        public ComponentVisibility Visibility { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ComponentColoringColor> _coloring;
        
        [System.Xml.Serialization.XmlArrayAttribute("Coloring")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Color")]
        public System.Collections.ObjectModel.Collection<ComponentColoringColor> Coloring
        {
            get
            {
                return _coloring;
            }
            private set
            {
                _coloring = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Coloring collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ColoringSpecified
        {
            get
            {
                return (this.Coloring.Count != 0);
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ViewSetupHints", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ViewSetupHints
    {
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlAttributeAttribute("SpacesVisible")]
        public bool SpacesVisibleValue { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the SpacesVisible property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public bool SpacesVisibleValueSpecified { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public System.Nullable<bool> SpacesVisible
        {
            get
            {
                if (this.SpacesVisibleValueSpecified)
                {
                    return this.SpacesVisibleValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.SpacesVisibleValue = value.GetValueOrDefault();
                this.SpacesVisibleValueSpecified = value.HasValue;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlAttributeAttribute("SpaceBoundariesVisible")]
        public bool SpaceBoundariesVisibleValue { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the SpaceBoundariesVisible property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public bool SpaceBoundariesVisibleValueSpecified { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public System.Nullable<bool> SpaceBoundariesVisible
        {
            get
            {
                if (this.SpaceBoundariesVisibleValueSpecified)
                {
                    return this.SpaceBoundariesVisibleValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.SpaceBoundariesVisibleValue = value.GetValueOrDefault();
                this.SpaceBoundariesVisibleValueSpecified = value.HasValue;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlAttributeAttribute("OpeningsVisible")]
        public bool OpeningsVisibleValue { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the OpeningsVisible property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public bool OpeningsVisibleValueSpecified { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public System.Nullable<bool> OpeningsVisible
        {
            get
            {
                if (this.OpeningsVisibleValueSpecified)
                {
                    return this.OpeningsVisibleValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OpeningsVisibleValue = value.GetValueOrDefault();
                this.OpeningsVisibleValueSpecified = value.HasValue;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ComponentSelection", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ComponentSelection
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Component> _component;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Component")]
        public System.Collections.ObjectModel.Collection<Component> Component
        {
            get
            {
                return _component;
            }
            private set
            {
                _component = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ComponentSelection" /> class.</para>
        /// </summary>
        public ComponentSelection()
        {
            this._component = new System.Collections.ObjectModel.Collection<Component>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Component", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Component
    {
        
        [System.Xml.Serialization.XmlElementAttribute("OriginatingSystem")]
        public string OriginatingSystem { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("AuthoringToolId")]
        public string AuthoringToolId { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 22.</para>
        /// <para xml:lang="en">Maximum length: 22.</para>
        /// <para xml:lang="en">Pattern: [0-9,A-Z,a-z,_$]*.</para>
        /// </summary>
        [System.ComponentModel.DataAnnotations.MinLengthAttribute(22)]
        [System.ComponentModel.DataAnnotations.MaxLengthAttribute(22)]
        [System.ComponentModel.DataAnnotations.RegularExpressionAttribute("[0-9,A-Z,a-z,_$]*")]
        [System.Xml.Serialization.XmlAttributeAttribute("IfcGuid", Namespace="", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string IfcGuid { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ComponentVisibility", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ComponentVisibility
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Component> _exceptions;
        
        [System.Xml.Serialization.XmlArrayAttribute("Exceptions")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Component")]
        public System.Collections.ObjectModel.Collection<Component> Exceptions
        {
            get
            {
                return _exceptions;
            }
            private set
            {
                _exceptions = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Exceptions collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExceptionsSpecified
        {
            get
            {
                return (this.Exceptions.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ComponentVisibility" /> class.</para>
        /// </summary>
        public ComponentVisibility()
        {
            this._exceptions = new System.Collections.ObjectModel.Collection<Component>();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlAttributeAttribute("DefaultVisibility")]
        public bool DefaultVisibilityValue { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the DefaultVisibility property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public bool DefaultVisibilityValueSpecified { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public System.Nullable<bool> DefaultVisibility
        {
            get
            {
                if (this.DefaultVisibilityValueSpecified)
                {
                    return this.DefaultVisibilityValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.DefaultVisibilityValue = value.GetValueOrDefault();
                this.DefaultVisibilityValueSpecified = value.HasValue;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ComponentVisibilityExceptions", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ComponentVisibilityExceptions
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Component> _component;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Component")]
        public System.Collections.ObjectModel.Collection<Component> Component
        {
            get
            {
                return _component;
            }
            private set
            {
                _component = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ComponentVisibilityExceptions" /> class.</para>
        /// </summary>
        public ComponentVisibilityExceptions()
        {
            this._component = new System.Collections.ObjectModel.Collection<Component>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ComponentColoring", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ComponentColoring
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ComponentColoringColor> _color;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Color")]
        public System.Collections.ObjectModel.Collection<ComponentColoringColor> Color
        {
            get
            {
                return _color;
            }
            private set
            {
                _color = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ComponentColoring" /> class.</para>
        /// </summary>
        public ComponentColoring()
        {
            this._color = new System.Collections.ObjectModel.Collection<ComponentColoringColor>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ComponentColoringColor", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ComponentColoringColor
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Component> _component;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Component")]
        public System.Collections.ObjectModel.Collection<Component> Component
        {
            get
            {
                return _component;
            }
            private set
            {
                _component = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ComponentColoringColor" /> class.</para>
        /// </summary>
        public ComponentColoringColor()
        {
            this._component = new System.Collections.ObjectModel.Collection<Component>();
        }
        
        /// <summary>
        /// <para xml:lang="en">Pattern: [0-9,A-F]{6}([0-9,A-F]{2})?.</para>
        /// </summary>
        [System.ComponentModel.DataAnnotations.RegularExpressionAttribute("[0-9,A-F]{6}([0-9,A-F]{2})?")]
        [System.Xml.Serialization.XmlAttributeAttribute("Color", Namespace="", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Color { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Line", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Line
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("StartPoint")]
        public Point StartPoint { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("EndPoint")]
        public Point EndPoint { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ClippingPlane", Namespace="")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ClippingPlane
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Location")]
        public Point Location { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Direction")]
        public Direction Direction { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BitmapFormat", Namespace="")]
    public enum BitmapFormat
    {
        
        [System.Xml.Serialization.XmlEnumAttribute("PNG")]
        Png,
        
        [System.Xml.Serialization.XmlEnumAttribute("JPG")]
        Jpg,
    }
    
    /// <summary>
    /// <para>VisualizationInfo documentation</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("VisualizationInfo documentation")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("VisualizationInfo", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("VisualizationInfo", Namespace="")]
    public partial class VisualizationInfo
    {
        
        [System.Xml.Serialization.XmlElementAttribute("Components")]
        public Components Components { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("OrthogonalCamera")]
        public OrthogonalCamera OrthogonalCamera { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("PerspectiveCamera")]
        public PerspectiveCamera PerspectiveCamera { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Line> _lines;
        
        [System.Xml.Serialization.XmlArrayAttribute("Lines")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Line")]
        public System.Collections.ObjectModel.Collection<Line> Lines
        {
            get
            {
                return _lines;
            }
            private set
            {
                _lines = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Lines collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LinesSpecified
        {
            get
            {
                return (this.Lines.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="VisualizationInfo" /> class.</para>
        /// </summary>
        public VisualizationInfo()
        {
            this._lines = new System.Collections.ObjectModel.Collection<Line>();
            this._clippingPlanes = new System.Collections.ObjectModel.Collection<ClippingPlane>();
            this._bitmap = new System.Collections.ObjectModel.Collection<VisualizationInfoBitmap>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ClippingPlane> _clippingPlanes;
        
        [System.Xml.Serialization.XmlArrayAttribute("ClippingPlanes")]
        [System.Xml.Serialization.XmlArrayItemAttribute("ClippingPlane")]
        public System.Collections.ObjectModel.Collection<ClippingPlane> ClippingPlanes
        {
            get
            {
                return _clippingPlanes;
            }
            private set
            {
                _clippingPlanes = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the ClippingPlanes collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClippingPlanesSpecified
        {
            get
            {
                return (this.ClippingPlanes.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<VisualizationInfoBitmap> _bitmap;
        
        [System.Xml.Serialization.XmlElementAttribute("Bitmap")]
        public System.Collections.ObjectModel.Collection<VisualizationInfoBitmap> Bitmap
        {
            get
            {
                return _bitmap;
            }
            private set
            {
                _bitmap = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Bitmap collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BitmapSpecified
        {
            get
            {
                return (this.Bitmap.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Pattern: [a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}.</para>
        /// </summary>
        [System.ComponentModel.DataAnnotations.RegularExpressionAttribute("[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlAttributeAttribute("Guid")]
        public string Guid { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("VisualizationInfoLines", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VisualizationInfoLines
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Line> _line;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Line")]
        public System.Collections.ObjectModel.Collection<Line> Line
        {
            get
            {
                return _line;
            }
            private set
            {
                _line = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="VisualizationInfoLines" /> class.</para>
        /// </summary>
        public VisualizationInfoLines()
        {
            this._line = new System.Collections.ObjectModel.Collection<Line>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("VisualizationInfoClippingPlanes", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VisualizationInfoClippingPlanes
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ClippingPlane> _clippingPlane;
        
        [System.Xml.Serialization.XmlElementAttribute("ClippingPlane")]
        public System.Collections.ObjectModel.Collection<ClippingPlane> ClippingPlane
        {
            get
            {
                return _clippingPlane;
            }
            private set
            {
                _clippingPlane = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the ClippingPlane collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClippingPlaneSpecified
        {
            get
            {
                return (this.ClippingPlane.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="VisualizationInfoClippingPlanes" /> class.</para>
        /// </summary>
        public VisualizationInfoClippingPlanes()
        {
            this._clippingPlane = new System.Collections.ObjectModel.Collection<ClippingPlane>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1144.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("VisualizationInfoBitmap", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VisualizationInfoBitmap
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Bitmap")]
        public BitmapFormat Bitmap { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public string Reference { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Location")]
        public Point Location { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Normal")]
        public Direction Normal { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Up")]
        public Direction Up { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlElementAttribute("Height")]
        public double Height { get; set; }
    }
}
