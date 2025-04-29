using APBD8.Models;
using APBD8.Models.DTOs;

namespace APBD8.Services;

public interface ITripsService
{
    Task<List<TripDTO>> GetTrips();
    Task<List<ClientsTripDTO>> GetClientsTrips(int clientId);
}