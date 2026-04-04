# 📖 Afrodit.Uwp.Controls.TitleBar

El componente **TitleBar** de Afrodit UI es un control nativo avanzado para UWP diseñado para integrarse de forma transparente con el Fluent Design System. Reemplaza la barra de título tradicional de Windows permitiendo la inyección de contenido, manejo de eventos de navegación (hamburguesa y retroceso), soporte completo para MVVM y una API fluida para C# Markup.

[![NuGet](https://img.shields.io/nuget/dt/Afrodit.Uwp.Controls.TitleBar.svg)](https://www.nuget.org/stats/packages/Afrodit.Uwp.Controls.TitleBar?groupby=Version) 
[![NuGet](https://img.shields.io/nuget/vpre/Afrodit.Uwp.Controls.TitleBar.svg)](https://www.nuget.org/packages/Afrodit.Uwp.Controls.TitleBar/)
[![Platform](https://img.shields.io/badge/platform-UWP-brightgreen.svg)]()
<a href="https://www.nuget.org/packages/Afrodit.Uwp.Controls.TitleBar">
    <img src="https://raw.githubusercontent.com/alexfalconflores/alexfalconflores/main/img/nuget-banner.svg" height=20 alt="Go to Nuget"/>
</a>

## 🚀 Instalación

```bash
Install-Package Afrodit.Uwp.Controls.TitleBar -Version 1.0.0
```
> Nota de Dependencia: Este paquete requiere `Microsoft.UI.Xaml` (WinUI 2.8+) preinstalado en el proyecto destino.

## 💻 Uso Básico (XAML)
Para usar el control en tu página, primero agrega el espacio de nombres en la cabecera de tu `Page`:

```xml
xmlns:afrodit="using:Afrodit.WinUI.Controls"
```

Luego, inserta el `TitleBar` en la fila superior de tu `Grid` principal. 
Aprovecha la propiedad `Content` nativa para inyectar un buscador o cualquier elemento en el centro de la barra:

```xml
<afrodit:TitleBar
    AppName="Nokana"
    Subtitle="Bandeja de Entrada"
    IsSubtitleVisible="True"
    IsBackButtonVisible="True"
    IsPaneToggleButtonVisible="True"
    Size="Tall"
    BackRequested="TitleBar_BackRequested"
    PaneToggleRequested="TitleBar_PaneToggleRequested">

    <AutoSuggestBox PlaceholderText="Buscar..." Width="280" VerticalAlignment="Center" />

    <afrodit:TitleBar.PaneLeftContent>
        <MenuBar Background="Transparent" Margin="8,0,0,0">
            <MenuBarItem Title="Archivo" />
            <MenuBarItem Title="Editar" />
        </MenuBar>
    </afrodit:TitleBar.PaneLeftContent>

    <afrodit:TitleBar.PaneRightContent>
        <PersonPicture DisplayName="Usuario" Height="32" Margin="0,0,16,0" />
    </afrodit:TitleBar.PaneRightContent>

</afrodit:TitleBar>
```

## ⚡ Uso Avanzado (C# Fluent API)
Si prefieres construir tu interfaz 100% desde el código (C# Markup), 
el espacio de nombres incluye métodos de extensión diseñados para crear una cadena fluida, tipada y segura:

```csharp
// Ejemplo de inicialización fluida
var titleBar = new TitleBar()
    .AppName("Irisa")
    .Subtitle("Redactando nota...")
    .Size(TitleBarSize.Tall)
    .IsBackButtonVisible(true)
    .Content(new SearchBox { PlaceholderText = "Buscar en el historial..." })
    .OnBackRequested(e => MyNavigationService.GoBack());
```

## 📚 Referencia de la API

### 🎨 Propiedades Visuales y de Estado

| Propiedad | Tipo | Descripción |
| :--- | :--- | :--- |
| **Size** | `TitleBarSize` | Altura de la barra. Valores: `Standard` (32px) o `Tall` (48px). |
| **AppIconSource** | `ImageSource` | Ruta del icono de la aplicación (ej. `Assets/StoreLogo.png`). |
| **IsAppIconVisible** | `Visibility` | Define si el icono de la aplicación se muestra o se oculta. |
| **AppName** | `string` | Nombre principal de la aplicación. |
| **Subtitle** | `string` | Texto secundario útil para estados (ej. "Preview", "Guardando..."). |
| **IsSubtitleVisible** | `bool` | Controla la visibilidad del subtítulo de forma independiente. |
| **IsBackButtonVisible** | `bool` | Muestra u oculta el botón de retroceso nativo. |
| **IsPaneToggleButton** | `bool` | Muestra u oculta el botón de Menú Hamburguesa nativo. |

### 📥 Zonas de Inyección de Contenido

| Propiedad | Tipo | Descripción |
| :--- | :--- | :--- |
| **PaneLeftContent** | `object` | Se renderiza tras el título. Ideal para controles `MenuBar`. |
| **Content** | `object` | Propiedad por defecto. Toma el espacio central y se expande (`Stretch`). |
| **PaneRightContent** | `object` | Se alinea a la derecha, protegido de los botones de sistema. |

### ⚡Eventos y Comandos (Soporte MVVM)

| Miembro | Tipo | Descripción |
| :--- | :--- | :--- |
| **BackRequested** | `Event` | Se dispara al hacer clic en el botón de retroceso. |
| **PaneToggleRequested** | `Event` | Se dispara al hacer clic en el menú hamburguesa. |
| **BackButtonCommand** | `ICommand` | Comando vinculado a la acción física del botón de retroceso. |
| **PaneToggleButtonCommand** | `ICommand` | Comando vinculado al toggle del panel lateral. |

Diseñado con ♥ para el ecosistema UWP moderno.