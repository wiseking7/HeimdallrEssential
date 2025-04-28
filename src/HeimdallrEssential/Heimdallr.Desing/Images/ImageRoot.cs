using Newtonsoft.Json;

namespace Heimdallr.Desing.Images;

public class ImageRoot
{
  [JsonProperty("images")]
  public List<ImageItem>? Items { get; set; }
}
