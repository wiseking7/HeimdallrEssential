using System.Diagnostics;
using System.Windows;

namespace Heimdallr.App;

internal class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    Window win = new Window();
    win.Title = "HEIMDALLR";

    // Loaded 이벤트 핸들러
    win.Loaded += (s, e) =>
    {
      Debug.WriteLine($"Width: {win.ActualWidth}, Height: {win.ActualHeight}");
    };

    win.Show();
  }


}
