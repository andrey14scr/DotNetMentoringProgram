namespace ADO.Models;

public class Order : IEntity
{
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid ProductId { get; set; }
}