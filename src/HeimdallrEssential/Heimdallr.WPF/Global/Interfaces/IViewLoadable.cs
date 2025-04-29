namespace Heimdallr.WPF.Global.Interfaces;

/// <summary>
/// WPF MVVM 패턴에서 ViewModel이 View의 Loaded 이벤트와 연결되었을 때 실행할 후처리 로직을 정의하는 데 사용
/// </summary>
public interface IViewLoadable
{
  void OnLoaded(IViewable view);
}
