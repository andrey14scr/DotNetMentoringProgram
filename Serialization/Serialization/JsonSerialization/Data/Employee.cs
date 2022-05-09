using System.Text.Json.Serialization;

namespace JsonSerialization.Data;

public class Employee
{
    [JsonPropertyName("Name")]
    public string EmployeeName { get; set; }
}