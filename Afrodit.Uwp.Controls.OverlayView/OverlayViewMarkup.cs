using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Afrodit.WinUI.Controls;


public static class OverlayViewMarkup
{
    public static T TitleBarHeight<T>(this T element, double height) where T : OverlayView
    {
        element.TitleBarHeight = height;
        return element;
    }

    public static T TitleTemplate<T>(this T element, DataTemplate titleTemplate) where T : OverlayView
    {
        element.TitleTemplate = titleTemplate;
        return element;
    }

    public static T Title<T>(this T element, object title) where T : OverlayView
    {
        element.Title = title;
        return element;
    }

    public static T CloseButtonToolTip<T>(this T element, string closeButtonToolTip) where T : OverlayView
    {
        element.CloseButtonToolTip = closeButtonToolTip;
        return element;
    }

    public static T Content<T>(this T element, object content) where T : OverlayView
    {
        element.Content = content;
        return element;
    }

    public static T Padding<T>(this T element, Thickness thickness) where T : OverlayView
    {
        element.Padding = thickness;
        return element;
    }

    public static T Padding<T>(this T element, double padding = 0) where T : OverlayView
    {
        element.Padding = new Thickness(padding);
        return element;
    }

    public static T Background<T>(this T element, Color color) where T : OverlayView
    {
        element.Background = new SolidColorBrush(color);
        return element;
    }

    // Nueva extensión para el Backdrop
    public static T BackdropBrush<T>(this T element, Brush brush) where T : OverlayView
    {
        element.BackdropBrush = brush;
        return element;
    }
}