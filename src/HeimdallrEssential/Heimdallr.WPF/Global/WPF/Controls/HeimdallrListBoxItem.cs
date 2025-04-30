﻿using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.WPF.Global.WPF.Controls;

public class HeimdallrListBoxItem : ListBoxItem
{
  #region CornerRadius

  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrListBoxItem),
          new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  #endregion
}
