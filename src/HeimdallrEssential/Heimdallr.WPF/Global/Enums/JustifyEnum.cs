namespace Heimdallr.WPF.Global.Enums;
/// <summary>
/// UI 요소의 정렬 방식을 정의하기 위한 열거형(enum)
/// 수평 레이아웃 배치에서 자식 요소들 사이의 간격을 어떻게 분배할지를 설정할 때 사용됩니다. 
/// 이 값들은 Flexbox, DockPanel, WrapPanel, StackPanel 같은 레이아웃 시스템에서 
/// 자주 사용되는 정렬 개념을 반영하고 있습니다.
/// </summary>
public enum JustifyEnum
{
  None,
  SpaceAround,
  SpaceBetween,
  SpaceEvenly
}
