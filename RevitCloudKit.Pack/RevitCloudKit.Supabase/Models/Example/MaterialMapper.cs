// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Extension method that converts a Revit Material to a
//           Supabase DTO. Shows the recommended mapper pattern:
//           static class, extension method on the Revit type.
//           Safe to delete and replace with your own mapper.
// ════════════════════════════════════════════════════════════════════════

namespace RevitCloudKit.Models.Example;

/// <summary>
///     Extension methods for converting Revit types to Supabase DTOs.
/// </summary>
public static class MaterialMapper
{
    /// <summary>
    ///     Converts a Revit <see cref="Material"/> to a <see cref="SupabaseMaterialModel"/>
    ///     ready for insertion. Id and CreatedAt are intentionally omitted —
    ///     the database generates them via DEFAULT.
    /// </summary>
    public static SupabaseMaterialModel ToSupabaseRecord(this Material material)
    {
        return new SupabaseMaterialModel
        {
            Name = material.Name,
            MaterialClass = material.MaterialClass,
            // Pack RGB into a single integer: 0x00RRGGBB
            ColorArgb = material.Color.IsValid
                ? (material.Color.Red << 16) | (material.Color.Green << 8) | material.Color.Blue
                : null
        };
    }
}
