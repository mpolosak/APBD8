using APBD8.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(ITripsService tripsService) : ControllerBase
{
    [HttpGet("{id:int}/trips")]
    public async Task<IActionResult> GetClientsTrips(int id)
    {
        var trips = await tripsService.GetClientsTrips(id);
        return Ok(trips);
    }
}