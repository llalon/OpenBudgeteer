using System;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace OpenBudgeteer.Core.Data.Connection;

public partial class PostgresConnector : BaseDatabaseConnector<NpgsqlConnectionStringBuilder>
{
    public PostgresConnector(IConfiguration configuration) : base(configuration)
    {
        if (string.IsNullOrEmpty(Server)) Server = "localhost";
        if (Port == 0) Port = 5432;
        
        if (string.IsNullOrEmpty(Database)) Database = "postgres";
        if (!DatabaseNameRegex().IsMatch(Database))
        {
            throw new InvalidOperationException("Database name provided is illegal or SQLi attempt");
        }
        
        if (string.IsNullOrEmpty(Username)) Username = Database;
        if (!DatabaseNameRegex().IsMatch(Username))
        {
            throw new InvalidOperationException("User name provided is illegal or SQLi attempt");
        }
    }

    public override NpgsqlConnectionStringBuilder BuildConnectionString()
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = Server,
            Port = Port,
            Database = Database,
            Username = Username,
            Password = Password
        };
    }

    public override NpgsqlConnectionStringBuilder BuildRootConnectionString()
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = Server,
            Port = Port,
            Username = "postgres",
            Password = RootPassword
        };
    }

    public override bool IsDatabaseAccessible(bool useRoot = false)
    {
        try
        {
            var connectionStringBuilder = useRoot ? BuildRootConnectionString() : BuildConnectionString();
            using var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
            connection.Open();
                
            using var command = new NpgsqlCommand("SELECT 1");
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.ExecuteScalar();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Database not accessible: { e.Message }");
            return false;
        }
    }
    
    [GeneratedRegex("^[a-zA-Z][0-9a-zA-Z$_]{0,63}$", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex DatabaseNameRegex();
}