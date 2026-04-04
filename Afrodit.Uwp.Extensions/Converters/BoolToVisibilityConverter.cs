using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Afrodit.WinUI.Extensions.Converters;

/// <summary>
/// Convierte un valor booleano (true/false) a un estado de visibilidad (Visible/Collapsed).
/// </summary>
public sealed class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isVisible && isVisible)
            return Visibility.Visible;

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
        => throw new NotImplementedException();
}
