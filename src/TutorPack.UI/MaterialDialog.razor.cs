using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TutorPack.UI;

/// <summary>A native modal surface with shared desktop and mobile scrolling behavior.</summary>
public partial class MaterialDialog
{
    [Inject] private IJSRuntime JS { get; set; } = default!;

    /// <summary>DOM identifier of the dialog's visible heading.</summary>
    [Parameter, EditorRequired] public string LabelledBy { get; set; } = string.Empty;

    /// <summary>Additional classes for the screen's size and layout.</summary>
    [Parameter] public string? Class { get; set; }

    /// <summary>Uses the whole viewport on mobile instead of a bottom sheet.</summary>
    [Parameter] public bool FullScreenOnMobile { get; set; }

    /// <summary>Notifies the parent after dismissal by Escape or the backdrop.</summary>
    [Parameter] public EventCallback Closed { get; set; }

    /// <summary>Header and scrollable content supplied by the screen.</summary>
    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

    private ElementReference _dialog;

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await JS.InvokeVoidAsync("tutorPackControls.showDialog", _dialog);
    }
}
