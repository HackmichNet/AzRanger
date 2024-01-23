using System.IO;
using System.Text.Json;

namespace AzRanger.Utilities
{
    internal static class JSONDumper
    {
        private static readonly JsonSerializerOptions DEFAULT_OPTIONS = new JsonSerializerOptions
        {
            MaxDepth = 16,
            IncludeFields = true,
            WriteIndented = true,
        };

        public static string GetSerialized<TValue>(TValue value, JsonSerializerOptions options = null)
        {
            if (options == null)
            {
                return JsonSerializer.Serialize(value, JSONDumper.DEFAULT_OPTIONS);
            }
            {
                return JsonSerializer.Serialize(value, options);
            }
        }

        public static void WriteToFile<TValue>(TValue value, string filePath, JsonSerializerOptions options = null)
        {
            using (StreamWriter stream = File.CreateText(filePath))
            {
                stream.Write(JSONDumper.GetSerialized(value, options));
            }
        }

        public static void WriteToFile<TValue>(TValue value, string filePath, string prefix, JsonSerializerOptions options = null)
        {
            using (StreamWriter stream = File.CreateText(filePath))
            {
                stream.Write(prefix + JSONDumper.GetSerialized(value, options));
            }
        }

    }
}
