using System.Data;
using APBD8.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD8.Services;

public class TripsService(IConfiguration config) : ITripsService
{
    private readonly string _connectionString = config.GetConnectionString("Default") ?? throw new InvalidOperationException();

    public async Task<List<TripDTO>> GetTrips()
    {
        var trips = new List<TripDTO>();
        
        var cmdText = "select IdTrip, Name from Trip";

        await using var conn = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(cmdText, conn);
        await conn.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            trips.Add(new TripDTO()
            {
                IdTrip = reader.GetInt32(0),
                Name = reader.GetString(1),
            });
        }

        return trips;
    }
}