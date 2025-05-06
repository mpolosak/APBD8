using APBD8.Models;

namespace APBD8.Repositories;

public interface ITripsRepository
{
    Task<bool> TripExists(int tripId);
    Task<List<Trip>> GetTrips();
    Task<List<string>> GetCountries(int idTrip);
    Task<List<ClientsTrip>> GetClientsTrips(int clientId);

    public Task AddClientToTrip(int clientId, int tripId, int registeredAt);
    
    public Task RemoveClientFromTrip(int clientId, int tripId);
}