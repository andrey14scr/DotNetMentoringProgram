using ADO.Models;
using System.Data.SqlClient;

namespace ADO.Repositories;

public abstract class Repository<T> where T : IEntity
{
    protected string ConnectionString;

    protected Repository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected SqlCommand BuildCommandWithParams(SqlConnection connection, string expression, List<SqlParameter> parameters)
    {
        var command = new SqlCommand(expression, connection);

        foreach (var param in parameters)
        {
            command.Parameters.Add(param);
        }

        return command;
    }

    protected async Task<T> GetScalarResult(SqlCommand command, T entity)
    {
        var result = await command.ExecuteNonQueryAsync();

        if (result == 1)
        {
            return entity;
        }

        return null;
    }
}