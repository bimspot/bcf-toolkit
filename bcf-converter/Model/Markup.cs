using System.Collections.Generic;

namespace bcf_converter.Model {
  public class Markup {
    public Header header;

    public Topic topic;

    // TODO: Comment
    public Viewpoints viewpoints;

    public Markup(Header header, Topic topic, Viewpoints viewpoints) {
      this.header = header;
      this.topic = topic;
      this.viewpoints = viewpoints;
    }
  }
}