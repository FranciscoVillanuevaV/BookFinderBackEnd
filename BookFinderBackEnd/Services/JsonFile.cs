using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BookFinderBackEnd.Services
{
    public static class JsonFile
    {
        public static output Read<output>(string relativePath)
        {
            string jsonData = ReadAsJson(relativePath);
            var data = JsonSerializer.Deserialize<output>(jsonData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return data;
        }

        public static string ReadAsJson(string relativePath)
        {
            string fullPath = Path.GetFullPath(relativePath);
            string jsonData = File.ReadAllText(fullPath);
            string jsonDataMin = Regex.Replace(jsonData, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
            return jsonDataMin;
        }
    }
}