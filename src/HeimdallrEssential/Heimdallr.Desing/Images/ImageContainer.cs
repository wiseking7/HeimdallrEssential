using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;

namespace Heimdallr.Desing.Images;

public class ImageContainer
{
  internal static ImageRoot? _data;
  internal static Dictionary<string, ImageItem>? _items;

  static ImageContainer()
  {
    Build();
  }

  private static void Build()
  {
    // 리소스 스트림을 통해 YAML 데이터를 읽어오는 부분
    Assembly assembly = Assembly.GetExecutingAssembly();
    var resourceName = "Heimdallr.Desing.Properties.Resources.images.yaml";

    using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new StreamReader(stream!))
    {
      StringReader r = new StringReader(reader.ReadToEnd());
      Deserializer deserializer = new Deserializer();
      object yamlObject = deserializer.Deserialize<object>(r);

      JsonSerializer js = new JsonSerializer();
      StringWriter w = new StringWriter();
      js.Serialize(w, yamlObject);
      string jsonText = w.ToString();

      // YAML 을 Json 으로 변환한 후, _data에 값을 할당
      _data = JsonConvert.DeserializeObject<ImageRoot>(jsonText);
      _items = new Dictionary<string, ImageItem>();


      // _data가 null 이 아닌지 확인한 후 Items 에 대해 루프
      if (_data != null && _data.Items != null)
      {
        foreach (var item in _data.Items)
        {
          // _data.Items 가 null 일 수 있으므로 null 체크 후 추가
          if (item?.Name != null)
          {
            _items.Add(item.Name, item);
          }
        }
      }
    }
  }
}
