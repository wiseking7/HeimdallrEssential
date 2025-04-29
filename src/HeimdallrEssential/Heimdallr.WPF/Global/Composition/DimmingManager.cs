using Heimdallr.WPF.Global.WPF.Controls;
using System.Windows;

namespace Heimdallr.WPF.Global.Composition;
/// <summary>
/// 디밍 상태를 컨트롤하기 위한 헬퍼 클래스
/// 전역적으로 ViewModel 또는 코드에서 UI에 의존 없이 디밍 상태를 바꾸고 싶을 때 활용됩니다.
/// </summary>
public class DimmingManager
{
  public DimmingManager()
  {

  }
  public void Dimming(bool isDimming)
  {
    // Application.Current.MainWindow가 DarkThemeWindow 타입인지 확인
    if (Application.Current.MainWindow is DarkThemeWindow window)
    {
      // 맞다면 Dimming 속성을 동적으로 제어 → 뷰모델 또는 다른 클래스에서 쉽게 디밍 제어 가능
      window.Dimming = isDimming;
    }
  }
}
