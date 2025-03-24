using System.Text.Json.Serialization;

namespace dotnet_tech_test.Models;

public abstract class Base
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(1)]
    public int Id { get; init; }
    
    [JsonPropertyName("title")]
    [JsonPropertyOrder(2)]
    public string? Title { get; set; }
}