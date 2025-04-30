using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.WPF.Global.WPF.Controls;

public class HeimdallrToggleSwitch : ToggleButton
{
  #region CornerRadius
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrToggleSwitch),
          new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  #endregion
}
