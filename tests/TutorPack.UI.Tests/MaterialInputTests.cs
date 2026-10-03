using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TutorPack.UI;

namespace TutorPack.UI.Tests;

public sealed class MaterialInputTests
{
    [Fact]
    public void OptionalDate_FormatsParsesAndClearsWithoutLosingValidation()
    {
        using var context = new BunitContext();
        var model = new Fields { Date = new DateOnly(2026, 9, 20) };
        var edit = new EditContext(model);
        var field = context.Render<CascadingValue<EditContext>>(parameters => parameters
            .Add(value => value.Value, edit)
            .AddChildContent<MaterialInput<DateOnly?>>(input => input
                .Add(value => value.Label, "Дата")
                .Add(value => value.Type, "date")
                .Add(value => value.Value, model.Date)
                .Add(value => value.ValueChanged, value => model.Date = value)
                .Add(value => value.ValueExpression, () => model.Date)));

        Assert.Equal("2026-09-20", field.Find("input").GetAttribute("value"));
        Assert.Equal(field.Find("input").Id, field.Find("label").GetAttribute("for"));
        field.Find("input").Change("invalid");
        Assert.NotEmpty(edit.GetValidationMessages());
        Assert.Equal("true", field.Find("input").GetAttribute("aria-invalid"));
        Assert.Equal(new DateOnly(2026, 9, 20), model.Date);
        field.Find("input").Change("2026-10-01");
        Assert.Equal(new DateOnly(2026, 10, 1), model.Date);
        Assert.Empty(edit.GetValidationMessages());
        field.Find("input").Change("");
        Assert.Null(model.Date);
        Assert.Empty(edit.GetValidationMessages());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Text_RespectsUpdateModeAndNativeFormAttributes(bool immediate)
    {
        using var context = new BunitContext();
        var model = new Fields();
        var field = context.Render<MaterialInput<string>>(parameters => parameters
            .Add(value => value.Label, "Пароль")
            .Add(value => value.Type, "password")
            .Add(value => value.Value, model.Text)
            .Add(value => value.ValueChanged, value => model.Text = value)
            .Add(value => value.ValueExpression, () => model.Text)
            .Add(value => value.Immediate, immediate)
            .AddUnmatched("name", "password")
            .AddUnmatched("autocomplete", "current-password"));

        Assert.Equal("password", field.Find("input").GetAttribute("name"));
        Assert.Equal("current-password", field.Find("input").GetAttribute("autocomplete"));
        field.Find("input").Input("new value");
        Assert.Equal(immediate ? "new value" : "", model.Text);
        field.Find("input").Change("new value");
        Assert.Equal("new value", model.Text);
    }

    [Fact]
    public void Multiline_PreservesTextAndDescribesItsHint()
    {
        using var context = new BunitContext();
        var model = new Fields { Text = "Первая строка\nВторая строка" };
        var field = context.Render<MaterialInput<string>>(parameters => parameters
            .Add(value => value.Label, "Причина")
            .Add(value => value.Hint, "До 500 символов")
            .Add(value => value.Multiline, true)
            .Add(value => value.Value, model.Text)
            .Add(value => value.ValueChanged, value => model.Text = value)
            .Add(value => value.ValueExpression, () => model.Text));
        var textarea = field.Find("textarea");
        var description = textarea.GetAttribute("aria-describedby");
        Assert.Contains("До 500 символов", field.Find($"#{description}").TextContent);
        textarea.Change("Новая\nпричина");
        Assert.Equal("Новая\nпричина", model.Text);
    }

    private sealed class Fields
    {
        public DateOnly? Date { get; set; }
        public string Text { get; set; } = "";
    }
}
