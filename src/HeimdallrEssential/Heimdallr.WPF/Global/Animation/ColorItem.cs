using Heimdallr.WPF.Global.Enums;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.WPF.Global.Animation;

/* WPF 애니메이션에서 색상 애니메이션을 정의하기 위해 사용됩니다. 
   이 클래스는 ColorAnimation 을 상속하고 있으며, WPF 에서 애니메이션을 제어하기 위해 다양한 속성과 이징 함수를 다루고 있습니다. */
public class ColorItem : ColorAnimation
{
  #region TargetName
  /// <summary>
  /// TargetName 종속성 속성을 등록, 이 속성은 애니메이션이 적용될 대상의 이름을 지정
  /// OnTargetNameChanged -> TargetName 이 변경될 때 호출되며, 애니메이션의 대상 개체를 설정합니다.
  /// </summary>
  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      "TargetName",
      typeof(string),
      typeof(ColorItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  public string TargetName
  {
    get { return (string)GetValue(TargetNameProperty); }
    set { SetValue(TargetNameProperty, value); }
  }
  #endregion

  #region Property
  /// <summary>
  /// Property 종속성 속성을 등록, 이 속성은 애니메이션의 속성 경로를 지정
  /// OnPropertyChanged -> Property가 변경될 때 호출, 이 메서드는 Storyboard.SetTargetProperty 를 사용하여 
  /// 애니메이션의 적용 속성을 설정합니다.
  /// </summary>
  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      "Property",
      typeof(PropertyPath),
      typeof(ColorItem),
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
    var item = (ColorItem)d;
    var targetName = (string)e.NewValue;

    Storyboard.SetTargetName(item, targetName);
  }

  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ColorItem)d;
    var propertyPath = (PropertyPath)e.NewValue;

    Storyboard.SetTargetProperty(item, propertyPath);
  }

  #region Mode
  /// <summary>
  /// Mode 종속성 속성 등록, 애니메이션에 적용할 OnEasingModeChanged 의 모드를 정의
  /// OnEasingModeChanged -> Mode 속성이 변경될 때 호출됩니다. 
  /// 이 메서드는 EasingFunctionBaseMode 에 따라 해당하는 이징 함수(IEasingFunction)를 설정
  /// </summary>
  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      "Mode",
      typeof(EasingFunctionBaseMode),
      typeof(ColorItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  public EasingFunctionBaseMode Mode
  {
    get { return (EasingFunctionBaseMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  #endregion

  /// <summary>
  /// 이 메서드는 Mode 속성이 변경될 때 호출됩니다
  /// Mode에 설정된 값에 따라 적절한 이징 함수를 선택하여 적용합니다.
  /// EasingFunction이 CubicEase인 경우에는 이미 해당 이징 함수의 EasingMode를 설정하고, 
  /// 그렇지 않으면 새로운 이징 함수를 설정합니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ColorItem)d;
    var easingMode = (EasingFunctionBaseMode)e.NewValue;

    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(easingMode);
      return;
    }

    item.EasingFunction = GetEasingFunc(easingMode);
  }

  /// <summary>
  /// 이 메서드는 EasingFunctionBaseMode 값에 맞는 이징 함수(IEasingFunction)를 생성하고 반환합니다.
  /// GetMode와 GetFunctonBase 메서드를 호출하여 EasingMode와 적절한 EasingFunctionBase를 설정합니다
  /// </summary>
  /// <param name="mode"></param>
  /// <returns></returns>
  private static IEasingFunction GetEasingFunc(EasingFunctionBaseMode mode)
  {
    EasingMode easingMode = GetMode(mode);
    EasingFunctionBase easingFunctionBase = GetFunctonBase(mode);

    easingFunctionBase.EasingMode = easingMode;

    return (IEasingFunction)easingFunctionBase;
  }

  /// <summary>
  /// EasingFunctionBaseMode 값에 따라 적절한 이징 함수 객체를 반환합니다. 
  /// 예를 들어 BackEase, BounceEase, CubicEase 등의 이징 함수를 반환합니다. 
  /// EaseIn, EaseOut, EaseInOut 구분에 따라 각각의 함수가 반환됩니다.
  /// </summary>
  /// <param name="mode"></param>
  /// <returns></returns>
  private static EasingFunctionBase GetFunctonBase(EasingFunctionBaseMode mode)
  {
    var enumString = mode.ToString()
      .Replace("EaseInOut", "") // 애니메이션이 처음에 느리게 시작하고, 중간에 빨라졌다가, 끝에 다시 느려짐. 
      .Replace("EaseIn", "")    // 애니메이션이 처음에 느리게 시작해서 점차 빨라짐.
      .Replace("EaseOut", "");  //애니메이션이 처음에 빠르게 시작해서 점차 느려짐.

    // 예상 가능한 값에 대해서는 기본값을 반환합니다.
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

  /// <summary>
  /// EasingFunctionBaseMode 값에 따라 EasingMode를 설정합니다. EaseInOut, EaseIn, EaseOut에 맞춰 이징 모드를 설정합니다.
  /// </summary>
  /// <param name="mode"></param>
  /// <returns></returns>
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
