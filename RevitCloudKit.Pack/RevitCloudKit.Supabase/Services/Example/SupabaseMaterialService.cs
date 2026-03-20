// ════════════════════════════════════════════════════════════════════════
// EXAMPLE — Full Supabase Postgrest CRUD + Storage implementation.
//           Shows: Get all, Get single, Insert with RETURNING,
//           Update with RETURNING, Delete by filter, and file Upload/Remove
//           against a Supabase Storage bucket.
//           Safe to delete and replace with your own service.
// ════════════════════════════════════════════════════════════════════════

using System.IO;
using RevitCloudKit.Models.Example;
using Supabase.Postgrest;
using Client = Supabase.Client;

namespace RevitCloudKit.Services.Example;

/// <summary>
///     <see cref="ICloudMaterialService"/> implementation backed by Supabase Postgrest.
/// </summary>
public class SupabaseMaterialService(Client supabaseClient) : ICloudMaterialService
{
    /// <summary>
    ///     Name of the bucket in Supabase Storage.
    ///     Create this bucket in your Supabase project under Storage.
    /// </summary>
    private const string BucketName = "textures";

    // ── Postgrest ─────────────────────────────────────────────────────

    public async Task<IReadOnlyList<SupabaseMaterialModel>> GetMaterialsAsync()
    {
        var response = await supabaseClient
            .From<SupabaseMaterialModel>()
            .Get();
        return response.Models;
    }

    public async Task<SupabaseMaterialModel?> GetMaterialAsync(string materialId)
    {
        var response = await supabaseClient
            .From<SupabaseMaterialModel>()
            .Where(m => m.Id == materialId)
            .Single();
        return response;
    }

    public async Task<SupabaseMaterialModel> PublishMaterialAsync(SupabaseMaterialModel material)
    {
        // Representation = ask Postgres to return the inserted row (including generated id, created_at).
        var options = new QueryOptions { Returning = QueryOptions.ReturnType.Representation };
        var result = await supabaseClient
            .From<SupabaseMaterialModel>()
            .Insert(material, options);
        return result.Model ?? throw new InvalidOperationException("Supabase did not return the created record.");
    }

    public async Task<SupabaseMaterialModel> UpdateMaterialAsync(SupabaseMaterialModel material)
    {
        var options = new QueryOptions { Returning = QueryOptions.ReturnType.Representation };
        var result = await supabaseClient
            .From<SupabaseMaterialModel>()
            .Update(material, options); // .Upsert() is also available
        return result.Model ?? throw new InvalidOperationException("Supabase did not return the updated record.");
    }

    public async Task DeleteMaterialAsync(string materialId)
    {
        await supabaseClient
            .From<SupabaseMaterialModel>()
            .Where(model => model.Id == materialId)
            .Delete();
    }

    // ── Storage ───────────────────────────────────────────────────────

    public async Task PublishTextureAsync(string texturePath)
    {
        var nameInStorage = Path.GetFileName(texturePath);
        await supabaseClient
            .Storage
            .From(BucketName)
            .Upload(texturePath, nameInStorage);
    }

    public async Task DeleteTextureAsync(string textureName)
    {
        await supabaseClient
            .Storage
            .From(BucketName)
            .Remove(textureName);
    }
}
