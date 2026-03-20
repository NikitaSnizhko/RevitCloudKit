using System.IO;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

namespace RevitCloudKit.Services;

public class SupabaseSessionHandler : IGotrueSessionPersistence<Session>
{
    private const string CacheFileName = "supabase_session_cache.json";

    public void SaveSession(Session session)
    {
        //Example of saving session.
        var cacheFile = GetCacheFilePath();

        var sessionJson = JsonConvert.SerializeObject(session);

        using var sessionFile = new StreamWriter(cacheFile, append:false);
        sessionFile.Write(sessionJson);
    }

    public void DestroySession()
    {
        //Example of destroying session.
        var cacheFile = GetCacheFilePath();
        if (File.Exists(cacheFile))
        {
            File.Delete(cacheFile);
        }
    }

    public Session? LoadSession()
    {
        //Example of loading session.
        var cacheFile = GetCacheFilePath();
        if (!File.Exists(cacheFile)) return null;

        using var sessionFile = new StreamReader(cacheFile);
        var sessionJsonString = sessionFile.ReadToEnd();
        return string.IsNullOrEmpty(sessionJsonString)
            ? null
            : JsonConvert.DeserializeObject<Session>(sessionJsonString);
    }

    private static string GetCacheFilePath()
    {
        // NOTE: Session JWT is stored as plain JSON in AppData. For production use, consider encrypting.
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var folder = Path.Combine(appDataPath, "RevitCloudKit");
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, CacheFileName);
    }
}