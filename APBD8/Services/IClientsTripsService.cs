using APBD8.Models;
using APBD8.Models.DTOs;

namespace APBD8.Services;

public interface IClientsTripsService
{
    Task<List<TripDTO>> GetTrips();
    Task<List<ClientsTripDTO>> GetClientsTrips(int clientId);

    public Task AddClientToTrip(int clientId, int tripId);
    public Task RemoveClientFromTrip(int clientId, int postId);
    public Task<int> AddClient(ClientDTO client);
    public Task<ClientDTO> GetClient(int id);
}