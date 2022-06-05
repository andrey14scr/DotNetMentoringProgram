using ADO.Abstraction;
using ADO.Models;
using System.Data;
using System.Data.SqlClient;

namespace ADO.Repositories;

public class ProductsRepository : Repository<Product>, IProductRepository
{
    private const string InsertExpression = "INSERT INTO Products (Id, Name, Description, Weight, Height, Width, Length) " +
                                            "VALUES (@Id, @Name, @Description, @Weight, @Height, @Width, @Length)";
    private const string SelectExpression = "SELECT * FROM Products WHERE Id = @id";
    private const string FetchExpression = "SELECT * FROM Products";
    private const string DeleteExpression = "DELETE FROM Products WHERE Id = @id";
    private const string UpdateExpression = "UPDATE Products SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length WHERE Id=@Id";

    public ProductsRepository(string connectionString) : base(connectionString)
    {

    }

    public async Task<Product> Create(Product entity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, InsertExpression, new List<SqlParameter>
        {
            new SqlParameter("@Id", entity.Id),
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@Description", entity.Description),
            new SqlParameter("@Weight", entity.Weight),
            new SqlParameter("@Height", entity.Height),
            new SqlParameter("@Width", entity.Width),
            new SqlParameter("@Length", entity.Length),
        });

        return await GetScalarResult(command, entity);
    }

    public async Task<Product> Select(Guid id)
    {
        var product = new Product();

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

            product.Id = (Guid)reader["Id"];
            product.Name = (string)reader["Name"];
            product.Description = (string)reader["Description"];
            product.Width = (double)reader["Width"];
            product.Height = (double)reader["Height"];
            product.Length = (double)reader["Length"];
            product.Weight = (double)reader["Weight"];
        }

        return product;
    }

    public async Task<IList<Product>> Fetch()
    {
        var products = new List<Product>();
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(FetchExpression, connection);
        var sda = new SqlDataAdapter(command);
        var dsData = new DataSet();
        sda.Fill(dsData);

        foreach (DataRow row in dsData.Tables[0].Rows)
        {
            var cells = row.ItemArray;
            products.Add(new Product
            {
                Id = (Guid)cells[0],
                Name = (string)cells[1],
                Description = (string)cells[2],
                Width = (double)cells[3],
                Height = (double)cells[4],
                Length = (double)cells[5],
                Weight = (double)cells[6]
            });
        }

        return products;
    }

    public async Task<Product> Update(Product entity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = BuildCommandWithParams(connection, UpdateExpression, new List<SqlParameter>
        {
            new SqlParameter("@Id", entity.Id),
            new SqlParameter("@Name", entity.Name),
            new SqlParameter("@Description", entity.Description),
            new SqlParameter("@Weight", entity.Weight),
            new SqlParameter("@Height", entity.Height),
            new SqlParameter("@Width", entity.Width),
            new SqlParameter("@Length", entity.Length),
        });

        return await GetScalarResult(command, entity);
    }

    public async Task<Product> Delete(Product entity)
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