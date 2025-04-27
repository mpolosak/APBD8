using APBD8.Models;

namespace APBD8.Repositories;

public interface ITripsRepository
{
    Task<List<Trip>> GetTrips();
    Task<List<String>> GetCountries(int idTrip);
}