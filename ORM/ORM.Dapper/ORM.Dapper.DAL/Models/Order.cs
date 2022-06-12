namespace ORM.Dapper.DAL.Models;

public class Order
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid ProductId { get; set; }
}