namespace ORM.Dapper.DAL.Models;

public class OrderInfo
{
    public int? Month { get; set; }
    public int? Year { get; set; }
    public string? Status { get; set; }
    public Guid? ProductId { get; set; }
}