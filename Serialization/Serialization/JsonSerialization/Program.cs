using System.Text.Json;
using JsonSerialization.Data;
using JsonSerialization.Properties;

var department = new Department
{
    DepartmentName = "Managers",
    Employees = new List<Employee>
    {
        new() { EmployeeName = "Antony" },
        new() { EmployeeName = "Maria" },
        new() { EmployeeName = "Tim" },
    }
};

await using (var fs = new FileStream(Resources.DataFile, FileMode.Create))
{
    await JsonSerializer.SerializeAsync(fs, department);
}

await using (var fs = new FileStream(Resources.DataFile, FileMode.Open))
{
    var data = await JsonSerializer.DeserializeAsync<Department>(fs);
    
    Console.WriteLine($"Department \"{data.DepartmentName}\":");
    foreach (var employee in data.Employees)
    {
        Console.WriteLine($" - {employee.EmployeeName}");
    }
}