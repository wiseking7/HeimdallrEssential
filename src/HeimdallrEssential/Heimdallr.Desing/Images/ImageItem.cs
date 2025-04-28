using Newtonsoft.Json;

namespace Heimdallr.Desing.Images;

public class ImageItem
{
  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("data")]
  public string? Data { get; set; }
}
