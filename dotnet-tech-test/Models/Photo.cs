using System.Text.Json.Serialization;

namespace dotnet_tech_test.Models;

public class Photo : Base
{
    [JsonPropertyName("albumId")]
    [JsonPropertyOrder(3)]
    public int AlbumId { get; init; }
    
    [JsonPropertyName("url")]
    [JsonPropertyOrder(4)]
    public string? Url { get; set; }
    
    [JsonPropertyName("thumbnailUrl")]
    [JsonPropertyOrder(5)]
    public string? ThumbnailUrl { get; set; }
}
