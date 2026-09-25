using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace TutorPack.UI;

/// <summary>Outlined text, date, time, or multiline input with shared labels and form validation.</summary>
public partial class MaterialInput<TValue>
{
    private readonly string _generatedId = $"material-input-{Guid.NewGuid():N}";
    private string? _parseError;

    /// <summary>Visible and accessible field label.</summary>
    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    /// <summary>Native HTML input type; dates retain the browser's date picker.</summary>
    [Parameter] public string Type { get; set; } = "text";
    /// <summary>Renders a textarea instead of a single-line input.</summary>
    [Parameter] public bool Multiline { get; set; }
    /// <summary>Updates the bound value while typing instead of on change.</summary>
    [Parameter] public bool Immediate { get; set; }
    /// <summary>Optional icon aligned like the icon in MaterialSelect.</summary>
    [Parameter] public AppIconName? LeadingIcon { get; set; }
    /// <summary>Optional description linked to the input.</summary>
    [Parameter] public string? Hint { get; set; }
    /// <summary>Optional stable input identifier.</summary>
    [Parameter] public string? Id { get; set; }
    /// <summary>Layout classes applied to the containing field.</summary>
    [Parameter] public string? Class { get; set; }

    private string InputId => Id ?? _generatedId;
    private bool Invalid => _parseError is not null || (EditContext?.GetValidationMessages(FieldIdentifier).Any() ?? false);
    private string? DescriptionId => Hint is not null || Invalid ? $"{InputId}-description" : null;

    private void Change(ChangeEventArgs args)
    {
        if (!Immediate)
            SetValue(args);
    }

    private void Input(ChangeEventArgs args)
    {
        if (Immediate)
            SetValue(args);
    }

    private void SetValue(ChangeEventArgs args)
    {
        _parseError = null;
        CurrentValueAsString = args.Value?.ToString();
    }

    /// <inheritdoc />
    protected override string? FormatValueAsString(TValue? value) => value switch
    {
        DateOnly date => date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        TimeOnly time => time.ToString("HH:mm", CultureInfo.InvariantCulture),
        _ => value?.ToString()
    };

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out TValue result,
        [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result!))
        {
            validationErrorMessage = null;
            return true;
        }
        validationErrorMessage = _parseError = $"Проверьте значение поля «{Label}».";
        return false;
    }
}
