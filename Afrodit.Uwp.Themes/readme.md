# 📖 Afrodit.Uwp.Themes

El paquete **Afrodit.Uwp.Themes** de Afrodit UI proporciona un conjunto de estilos y recursos temáticos predefinidos para aplicaciones UWP. Incluye temas claros, oscuros y personalizados que se integran perfectamente con el Fluent Design System, permitiendo a los desarrolladores aplicar una apariencia moderna y coherente a sus aplicaciones con facilidad.


[![NuGet](https://img.shields.io/nuget/dt/Afrodit.Uwp.Themes.svg)](https://www.nuget.org/stats/packages/Afrodit.Uwp.Themes?groupby=Version) 
[![NuGet](https://img.shields.io/nuget/vpre/Afrodit.Uwp.Themes.svg)](https://www.nuget.org/packages/Afrodit.Uwp.Themes/)
[![Platform](https://img.shields.io/badge/platform-UWP-brightgreen.svg)]()
<a href="https://www.nuget.org/packages/Afrodit.Uwp.Themes">
    <img src="https://raw.githubusercontent.com/alexfalconflores/alexfalconflores/main/img/nuget-banner.svg" height=20 alt="Go to Nuget"/>
</a>

## 🚀 Instalación

```bash
Install-Package Afrodit.Uwp.Themes
```
> Nota de Dependencia: Este paquete requiere `Microsoft.UI.Xaml` (WinUI 2.8+) preinstalado en el proyecto destino.

## 💻 Uso Básico (XAML)

Para aplicar un tema a tu aplicación, simplemente agrega el diccionario de recursos del tema deseado en la sección `Application.Resources` de tu `App.xaml`:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
            
            <ResourceDictionary Source="ms-appx:///Afrodit.Uwp.Themes/AfroditResources.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

## Catalogo de Temas Disponibles

### Button
Superan el estilo por defecto de Windows implementando semántica real y Lightweight Styling para lograr animaciones perfectas sin parpadeos.

```xaml
<Button Content="Eliminar" Style="{StaticResource DangerButtonStyle}" />
<Button Content="Guardar" Style="{StaticResource SuccessButtonStyle}" />
<Button Content="Precaución" Style="{StaticResource WarningButtonStyle}" />
<Button Content="Información" Style="{StaticResource InfoButtonStyle}" />
<Button Content="Secundario" Style="{StaticResource OutlineButtonStyle}" />
<Button Content="Discreto" Style="{StaticResource SubtleButtonStyle}" />
<Button Content="Moderno" Style="{StaticResource SoftButtonStyle}" />
<Button Style="{StaticResource IconButtonStyle}">
    <FontIcon Glyph="&#xE713;" FontSize="16" />
</Button>

<Button Style="{StaticResource FabButtonStyle}">
    <FontIcon Glyph="&#xE710;" FontSize="20" />
</Button>
```
### Button Icon & FABs
```xaml
<Button Style="{StaticResource IconButtonStyle}">
    <FontIcon Glyph="&#xE713;" FontSize="16" />
</Button>

<Button Style="{StaticResource FabButtonStyle}">
    <FontIcon Glyph="&#xE710;" FontSize="20" />
</Button>
```

Variantes disponibles: `AccentIconButtonStyle`, `DangerIconButtonStyle`, `SuccessIconButtonStyle`, `WarningIconButtonStyle`, `InfoIconButtonStyle`, `OutlineIconButtonStyle`, `SubtleIconButtonStyle`, `SoftIconButtonStyle`.

### Input
```xaml
<TextBox Style="{StaticResource AfroditTextBoxStyle}" PlaceholderText="Escribe aquí..." />

<TextBox Style="{StaticResource AfroditGhostTextBoxStyle}" PlaceholderText="Sin título" />
```

### Typography
```xaml
<TextBlock Text="Título Principal" Style="{StaticResource AfroditHeaderStyle}" />
<TextBlock Text="Subtítulo" Style="{StaticResource AfroditTitleStyle}" />
<TextBlock Text="Cuerpo de lectura" Style="{StaticResource AfroditBodyStyle}" />
<TextBlock Text="Mensaje de error" Style="{StaticResource AfroditDangerTextStyle}" />
<TextBlock Text="Metadatos o Fecha" Style="{StaticResource AfroditCaptionStyle}" />
```

Diseñado con ♥ para el ecosistema UWP moderno.