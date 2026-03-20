// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Service interface for Supabase Postgrest CRUD + Storage.
//           Shows how to design a clean service contract that covers
//           the full lifecycle: read, create, update, delete, and
//           file uploads to Supabase Storage buckets.
//           Safe to delete and replace with your own service interface.
// ════════════════════════════════════════════════════════════════════════

using RevitCloudKit.Models.Example;

namespace RevitCloudKit.Services.Example;

/// <summary>
///     Cloud-side material operations against the Supabase backend.
///     Covers Postgrest table operations and Storage bucket file management.
/// </summary>
public interface ICloudMaterialService
{
    // ── Postgrest (database table) ────────────────────────────────────

    Task<IReadOnlyList<SupabaseMaterialModel>> GetMaterialsAsync();

    Task<SupabaseMaterialModel?> GetMaterialAsync(string materialId);

    Task<SupabaseMaterialModel> PublishMaterialAsync(SupabaseMaterialModel material);

    Task<SupabaseMaterialModel> UpdateMaterialAsync(SupabaseMaterialModel material);

    Task DeleteMaterialAsync(string materialId);

    // ── Storage (file bucket) ─────────────────────────────────────────

    Task PublishTextureAsync(string texturePath);

    Task DeleteTextureAsync(string textureName);
}
