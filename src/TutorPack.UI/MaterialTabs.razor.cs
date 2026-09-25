using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace TutorPack.UI;

/// <summary>Accessible horizontal tabs, rendered only when there is more than one option.</summary>
/// <typeparam name="TValue">The application's tab identifier.</typeparam>
public partial class MaterialTabs<TValue>
{
    /// <summary>Tabs in display and keyboard navigation order.</summary>
    [Parameter, EditorRequired]
    public IReadOnlyList<MaterialTabOption<TValue>> Options { get; set; } = [];

    /// <summary>The selected tab.</summary>
    [Parameter]
    public TValue Value { get; set; } = default!;

    /// <summary>Notifies the parent when another tab is selected.</summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>Accessible name of the tab list.</summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = string.Empty;

    private readonly Dictionary<string, ElementReference> _elements = [];

    private bool IsSelected(MaterialTabOption<TValue> option) =>
        EqualityComparer<TValue>.Default.Equals(Value, option.Value);

    private async Task HandleKeyAsync(KeyboardEventArgs args, int index)
    {
        var target = args.Key switch
        {
            "ArrowLeft" => (index + Options.Count - 1) % Options.Count,
            "ArrowRight" => (index + 1) % Options.Count,
            "Home" => 0,
            "End" => Options.Count - 1,
            _ => -1,
        };
        if (target < 0)
            return;

        await _elements[Options[target].Id].FocusAsync(preventScroll: true);
        await ValueChanged.InvokeAsync(Options[target].Value);
    }
}

/// <summary>A tab and its connection to the panel rendered by the parent.</summary>
/// <param name="Value">Application tab identifier.</param>
/// <param name="Label">Visible tab name.</param>
/// <param name="Id">Unique DOM identifier for the tab.</param>
/// <param name="PanelId">DOM identifier of the controlled panel.</param>
/// <param name="Count">Optional count displayed alongside the name.</param>
public sealed record MaterialTabOption<TValue>(TValue Value, string Label, string Id, string PanelId, int? Count = null);
