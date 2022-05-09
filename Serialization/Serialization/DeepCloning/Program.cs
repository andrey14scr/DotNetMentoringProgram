using DeepCloning;
using DeepCloning.Data;

var department = new Department
{
    DepartmentName = "Workers",
    Employees = new List<Employee>
    {
        new() { EmployeeName = "Mihail" },
        new() { EmployeeName = "Vladislav" },
        new() { EmployeeName = "Hanna" },
    }
};

var copy = DeepCloner.Copy<Department>(department);

Console.WriteLine(department.DepartmentName);
Console.WriteLine(copy.DepartmentName);
department.DepartmentName = "123";
Console.WriteLine(department.DepartmentName);
Console.WriteLine(copy.DepartmentName);