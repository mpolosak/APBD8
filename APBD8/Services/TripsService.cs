using System.Data;
using APBD8.Models.DTOs;
using APBD8.Repositories;
using Microsoft.Data.SqlClient;

namespace APBD8.Services;

public class TripsService(ITripsRepository repository) : ITripsService
{
    public async Task<List<TripDTO>> GetTrips()
    {
        var trips = await repository.GetTrips();
        var res = await Task.WhenAll(trips.Select(async t =>
        {
            var countries = await repository.GetCountries(t.IdTrip);
            return new TripDTO()
            {
                IdTrip = t.IdTrip,
                Name = t.Name,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                Description = t.Description,
                MaxPeople = t.MaxPeople,
                Countries = countries,
            };
        }));
        
        return res.ToList();
    }

    public async Task<List<ClientsTripDTO>> GetClientsTrips(int clientId)
    {
        var trips = await repository.GetClientsTrips(clientId);
        var res = await Task.WhenAll(trips.Select(async t =>
        {
            var countries = await repository.GetCountries(t.IdTrip);
            return new ClientsTripDTO()
            {
                IdTrip = t.IdTrip,
                Name = t.Name,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                Description = t.Description,
                MaxPeople = t.MaxPeople,
                Countries = countries,
                RegisteredAt = t.RegisteredAt,
                PaymentDate = t.PaymentDate,
            };
        }));
        return res.ToList();
    }
}