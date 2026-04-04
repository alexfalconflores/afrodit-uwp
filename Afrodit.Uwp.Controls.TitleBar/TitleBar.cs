using Afrodit.WinUI.Helpers;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Afrodit.WinUI.Controls;

public enum TitleBarSize
{
    Short = 32,
    Tall = 48
}

[ContentProperty(Name = "Content")]
public class TitleBar : Control
{
    private UIElement _appTitleBarGrid;
    private UIElement _iAppTitleBarGrid;
    private ColumnDefinition _rightMarginColumn;  // <-- NUEVO: Para el espacio de los botones
    private ColumnDefinition _iRightMarginColumn; // <-- NUEVO: Para el espacio de los botones interactivos
    private readonly ApplicationViewTitleBar _titleBar = ApplicationView.GetForCurrentView().TitleBar;
    private Button _paneToggleButton;
    private Button _goBackButtonControl;

    public TitleBar()
    {
        // Esto le indica al runtime que busque el estilo en Themes/Generic.xaml
        this.DefaultStyleKey = typeof(TitleBar);
    }

    #region Eventos Públicos (Patrón Requested)

    // Usamos el TypedEventHandler que es estándar en UWP/WinUI
    public event TypedEventHandler<TitleBar, EventArgs> BackRequested;
    public event TypedEventHandler<TitleBar, EventArgs> PaneToggleRequested;

    #endregion

    #region Dependency Properties
    public TitleBarSize Size
    {
        get => (TitleBarSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(TitleBarSize), typeof(TitleBar), new PropertyMetadata(TitleBarSize.Short));

    public ImageSource AppIconSource
    {
        get => (ImageSource)GetValue(AppIconSourceProperty);
        set => SetValue(AppIconSourceProperty, value);
    }
    public static readonly DependencyProperty AppIconSourceProperty =
        DependencyProperty.Register(nameof(AppIconSource), typeof(ImageSource), typeof(TitleBar), new PropertyMetadata(null));

    public Visibility IsAppIconVisible
    {
        get => (Visibility)GetValue(IsAppIconVisibleProperty);
        set => SetValue(IsAppIconVisibleProperty, value);
    }
    public static readonly DependencyProperty IsAppIconVisibleProperty =
        DependencyProperty.Register(nameof(IsAppIconVisible), typeof(Visibility), typeof(TitleBar), new PropertyMetadata(Visibility.Visible));

    public string AppName
    {
        get => (string)GetValue(AppNameProperty);
        set => SetValue(AppNameProperty, value);
    }
    public static readonly DependencyProperty AppNameProperty =
        DependencyProperty.Register(nameof(AppName), typeof(string), typeof(TitleBar), new PropertyMetadata(string.Empty));

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(TitleBar), new PropertyMetadata(string.Empty));

