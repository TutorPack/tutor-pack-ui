using Microsoft.AspNetCore.Components;

namespace TutorPack.UI;

/// <summary>Search field with shared appearance and immediate input notifications.</summary>
public partial class MaterialSearch
{
    /// <summary>Current search text.</summary>
    [Parameter]
    public string Value { get; set; } = string.Empty;

    /// <summary>Notifies the parent on input; filtering or debouncing remains in the application.</summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>Accessible name, available even when the field contains text.</summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>Hint displayed when the field is empty.</summary>
    [Parameter]
    public string Placeholder { get; set; } = "Поиск";

    /// <summary>Prevents editing while the associated operation is unavailable.</summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>Optional input length limit imposed by the search endpoint.</summary>
    [Parameter]
    public int? MaxLength { get; set; }

    private Task HandleInputAsync(ChangeEventArgs args) =>
        ValueChanged.InvokeAsync(args.Value?.ToString() ?? string.Empty);
}
