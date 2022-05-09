#pragma warning disable SYSLIB0011

using System.Runtime.Serialization.Formatters.Binary;
using BinarySerialization.Data;
using BinarySerialization.Properties;

var department = new Department
{
    DepartmentName = "Managers", Employees = new List<Employee>
    {
        new() { EmployeeName = "Bill" },
        new() { EmployeeName = "Steven" },
        new() { EmployeeName = "John" },
    }
};

var formatter = new BinaryFormatter();

using (var fs = new FileStream(Resources.DataFile, FileMode.OpenOrCreate))
{
    formatter.Serialize(fs, department);
}

using (var fs = new FileStream(Resources.DataFile, FileMode.OpenOrCreate))
{
    var data = (Department)formatter.Deserialize(fs);

    Console.WriteLine($"Department \"{data.DepartmentName}\":");
    foreach (var employee in data.Employees)
    {
        Console.WriteLine($" - {employee.EmployeeName}");
    }
}