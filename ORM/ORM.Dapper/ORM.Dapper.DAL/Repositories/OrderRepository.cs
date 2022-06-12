using System.Data;
using System.Data.SqlClient;
using Dapper;
using ORM.Dapper.DAL.Models;
using ORM.Dapper.DAL.Repositories.Abstraction;

namespace ORM.Dapper.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private const string InsertExpression = "INSERT INTO Orders (Id, Status, CreatedDate, UpdatedDate, ProductId) " +
                                            "VALUES (@Id, @Status, @CreatedDate, @UpdatedDate, @ProductId)";
    private const string SelectExpression = "SELECT * FROM Orders WHERE Id = @id";
    private const string UpdateExpression = "UPDATE Orders SET Status=@Status, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate, ProductId=@ProductId WHERE Id=@Id";
    private const string DeleteExpression = "DELETE FROM Orders WHERE Id = @id";

    private const string FetchProcedure = "FetchOrders";
    private const string BulkDeleteProcedure = "DeleteOrders";

    private readonly string _connectionString;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Create(Order entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(InsertExpression, entity);
    }

    public async Task<Order> Select(Guid id)
    {
        await using var connection = new SqlConnection(_connectionString);
        var order = await connection.QueryFirstOrDefaultAsync<Order>(SelectExpression, new { Id = id });
        return order;
    }

    public async Task<IList<Order>> Fetch()
    {
        return await Fetch(new OrderInfo());
    }

    public async Task Update(Order entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(UpdateExpression, entity);
    }

    public async Task Delete(Order entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(DeleteExpression, new { Id = entity.Id });
    }

    public async Task<IList<Order>> Fetch(OrderInfo orderInfo)
    {
        await using var connection = new SqlConnection(_connectionString);
        var products = await connection.QueryAsync<Order>(FetchProcedure, orderInfo, commandType: CommandType.StoredProcedure);
        return products.ToList();
    }

    public async Task BulkDelete(OrderInfo orderInfo)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(BulkDeleteProcedure, orderInfo, commandType: CommandType.StoredProcedure);
    }
}