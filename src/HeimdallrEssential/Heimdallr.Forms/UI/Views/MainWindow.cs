using Heimdallr.WPF.Global.WPF.Controls;
using System.Windows;

namespace Heimdallr.Forms.UI.Views;

public class MainWindow : DarkThemeWindow
{
  static MainWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow),
      new FrameworkPropertyMetadata(typeof(MainWindow)));
  }
}
