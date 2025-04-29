using System.ComponentModel;
using System.Windows;

namespace Heimdallr.WPF.Global.Interfaces;

/// <summary>
/// View(UserControl, Window, Page) UI 요소에 접근, 
/// ViewModel(View에 연결된 ViewModel 개체로 데이터바인딩 지원 ViewModel의 속성이 바뀌면 View가 자동으로 갱신됩니다.)
/// </summary>
public interface IViewable
{
  FrameworkElement View { get; }
  INotifyPropertyChanged ViewModel { get; }
}