using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Heimdallr.Desing.Geometies;

public static class GeometryContainer
{
  public static Dictionary<string, GeometryItem>? _items;

  static GeometryContainer()
  {
    Build();
  }

  // JSON을 로드하고 _items 딕셔너리를 생성
  private static void Build()
  {
    // JSON 데이터 로드 (예: 리소스에서)
    string jsonData = LoadJson(); // JSON을 파일에서 로드하는 메서드 호출

    // GeometryRoot로 디시리얼라이즈
    var geometryRoot = JsonConvert.DeserializeObject<GeometryRoot>(jsonData);

    _items = new Dictionary<string, GeometryItem>();

    // Items가 null이 아니면 각 항목을 딕셔너리에 추가
    if (geometryRoot?.Items != null)
    {
      foreach (var item in geometryRoot.Items)
      {
        if (!string.IsNullOrEmpty(item.Name))
        {
          _items[item.Name] = item; // Name을 Key로, GeometryItem을 Value로 딕셔너리에 추가
        }
      }
    }
  }

  // JSON 파일을 리소스나 파일에서 로드하는 메서드
  private static string LoadJson()
  {
    // 리소스 또는 파일에서 JSON을 읽어오는 방법 (수정 필요)
    Assembly assembly = Assembly.GetExecutingAssembly();
    string resourceName = "Heimdallr.Desing.Properties.Resources.geometries.json"; // JSON 파일 경로

    // 경로 확인용 로그 출력
    Debug.WriteLine($"Resource 경로: {resourceName}");

    // 리소스를 찾을 수 있는지 확인
    using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
    {
      if (stream == null)
      {
        throw new FileNotFoundException($"리소스 파일을 찾을 수 없습니다: {resourceName}");
      }

      using (StreamReader reader = new StreamReader(stream))
      {
        return reader.ReadToEnd();
      }
    }
  }
}
