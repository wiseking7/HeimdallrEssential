using System.Runtime.CompilerServices;

namespace Heimdallr.Desing.Geometies;

public static class GeometryConverter
{
  public static string GetData([CallerMemberName] string? name = null)
  {
    if (name == null)
    {
      throw new ArgumentNullException(nameof(name), "CallerMemberName은 null일 수 없습니다.");
    }

    // GeometryContainer에서 해당 아이템을 찾기
    if (GeometryContainer._items == null || !GeometryContainer._items.ContainsKey(name))
    {
      throw new KeyNotFoundException($"Geometry Item을 찾을 수 없습니다: '{name}'.");
    }

    // 해당 아이템의 Data 반환
    var item = GeometryContainer._items[name];

    if (item == null || string.IsNullOrEmpty(item.Data))
    {
      throw new InvalidOperationException($"Geometry Item '{name}'에 대한 Data를 사용할 수 없습니다.");
    }

    return item.Data;
  }
}
