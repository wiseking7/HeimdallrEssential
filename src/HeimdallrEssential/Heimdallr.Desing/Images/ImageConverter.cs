using System.Runtime.CompilerServices;

namespace Heimdallr.Desing.Images;

public static class ImageConverter
{
  public static string GetData([CallerMemberName] string? name = null)
  {
    // name이 null인 경우 예외 처리 (예를 들어 CallerMemberName 사용시 name이 null일 수 있음)
    if (name == null)
    {
      throw new ArgumentNullException(nameof(name), "CallerMemberName은 null일 수 없습니다");
    }

    // ImageContainer._items에서 name에 해당하는 항목이 존재하는지 확인
    if (ImageContainer._items == null || !ImageContainer._items.ContainsKey(name))
    {
      throw new KeyNotFoundException($"이름: '{name}'인 Image Item을 찾을 수 없습니다");
    }

    // _items에서 name에 해당하는 ImageItem을 가져와 Data를 반환
    var item = ImageContainer._items[name];

    // item.Data가 null이거나 빈 문자열일 경우 예외 처리
    if (string.IsNullOrEmpty(item.Data))
    {
      throw new InvalidOperationException($"Image Item '{name}'에 대한 Data를 사용할 수 없습니다");
    }

    return item.Data;
  }
}