    public bool IsSubtitleVisible // Ahora es bool
    {
        get => (bool)GetValue(IsSubtitleVisibleProperty);
        set => SetValue(IsSubtitleVisibleProperty, value);
    }
    public static readonly DependencyProperty IsSubtitleVisibleProperty =
        DependencyProperty.Register(nameof(IsSubtitleVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(false));

    // Propiedades de navegación y utilidades visuales
    public ICommand BackButtonCommand
    {
        get => (ICommand)GetValue(BackButtonCommandProperty);
        set => SetValue(BackButtonCommandProperty, value);
    }
    public static readonly DependencyProperty BackButtonCommandProperty =
        DependencyProperty.Register(nameof(BackButtonCommand), typeof(ICommand), typeof(TitleBar), new PropertyMetadata(null));

    public object BackButtonCommandParameter
    {
        get => (object)GetValue(BackButtonCommandParameterProperty);
        set => SetValue(BackButtonCommandParameterProperty, value);
    }
    public static readonly DependencyProperty BackButtonCommandParameterProperty =
        DependencyProperty.Register(nameof(BackButtonCommandParameter), typeof(object), typeof(TitleBar), new PropertyMetadata(null));

    public bool IsBackButtonVisible
    {
        get => (bool)GetValue(IsBackButtonVisibleProperty);
        set => SetValue(IsBackButtonVisibleProperty, value);
    }
    public static readonly DependencyProperty IsBackButtonVisibleProperty =
        DependencyProperty.Register(nameof(IsBackButtonVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

    public ICommand PaneToggleButtonCommand
    {
        get => (ICommand)GetValue(PaneToggleButtonCommandProperty);
        set => SetValue(PaneToggleButtonCommandProperty, value);
    }
    public static readonly DependencyProperty PaneToggleButtonCommandProperty =
        DependencyProperty.Register(nameof(PaneToggleButtonCommand), typeof(ICommand), typeof(TitleBar), new PropertyMetadata(null));

    public object PaneToggleButtonParameter
    {
        get => (object)GetValue(PaneToggleButtonParameterProperty);
        set => SetValue(PaneToggleButtonParameterProperty, value);
    }
    public static readonly DependencyProperty PaneToggleButtonParameterProperty =
        DependencyProperty.Register(nameof(PaneToggleButtonParameter), typeof(object), typeof(TitleBar), new PropertyMetadata(null));

    public bool IsPaneToggleButtonVisible
    {
        get => (bool)GetValue(IsPaneToggleButtonVisibleProperty);
        set => SetValue(IsPaneToggleButtonVisibleProperty, value);
    }
    public static readonly DependencyProperty IsPaneToggleButtonVisibleProperty =
        DependencyProperty.Register(nameof(IsPaneToggleButtonVisible), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(TitleBar), new PropertyMetadata(null));

    public UIElement PaneLeftContent
    {
        get => (UIElement)GetValue(PaneLeftContentProperty);
        set => SetValue(PaneLeftContentProperty, value);
    }
    public static readonly DependencyProperty PaneLeftContentProperty =
        DependencyProperty.Register(nameof(PaneLeftContent), typeof(UIElement), typeof(TitleBar), new PropertyMetadata(null));

    public UIElement PaneRightContent
    {
        get => (UIElement)GetValue(PaneRightContentProperty);
        set => SetValue(PaneRightContentProperty, value);
    }
    public static readonly DependencyProperty PaneRightContentProperty =
        DependencyProperty.Register(nameof(PaneRightContent), typeof(UIElement), typeof(TitleBar), new PropertyMetadata(null));

    #endregion

    #region Lógica Nativa del Control

    // OnApplyTemplate reemplaza tu antiguo constructor InitializeTitleBar.
    // Se ejecuta cuando el XAML del Generic.xaml se une a esta clase.
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // Capturamos los elementos definidos en el Generic.xaml usando sus nombres (x:Name)
        _appTitleBarGrid = GetTemplateChild("AppTitleBarGrid") as UIElement;
        _iAppTitleBarGrid = GetTemplateChild("IAppTitleBarGrid") as UIElement;
        _paneToggleButton = GetTemplateChild("PaneToggleButton") as Button;
        _goBackButtonControl = GetTemplateChild("GoBackButtonControl") as Button;

        _rightMarginColumn = GetTemplateChild("RightTitleBarMarginColumn") as ColumnDefinition;
        _iRightMarginColumn = GetTemplateChild("IRightTitleBarMarginColumn") as ColumnDefinition;

        if (_appTitleBarGrid != null)
        {
            InitializeSystemTitleBar();
            UpdateTitleBarColors();
            this.ActualThemeChanged += OnThemeChanged;
        }

        // Suscribimos los eventos de clic nativos
        if (_paneToggleButton != null)
        {
            _paneToggleButton.Click += OnPaneToggleButtonClicked;
        }

        if (_goBackButtonControl != null)
        {
            _goBackButtonControl.Click += OnGoBackButtonClicked;
        }
    }

    private void OnPaneToggleButtonClicked(object sender, RoutedEventArgs e)
    {
        // 1. Disparamos la 'Solicitud' hacia el exterior (Code-Behind)
        PaneToggleRequested?.Invoke(this, EventArgs.Empty);

        bool commandExecuted = false;
        // 1. Si estás usando MVVM y le pasaste un Command por XAML, lo priorizamos
        if (PaneToggleButtonCommand != null && PaneToggleButtonCommand.CanExecute(PaneToggleButtonParameter))
        {
            PaneToggleButtonCommand.Execute(PaneToggleButtonParameter);
            commandExecuted = true;
        }

        // 3. Fallback: Magia Afrodit
        // Si NO hay Command, entonces Afrodit hace el trabajo sucio.
        // (Nota: Si el evento PaneToggleRequested hizo algo en Code-Behind, 
        // igual abriremos el panel a menos que implementemos una clase HandledEventArgs).
        if (!commandExecuted)
        {
            NavigationPaneService.TogglePane();
        }
    }

    private void OnGoBackButtonClicked(object sender, RoutedEventArgs e)
    {
        BackRequested?.Invoke(this, EventArgs.Empty);

        if (BackButtonCommand != null && BackButtonCommand.CanExecute(BackButtonCommandParameter))
        {
            BackButtonCommand.Execute(BackButtonCommandParameter);
        }
    }

    private void InitializeSystemTitleBar()
    {
        var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        coreTitleBar.ExtendViewIntoTitleBar = true;
        Window.Current.SetTitleBar(_appTitleBarGrid);

        UpdateSystemOverlayMargins(coreTitleBar);

        coreTitleBar.LayoutMetricsChanged += (s, e) => UpdateSystemOverlayMargins(s);

        // Control de opacidad cuando la ventana pierde el foco
        Window.Current.Activated += (s, e) =>
        {
            var opacity = e.WindowActivationState != CoreWindowActivationState.Deactivated ? 1.0 : 0.5;
            if (_appTitleBarGrid != null) _appTitleBarGrid.Opacity = opacity;
            if (_iAppTitleBarGrid != null) _iAppTitleBarGrid.Opacity = opacity;
        };
    }

    private void UpdateSystemOverlayMargins(CoreApplicationViewTitleBar coreTitleBar)
    {
        // SystemOverlayRightInset nos dice exactamente cuántos píxeles ocupan Min/Max/Close
        var rightInset = coreTitleBar.SystemOverlayRightInset;

        if (_rightMarginColumn != null)
            _rightMarginColumn.Width = new GridLength(rightInset);

        if (_iRightMarginColumn != null)
            _iRightMarginColumn.Width = new GridLength(rightInset);
    }

    private void OnThemeChanged(FrameworkElement sender, object args)
    {
        UpdateTitleBarColors();
    }

    private void UpdateTitleBarColors()
    {
        if (_titleBar == null) return;

        // Determinamos el tema actual evaluando el control o el sistema
        bool isLightTheme = this.ActualTheme == ElementTheme.Light ||
                            (this.ActualTheme == ElementTheme.Default && Application.Current.RequestedTheme == ApplicationTheme.Light);

        // Paleta de Colores Afrodit (WinUI 2.8 Style)
        var foregroundColor = isLightTheme ? Color.FromArgb(255, 26, 26, 26) : Color.FromArgb(255, 255, 255, 255);
        var hoverForegroundColor = isLightTheme ? Color.FromArgb(255, 25, 25, 25) : Color.FromArgb(255, 255, 255, 255);
        var pressedForegroundColor = isLightTheme ? Color.FromArgb(255, 96, 96, 96) : Color.FromArgb(255, 208, 208, 208);
        var inactiveForegroundColor = isLightTheme ? Color.FromArgb(255, 160, 160, 160) : Color.FromArgb(255, 111, 111, 111);

        var hoverBackgroundColor = isLightTheme ? Color.FromArgb(255, 221, 221, 221) : Color.FromArgb(255, 41, 41, 41);
        var pressedBackgroundColor = isLightTheme ? Color.FromArgb(255, 224, 224, 224) : Color.FromArgb(255, 56, 56, 56);
        var transparent = Colors.Transparent;

        // Inyección directa a la API de Windows
        _titleBar.ForegroundColor = foregroundColor;
        _titleBar.ButtonForegroundColor = foregroundColor;
        _titleBar.ButtonHoverForegroundColor = hoverForegroundColor;
        _titleBar.ButtonPressedForegroundColor = pressedForegroundColor;
        _titleBar.ButtonInactiveForegroundColor = inactiveForegroundColor;
        _titleBar.InactiveForegroundColor = inactiveForegroundColor;

        _titleBar.BackgroundColor = transparent;
        _titleBar.ButtonBackgroundColor = transparent;
        _titleBar.InactiveBackgroundColor = transparent;
        _titleBar.ButtonInactiveBackgroundColor = transparent;
        _titleBar.ButtonHoverBackgroundColor = hoverBackgroundColor;
        _titleBar.ButtonPressedBackgroundColor = pressedBackgroundColor;
    }

    #endregion
}

