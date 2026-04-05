using Afrodit.WinUI.Controls;
using Afrodit.WinUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Afrodit.Uwp.Sandbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // 1. Configuras el título que verá Windows (Barra de tareas / Task Manager)
            var view = ApplicationView.GetForCurrentView();
            view.Title = "Sandbox"; // Windows mostrará: "NombreDeApp - Sandbox"

            NavigationPaneService.NavigationView = SandboxNavView;
        }

        // Evento del Botón Atrás
        private void AppTitleBar_BackRequested(TitleBar sender, EventArgs args)
        {
            StatusTextBlock.Text = "🔙 Disparaste el evento: Ir Atrás";

            // Opcional: Aquí iría tu lógica real:
            // if (MyFrame.CanGoBack) MyFrame.GoBack();
        }

        // Evento del Botón de Menú
        private void AppTitleBar_PaneToggleRequested(TitleBar sender, EventArgs args)
        {
            StatusTextBlock.Text = "🍔 Disparaste el evento: Menú Hamburguesa";

            // Nota: Como no hemos bloqueado la ejecución con un Command falso, 
            // además de cambiar este texto, verás que Afrodit abre el panel lateral 
            // automáticamente gracias a nuestro fallback interno en TitleBar.cs.
        }

        private async void OnOpenOverlayClicked(object sender, RoutedEventArgs e)
        {
            // 1. Construimos el contenido interno que irá dentro del Overlay
            var internalStack = new StackPanel { Spacing = 12 };
            internalStack.Children.Add(new TextBlock
            {
                Text = "¿Estás seguro de que deseas eliminar este elemento de Nokana? Esta acción no se puede deshacer.",
                TextWrapping = TextWrapping.Wrap
            });

            var confirmButton = new Button { Content = "Sí, eliminar", Style = (Style)Application.Current.Resources["AccentButtonStyle"] };
            internalStack.Children.Add(confirmButton);

            // 2. Construimos y configuramos el Overlay usando tu Fluent API
            var testOverlay = new OverlayView()
                .Title("Confirmación de Seguridad")
                .CloseButtonToolTip("Cancelar y cerrar")
                //.TitleBarHeight(48) // Ajusta esto si tu TitleBar está en modo Tall
                .Content(internalStack);

            // 3. Programamos el cierre manual si hace clic en el botón interno
            confirmButton.Click += (s, args) => testOverlay.Hide();

            // 4. Mostramos el Overlay y esperamos su resultado
            var result = await testOverlay.ShowAsync();

            // 5. (Opcional) Vemos en consola cómo se cerró
            System.Diagnostics.Debug.WriteLine($"Overlay finalizado con resultado: {result}");
        }
    }
}
