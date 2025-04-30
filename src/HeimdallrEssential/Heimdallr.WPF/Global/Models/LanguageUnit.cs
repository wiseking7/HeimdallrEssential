﻿using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Models;
/// <summary>
/// 다국어 지원 기능을 구성하기 위해 설계된 클래스입니다. 
/// 주로 여러 언어에 대한 문자열 값을 하나의 객체로 관리하고, 코드에서 언어 코드로 값을 동적으로 가져올 수 있도록 하기 위한 목적
/// </summary>
public class LanguageUnit
{
  [YamlMember(Alias = "usa")]
  public string? USA { get; set; }
  [YamlMember(Alias = "kor")]
  public string? KOR { get; set; }
  [YamlMember(Alias = "chn")]
  public string? CHN { get; set; }
  [YamlMember(Alias = "jpn")]
  public string? JPN { get; set; }
  [YamlMember(Alias = "vnm")]
  public string? VNM { get; set; }
  [YamlMember(Alias = "esp")]
  public string? ESP { get; set; }

  internal string? Get(string item)
  {
    switch (item.ToUpper())
    {
      case "USA": return USA;
      case "KOR": return KOR;
      case "CHN": return CHN;
      case "JPN": return JPN;
      case "VNM": return VNM;
      case "ESP": return ESP;
    }

    // // 일관성 있게 초기화할 수 있음
    return string.Empty;
  }
}
