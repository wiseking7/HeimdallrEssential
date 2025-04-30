using Heimdallr.Forms.UI.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Heimdallr.App;

internal class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    MainWindow win = new();
    win.Title = "HEIMDALLR";

    //Title 색상변경
    win.TitleHeaderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7A1CAC"));

    // 매그러운 모서리 
    //win.SnapsToDevicePixels = false;
    //win.UseLayoutRounding = true;

    // 헤더만 흐림효과 적용
    //win.Dimming = true;
    //win.DimmingOpacity = 4.0;
    //win.DimmingColor = new SolidColorBrush(Color.FromArgb(128, 20, 20, 20)); // 반투명한 검정

    // Background 변경(Content)
    var brush = new BrushConverter().ConvertFrom("#FF393E46") as Brush;
    if (brush != null)
    {
      win.Background = brush;
    }

    // Loaded 이벤트 핸들러
    win.Loaded += (s, e) =>
    {
      Debug.WriteLine($"Width: {win.ActualWidth}, Height: {win.ActualHeight}");
    };

    win.Show();
  }
}
