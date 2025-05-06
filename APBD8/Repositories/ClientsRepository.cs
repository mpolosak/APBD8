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

    public async Task<ClientDTO?> GetClient(int id)
    {
        const string cmdText = @"SELECT FirstName, LastName, Email, Telephone, PESEL FROM Client WHERE IdClient = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        command.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new ClientDTO()
        {
            FirstName = reader.GetString(0),
            LastName = reader.GetString(1),
            Email = reader.GetString(2),
            Telephone = reader.GetString(3),
            PESEL = reader.GetString(4)
        };
    }
}