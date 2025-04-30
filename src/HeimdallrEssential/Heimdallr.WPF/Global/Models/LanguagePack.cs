using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Models;

/// <summary>
/// 언어 
/// </summary>
public class LanguagePack
{
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }
  [YamlMember(Alias = "items")]
  public LanguageUnit? Fonts { get; set; }
}
