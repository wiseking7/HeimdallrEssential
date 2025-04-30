using System.Windows;

namespace Heimdallr.Forms.UI.Views;

public class MainWindow : Window
{
  static MainWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow),
      new FrameworkPropertyMetadata(typeof(MainWindow)));
  }
}
