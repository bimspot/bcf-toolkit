using System.Collections.Generic;
using System.Xml.Serialization;

namespace bcf_converter.Model {
  /// <summary>
  ///   Topic node contains reference information of the topic.
  /// </summary>
  [XmlRoot("Topic")]
  public struct Topic {
    /// <summary>
    ///   The unique Guid of the topic.
    /// </summary>
    [XmlAttribute]
    public string guid;

    /// <summary>
    ///   Type of the topic (Predefined list in “extension.xsd”)
    /// </summary>
    [XmlAttribute]
    [Newtonsoft.Json.JsonProperty("topic_type")]
    public string topicType;

    /// <summary>
    ///   Type of the topic (Predefined list in “extension.xsd”)
    /// </summary>
    [XmlAttribute]
    [Newtonsoft.Json.JsonProperty("topic_status")]
    public string topicStatus;

    /// <summary>
    ///   Title of the topic.
    /// </summary>
    public string title;

    /// <summary>
    ///   Topic priority. The list of possible values are defined in the
    ///   extension schema.
    /// </summary>
    public string priority;

    /// <summary>
    ///   Number to maintain the order of the topics.
    /// </summary>
    public string index;

    /// <summary>
    ///   Tags for grouping Topics. The list of possible values are defined
    ///   in the extension schema.
    /// </summary>
    public string labels;

    /// <summary>
    ///   Date when the topic was created.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("creation_date")]
    public string creationDate;

    /// <summary>
    ///   User who created the topic.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("creation_author")]
    public string creationAuthor;

    /// <summary>
    ///   Date when the topic was last modified. Exists only when Topic has
    ///   been modified after creation.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("modified_date")]
    public string modifiedDate;

    /// <summary>
    ///   User who modified the topic. Exists only when Topic has been modified
    ///   after creation.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("modified_author")]
    public string modifiedAuthor;

    /// <summary>
    ///   Date until when the topics issue needs to be resolved.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("due_date")]
    public string dueDate;

    /// <summary>
    ///   Date until when the topics issue needs to be resolved.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("assigned_to")]
    public string assignedTo;

    /// <summary>
    ///   Description of the topic.
    /// </summary>
    public string description;
  }
}