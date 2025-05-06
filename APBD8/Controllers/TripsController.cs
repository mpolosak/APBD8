using APBD8.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController(IClientsTripsService clientsTripsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await clientsTripsService.GetTrips();
        return Ok(trips);
    }
}