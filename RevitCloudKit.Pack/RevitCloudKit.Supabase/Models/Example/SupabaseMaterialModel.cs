// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Supabase Postgrest DTO mapped to the "materials" table.
//           Study this to understand how to define column mappings,
//           primary keys, nullable fields, and NullValueHandling.
//           Safe to delete and replace with your own table model.
// ════════════════════════════════════════════════════════════════════════

using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RevitCloudKit.Models.Example;

/// <summary>
///     Supabase Postgrest DTO for the "materials" table.
///     All properties are nullable because Supabase deserializes via reflection,
///     bypassing constructors — nullable avoids false non-null guarantees.
///     Supabase SQL:
///     <code>
///     create table materials (
///         id             uuid        primary key default gen_random_uuid(),
///         name           text        not null,
///         material_class text,
///         color_argb     integer,
///         created_at     timestamptz default now()
///     );
///     </code>
/// </summary>
[Table("materials")]
public class SupabaseMaterialModel : BaseModel
{
    [PrimaryKey("id", false)]
    public string? Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("material_class")]
    public string? MaterialClass { get; set; }

    [Column("color_argb")]
    public int? ColorArgb { get; set; }

    // NullValueHandling.Ignore omits this field from INSERT payloads,
    // so Postgres DEFAULT now() fires instead of receiving DateTime.MinValue.
    [Column("created_at", NullValueHandling.Ignore)]
    public DateTime? CreatedAt { get; set; }
}
