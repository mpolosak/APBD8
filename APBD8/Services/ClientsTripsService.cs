using System.Data;
using APBD8.Exceptions;
using APBD8.Models.DTOs;
using APBD8.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace APBD8.Services;

public class ClientsTripsService(ITripsRepository repository, IClientsRepository clientsRepository) : IClientsTripsService
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
        if (!await clientsRepository.ClientExists(clientId))
        {
            throw new NotFoundException("Client not found");
        }
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
        if (res.IsNullOrEmpty())
            throw new NotFoundException("No trips found for specified client");
        return res.ToList();
    }

    public async Task AddClientToTrip(int clientId, int tripId)
    {
        if (!await clientsRepository.ClientExists(clientId))
            throw new NotFoundException("Client not found");
        if (!await repository.TripExists(tripId))
            throw new NotFoundException("Trip not found");
        var registeredAt = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
        await repository.AddClientToTrip(clientId, tripId, registeredAt);
    }

    public async Task RemoveClientFromTrip(int clientId, int postId)
    {
        await repository.RemoveClientFromTrip(clientId, postId);
    }
    
    public async Task<int> AddClient(ClientDTO client)
    {
        return await clientsRepository.AddClient(client);
    }

    public async Task<ClientDTO> GetClient(int id)
    {
        var client = await clientsRepository.GetClient(id);
        if (client == null)
            throw new NotFoundException($"Client not found");
        return client;
    }
}