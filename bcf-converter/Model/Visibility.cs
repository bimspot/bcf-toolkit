using System.Collections.Generic;

namespace bcf_converter.Model {
  /// <summary>
  ///   The Visibility element states the components DefaultVisibility and
  ///   lists all Exceptions that apply to specific components.
  /// </summary>
  public struct Visibility {
    /// <summary>
    ///   The default visibility for elements of certain types.
    /// </summary>
    public ViewSetupHints viewSetupHints;

    /// <summary>
    ///   The default visibility of the elements in the BCF.
    /// </summary>
    public bool defaultVisibility;

    /// <summary>
    ///   A list of elements of which visibility is other then the default one.
    /// </summary>
    public List<Component> exceptions;

    /// <summary>
    ///   Creates and returns an instance of the Visibility.
    /// </summary>
    /// <param name="viewSetupHints"></param>
    /// <param name="defaultVisibility"></param>
    /// <param name="exceptions"></param>
    public Visibility(ViewSetupHints viewSetupHints, bool defaultVisibility,
      List<Component> exceptions) {
      this.viewSetupHints = viewSetupHints;
      this.defaultVisibility = defaultVisibility;
      this.exceptions = exceptions;
    }

    /// <summary>
    ///   This element contains information about the default visibility for
    ///   elements of certain types (SpacesVisible, SpaceBoundariesVisible and
    ///   OpeningsVisible) that should be applied if not stated otherwise.
    /// </summary>
    public struct ViewSetupHints {
      /// <summary>
      ///   The default visibility of the spaces.
      /// </summary>
      public bool spacesVisible;

      /// <summary>
      ///   The default visibility of the space boundaries.
      /// </summary>
      public bool spaceBoundariesVisible;

      /// <summary>
      ///   The default visibility of the openings.
      /// </summary>
      public bool openingsVisible;

      /// <summary>
      ///   Creates and returns an instance of the ViewSetupHints.
      /// </summary>
      /// <param name="spacesVisible">
      ///   The default visibility of the spaces.
      /// </param>
      /// <param name="spaceBoundariesVisible">
      ///   The default visibility of the space boundaries.
      /// </param>
      /// <param name="openingsVisible">
      ///   The default visibility of the openings.
      /// </param>
      public ViewSetupHints(bool spacesVisible, bool spaceBoundariesVisible,
        bool openingsVisible) {
        this.spacesVisible = spacesVisible;
        this.spaceBoundariesVisible = spaceBoundariesVisible;
        this.openingsVisible = openingsVisible;
      }
    }
  }
}