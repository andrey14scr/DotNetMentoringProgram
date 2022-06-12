using System.Data.SqlClient;
using Dapper;
using ORM.Dapper.DAL.Models;
using ORM.Dapper.DAL.Repositories.Abstraction;

namespace ORM.Dapper.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private const string InsertExpression = "INSERT INTO Products (Id, Name, Description, Weight, Height, Width, Length) " +
                                            "VALUES (@Id, @Name, @Description, @Weight, @Height, @Width, @Length)";
    private const string SelectExpression = "SELECT * FROM Products WHERE Id = @id";
    private const string FetchExpression = "SELECT * FROM Products";
    private const string DeleteExpression = "DELETE FROM Products WHERE Id = @id";
    private const string UpdateExpression = "UPDATE Products SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length WHERE Id=@Id";

    private readonly string _connectionString;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Create(Product entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(InsertExpression, entity);
    }

    public async Task<Product> Select(Guid id)
    {
        await using var connection = new SqlConnection(_connectionString);
        var product = await connection.QueryFirstOrDefaultAsync<Product>(SelectExpression, new { Id = id});
        return product;
    }

    public async Task<IList<Product>> Fetch()
    {
        await using var connection = new SqlConnection(_connectionString);
        var products = await connection.QueryAsync<Product>(FetchExpression);
        return products.ToList();
    }

    public async Task Update(Product entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(UpdateExpression, entity);
    }

    public async Task Delete(Product entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(DeleteExpression, new { Id = entity.Id });
    }
}