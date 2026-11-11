using Labb_3code_along.Models;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Labb_3code_along.Data;

internal class JsonFileHandler
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task SaveAsync(string filePath, List<QuestionPack> packs)
    {
        await using FileStream stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, packs, _options);
    }

    public static async Task<List<QuestionPack>> LoadAsync(string filePath)
    {
        await using FileStream stream = File.OpenRead(filePath);

        return await JsonSerializer.DeserializeAsync<List<QuestionPack>>(
            stream, _options) ?? new List<QuestionPack>();
    }
}