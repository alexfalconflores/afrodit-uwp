using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Afrodit.WinUI.Extensions.Converters;

/// <summary>
/// Convierte un valor de enumeración en un GridLength para su uso en definiciones de filas o columnas.
/// Útil para manejar tamaños dinámicos (Short/Tall) en el TitleBar.
/// </summary>
public sealed class EnumToGridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Verificamos si es un Enum de forma segura
        if (value is Enum)
        {
            // System.Convert extrae el número base (32 o 48) sin importar el tipo de Enum
            double pixelValue = System.Convert.ToDouble(value);
            return new GridLength(pixelValue, GridUnitType.Pixel);
        }

        // Fallback de seguridad
        return new GridLength(32, GridUnitType.Pixel);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
        => throw new NotImplementedException();
}
