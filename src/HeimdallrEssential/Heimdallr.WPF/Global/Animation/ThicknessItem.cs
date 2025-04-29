using Heimdallr.WPF.Global.Enums;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.WPF.Global.Animation;

/* Margin, Padding, BorderThickness 등의 Thickness 속성에 애니메이션을 적용할 때 사용 */
public class ThicknessItem : ThicknessAnimation
{
  #region TargetName Storyboard의 타겟 요소 이름을 설정
  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      "TargetName",
      typeof(string),
      typeof(ThicknessItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  public string TargetName
  {
    get => (string)GetValue(TargetNameProperty);
    set => SetValue(TargetNameProperty, value);
  }

  private static void OnTargetNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessItem)d;
    var newName = e.NewValue as string;

    if (!string.IsNullOrWhiteSpace(newName))
      Storyboard.SetTargetName(item, newName);

    else
      throw new ArgumentNullException(nameof(TargetName), "TargetName은 null 또는 빈 문자열일 수 없습니다.");
  }

  #endregion

  #region Property 애니메이션이 적용될 속성 경로 지정 (Margin, Padding 등)
  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      "Property",
      typeof(PropertyPath),
      typeof(ThicknessItem),
      new PropertyMetadata(null, OnPropertyChanged)
  );

  public PropertyPath Property
  {
    get => (PropertyPath)GetValue(PropertyProperty);
    set => SetValue(PropertyProperty, value);
  }

  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessItem)d;
    var path = e.NewValue as PropertyPath;

    if (path != null)
      Storyboard.SetTargetProperty(item, path);

    else
      throw new ArgumentNullException(nameof(Property), "PropertyPath는 null일 수 없습니다.");
  }

  #endregion

  #region Mode 사용자 정의 EasingFunctionBaseMode Enum을 통해 다양한 이징 함수 지원

  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      "Mode",
      typeof(EasingFunctionBaseMode),
      typeof(ThicknessItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  public EasingFunctionBaseMode Mode
  {
    get => (EasingFunctionBaseMode)GetValue(ModeProperty);
    set => SetValue(ModeProperty, value);
  }

  /// <summary>
  /// CubicEase는 기본 설정일 수 있으므로 성능을 위해 다시 인스턴스화하지 않음
  /// 그렇지 않으면 새로운 easing 함수 개체를 설정
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessItem)d;

    if (e.NewValue is not EasingFunctionBaseMode mode)
      throw new ArgumentNullException(nameof(Mode), "EasingFunctionBaseMode 값이 유효하지 않습니다.");

    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(mode);
    }
    else
    {
      item.EasingFunction = GetEasingFunction(mode);
    }
  }

  private static IEasingFunction GetEasingFunction(EasingFunctionBaseMode mode)
  {
    var easingFunction = GetFunctionBase(mode);
    easingFunction.EasingMode = GetMode(mode);
    return easingFunction;
  }

  /// <summary>
  /// Enum 문자열을 기반으로 WPF의 EasingFunctionBase 구현체를 생성
  /// switch 구문 사용으로 가독성 및 오류 감지 우수
  /// </summary>
  /// <param name="mode"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  private static EasingFunctionBase GetFunctionBase(EasingFunctionBaseMode mode)
  {
    var enumString = mode.ToString()
        .Replace("EaseInOut", "")
        .Replace("EaseIn", "")
        .Replace("EaseOut", "");

    return enumString switch
    {
      "Back" => new BackEase(),
      "Bounce" => new BounceEase(),
      "Circle" => new CircleEase(),
      "Cubic" => new CubicEase(),
      "Elastic" => new ElasticEase(),
      "Exponential" => new ExponentialEase(),
      "Power" => new PowerEase(),
      "Quadratic" => new QuadraticEase(),
      "Quartic" => new QuarticEase(),
      "Quintic" => new QuinticEase(),
      "Sine" => new SineEase(),
      _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "정의되지 않은 이징 모드입니다.")
    };
  }

  /// <summary>
  /// EasingMode (In, Out, InOut)를 자동 매핑합니다
  /// </summary>
  /// <param name="mode"></param>
  /// <returns></returns>
  private static EasingMode GetMode(EasingFunctionBaseMode mode)
  {
    string name = mode.ToString();

    if (name.Contains("EaseInOut"))
      return EasingMode.EaseInOut;
    else if (name.Contains("EaseIn"))
      return EasingMode.EaseIn;
    else
      return EasingMode.EaseOut;
  }

  #endregion
}
