using Heimdallr.WPF.Global.Interfaces;
using System.ComponentModel;
using System.Windows;

namespace Heimdallr.WPF.Global.Composition;

/// <summary>
/// MVVM 패턴을 사용하는 WPF 애플리케이션에서 View와 ViewModel을 자동으로 연결(Auto-Wiring)해주는 기능
/// ViewModel 을 View 에 자동으로 바인딩하고, ViewModel 의 초기화 및 로딩 이벤트를 연결
/// </summary>
public class AutoWireManager
{
  /// <summary>
  /// View 를 보관하는 내부 필드
  /// readonly로 선언할 수 없음: InitializeAutoWire()에서 할당됨
  /// </summary>
  private FrameworkElement? _view;

  /// <summary>
  /// ViewModel 자동 연결을 시작하는 메서드입니다.
  /// </summary>
  /// <param name="view"></param>
  public void InitializeAutoWire(FrameworkElement view)
  {
    _view = view ?? throw new ArgumentNullException(nameof(view));

    ViewModelLocationProvider.AutoWireViewModelChanged(view, AutoWireViewModelChanged);
  }

  /// <summary>
  /// 실제로 ViewModel 을 연결하고 초기화, 로딩 이벤트를 바인딩하는 콜백입니다.
  /// </summary>
  /// <param name="view"></param>
  /// <param name="dataContext"></param>
  public virtual void AutoWireViewModelChanged(object view, object dataContext)
  {
    if (_view == null || dataContext == null)
      return;

    _view.DataContext = dataContext;

    if (dataContext is IViewInitializable viewModel)
    {
      if (view is IViewable viewable)
        viewModel.OnViewWired(viewable);
    }

    if (dataContext is IViewLoadable && view is FrameworkElement frameworkElement)
    {
      frameworkElement.Loaded += HeimdallrContent_Loaded;
    }
  }

  /// <summary>
  /// View 가 로딩되었을 때 ViewModel 의 후처리를 호출하는 메서드입니다
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HeimdallrContent_Loaded(object sender, RoutedEventArgs e)
  {
    if (sender is FrameworkElement frameworkElement && frameworkElement.DataContext is IViewLoadable viewmodel)
    {
      frameworkElement.Loaded -= HeimdallrContent_Loaded;

      if (frameworkElement is IViewable viewable)
        viewmodel.OnLoaded(viewable);
    }
  }

  /// <summary>
  /// 내부 View 반환
  /// </summary>
  /// <returns></returns>
  public FrameworkElement GetView()
  {
    return _view ?? throw new InvalidOperationException("View 가 초기화되지 않았습니다");
  }

  /// <summary>
  /// ViewModel(INotifyPropertyChanged) 반환
  /// </summary>
  /// <returns></returns>
  public INotifyPropertyChanged? GetDataContext()
  {
    return _view?.DataContext as INotifyPropertyChanged;
  }
}
