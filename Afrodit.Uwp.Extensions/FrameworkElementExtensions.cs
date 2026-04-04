using Windows.UI.Xaml;

namespace Afrodit.WinUI.Extensions;

/// <summary>
/// Proporciona utilidades avanzadas para FrameworkElements, como habilitar bindings en tiempo real 
/// para ActualWidth y ActualHeight, superando las limitaciones nativas de UWP.
/// </summary>
public static class FrameworkElementExtensions
{
    public static readonly DependencyProperty EnableActualSizeBindingProperty = DependencyProperty.RegisterAttached(
        "EnableActualSizeBinding", typeof(bool), typeof(FrameworkElementExtensions), new PropertyMetadata(false, OnEnableActualSizeBindingPropertyChanged));

    public static readonly DependencyProperty ActualHeightProperty = DependencyProperty.RegisterAttached(
        "ActualHeight", typeof(double), typeof(FrameworkElementExtensions), new PropertyMetadata(double.NaN));

    public static readonly DependencyProperty ActualWidthProperty = DependencyProperty.RegisterAttached(
        "ActualWidth", typeof(double), typeof(FrameworkElementExtensions), new PropertyMetadata(double.NaN));

    public static bool GetEnableActualSizeBinding(FrameworkElement obj) => (bool)obj.GetValue(EnableActualSizeBindingProperty);
    public static void SetEnableActualSizeBinding(FrameworkElement obj, bool value) => obj.SetValue(EnableActualSizeBindingProperty, value);

    public static double GetActualHeight(FrameworkElement obj) => (double)obj.GetValue(ActualHeightProperty);
    public static void SetActualHeight(FrameworkElement obj, double value) => obj.SetValue(ActualHeightProperty, value);

    public static double GetActualWidth(FrameworkElement obj) => (double)obj.GetValue(ActualWidthProperty);
    public static void SetActualWidth(FrameworkElement obj, double value) => obj.SetValue(ActualWidthProperty, value);

    private static void OnEnableActualSizeBindingPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is not FrameworkElement baseElement) return;

        if ((bool)args.NewValue)
        {
            UpdateActualSizeProperties(baseElement, null);
            baseElement.SizeChanged += UpdateActualSizeProperties;
        }
        else
        {
            baseElement.SizeChanged -= UpdateActualSizeProperties;
        }
    }

    private static void UpdateActualSizeProperties(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement baseElement) return;

        var currentHeight = GetActualHeight(baseElement);
        if (currentHeight != baseElement.ActualHeight)
        {
            SetActualHeight(baseElement, baseElement.ActualHeight);
        }

        var currentWidth = GetActualWidth(baseElement);
        if (currentWidth != baseElement.ActualWidth)
        {
            SetActualWidth(baseElement, baseElement.ActualWidth);
        }
    }
}