namespace Heimdallr.WPF.Global.Composition;

/// <summary>
/// WPF 또는 기타 .NET 애플리케이션에서 테마 및 로케일(언어/문화 설정) 관련 자원의 기본 경로와 설정을 초기화하기 위한 추상 클래스
/// 추상 메서드 4개를 통해 동적으로 값을 가져와서, 생성자에서 해당 값을 초기화
/// 이 클래스는 직접 인스턴스화할 수 없고, 상속을 통해 구체 클래스에서 사용
/// </summary>
public abstract class BaseResourceInitializer
{
  // 읽기 전용 속성
  public string ThemePath { get; } // 테마 리스소 파일 경로
  public string DefaultLocale { get; }  // 기본 로케일 설정 (예: "en-US", "ko-KR")
  public string DefaultThemeName { get; } // 기본 테마 이름 (예: "Dark", "Light")
  public string LocalePath { get; } //지역화 리소스 파일 경로 (예: Resources/Locales) 

  // 추상메서드 -> 메서드들은 반드시 자식 클래스에서 구현해야(설정 파일에서 값을 읽거나,
  // 리소스 구조에 따라 경로를 계산하는 등의 작업을 하게 됩니다
  protected abstract string FetchThemePath(); // 테마 경로 가져오기
  protected abstract string DetermineDefaultThemeName(); // 기본 테마 이름 결정

  protected abstract string FetchLocalePath(); // 로케일 경로 가져오기
  protected abstract string DetermineDefaultLocale(); // 기본 로케일 결정

  /// <summary>
  /// 추상 메서드를 호출해 각 속성을 초기화
  /// 정적 구성 값을 코드에 하드코딩하지 않고 유연하게 처리할 수 있도록 합니다.
  /// 예를 들어 App.xaml.cs에서 AppResourceInitializer : BaseResourceInitializer 같은 클래스를 만들어 구성 가능
  /// </summary>
  public BaseResourceInitializer()
  {
    ThemePath = FetchThemePath();
    DefaultThemeName = DetermineDefaultThemeName();
    LocalePath = FetchLocalePath();
    DefaultLocale = DetermineDefaultLocale();
  }
}
/* 사용예제
public class AppResourceInitializer : BaseResourceInitializer
{
  protected override string FetchThemePath() => "Themes/";
  protected override string DetermineDefaultThemeName() => "Dark";
  protected override string FetchLocalePath() => "Locales/";
  protected override string DetermineDefaultLocale() => "ko-KR";
} */
