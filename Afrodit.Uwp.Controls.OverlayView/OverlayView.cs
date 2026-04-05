using System;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Afrodit.WinUI.Controls;

public enum OverlayViewResult { None, Close }
public enum OverlayCloseReason { CloseButton, EscapeKey, Programmatic, SystemBack }

public sealed class OverlayClosedEventArgs : EventArgs
{
    public OverlayViewResult Result { get; }
    public OverlayCloseReason Reason { get; }
    public OverlayClosedEventArgs(OverlayViewResult result, OverlayCloseReason reason)
    {
        Result = result; Reason = reason;
    }
}

[TemplatePart(Name = "PART_LayoutRoot", Type = typeof(Grid))]
[TemplatePart(Name = "PART_Backdrop", Type = typeof(Grid))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
public class OverlayView : ContentControl
{
    private readonly Popup _popup;
    private Grid _layoutRoot;
    private Grid _header;
    private Grid _backdrop;
    private Button _closeButton;
    private TaskCompletionSource<OverlayViewResult> _tcs;
    private DependencyObject _prevFocus;
    private bool _isShowing;

    public OverlayView()
    {
        this.DefaultStyleKey = typeof(OverlayView);

        _popup = new Popup();
        _popup.IsLightDismissEnabled = false;
        _popup.ShouldConstrainToRootBounds = true;

        var transitions = new TransitionCollection();
        transitions.Add(new PopupThemeTransition());
        _popup.ChildTransitions = transitions;

        // EL CAMBIO: El Popu ahora usa este propio control (this) como Child
        _popup.Child = this;
    }

    // Dependency Properties siguen igual (Title, CloseButtonToolTip, TitleBarHeight, BackdropBrush)
    public object Title { get => GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(object), typeof(OverlayView), new PropertyMetadata(null));

    public DataTemplate TitleTemplate { get => (DataTemplate)GetValue(TitleTemplateProperty); set => SetValue(TitleTemplateProperty, value); }
    public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(nameof(TitleTemplate), typeof(DataTemplate), typeof(OverlayView), new PropertyMetadata(null));

    public string CloseButtonToolTip { get => (string)GetValue(CloseButtonToolTipProperty); set => SetValue(CloseButtonToolTipProperty, value); }
    public static readonly DependencyProperty CloseButtonToolTipProperty = DependencyProperty.Register(nameof(CloseButtonToolTip), typeof(string), typeof(OverlayView), new PropertyMetadata("Cerrar"));

    public double TitleBarHeight { get => (double)GetValue(TitleBarHeightProperty); set => SetValue(TitleBarHeightProperty, value); }
    public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(OverlayView), new PropertyMetadata(0d));

    public Brush BackdropBrush { get => (Brush)GetValue(BackdropBrushProperty); set => SetValue(BackdropBrushProperty, value); }
    public static readonly DependencyProperty BackdropBrushProperty = DependencyProperty.Register(nameof(BackdropBrush), typeof(Brush), typeof(OverlayView), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

    public event EventHandler Opened;
    public event EventHandler<OverlayClosedEventArgs> Closed;

    // NUEVO: Aquí enganchamos la lógica con el XAML ya renderizado
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _layoutRoot = GetTemplateChild("PART_LayoutRoot") as Grid;
        _header = GetTemplateChild("PART_Header") as Grid;
        _backdrop = GetTemplateChild("PART_Backdrop") as Grid;
        _closeButton = GetTemplateChild("PART_CloseButton") as Button;

        if (_closeButton != null)
        {
            _closeButton.Click -= OnCloseButtonClicked; // Prevenir doble suscripción
            _closeButton.Click += OnCloseButtonClicked;
        }

        if (_backdrop != null)
        {
            _backdrop.Tapped -= OnBackdropTapped; // Prevenir doble suscripción
            _backdrop.Tapped += OnBackdropTapped;
        }

        UpdateLayoutSize();
    }

    private void OnBackdropTapped(object sender, TappedRoutedEventArgs e)
    {
        e.Handled = true;
    }

    public async Task<OverlayViewResult> ShowAsync()
    {
        if (_isShowing) return OverlayViewResult.None;
        _isShowing = true;

        _tcs = new TaskCompletionSource<OverlayViewResult>();
        _prevFocus = FocusManager.GetFocusedElement() as DependencyObject;

        if (this.DataContext == null && Window.Current.Content is FrameworkElement rootFrame)
        {
            this.DataContext = rootFrame.DataContext;
        }

        Window.Current.CoreWindow.KeyDown += OnCoreWindowKeyDown;
        Window.Current.SizeChanged += OnWindowSizeChanged;
        SystemNavigationManager.GetForCurrentView().BackRequested += OnSystemBackRequested;

        _popup.VerticalOffset = TitleBarHeight;
        _popup.IsOpen = true;
        Opened?.Invoke(this, EventArgs.Empty);

        await Task.Delay(50); // Tiempo para el renderizado

        if (_layoutRoot != null)
        {
            await FocusManager.TryFocusAsync(_layoutRoot, FocusState.Programmatic);
        }

        return await _tcs.Task;
    }

    public void Hide() => Complete(OverlayViewResult.None, OverlayCloseReason.Programmatic);

    private void Complete(OverlayViewResult result, OverlayCloseReason reason)
    {
        if (_popup?.IsOpen == true)
        {
            _popup.IsOpen = false;
            _isShowing = false;

            Window.Current.CoreWindow.KeyDown -= OnCoreWindowKeyDown;
            Window.Current.SizeChanged -= OnWindowSizeChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnSystemBackRequested;

            if (_prevFocus != null)
            {
                try { Windows.Foundation.IAsyncOperation<FocusMovementResult> asyncOperation = FocusManager.TryFocusAsync(_prevFocus, FocusState.Programmatic); } catch { }
            }

            Closed?.Invoke(this, new OverlayClosedEventArgs(result, reason));
            _tcs?.TrySetResult(result);
        }
    }

    private void OnSystemBackRequested(object sender, BackRequestedEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = true; // Consumimos el evento para que la app no cambie de página
            Complete(OverlayViewResult.Close, OverlayCloseReason.SystemBack);
        }
    }

    private void OnCloseButtonClicked(object sender, RoutedEventArgs e) => Complete(OverlayViewResult.Close, OverlayCloseReason.CloseButton);

    private void OnCoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
    {
        if (args.VirtualKey == VirtualKey.Escape)
        {
            Complete(OverlayViewResult.Close, OverlayCloseReason.EscapeKey);
            args.Handled = true; // Consumimos el evento para que no afecte otra cosa
        }
    }

    private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e) => UpdateLayoutSize();

    private static void OnTitleBarHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is OverlayView view) view.UpdateLayoutSize();
    }

    private void UpdateLayoutSize()
    {
        if (_layoutRoot == null) return;

        var b = Window.Current.Bounds;
        var w = b.Width;
        var h = Math.Max(0, b.Height - TitleBarHeight);

        // Redimensionamos este propio control
        this.Width = w;
        this.Height = h;
        if (_popup != null) _popup.VerticalOffset = TitleBarHeight;

        if (_header != null)
        {
            _header.Margin = TitleBarHeight == 0
                ? new Thickness(0, 48, 0, 0)
                : new Thickness(0);
        }
    }
}
