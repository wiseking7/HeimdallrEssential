using Newtonsoft.Json;

namespace Heimdallr.Desing.Geometies;

public class GeometryRoot
{
  [JsonProperty("geometries")]
  public List<GeometryItem>? Items { get; set; }
}
