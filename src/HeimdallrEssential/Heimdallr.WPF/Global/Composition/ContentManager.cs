using Heimdallr.WPF.Global.Interfaces;

namespace Heimdallr.WPF.Global.Composition;

/// <summary>
/// Region 기반의 View 전환을 간결하게 처리하기 위한 헬퍼 유틸리티 클래스입니다.
/// 종속성 주입 기반으로 구성되며, 동적 View 로딩, View 활성화/비활성화를 수행
/// </summary>
public class ContentManager
{
  private readonly IContainerProvider _containerProvider;
  private readonly IRegionManager _regionManager;

  /// <summary>
  /// Prism 의 DI 컨테이너, 등록된 ViewModel 또는 View 인스턴스를 이름으로 Resolve 할 수 있음.
  /// </summary>
  /// <param name="containerProvider"></param>
  /// <param name="regionManager"></param>
  public ContentManager(IContainerProvider containerProvider, IRegionManager regionManager)
  {
    _containerProvider = containerProvider;
    _regionManager = regionManager;
  }

  /// <summary>
  /// 오버로드 메서드: 기본적으로 IViewable 인터페이스를 따르는 View를 활성화할 때 사용.(내부적으로 Generic T 버전 호출)
  /// </summary>
  /// <param name="regionName"></param>
  /// <param name="contentName"></param>
  public void ActiveContent(string regionName, string contentName)
  {
    ActiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// 제네릭 버전으로, 뷰 타입을 명확히 지정해 활성화 가능
  /// 예)ActiveContent<UserControl>("MainRegion", "UserProfileView")
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="regionName"></param>
  /// <param name="contentName"></param>
  public void ActiveContent<T>(string regionName, string contentName)
  {
    // Prism RegionManager에서 지정된 이름의 Region을 찾습니다.
    IRegion region = _regionManager.Regions[regionName];

    // DI 컨테이너로부터 등록된 View 인스턴스를 이름(contentName)으로 찾아 반환
    T content = _containerProvider.Resolve<T>(contentName);

    if (!region.Views.Contains(content))
    {
      // Region에 해당 View가 아직 없으면 추가
      region.Add(content);
    }

    // Region 내 해당 View를 활성화 (즉, 사용자에게 보여짐)
    region.Activate(content);
  }

  /// <summary>
  /// 비활성화 IViewable로 오버로드 제공
  /// </summary>
  /// <param name="regionName"></param>
  /// <param name="contentName"></param>
  public void DeactiveContent(string regionName, string contentName)
  {
    DeactiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// Region 에서 해당 View를 비활성화 (숨기기 등, 다른 View와 교체될 때 사용)
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="regionName"></param>
  /// <param name="contentName"></param>
  public void DeactiveContent<T>(string regionName, string contentName)
  {
    IRegion region = _regionManager.Regions[regionName];

    T content = _containerProvider.Resolve<T>(contentName);

    if (region.Views.Contains(content))
    {
      // 등록된 View가 있는 경우에만 비활성화 실행
      region.Deactivate(content);
    }
  }
}
