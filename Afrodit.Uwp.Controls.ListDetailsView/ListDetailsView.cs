using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Afrodit.WinUI.Controls;

[TemplatePart(Name = "ListColumn", Type = typeof(ColumnDefinition))]
[TemplatePart(Name = "DetailsColumn", Type = typeof(ColumnDefinition))]
[TemplatePart(Name = "InlineBackButton", Type = typeof(Button))]
public sealed class ListDetailsView : Control
{
    private Button _inlineBackButton;

    public ListDetailsView()
    {
        this.DefaultStyleKey = typeof(ListDetailsView);
        this.SizeChanged += OnSizeChanged;
    }

    public event EventHandler<object> SelectionChanged;
    public event EventHandler BackRequested;

    public void GoBack()
    {
        if (CanGoBack)
        {
            SelectedItem = null;
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(ListDetailsView), new PropertyMetadata(null));

    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ListDetailsView), new PropertyMetadata(null, OnSelectedItemChanged));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(ListDetailsView), new PropertyMetadata(null));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly DependencyProperty DetailsTemplateProperty =
        DependencyProperty.Register(nameof(DetailsTemplate), typeof(DataTemplate), typeof(ListDetailsView), new PropertyMetadata(null));

    public DataTemplate DetailsTemplate
    {
        get => (DataTemplate)GetValue(DetailsTemplateProperty);
        set => SetValue(DetailsTemplateProperty, value);
    }

    public static readonly DependencyProperty NoSelectionContentProperty =
        DependencyProperty.Register(nameof(NoSelectionContent), typeof(object), typeof(ListDetailsView), new PropertyMetadata("Selecciona un elemento para ver los detalles."));

    public object NoSelectionContent
    {
        get => GetValue(NoSelectionContentProperty);
        set => SetValue(NoSelectionContentProperty, value);
    }

    public static readonly DependencyProperty CompactModeThresholdWidthProperty =
        DependencyProperty.Register(nameof(CompactModeThresholdWidth), typeof(double), typeof(ListDetailsView), new PropertyMetadata(720.0, OnThresholdChanged));

    public double CompactModeThresholdWidth
    {
        get => (double)GetValue(CompactModeThresholdWidthProperty);
        set => SetValue(CompactModeThresholdWidthProperty, value);
    }

    public static readonly DependencyProperty ListPaneWidthProperty =
        DependencyProperty.Register(nameof(ListPaneWidth), typeof(double), typeof(ListDetailsView), new PropertyMetadata(320.0));

    public double ListPaneWidth
    {
        get => (double)GetValue(ListPaneWidthProperty);
        set => SetValue(ListPaneWidthProperty, value);
    }

    public static readonly DependencyProperty InlineBackButtonVisibilityProperty =
        DependencyProperty.Register(nameof(InlineBackButtonVisibility), typeof(Visibility), typeof(ListDetailsView), new PropertyMetadata(Visibility.Visible));

    public Visibility InlineBackButtonVisibility
    {
        get => (Visibility)GetValue(InlineBackButtonVisibilityProperty);
        set => SetValue(InlineBackButtonVisibilityProperty, value);
    }

    public static readonly DependencyProperty CanGoBackProperty =
        DependencyProperty.Register(nameof(CanGoBack), typeof(bool), typeof(ListDetailsView), new PropertyMetadata(false));

    public bool CanGoBack
    {
        get => (bool)GetValue(CanGoBackProperty);
        private set => SetValue(CanGoBackProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_inlineBackButton != null)
            _inlineBackButton.Click -= OnBackButtonClicked;

        _inlineBackButton = GetTemplateChild("InlineBackButton") as Button;

        if (_inlineBackButton != null)
            _inlineBackButton.Click += OnBackButtonClicked;

        UpdateVisualStates(false);
    }

    private void OnBackButtonClicked(object sender, RoutedEventArgs e)
    {
        GoBack();
    }


    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (ListDetailsView)d;
        control.SelectionChanged?.Invoke(control, e.NewValue);
        control.UpdateVisualStates(true);
    }

    private static void OnThresholdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((ListDetailsView)d).UpdateVisualStates(false);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var wasCompact = e.PreviousSize.Width < CompactModeThresholdWidth;
        var isCompact = e.NewSize.Width < CompactModeThresholdWidth;

        UpdateCanGoBackState();

        if (wasCompact != isCompact)
        {
            UpdateVisualStates(true);
        }
    }

    private void UpdateCanGoBackState()
    {
        var isCompact = this.ActualWidth < CompactModeThresholdWidth;
        var hasSelection = SelectedItem != null;
        CanGoBack = isCompact && hasSelection;
    }

    private void UpdateVisualStates(bool useTransitions)
    {
        var hasSelection = SelectedItem != null;
        var selectionState = hasSelection ? "HasSelection" : "NoSelection";

        VisualStateManager.GoToState(this, selectionState, useTransitions);

        UpdateCanGoBackState();
        var isCompact = this.ActualWidth < CompactModeThresholdWidth;

        string layoutState;

        if (isCompact)
        {
            if (hasSelection)
            {
                layoutState = "NarrowDetails";
            }
            else
            {
                layoutState = "NarrowList";
            }
        }
        else
        {
            layoutState = "Wide";
        }

        VisualStateManager.GoToState(this, layoutState, useTransitions);
    }
}
