using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dotnet_tech_test.Models;

public class Album : Base
{
    [JsonPropertyName("userId")]
    [JsonPropertyOrder(3)]
    public int UserId { get; init; }
    
    [JsonPropertyOrder(4)]
    public List<Photo> Photos { get; set; } = [];
}