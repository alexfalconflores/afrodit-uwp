using Microsoft.UI.Xaml.Controls.AnimatedVisuals;
using Microsoft.UI.Xaml.Controls;

namespace Afrodit.WinUI.Animations.Fluent;

/// <summary>
/// Proporciona acceso a las animaciones nativas de Fluent Design de WinUI 2.8
/// mapeadas al ecosistema Afrodit.
/// </summary>
public static class FluentIcons
{
    // Mapeamos la animación nativa de "Volver"
    public static IAnimatedVisualSource BackDirection => new AnimatedBackVisualSource();

    // Mapeamos la animación nativa del menú hamburguesa
    public static IAnimatedVisualSource GlobalNavigation => new AnimatedGlobalNavigationButtonVisualSource();
}
