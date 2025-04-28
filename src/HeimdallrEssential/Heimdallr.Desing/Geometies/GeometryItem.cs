using Newtonsoft.Json;

namespace Heimdallr.Desing.Geometies;

public class GeometryItem
{
  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("data")]
  public string? Data { get; set; }
}
