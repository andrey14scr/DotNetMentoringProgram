using System.Text.Json.Serialization;

namespace JsonSerialization.Data;

public class Department
{
    [JsonPropertyName("Name")]
    public string DepartmentName { get; set; }
    public IList<Employee> Employees { get; set; }
}