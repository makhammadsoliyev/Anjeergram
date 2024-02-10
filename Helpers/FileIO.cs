using Newtonsoft.Json;

namespace Anjeergram.Helpers;

public static class FileIO
{
    public static async Task<List<T>> ReadAsync<T>(string path)
    {
        var content = await File.ReadAllTextAsync(path);

        return JsonConvert.DeserializeObject<List<T>>(content) ?? new List<T>();
    }

    public static async Task WriteAsync<T>(string path, List<T> values)
    {
        var content = JsonConvert.SerializeObject(values, Formatting.Indented);

        await File.WriteAllTextAsync(path, content);
    }
}
