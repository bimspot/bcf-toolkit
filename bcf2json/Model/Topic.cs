using System.Collections.Generic;

namespace bcf2json.Model {
  /// <summary>
  ///   Topic node contains reference information of the topic.
  /// </summary>
  public struct Topic {
    /// <summary>
    ///   The unique Guid of the topic.
    /// </summary>
    public string guid;

    /// <summary>
    ///   Type of the topic (Predefined list in “extension.xsd”)
    /// </summary>
    public string type;

    /// <summary>
    ///   Type of the topic (Predefined list in “extension.xsd”)
    /// </summary>
    public string status;

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
    public string creationDate;

    /// <summary>
    ///   User who created the topic.
    /// </summary>
    public string creationAuthor;

    /// <summary>
    ///   Date when the topic was last modified. Exists only when Topic has
    ///   been modified after creation.
    /// </summary>
    public string modifiedDate;

    /// <summary>
    ///   User who modified the topic. Exists only when Topic has been modified
    ///   after creation.
    /// </summary>
    public string modifiedAuthor;

    /// <summary>
    ///   Date until when the topics issue needs to be resolved.
    /// </summary>
    public string dueDate;

    /// <summary>
    ///   Date until when the topics issue needs to be resolved.
    /// </summary>
    public string assignedTo;

    /// <summary>
    ///   Description of the topic.
    /// </summary>
    public string description;

    /// <summary>
    ///   The markup file can contain multiple viewpoints related to one or
    ///   more comments.
    /// </summary>
    public List<Viewpoints> viewpoints;
  }
}