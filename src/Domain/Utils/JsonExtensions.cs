using System.Text.Json;

namespace Scroll.Library.Utils;

public static class JsonExtensions
{
    public static string ToJson<T>(this T obj) =>
        JsonSerializer.Serialize(obj);

    public static T? FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json);

    public static T? Clone<T>(this T obj) =>
        obj.ToJson().FromJson<T>();
}