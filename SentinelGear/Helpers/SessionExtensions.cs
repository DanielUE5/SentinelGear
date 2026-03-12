using System.Text.Json;

namespace SentinelGear.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string serializedValue = JsonSerializer.Serialize(value);
            session.SetString(key, serializedValue);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            string? serializedValue = session.GetString(key);

            if (string.IsNullOrWhiteSpace(serializedValue))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(serializedValue);
        }
    }
}