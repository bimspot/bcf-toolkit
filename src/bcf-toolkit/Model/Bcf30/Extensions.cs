//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.0.878.0 using the following command:
// xscgen -n bcf.bcf30 extensions.xsd
namespace BcfToolkit.Model.Bcf30
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Extensions", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Extensions", Namespace="")]
    public partial class Extensions
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicTypes;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("TopicTypes", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("TopicType", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicTypes
        {
            get
            {
                return _topicTypes;
            }
            private set
            {
                _topicTypes = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicTypes collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicTypesSpecified
        {
            get
            {
                return (this.TopicTypes.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Extensions" /> class.</para>
        /// </summary>
        public Extensions()
        {
            this._topicTypes = new System.Collections.ObjectModel.Collection<string>();
            this._topicStatuses = new System.Collections.ObjectModel.Collection<string>();
            this._priorities = new System.Collections.ObjectModel.Collection<string>();
            this._topicLabels = new System.Collections.ObjectModel.Collection<string>();
            this._users = new System.Collections.ObjectModel.Collection<string>();
            this._snippetTypes = new System.Collections.ObjectModel.Collection<string>();
            this._stages = new System.Collections.ObjectModel.Collection<string>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicStatuses;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("TopicStatuses", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("TopicStatus", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicStatuses
        {
            get
            {
                return _topicStatuses;
            }
            private set
            {
                _topicStatuses = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicStatuses collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicStatusesSpecified
        {
            get
            {
                return (this.TopicStatuses.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _priorities;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("Priorities", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Priority", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> Priorities
        {
            get
            {
                return _priorities;
            }
            private set
            {
                _priorities = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Priorities collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrioritiesSpecified
        {
            get
            {
                return (this.Priorities.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicLabels;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("TopicLabels", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("TopicLabel", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicLabels
        {
            get
            {
                return _topicLabels;
            }
            private set
            {
                _topicLabels = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicLabels collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicLabelsSpecified
        {
            get
            {
                return (this.TopicLabels.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _users;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("Users", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("User", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> Users
        {
            get
            {
                return _users;
            }
            private set
            {
                _users = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Users collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UsersSpecified
        {
            get
            {
                return (this.Users.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _snippetTypes;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("SnippetTypes", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SnippetType", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> SnippetTypes
        {
            get
            {
                return _snippetTypes;
            }
            private set
            {
                _snippetTypes = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the SnippetTypes collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SnippetTypesSpecified
        {
            get
            {
                return (this.SnippetTypes.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _stages;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlArrayAttribute("Stages", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Stage", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> Stages
        {
            get
            {
                return _stages;
            }
            private set
            {
                _stages = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Stages collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StagesSpecified
        {
            get
            {
                return (this.Stages.Count != 0);
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsTopicTypes", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsTopicTypes
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicType;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("TopicType", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicType
        {
            get
            {
                return _topicType;
            }
            private set
            {
                _topicType = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicType collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicTypeSpecified
        {
            get
            {
                return (this.TopicType.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsTopicTypes" /> class.</para>
        /// </summary>
        public ExtensionsTopicTypes()
        {
            this._topicType = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsTopicStatuses", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsTopicStatuses
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicStatus;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("TopicStatus", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicStatus
        {
            get
            {
                return _topicStatus;
            }
            private set
            {
                _topicStatus = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicStatus collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicStatusSpecified
        {
            get
            {
                return (this.TopicStatus.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsTopicStatuses" /> class.</para>
        /// </summary>
        public ExtensionsTopicStatuses()
        {
            this._topicStatus = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsPriorities", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsPriorities
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _priority;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("Priority", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> Priority
        {
            get
            {
                return _priority;
            }
            private set
            {
                _priority = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Priority collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrioritySpecified
        {
            get
            {
                return (this.Priority.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsPriorities" /> class.</para>
        /// </summary>
        public ExtensionsPriorities()
        {
            this._priority = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsTopicLabels", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsTopicLabels
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _topicLabel;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("TopicLabel", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> TopicLabel
        {
            get
            {
                return _topicLabel;
            }
            private set
            {
                _topicLabel = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TopicLabel collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopicLabelSpecified
        {
            get
            {
                return (this.TopicLabel.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsTopicLabels" /> class.</para>
        /// </summary>
        public ExtensionsTopicLabels()
        {
            this._topicLabel = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsUsers", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsUsers
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _user;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("User", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> User
        {
            get
            {
                return _user;
            }
            private set
            {
                _user = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the User collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserSpecified
        {
            get
            {
                return (this.User.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsUsers" /> class.</para>
        /// </summary>
        public ExtensionsUsers()
        {
            this._user = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsSnippetTypes", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsSnippetTypes
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _snippetType;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("SnippetType", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> SnippetType
        {
            get
            {
                return _snippetType;
            }
            private set
            {
                _snippetType = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the SnippetType collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SnippetTypeSpecified
        {
            get
            {
                return (this.SnippetType.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsSnippetTypes" /> class.</para>
        /// </summary>
        public ExtensionsSnippetTypes()
        {
            this._snippetType = new System.Collections.ObjectModel.Collection<string>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.878.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ExtensionsStages", Namespace="", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtensionsStages
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _stage;
        
        /// <summary>
        /// <para xml:lang="en">Minimum length: 1.</para>
        /// </summary>
        // NOTIFICATION: this data annotation wrongly works
        // [System.ComponentModel.DataAnnotations.MinLengthAttribute(1)]
        [System.Xml.Serialization.XmlElementAttribute("Stage", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.Collections.ObjectModel.Collection<string> Stage
        {
            get
            {
                return _stage;
            }
            private set
            {
                _stage = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Stage collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StageSpecified
        {
            get
            {
                return (this.Stage.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ExtensionsStages" /> class.</para>
        /// </summary>
        public ExtensionsStages()
        {
            this._stage = new System.Collections.ObjectModel.Collection<string>();
        }
    }
}
