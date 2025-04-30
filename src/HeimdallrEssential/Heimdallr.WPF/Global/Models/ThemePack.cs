using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Models;

/// <summary>
/// 테마색상 키
/// </summary>
public class ThemePack
{
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }
  [YamlMember(Alias = "colors")]
  public SolidColorBrushUnit? Colors { get; set; }
}
