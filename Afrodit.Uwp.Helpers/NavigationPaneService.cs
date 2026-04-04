using Microsoft.UI.Xaml.Controls;

namespace Afrodit.WinUI.Helpers;

/// <summary>
/// Gestiona el estado del panel de un NavigationView de forma centralizada.
/// </summary>
public static class NavigationPaneService
{
    /// <summary>
    /// Instancia del NavigationView que se está gestionando.
    /// </summary>
    public static NavigationView NavigationView { get; set; }

    /// <summary>
    /// Alterna el estado (Abierto/Cerrado) del panel.
    /// </summary>
    /// <returns>True si el panel quedó abierto, false si se cerró o si la instancia es nula.</returns>
    public static bool TogglePane()
    {
        if (NavigationView is null) return false;

        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
        return NavigationView.IsPaneOpen;
    }

    /// <summary>
    /// Indica si el panel está actualmente abierto.
    /// </summary>
    public static bool IsPaneOpen => NavigationView?.IsPaneOpen ?? false;
}