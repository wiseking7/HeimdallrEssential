using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.WPF.Global.WPF.Controls;

public class CloseButton : Button
{
  static CloseButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton),
      new FrameworkPropertyMetadata(typeof(CloseButton)));
  }
}
