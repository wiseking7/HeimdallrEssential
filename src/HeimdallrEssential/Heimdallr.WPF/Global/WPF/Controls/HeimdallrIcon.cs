using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Heimdallr.WPF.Global.WPF.Controls;


public enum IconMode
{
  None,
  Icon,
  Image
}

#region IconType (Geometry)
public enum IconType
{
  None, Twitter, Power, CheckDecagram, Email, EmailOutline, BellOutline, DotsHorizontal, Instagram,
  Facebook, Linkedin, YouTube, Link, LinkBox, LinkVariant, Domain, MapMaker, MapMarkerOutline,
  Microsoft, Apple, Google, Netflix, Star, AccountMultipleOutline, Image, Heart, HeartOutline,
  Pin, CardMultiple, CardMultipleOutline, Comment, CommentOutline, Close, CheckCircle,
  Check, Crop, MicrosoftVisualStudio, MoveOpenPlay, MenuBar, GoogleTranslate, EyedropperVariant,
  CogRefreshOutline, MonitorShimmer, ChevronRight, ChevronDown, CursorPointer, CalendarMonth,
  Minimize, Web, Palette, Contentpaste, Checkbold, FolderOpenOutline, FolderOpen, FolderRable,
  Maximize, Restore, Resize, SelectAll, ArrowLeftBold, ArrowRightBold, ArrowUpBold, ConsoleLine,
  Plus, ArrowAll, MicrosoftWindows, ArrowDownBox, TextBox, Folder, FolderOutline, DesktopClassic,
  Harddisk, File, FileWord, FileCheck, FileZip, FilePdf, FileImage, DotsHorizontalCircle,
  Home, HomeOutline, PlusBox, Database, DatabasePlus, Delete, Grid, AccountGroup, PokerChip, Creditcardchip,
  CreditcardchipOutline, Memory, Account, HomeCircle, HomeCircleOutline, Cash, Cash100, CashMulti, History,
  Cloud, EyeCircle, CalendarBlankOutline, FormatListBulleted, ClipboardCheck, ChartBubble, ChartPie, Cog,
  AccountBox, CogOutline, Security, ShieldLock, Timetable, ClipboardTextClock, Information, InformationOutline,
  AccountCircle, FilterVariant, Ruler, ArrowLeft, ArrowRight, ArrowLeftThin, ArrowRightThin, KeyboardBackspace,
  ButtonCursor, Import, Export, Trash, TrashOutline, DeleteEmpty, Magnify, ViewColumn, ViewGrid, SkipPrevious,
  SkipNext, CardSuitClub, CardSuitHeart, CardSuitSpade, CardSuitDiamond, ViewAgenda, ViewCompact, WeatherNight,
  WhiteBalanceSunny, SwapHorizontal, MenuUp, MenuDown, Cardsplayingspademultipleoutline, Broadcast, Bug,
}
#endregion

public enum ImageType
{
  None, AppStore, Benz, Bmw, Chelsea, CrystalPalace, Disney, DisneyPlus, Everton, Honda, Hyundai, LeicesterCity,
  ManchesterCity, ManchesterUnited, NewCastle, Porsche, AmazonPrime, QQ, SouthHampton, Spotify, Sunderland, SwanseaCity,
  Tesla, Tinder, Tottenham, WestBromwichAlbion, USA, KOR, CHN, JPN, VNM, ESP, Print, Product, Productreturn, PurchaseBuy,
  PurchaseBuySell, RegisterBlack, RegisterGray, RegisterGreen, Reports, Role, Rotation, Search, Security, SettingBlack,
  SettingBlue, SettingGreen, SettingViolet, SettingYellow, Submit, Transaction, Update, VAT, Prince, Broker, Brand,
  Cancel, Category, Contact, CustomerGreen, Database, Heimdallr, Inventory, InvoiceItem, Invoices, Login, Logout, PowerBlue
}

public class HeimdallrIcon : ContentControl
{
  #region IconMode
  public IconMode Mode
  {
    get { return (IconMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  public static readonly DependencyProperty ModeProperty =
      DependencyProperty.Register(nameof(Mode), typeof(IconMode), typeof(HeimdallrIcon),
        new PropertyMetadata(IconMode.None));
  #endregion

  #region IconType
  public IconType Icon
  {
    get { return (IconType)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }

  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType),
        typeof(HeimdallrIcon), new PropertyMetadata(IconType.None, IconPropertyChanged));

  private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;
    string geometryData = Desing.Geometies.GeometryConverter.GetData(heimdallrIcon.Icon.ToString());

    heimdallrIcon.Data = Geometry.Parse(geometryData);
    heimdallrIcon.Mode = IconMode.Icon;
  }
  #endregion

  #region Image
  public ImageType Image
  {
    get { return (ImageType)GetValue(ImageProperty); }
    set { SetValue(ImageProperty, value); }
  }

  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType), typeof(HeimdallrIcon),
        new PropertyMetadata(ImageType.None, ImagePropertyChanged));

  private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;
    try
    {
      string base64 = Desing.Images.ImageConverter.GetData(heimdallrIcon.Image.ToString());

      byte[] binaryData = Convert.FromBase64String(base64);

      BitmapImage bitmapImage = new();
      bitmapImage.BeginInit();
      bitmapImage.StreamSource = new MemoryStream(binaryData);
      bitmapImage.EndInit();

      heimdallrIcon.Source = bitmapImage;
      heimdallrIcon.Mode = IconMode.Image;
    }
    catch (KeyNotFoundException ex)
    {
      // KeyNotFoundException 처리: 예외 로그 출력
      Debug.WriteLine($"이미지 항목을 찾을 수 없습니다: {ex.Message}");
      heimdallrIcon.Source = null!; // 예외 발생 시 이미지를 비워둠
    }
    catch (Exception ex)
    {
      // 다른 예외 처리
      Debug.WriteLine($"이미지 로딩 중 오류 발생: {ex.Message}");
    }
  }
  #endregion

  #region Fill
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIcon),
        new PropertyMetadata(Brushes.Silver));
  #endregion

  #region Data
  public Geometry Data
  {
    get { return (Geometry)GetValue(DataProperty); }
    set { SetValue(DataProperty, value); }
  }
  public static readonly DependencyProperty DataProperty =
      DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  #region Source
  public ImageSource Source
  {
    get { return (ImageSource)GetValue(SourceProperty); }
    set { SetValue(SourceProperty, value); }
  }
  public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
  }

  static HeimdallrIcon()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIcon),
      new FrameworkPropertyMetadata(typeof(HeimdallrIcon)));
  }
}
