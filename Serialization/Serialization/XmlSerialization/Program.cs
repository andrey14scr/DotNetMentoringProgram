using System.Xml.Serialization;
using XmlSerialization.Data;
using XmlSerialization.Properties;

var department = new Department
{
    DepartmentName = "Managers",
    Employees = new List<Employee>
    {
        new() { EmployeeName = "Liza" },
        new() { EmployeeName = "Victor" },
        new() { EmployeeName = "Olga" },
    }
};

var xmlSerializer = new XmlSerializer(typeof(Department));

using (var fs = new FileStream(Resources.DataFile, FileMode.Create))
{
    xmlSerializer.Serialize(fs, department);
}

using (var fs = new FileStream(Resources.DataFile, FileMode.Open))
{
    var data = xmlSerializer.Deserialize(fs) as Department;

    Console.WriteLine($"Department \"{data.DepartmentName}\":");
    foreach (var employee in data.Employees)
    {
        Console.WriteLine($" - {employee.EmployeeName}");
    }
}