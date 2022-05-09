using System.Xml.Serialization;

namespace XmlSerialization.Data;

[Serializable]
public class Department
{
    [XmlElement(ElementName = "Name")]
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }
}