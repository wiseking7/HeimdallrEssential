using Heimdallr.WPF.Evemts;
using Heimdallr.WPF.Global.Interfaces;
using Heimdallr.WPF.Global.Models;
using Heimdallr.WPF.Global.WPF.Controls;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using YamlDotNet.Serialization;

namespace Heimdallr.WPF.Global.Composition;

/// <summary>
/// Prism 프레임워크 기반 WPF 앱에서 테마 및 언어 리소스를 로딩, 전환, 관리하는 역할
/// </summary>
public class ResourceManager : IResourceManager
{
  private string _currentTheme;
  private string _currentLanguage;
  private readonly HeimdallrApplication _app;
  private readonly BaseResourceInitializer _themeInitializer;
  private readonly IEventHub _eventHub;

  internal Dictionary<string, ResourceDictionary> ThemeResources { get; private set; }
  internal Dictionary<string, ResourceDictionary> LanguageResources { get; private set; }
  internal List<ThemeModel> Themes { get; private set; }
  internal List<ThemeModel> Languages { get; private set; }

  /// <summary>
  /// 생성자 앱 실행 시 테마 및 언어 관련 리소스를 초기화, 초기 테마 및 언어 설정을 적용
  /// </summary>
  /// <param name="app"></param>
  /// <param name="themeInitializer"></param>
  /// <param name="eventHub"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public ResourceManager(HeimdallrApplication app,
                         BaseResourceInitializer themeInitializer,
                         IEventHub eventHub)
  {
    _app = app ?? throw new ArgumentNullException(nameof(app));
    _themeInitializer = themeInitializer ?? throw new ArgumentNullException(nameof(themeInitializer));
    _eventHub = eventHub ?? throw new ArgumentNullException(nameof(eventHub));

    _currentTheme = _themeInitializer.DefaultThemeName;
    _currentLanguage = _themeInitializer.DefaultLocale;

    ThemeResources = LoadResources(_themeInitializer.ThemePath);
    LanguageResources = LoadResources(_themeInitializer.LocalePath);

    Themes = GetResourceList(ThemeResources);
    this.Languages = GetResourceList(LanguageResources);

    SwitchTheme(_themeInitializer.DefaultThemeName);
    SwitchLanguage(_themeInitializer.DefaultLocale);
  }

  /// <summary>
  /// 리소스파일(YAML 등) 로드하여, 색상, 폰트, 언어 정보를 ResourceDictionary 형태로 변환
  /// </summary>
  /// <param name="resourcePath"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  /// <exception cref="FileNotFoundException"></exception>
  private Dictionary<string, ResourceDictionary> LoadResources(string resourcePath)
  {
    var result = new Dictionary<string, ResourceDictionary>();

    var assembly = Assembly.GetEntryAssembly()
                 ?? throw new InvalidOperationException("EntryAssembly를 가져올 수 없습니다.");

    using var stream = assembly.GetManifestResourceStream(resourcePath)
                      ?? throw new FileNotFoundException($"리소스를 찾을 수 없습니다: {resourcePath}");

    using var reader = new StreamReader(stream);
    var yaml = reader.ReadToEnd();

    var deserializer = new Deserializer();
    var themeObject = deserializer.Deserialize<ThemeRoot>(new StringReader(yaml));

    if (themeObject == null)
    {
      throw new InvalidOperationException("테마 파일 디시리얼라이징 실패");
    }

    ProcessThemeData(themeObject, result);
    return result;
  }

  /// <summary>
  /// YAML에서 파싱한 ThemeRoot 데이터에서 실제 색상, 폰트, 언어 정보를 ResourceDictionary로 분리하여 저장합니다.
  /// </summary>
  /// <param name="themeObject"></param>
  /// <param name="result"></param>
  private void ProcessThemeData(ThemeRoot themeObject, Dictionary<string, ResourceDictionary> result)
  {
    if (themeObject.Themes != null)
    {
      foreach (var theme in themeObject.Themes)
      {
        foreach (var prop in theme.Colors!.GetType().GetProperties())
        {
          var colorValue = theme.Colors.Get(prop.Name);
          var color = (Color)ColorConverter.ConvertFromString(colorValue);

          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][theme.Key] = color;
        }
      }
    }

    if (themeObject.Fonts != null)
    {
      foreach (var theme in themeObject.Fonts)
      {
        foreach (var prop in theme.Fonts!.GetType().GetProperties())
        {
          var fontValue = theme.Fonts.Get(prop.Name);
          var font = new FontFamily(fontValue);

          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][theme.Key] = font;
        }
      }
    }

    if (themeObject.Languages != null)
    {
      foreach (var lang in themeObject.Languages)
      {
        foreach (var prop in lang.Fonts!.GetType().GetProperties())
        {
          var text = lang.Fonts.Get(prop.Name);

          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][lang.Key] = text;
        }
      }
    }
  }

  /// <summary>
  /// Theme/Language의 Key 값을 바탕으로 리스트 생성 (콤보박스 등 UI 바인딩용).
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  private List<ThemeModel> GetResourceList(Dictionary<string, ResourceDictionary> source)
  {
    return source.Select(kvp => new ThemeModel(kvp.Key, kvp.Key)).ToList();
  }

  /// <summary>
  /// 현재 앱에서 사용 중인 테마를 다른 테마로 전환합니다. 
  /// </summary>
  /// <param name="value"></param>
  public void SwitchTheme(string value)
  {
    if (ThemeResources.TryGetValue(_currentTheme, out var oldTheme))
    {
      _app.Resources.MergedDictionaries.Remove(oldTheme);
    }

    if (ThemeResources.TryGetValue(value, out var newTheme))
    {
      _app.Resources.MergedDictionaries.Add(newTheme);
      _currentTheme = value;
      _eventHub.Publish<SwitchThemePubsub, string>(value);
    }
  }

  /// <summary>
  /// 현재 앱의 언어 리소스를 전환합니다.
  /// </summary>
  /// <param name="value"></param>
  public void SwitchLanguage(string value)
  {
    if (LanguageResources.TryGetValue(_currentLanguage, out var oldLang))
    {
      _app.Resources.MergedDictionaries.Remove(oldLang);
    }

    if (LanguageResources.TryGetValue(value, out var newLang))
    {
      _app.Resources.MergedDictionaries.Add(newLang);
      _currentLanguage = value;
      _eventHub.Publish<SwitchLanguagePubsub, string>(value);
    }
  }
}
