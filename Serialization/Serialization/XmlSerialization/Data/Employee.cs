using System.Xml.Serialization;

namespace XmlSerialization.Data;

[Serializable]
public class Employee
{
    [XmlElement(ElementName = "Name")]
    public string EmployeeName { get; set; }
}