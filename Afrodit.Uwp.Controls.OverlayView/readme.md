# Afrodit UI - OverlayView Control

Un control modal inmersivo a pantalla completa para aplicaciones UWP (Universal Windows Platform), diseñado como parte integral del ecosistema **Afrodit UI**. 

A diferencia del `ContentDialog` tradicional, `OverlayView` está optimizado para apoderarse de toda la superficie de la ventana, proporcionando una experiencia inmersiva ideal para vistas de configuración, creación de registros o flujos críticos, manteniendo una estricta seguridad de foco y navegación.

## 🚀 Características Principales

* **Fluent API Integrada:** Configuración 100% mediante C# Markup encadenado, eliminando la necesidad de ensuciar el XAML principal.
* **Geometría Consciente del Sistema:** Ajuste dinámico a través de `TitleBarHeight` para evitar colisiones con los *Caption Buttons* (Minimizar, Maximizar, Cerrar) del Desktop Window Manager (DWM).
* **Focus Trapping (A11y):** Bucle de foco de teclado integrado para evitar que el usuario interactúe accidentalmente con la aplicación en segundo plano mediante la tecla `Tab`.
* **Seguridad de Navegación:** Intercepción nativa de la tecla `ESC` y el botón de retroceso del sistema (`SystemNavigationManager`), previniendo cierres accidentales o navegaciones fantasma.
* **Prevención de Reentrada:** Semáforo asíncrono integrado que bloquea dobles clics rápidos, garantizando la estabilidad de la UI.
* **Modalidad Estricta:** Bloqueo absoluto de *Light Dismiss* por clic exterior para forzar la interacción explícita del usuario con el contenido del modal.
* **Soporte MVVM:** Herencia automática del `DataContext` desde el árbol visual raíz hacia la isla del `Popup`.

## 📦 Instalación

Disponible a través del administrador de paquetes NuGet local de Afrodit:

```powershell
Install-Package Afrodit.Uwp.Controls.OverlayView
```

## 🛠️ Uso Básico (C# Markup)

La forma recomendada de instanciar y mostrar el OverlayView es utilizando su Fluent API directamente desde el Code-Behind o el ViewModel:

```csharp
using Afrodit.WinUI.Controls;

// 1. Construir el contenido interno
var internalStack = new StackPanel { Spacing = 12 };
internalStack.Children.Add(new TextBlock { Text = "¿Deseas continuar con esta acción crítica?" });

var confirmButton = new Button { Content = "Confirmar" };
internalStack.Children.Add(confirmButton);

// 2. Configurar el OverlayView
var myOverlay = new OverlayView()
    .Title("Atención")
    .TitleBarHeight(48) // Ajustar si se usa ExtendViewIntoTitleBar
    .CloseButtonToolTip("Cancelar")
    .Content(internalStack);

// 3. Programar el cierre desde el contenido interno
confirmButton.Click += (s, args) => myOverlay.Hide();

// 4. Mostrar y esperar resultado
var result = await myOverlay.ShowAsync();

if (result == OverlayViewResult.Close)
{
    // Lógica al cerrar (por la X, ESC o System Back)
}
```
## ⚙️ Referencia de la API

Propiedades de Dependencia

| Propiedad | Tipo | Descripción |
| :--- | :--- | :--- |
| `Title` | `object` | El título que se mostrará en la cabecera. Soporta strings o controles UI complejos. |
| `TitleTemplate` | `DataTemplate` | Plantilla opcional para estilizar el objeto inyectado en el título. |
| `TitleBarHeight` | `double` | Altura reservada en la parte superior. Si es `0`, el encabezado desciende automáticamente 48px para proteger los controles del sistema. |
| `CloseButtonToolTip` | `string` | Texto de Accesibilidad (A11y) y Tooltip para el botón nativo de cierre. |
| `BackdropBrush` | `Brush` | Pincel para el oscurecimiento del fondo. Por defecto es un negro traslúcido. |

Métodos
- `Task<OverlayViewResult> ShowAsync()`: Despliega el overlay de forma asíncrona y devuelve una tarea que se completa cuando se cierra.
- `void Hide()`: Cierra el overlay programáticamente y resuelve la tarea con el resultado `None`.

Eventos
- `Opened`: Se dispara cuando la superficie del overlay y sus animaciones de transición han comenzado a mostrarse.
- `Closed`: Se dispara cuando el overlay se oculta. Proporciona `OverlayClosedEventArgs` detallando el `Result` y el `Reason` exacto del cierre (`CloseButton`, `EscapeKey`, `SystemBack`, `Programmatic`).

Diseñado con ♥ para el ecosistema UWP moderno.