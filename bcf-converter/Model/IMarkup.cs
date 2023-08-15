namespace bcf;

public interface IMarkup {
  public ITopic? GetTopic();
  public IViewPoint? GetFirstViewPoint();
}

public interface ITopic {
  public string Guid { get; set; }
}

public interface IViewPoint {
  public IVisualizationInfo? VisualizationInfo { get; set; }
  public string? SnapshotData { get; set; }
  public string Snapshot { get; set; }
}

public interface IVisualizationInfo { }