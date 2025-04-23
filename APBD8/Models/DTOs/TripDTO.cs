namespace APBD8.Models.DTOs;

public class TripDTO
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public List<Object> Objects { get; set; }
}

public class CountryDTO
{
    public string Name { get; set; }
}