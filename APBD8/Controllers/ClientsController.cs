using APBD8.Exceptions;
using APBD8.Models.DTOs;
using APBD8.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(IClientsTripsService service) : ControllerBase
{
    [HttpGet("{id:int}/trips")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetClientsTrips(int id)
    {
        var trips = await service.GetClientsTrips(id);
        return Ok(trips);
    }

    [HttpPut("{clientId:int}/trips/{postId:int}")]
    public async Task<IActionResult> PutClientsTrips(int clientId, int postId)
    {
        await service.AddClientToTrip(clientId, postId);
        return Created();
    }

    [HttpDelete("{clientId:int}/trips/{postId:int}")]
    public async Task<IActionResult> PostClientsTrips(int clientId, int postId)
    {
        await service.RemoveClientFromTrip(clientId, postId);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await service.GetClient(id);
        return Ok(client);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] ClientDTO client)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var id = await service.AddClient(client);
        return Created($"api/clients/{id}", id);
    }
    
}