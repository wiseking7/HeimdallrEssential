﻿using Heimdallr.WPF.Global.WPF.Controls;

namespace Heimdallr.WPF.Global.Models;

/// <summary>
/// 테마와 관련된 정보를 담고 있는 모델 클래스로, 주로 테마 관리 시스템에서 사용될 수 있습니다. 
/// 이 클래스는 테마 코드, 테마 이름, 아이콘 타입, 값 등을 저장합니다. 
/// 이를 통해 테마 관련 데이터를 관리하고, 사용자가 선택한 테마에 맞는 값을 활용할 수 있습니다
/// </summary>
public class ThemeModel
{
  // 테마의 고유 코드입니다. 예를 들어, Black, Light, Dark 등의 테마를 나타낼 수 있습니다.
  public string Code { get; set; }

  // 테마의 이름을 나타내는 속성입니다. 예를 들어 Dark, Light 등의 설명적 이름이 될 수 있습니다.
  public string Theme { get; set; }

  // 테마와 연결된 아이콘의 종류를 나타냅니다. 이는 IconType이라는 열거형으로 정의될 수 있으며, 테마에 맞는 아이콘을 설정하는 데 사용됩니다.
  public IconType IconType { get; set; }

  // Code를 문자열로 변환하여 저장합니다. 예를 들어 Code가 "Dark"이면 Value는 "Dark" 문자열을 저장합니다.
  public string Value { get; set; }

  public ThemeModel(string code, string theme)
  {
    Code = code;
    Theme = theme;
    Value = code.ToString();
    //IconType = GetThemeIcon(code);
  }
  //private static IconType GetThemeIcon(string key)
  //{
  //    return key switch
  //    {
  //        ThemeMode.Black => IconType.Account,
  //        ThemeMode.Light => IconType.CalendarBlankOutline,
  //        _ => IconType.Account
  //    };
  //}
}
