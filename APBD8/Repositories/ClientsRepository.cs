using APBD8.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD8.Repositories;

public class ClientsRepository(IConfiguration config) : IClientsRepository
{
    private readonly string _connectionString = config.GetConnectionString("Default") ?? throw new Exception("Connection string not found");
    public async Task<int> AddClient(ClientDTO client)
    {
        const string cmdText = @"INSERT INTO Client(FirstName, LastName, Email, Telephone, PESEL)
            VALUES (@FirstName, @LastName, @Email, @Telephone, @PESEL);
            SELECT SCOPE_IDENTITY();";
        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        command.Parameters.AddWithValue("@FirstName", client.FirstName);
        command.Parameters.AddWithValue("@LastName", client.LastName);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue("@Telephone", (object?) client.Telephone ?? DBNull.Value);
        command.Parameters.AddWithValue("@PESEL", (object?) client.PESEL ?? DBNull.Value);
        
        await conn.OpenAsync();
        var id = Convert.ToInt32(await command.ExecuteScalarAsync());
        return id;
    }
}