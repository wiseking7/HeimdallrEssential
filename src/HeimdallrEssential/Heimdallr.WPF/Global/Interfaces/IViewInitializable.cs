namespace Heimdallr.WPF.Global.Interfaces;

/// <summary>
/// MVVM 아키텍처에서 ViewModel이 View와 연결되었을 때 실행될 초기화 작업을 정의하기 위해 사용됩니다. 주로 ViewModel의 수명주기 관리
/// </summary>
public interface IViewInitializable
{
  void OnViewWired(IViewable view);
}
