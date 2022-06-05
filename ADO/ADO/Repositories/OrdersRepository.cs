using ADO.Abstraction;
using ADO.Models;
using System.Data;
using System.Data.SqlClient;

namespace ADO.Repositories;

public class OrdersRepository : Repository<Order>, IOrderRepository
{
    private const string InsertExpression = "INSERT INTO Orders (Id, Status, CreatedDate, UpdatedDate, ProductId) " +
                                            "VALUES (@Id, @Status, @CreatedDate, @UpdatedDate, @ProductId)";
    private const string SelectExpression = "SELECT * FROM Orders WHERE Id = @id";
    private const string UpdateExpression = "UPDATE Orders SET Status=@Status, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate, ProductId=@ProductId WHERE Id=@Id";
    private const string DeleteExpression = "DELETE FROM Orders WHERE Id = @id";

    private const string FetchProcedure = "FetchOrders";
    private const string BulkDeleteProcedure = "DeleteOrders";

    public OrdersRepository(string connectionString) : base(connectionString)
    {

    }

    public async Task<Order> Create(Order entity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, InsertExpression, new List<SqlParameter>
        {
            new SqlParameter("@Id", entity.Id),
            new SqlParameter("@Status", entity.Status),
            new SqlParameter("@CreatedDate", entity.CreatedDate),
            new SqlParameter("@UpdatedDate", entity.UpdatedDate),
            new SqlParameter("@ProductId", entity.ProductId),
        });

        return await GetScalarResult(command, entity);
    }

    public async Task<Order> Select(Guid id)
    {
        var order = new Order();

        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, SelectExpression, new List<SqlParameter>
        {
            new SqlParameter("@id", id),
        });

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            await reader.ReadAsync();

            order.Id = (Guid)reader["Id"];
            order.Status = (string)reader["Status"];
            order.CreatedDate = (DateTime)reader["CreatedDate"];
            order.UpdatedDate = (DateTime)reader["UpdatedDate"];
            order.ProductId = (Guid)reader["ProductId"];
        }

        return order;
    }

    public async Task<IList<Order>> Fetch()
    {
        var orders = new List<Order>();

        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(FetchProcedure, connection);
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                orders.Add(new Order
                {
                    Id = (Guid)reader["Id"],
                    Status = (string)reader["Status"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    UpdatedDate = (DateTime)reader["UpdatedDate"],
                    ProductId = (Guid)reader["ProductId"],
                });
            }
        }

        return orders;
    }

    public async Task<IList<Order>> Fetch(OrderInfo orderInfo)
    {
        var orders = new List<Order>();

        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, FetchProcedure, new List<SqlParameter>
        {
            new SqlParameter("@status", orderInfo.Status),
            new SqlParameter("@month", orderInfo.Month),
            new SqlParameter("@productId", orderInfo.ProductId),
            new SqlParameter("@year", orderInfo.Year),
        });
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                orders.Add(new Order
                {
                    Id = (Guid)reader["Id"],
                    Status = (string)reader["Status"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    UpdatedDate = (DateTime)reader["UpdatedDate"],
                    ProductId = (Guid)reader["ProductId"],
                });
            }
        }

        return orders;
    }

    public async Task BulkDelete(OrderInfo orderInfo)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        SqlTransaction transaction = connection.BeginTransaction();
        var command = BuildCommandWithParams(connection, BulkDeleteProcedure, new List<SqlParameter>
        {
            new SqlParameter("@status", orderInfo.Status),
            new SqlParameter("@month", orderInfo.Month),
            new SqlParameter("@productId", orderInfo.ProductId),
            new SqlParameter("@year", orderInfo.Year),
        });
        command.Transaction = transaction;
        command.CommandType = CommandType.StoredProcedure;

        try
        {
            await command.ExecuteReaderAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> Update(Order entity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, UpdateExpression, new List<SqlParameter>
        {
            new SqlParameter("@Id", entity.Id),
            new SqlParameter("@Status", entity.Status),
            new SqlParameter("@CreatedDate", entity.CreatedDate),
            new SqlParameter("@UpdatedDate", entity.UpdatedDate),
            new SqlParameter("@ProductId", entity.ProductId),
        });

        return await GetScalarResult(command, entity);
    }

    public async Task<Order> Delete(Order entity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, DeleteExpression, new List<SqlParameter>
        {
            new SqlParameter("@id", entity.Id),
        });

        return await GetScalarResult(command, entity);
    }
}