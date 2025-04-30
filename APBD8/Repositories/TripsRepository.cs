using APBD8.Models;
using APBD8.Models.DTOs;
using APBD8.Services;
using Microsoft.Data.SqlClient;

namespace APBD8.Repositories;

public class TripsRepository(IConfiguration config) : ITripsRepository
{
    private readonly string _connectionString = config.GetConnectionString("Default") ?? throw new InvalidOperationException();

    public async Task<List<Trip>> GetTrips()
    {
        var trips = new List<Trip>();
        
        const string cmdText = "select IdTrip, Name, Description, DateFrom, DateTo, MaxPeople from Trip";

        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        await conn.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            trips.Add(new Trip()
            {
                IdTrip = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                DateFrom = reader.GetDateTime(3),
                DateTo = reader.GetDateTime(4),
                MaxPeople = reader.GetInt32(5),
            });
        }

        return trips;
    }

    public async Task<List<string>> GetCountries(int idTrip)
    {
        const string cmdText =
            "SELECT Name FROM Country_Trip JOIN Country on Country.IdCountry = Country_Trip.IdCountry WHERE Country_Trip.IdTrip = @IdTrip";
        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        command.Parameters.AddWithValue("@IdTrip", idTrip);
        await conn.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var countries = new List<string>();
        while (await reader.ReadAsync())
        {
            countries.Add(reader.GetString(0));
        }

        return countries;
    }

    public async Task<List<ClientsTrip>> GetClientsTrips(int clientId)
    {
        var clientsTrips = new List<ClientsTrip>();
        const string cmdText = @"SELECT trip.IdTrip, Name, Description, DateFrom, DateTo, MaxPeople, RegisteredAt, PaymentDate 
            FROM trip JOIN client_trip ON trip.IdTrip = client_trip.IdTrip WHERE IdClient = @IdClient;";
        
        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        
        command.Parameters.AddWithValue("@IdClient", clientId);
        await conn.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            clientsTrips.Add(new ClientsTrip()
            {
                IdTrip = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                DateFrom = reader.GetDateTime(3),
                DateTo = reader.GetDateTime(4),
                MaxPeople = reader.GetInt32(5),
                RegisteredAt = reader.GetInt32(6),
                PaymentDate = reader.IsDBNull(7) ? null : reader.GetInt32(7),
            });
        }
        
        return clientsTrips;
    }

    public async Task AddClientToTrip(int clientId, int tripId, int registeredAt)
    {
        const string  cmdText = "INSERT INTO Client_Trip(IdClient, IdTrip, RegisteredAt) VALUES (@ClientId, @PostId, @RegisteredAt);";
        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@PostId", tripId);
        command.Parameters.AddWithValue("@RegisteredAt", registeredAt);
        await conn.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}