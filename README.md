# Heimdallr Essential

## Heimdallr.App [실행]
- [x] 1.1 App.cs
- [x] 1.2 Starter.cs

## Heimdallr.Desing [라이브러리 프로젝트] 
### Newtonsoft.json (13.0.3) 
### YamDotNet.NetCore (1.0.0)
- [x] 1.1 Properties 폴더
    - [x] 1.1.1 geometries.json
    - [x] 1.1.2 images.yaml
- [x] 1.2 Geometies [Folder] -> Geometry 를 변환
  - [x] 1.2.1 GeometryContainer.cs
  - [x] 1.2.2 GeometryConverter.cs
  - [x] 1.2.3 GeometryData.cs
  - [x] 1.2.4 GeometryItem.cs
  - [x] 1.2.5 GeometryRoot.cs
- [x] 1.3 Images [Folder] -> Image 를 변환
  - [x] 1.3.1 ImageContainer.cs
  - [x] 1.3.2 ImageConverter.cs
  - [x] 1.3.3 ImageData.cs
  - [x] 1.3.4 ImageItem.cs
  - [x] 1.3.5 ImageRoot.cs  
       
## Heimdallr.Forms [라이브러리 프로젝트]
- [x] 1.1. Properties [Folder]
    - [x] 1.1.1. AssemblyInfo.cs
- [x] 1.2 Local [Folder]
  - [x] 1.2.1 ViewModel [Folder] ManWindowViewModel.cs 적용
- [x] 1.3 Themes [Folder]
  - [x] 1.3.1 Views [Folder]
    - [x] 1.3.1.1 MainWindow.xaml
  - [x] 1.3.2 Generic.xaml
- [x] 1.4 UI [Folder]
  - [x] 1.4.1 Views [Folder]
    - [x] 1.4.1.1 MainWindow.cs

## Heimdallr.WPF [라이브러리 프로젝트]
- [x] 1.1 Properties [Folder]
    - [x] 1.1.1 AssemblyInfo.cs
- [x] 1.2 Events [Folder]
    - [x] 1.2.1 SwitchLanguagePubsub.cs
    - [x] 1.2.2 SwitchThemePubsub.cs
- [x] 1.3 Global [Folder]
  - [x] 1.3.1 Animation [Folder]
    - [x] 1.3.1.1 ColorItem.cs
    - [x] 1.3.1.2 DoubleItem.cs
    - [x] 1.3.1.3 ThicknessItem.cs
 - [x] 1.3.2 Composition [Folder]
    - [x] 1.3.2.1 AutoWireManager.cs
    - [x] 1.3.2.2 BaseResourceInitializer.cs
    - [x] 1.3.2.3 ContentManager.cs
    - [x] 1.3.2.4 DimmingManager.cs
    - [x] 1.3.2.5 ResourceManager.cs
  - [x] 1.3.3 Converters [Folder]
    - [x] 1.3.3.01 BaseValueConverter.cs [제네릭클래스 T 생성자를 가져야함, MrakupExtension XAML 에서 사용할 수있도록 확장 기능]
    - [x] 1.3.3.02 BooleanToVisibilityConverter.cs [bool 값을 Visibility로 변환하기 위한 ValueConverter]
    - [x] 1.3.3.03 BoolToColorConverter.cs [True/False 값에 따라 색상을 반환]
    - [x] 1.3.3.04 ComparisonConverter.cs [CheckBox, RadioButton, ComboBox, ListBox 등 선택 상태를 비교 기반으로 관리]
    - [x] 1.3.3.05 ComparisonMultiConverter.cs [다중 값(MultiBinding) 비교를 위한 컨버터]
    - [x] 1.3.3.06 DateFormatConverter.cs [날짜 형식 지정 변환기 yyyy-MM-dd]
    - [x] 1.3.3.07 EnumToTextConverter.cs [Enum 값을 문자열로 변환]
    - [x] 1.3.3.08 HelperConverter.cs [여러 개의 소스 값을 변환해 하나의 타겟 값으로 변경]
    - [x] 1.3.3.09 IndexToNumberConverter.cs [ListView, ListBox, ComboBox, WrapPanel 등에서 특정 아이템에 번호 매김]
    - [x] 1.3.3.10 InverseComparisonConverter.cs [어떤 항목이 선택되지 않았을 때만 체크되도록]
    - [x] 1.3.3.11 MultiBooleanToVisibilityConverter.cs [다중 값 bool 가시성 변환기]
    - [x] 1.3.3.12 NullableBoolToTextConverter.cs [bool? (nullable bool)을 "예", "아니오", "모름" 등의 문자열로 변환]
    - [x] 1.3.3.13 NullableIntConverter.cs [TextBox 에 Int 기본값 0 을 빈공간으로 처리]
    - [x] 1.3.3.14 NumberCommaConverter.cs [숫자를 콤마 표기 천단위]
    - [x] 1.3.3.15 PasswordToVisibilityConverter.cs [비밀번호 입력이나 특정 값의 상태에 따라 Visibility 값을 반환]
    - [x] 1.3.3.16 MobileNumberConverter.cs [휴대폰 번호로 변환]
    - [x] 1.3.3.17 ResourceBinding.cs [XAML에서 ResourceBinding을 적용할 때, 리소스 키와 대상 속성을 연결하는 역할]
    - [x] 1.3.3.18 StringToVisibilityConverter.cs [문자열이 비어 있는지 여부에 따라 Visibility를 반환]
    - [x] 1.3.3.19 ValidatingBorderBrushConverter.cs [Border 색상 변환 HexCode]
    - [x] 1.3.3.20 ValidatingBorderThicknessConverter.cs [Thickness 두께 변환 ]
    - [x] 1.3.3.21
    - [x] 1.3.3.22
    - [x] 1.3.3.23
    - [x] 1.3.3.24
    - [x] 1.3.3.25
    - [x] 1.3.3.26
    - [x] 1.3.3.27
    - [x] 1.3.3.28
    - [x] 1.3.3.29
    - [x] 1.3.3.30
    - [x] 1.3.3.31
    - [x] 1.3.3.32
    - [x] 1.3.3.33
    - [x] 1.3.3.34
    - [x] 1.3.3.35
    - [x] 1.3.3.36
    - [x] 1.3.3.37
