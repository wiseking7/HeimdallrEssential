using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Models;

/// <summary>
/// 테마, 폰트, 언어
/// </summary>
public class ThemeRoot
{
  [YamlMember(Alias = "themes")]
  public List<ThemePack>? Themes { get; set; }
  [YamlMember(Alias = "fonts")]
  public List<FontPack>? Fonts { get; set; }
  [YamlMember(Alias = "languages")]
  public List<LanguagePack>? Languages { get; set; }
}
