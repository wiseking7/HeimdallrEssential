using Heimdallr.WPF.Global.Enums;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.WPF.Global.Animation;

/* WPF 에서 애니메이션을 제어하기 위한 사용자 정의 클래스 ValueItem 을 정의한 것입니다. 
   DoubleAnimation을 상속받아, 더 정교한 애니메이션 제어를 가능하게 합니다. 
   특히 이 클래스는 Storyboard 에서 사용할 수 있도록 대상 속성과 대상 이름, 
   그리고 이징 함수(Easing Function)를 지정할 수 있게 설계되어 있습니다. 
   숫자 값(double)의 변화 애니메이션을 정의 
   예: 위치, 크기, 투명도, 각도 등 수치 변화가 필요한 모든 UI 속성에 사용 가능.*/
public class ValueItem : DoubleAnimation
{
  #region TargetName

  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      "TargetName",
      typeof(string),
      typeof(ValueItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  public string TargetName
  {
    get { return (string)GetValue(TargetNameProperty); }
    set { SetValue(TargetNameProperty, value); }
  }
  #endregion

  #region Property

  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      "Property",
      typeof(PropertyPath),
      typeof(ValueItem),
      new PropertyMetadata(null, OnPropertyChanged)
  );

  public PropertyPath Property
  {
    get { return (PropertyPath)GetValue(PropertyProperty); }
    set { SetValue(PropertyProperty, value); }
  }
  #endregion

  private static void OnTargetNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ValueItem)d;
    var targetName = (string)e.NewValue;

    Storyboard.SetTargetName(item, targetName);
  }

  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ValueItem)d;
    var propertyPath = (PropertyPath)e.NewValue;

    Storyboard.SetTargetProperty(item, propertyPath);
  }

  #region Mode

  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      "Mode",
      typeof(EasingFunctionBaseMode),
      typeof(ValueItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  public EasingFunctionBaseMode Mode
  {
    get { return (EasingFunctionBaseMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  #endregion

  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ValueItem)d;
    var easingMode = (EasingFunctionBaseMode)e.NewValue;

    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(easingMode);
      return;
    }

    item.EasingFunction = GetEasingFunc(easingMode);
  }
  private static IEasingFunction GetEasingFunc(EasingFunctionBaseMode mode)
  {
    EasingMode easingMode = GetMode(mode);
    EasingFunctionBase easingFunctionBase = GetFunctonBase(mode);

    easingFunctionBase.EasingMode = easingMode;

    return (IEasingFunction)easingFunctionBase;
  }

  private static EasingFunctionBase GetFunctonBase(EasingFunctionBaseMode mode)
  {
    var enumString = mode.ToString()
      .Replace("EaseInOut", "")
      .Replace("EaseIn", "")
      .Replace("EaseOut", "");

    if (enumString == "Back")
      return new BackEase();
    if (enumString == "Bounce")
      return new BounceEase();
    if (enumString == "Circle")
      return new CircleEase();
    if (enumString == "Cubic")
      return new CubicEase();
    if (enumString == "Elastic")
      return new ElasticEase();
    if (enumString == "Exponential")
      return new ExponentialEase();
    if (enumString == "Power")
      return new PowerEase();
    if (enumString == "Quadratic")
      return new QuadraticEase();
    if (enumString == "Quartic")
      return new QuarticEase();
    if (enumString == "Quintic")
      return new QuinticEase();
    if (enumString == "Sine")
      return new SineEase();

    // 기본값 반환 : 예를 들어 CubicEase 반환
    /* CubicEase -> 이징 함수 (Easing Function) 중 하나, 이 클래스는 애니메이션의 진행 속도(타이밍)를 제어하는 데 사용
       이징 함수는 애니메이션의 진행 과정을 다르게 하여 더 자연스럽고 미려한 효과를 만듭니다. 
       특히, CubicEase는 3차 방정식(Cubic Equation)을 기반으로 하여 애니메이션이 진행되는 속도를 조절합니다.
      */
    var defaultFunction = new CubicEase();

    // 예상되지 않는 값이 들어왔다면 예외를 던져서 문제를 알려줍니다.
    if (enumString != "Back" && enumString != "Bounce" && enumString != "Circle" &&
        enumString != "Cubic" && enumString != "Elastic" && enumString != "Exponential" &&
        enumString != "Power" && enumString != "Quadratic" && enumString != "Quartic" &&
        enumString != "Quintic" && enumString != "Sine")
    {
      throw new ArgumentOutOfRangeException(
          $"예기치 않은 EasingFunctionBaseMode: {mode}. 기본값을 반환합니다: {defaultFunction.GetType().Name}");
    }

    return defaultFunction; // 기본값을 반환
  }

  private static EasingMode GetMode(EasingFunctionBaseMode mode)
  {
    var enumString = mode.ToString();

    if (enumString.Contains("EaseInOut"))
      return EasingMode.EaseInOut;
    else if (enumString.Contains("EaseIn"))
      return EasingMode.EaseIn;

    return EasingMode.EaseOut;
  }
}
/* 사용예
 
<local:ValueItem 
  TargetName="MyButton"
  Property="RenderTransform.(RotateTransform.Angle)"
  Mode="BackEaseOut"
  From="0"
  To="360"
  Duration="0:0:2" />




 
 */