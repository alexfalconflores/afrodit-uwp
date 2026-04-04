using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Afrodit.WinUI.Controls;

/// <summary>
/// Proporciona una sintaxis fluida (Fluent API) para construir y configurar 
/// el componente TitleBar directamente desde C#.
/// </summary>
public static class TitleBarMarkup
{
    public static T DataContext<T>(this T element, object dataContext) where T : TitleBar
    {
        element.DataContext = dataContext;
        return element;
    }

    #region Size & Background

    public static T Size<T>(this T element, TitleBarSize titleBarSize) where T : TitleBar
    {
        element.Size = titleBarSize;
        return element;
    }

    public static T Size<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.SizeProperty, binding);
        return element;
    }

    public static T Background<T>(this T element, Brush background) where T : TitleBar
    {
        element.Background = background;
        return element;
    }

    public static T Background<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, Control.BackgroundProperty, binding);
        return element;
    }

    #endregion

    #region App Icon & Name

    public static T AppIconSource<T>(this T element, ImageSource imageSource) where T : TitleBar
    {
        element.AppIconSource = imageSource;
        return element;
    }

    public static T AppIconSource<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.AppIconSourceProperty, binding);
        return element;
    }

    public static T AppName<T>(this T element, string appName) where T : TitleBar
    {
        element.AppName = appName;
        return element;
    }

    public static T AppName<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.AppNameProperty, binding);
        return element;
    }

    #endregion

    #region Subtitle (Anteriormente ReleaseTag)

    public static T Subtitle<T>(this T element, string subtitle) where T : TitleBar
    {
        element.Subtitle = subtitle;
        return element;
    }

    public static T Subtitle<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.SubtitleProperty, binding);
        return element;
    }

    public static T IsSubtitleVisible<T>(this T element, bool isVisible) where T : TitleBar
    {
        element.IsSubtitleVisible = isVisible;
        return element;
    }

    public static T IsSubtitleVisible<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.IsSubtitleVisibleProperty, binding);
        return element;
    }

    #endregion

    #region Navegación y Botones (Ahora usando bool en lugar de Visibility)
    public static T IsBackButtonVisible<T>(this T element, bool isVisible) where T : TitleBar
    {
        element.IsBackButtonVisible = isVisible;
        return element;
    }

    public static T IsBackButtonVisible<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.IsBackButtonVisibleProperty, binding);
        return element;
    }

    public static T IsPaneToggleButtonVisible<T>(this T element, bool isVisible) where T : TitleBar
    {
        element.IsPaneToggleButtonVisible = isVisible;
        return element;
    }

    public static T IsPaneToggleButtonVisible<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.IsPaneToggleButtonVisibleProperty, binding);
        return element;
    }

    #endregion

    #region Inyecciones de Contenido Secundario (AutoSuggestBox, Paneles)

    public static T Content<T>(this T element, object content) where T : TitleBar
    {
        element.Content = content;
        return element;
    }

    public static T PaneLeftContent<T>(this T element, UIElement uIElement) where T : TitleBar
    {
        element.PaneLeftContent = uIElement;
        return element;
    }

    public static T PaneRightContent<T>(this T element, UIElement uIElement) where T : TitleBar
    {
        element.PaneRightContent = uIElement;
        return element;
    }

    #endregion

    #region Eventos (Requested Pattern)

    /// <summary>
    /// Suscribe un manejador al evento BackRequested.
    /// </summary>
    public static T OnBackRequested<T>(this T element, Windows.Foundation.TypedEventHandler<TitleBar, EventArgs> handler) where T : TitleBar
    {
        element.BackRequested += handler;
        return element;
    }

    /// <summary>
    /// Suscribe un manejador al evento PaneToggleRequested.
    /// </summary>
    public static T OnPaneToggleRequested<T>(this T element, Windows.Foundation.TypedEventHandler<TitleBar, EventArgs> handler) where T : TitleBar
    {
        element.PaneToggleRequested += handler;
        return element;
    }

    #endregion

    #region Commands y Parámetros (MVVM)

    // --- Back Button ---

    public static T BackButtonCommand<T>(this T element, ICommand command) where T : TitleBar
    {
        element.BackButtonCommand = command;
        return element;
    }

    public static T BackButtonCommand<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.BackButtonCommandProperty, binding);
        return element;
    }

    public static T BackButtonCommandParameter<T>(this T element, object parameter) where T : TitleBar
    {
        element.BackButtonCommandParameter = parameter;
        return element;
    }

    public static T BackButtonCommandParameter<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.BackButtonCommandParameterProperty, binding);
        return element;
    }

    // --- Pane Toggle Button ---

    public static T PaneToggleButtonCommand<T>(this T element, ICommand command) where T : TitleBar
    {
        element.PaneToggleButtonCommand = command;
        return element;
    }

    public static T PaneToggleButtonCommand<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.PaneToggleButtonCommandProperty, binding);
        return element;
    }

    public static T PaneToggleButtonParameter<T>(this T element, object parameter) where T : TitleBar
    {
        element.PaneToggleButtonParameter = parameter;
        return element;
    }

    public static T PaneToggleButtonParameter<T>(this T element, Binding binding) where T : TitleBar
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        BindingOperations.SetBinding(element, TitleBar.PaneToggleButtonParameterProperty, binding);
        return element;
    }

    #endregion
}
