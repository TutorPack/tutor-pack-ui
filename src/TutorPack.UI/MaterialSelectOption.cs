namespace TutorPack.UI;

/// <summary>
/// A value and label displayed by <see cref="MaterialSelect{TValue}"/>.
/// </summary>
public sealed record MaterialSelectOption<TValue>(TValue Value, string Label)
    where TValue : struct;
