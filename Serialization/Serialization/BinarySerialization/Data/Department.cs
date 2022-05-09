namespace BinarySerialization.Data;

[Serializable]
public class Department
{
    public string DepartmentName { get; set; }
    public IList<Employee> Employees { get; set; }
}