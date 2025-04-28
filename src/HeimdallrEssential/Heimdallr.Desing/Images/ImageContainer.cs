using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Heimdallr.Desing.Images;

public class ImageContainer
{
  public static ImageRoot? _data;
  public static Dictionary<string, ImageItem>? _items;

  public ImageContainer()
  {
    Build();
  }

  private static void Build()
  {
    Assembly assembly = Assembly.GetExecutingAssembly();
    var resourceName = "Jamesnet.Design.Properties.Resources.images.json"; // JSON 파일의 경로로 수정

    using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new StreamReader(stream!))
    {
      string jsonText = reader.ReadToEnd();  // JSON 파일을 문자열로 읽음

      // JSON 문자열을 ImageRoot 객체로 디시리얼라이즈
      _data = JsonConvert.DeserializeObject<ImageRoot>(jsonText);

      // _data.Items의 각 항목을 Dictionary에 추가
      _items = new Dictionary<string, ImageItem>();

      // _data 또는 _data.Items가 null이 아닐 경우에만 실행
      if (_data?.Items != null)
      {
        foreach (var item in _data.Items)
        {
          // item.Name이 null일 수 있으므로 null 체크
          if (!string.IsNullOrEmpty(item.Name))
          {
            _items.Add(item.Name, item);
          }
        }
      }
    }
  }
}
