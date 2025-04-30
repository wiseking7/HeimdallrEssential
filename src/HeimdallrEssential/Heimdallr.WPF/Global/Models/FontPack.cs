using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Models;

/// <summary>
/// 폰트
/// </summary>
public class FontPack
{
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }
  [YamlMember(Alias = "fonts")]
  public FontFamilyUnit? Fonts { get; set; }
}
