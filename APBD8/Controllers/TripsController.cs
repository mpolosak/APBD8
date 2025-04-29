using APBD8.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController(ITripsService tripsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await tripsService.GetTrips();
        return Ok(trips);
    }
}