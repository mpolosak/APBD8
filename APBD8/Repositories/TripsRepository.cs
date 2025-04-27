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
}