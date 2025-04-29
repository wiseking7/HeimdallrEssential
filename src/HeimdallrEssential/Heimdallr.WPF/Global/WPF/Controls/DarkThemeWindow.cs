using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.WPF.Global.WPF.Controls;

/// <summary>
/// DarkThemeWindow 클래스는 WPF 기반의 커스텀 창을 정의하며, 일반적인 기능(닫기, 최소화, 최대화, 드래그 이동)과 
/// 함께 다크 테마, 디밍 처리(어두워짐), 태스크바 표시 여부 제어 같은 기능들을 포함
/// </summary>
public class DarkThemeWindow : HeimdallrWindow
{
  // WPF의 바인딩/스타일/트리거에 활용될 수 있도록 선언된 속성
  // 주로 뷰(View)와 XAML 리소스 바인딩을 위한 의존 속성
  public static readonly DependencyProperty PopupOpenProperty;
  public static readonly DependencyProperty TitleHeaderBackgroundProperty;
  public static readonly DependencyProperty CloseCommandProperty;
  public static readonly new DependencyProperty TitleProperty;
  public static readonly DependencyProperty IsShowTaskBarProperty;

  /// <summary>
  /// 윈도우 타이틀 (WPF Window.Title 숨김 처리)
  /// </summary>
  public new object Title
  {
    get => GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  /// <summary>
  /// 닫기 버튼 클릭 시 실행할 명령
  /// </summary>
  public ICommand CloseCommand
  {
    get => (ICommand)GetValue(CloseCommandProperty);
    set => SetValue(CloseCommandProperty, value);
  }

  /// <summary>
  /// 타이틀 헤더 배경색
  /// </summary>
  public Brush TitleHeaderBackground
  {
    get => (Brush)GetValue(TitleHeaderBackgroundProperty);
    set => SetValue(TitleHeaderBackgroundProperty, value);
  }

  /// <summary>
  /// 최대화 시 태스크바 위로 창이 올라올지 여부
  /// </summary>
  public bool IsShowTaskBar
  {
    get { return (bool)GetValue(IsShowTaskBarProperty); }
    set { SetValue(IsShowTaskBarProperty, value); }
  }

  /// <summary>
  /// 팝업 열기/닫기 상태
  /// </summary>
  public bool PopupOpen
  {
    get => (bool)GetValue(PopupOpenProperty);
    set => SetValue(PopupOpenProperty, value);
  }

  #region Dimming
  public static readonly DependencyProperty DimmingProperty;
  public static readonly DependencyProperty DimmingColorProperty;
  public static readonly DependencyProperty DimmingOpacityProperty;

  /// <summary>
  /// 디밍 효과 여부 (배경 어둡게 처리)
  /// </summary>
  public bool Dimming
  {
    get => (bool)GetValue(DimmingProperty);
    set => SetValue(DimmingProperty, value);
  }

  /// <summary>
  /// 디밍 배경색
  /// </summary>
  public Brush DimmingColor
  {
    get => (Brush)GetValue(DimmingColorProperty);
    set => SetValue(DimmingColorProperty, value);
  }

  /// <summary>
  /// 디밍 투명도
  /// </summary>
  public double DimmingOpacity
  {
    get => (double)GetValue(DimmingOpacityProperty);
    set => SetValue(DimmingOpacityProperty, value);
  }
  #endregion

  private MaximizeButton? maximBtn;

  /// <summary>
  /// 투명창 설정 (AllowsTransparency) + 타이틀바 제거 (WindowStyle.None) → 커스텀 타이틀 영역 사용 가능
  /// 최대화 상태 변화 감지: StateChanged 이벤트로 최대화 버튼 상태 연동
  /// </summary>
  static DarkThemeWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkThemeWindow),
      new FrameworkPropertyMetadata(typeof(DarkThemeWindow)));

    CloseCommandProperty = DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(DarkThemeWindow),
      new PropertyMetadata(null));

    TitleProperty = DependencyProperty.Register(nameof(Title), typeof(object), typeof(DarkThemeWindow),
      new UIPropertyMetadata(null));

    TitleHeaderBackgroundProperty = DependencyProperty.Register(nameof(TitleHeaderBackground), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#252525"))));

    DimmingProperty = DependencyProperty.Register(nameof(Dimming), typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, (e, a) =>
    {
      //Console.WriteLine ("");
    }));

    DimmingColorProperty = DependencyProperty.Register(nameof(DimmingColor), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#141414"))));

    DimmingOpacityProperty = DependencyProperty.Register(nameof(DimmingOpacity), typeof(double), typeof(DarkThemeWindow),
      new PropertyMetadata(0.8));

    IsShowTaskBarProperty = DependencyProperty.Register("IsShowTaskBar", typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(true, (d, e) =>
    {
      var win = (DarkThemeWindow)d;
      win.MaxHeightSet();
    }));

    PopupOpenProperty = DependencyProperty.Register("PopupOpen", typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, OnPopupOpenChanged));
  }

  private static void OnPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DarkThemeWindow window)
    {
      bool isOpen = (bool)e.NewValue;
      // 예시: 팝업 열기/닫기 처리
      if (isOpen)
      {
        // 팝업 열기 로직
      }
      else
      {
        // 팝업 닫기 로직
      }
    }
  }

  public DarkThemeWindow()
  {
    MaxHeightSet();

    this.AllowsTransparency = true;
    this.WindowStyle = WindowStyle.None;

    this.StateChanged += (s, e) =>
    {
      maximBtn!.IsMaximize = !maximBtn.IsMaximize;
    };
  }

  /// <summary>
  /// ControlTemplate에 정의된 버튼(PART_CloseButton, PART_MinButton, PART_MaxButton, PART_DragBar)에 이벤트 바인딩
  /// 사용자 지정 UI에 윈도우 기능 버튼 바인딩하는 핵심 메서드, 드래그 이동, 창 상태 변경 구현
  /// </summary>
  public override void OnApplyTemplate()
  {
    if (GetTemplateChild("PART_CloseButton") is CloseButton btn)
    {
      btn.Click += (s, e) => WindowClose();
    }

    if (GetTemplateChild("PART_MinButton") is MinimizeButton minbtn)
    {
      minbtn.Click += (s, e) => WindowState = WindowState.Minimized;
    }

    if (GetTemplateChild("PART_MaxButton") is MaximizeButton maxbtn)
    {
      maximBtn = maxbtn;
      maxbtn.Click += (s, e) =>
      {
        this.WindowState = maxbtn.IsMaximize ? WindowState.Normal : WindowState.Maximized;
      };
    }

    if (GetTemplateChild("PART_DragBar") is DraggableBar bar)
    {
      bar.MouseDown += WindowDragMove;
    }

    maximBtn!.IsMaximize = this.WindowState == WindowState.Maximized;
  }

  /// <summary>
  /// 닫기 명령처리 Command 없으면 기본 Close 호출
  /// </summary>
  private void WindowClose()
  {
    if (CloseCommand == null)
    {
      Close();
    }
    else
    {
      CloseCommand.Execute(this);
    }
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
  }

  /// <summary>
  /// 드래그바 클릭 시 창 이동 기능
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void WindowDragMove(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton == MouseButtonState.Pressed)
    {
      GetWindow(this).DragMove();
    }
  }

  /// <summary>
  /// 창이 최대화될 때 태스크바 포함 여부 설정 (SystemParameters)
  /// </summary>
  private void MaxHeightSet()
  {
    this.MaxHeight = IsShowTaskBar ? SystemParameters.MaximizedPrimaryScreenHeight : Double.PositiveInfinity;
  }
}
