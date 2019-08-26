using System;
using System.Xml.Linq;
using bcf2json.Model;

namespace bcf2json {
  class Program {
    static void main(string[] args) {
      if (args.Length < 1) {
        Console
          .WriteLine("Please specify the path to the BCFZIP.");
        Console.WriteLine(@"
          Usage:
          
          $ bcf2json /path/to/some.bcfzip

        ");
        Environment.Exit(1);
        
      }
      
      
      // Unzip
        // Loop through folders
          // Load markup.bcf
            // Parse topic information:
            //   Guid, TopicType, TopicStatus
            //   Title, CreationDate, CreationAuthor, Description
            
            // Parse viewpoints
            //  load referenced snapshot into Base64
              // snapshot_type, snapshot_data
            //  parse referenced viewpoint.bcfv 2.0
            
              // Components
                // Collect all visible ("selection")
                  // ifc_guid, originating_system, authoring_tool_id
                  
                // Collect all selected ("visibility")
                  // default_visibility, exceptions, view_setup_hints
                
                // Collect all colours ("coloring")
                  // color, components
            // PerspectiveCamera
    }
  }
}