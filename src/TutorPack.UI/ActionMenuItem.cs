namespace TutorPack.UI;

/// <summary>
/// Describes a command displayed by <see cref="ActionMenu"/>.
/// </summary>
public sealed record ActionMenuItem(
    string Id,
    string Label,
    AppIconName Icon,
    bool IsDestructive = false);
