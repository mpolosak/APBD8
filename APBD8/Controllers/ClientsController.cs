using APBD8.Exceptions;
using APBD8.Models.DTOs;
using APBD8.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(ITripsService tripsService, IClientsService clientsService) : ControllerBase
{
    [HttpGet("{id:int}/trips")]
    public async Task<IActionResult> GetClientsTrips(int id)
    {
        var trips = await tripsService.GetClientsTrips(id);
        return Ok(trips);
    }

    [HttpPut("{clientId:int}/trips/{postId:int}")]
    public async Task<IActionResult> PutClientsTrips(int clientId, int postId)
    {
        await tripsService.AddClientToTrip(clientId, postId);
        return Created();
    }

    [HttpDelete("{clientId:int}/trips/{postId:int}")]
    public async Task<IActionResult> PostClientsTrips(int clientId, int postId)
    {
        await tripsService.RemoveClientFromTrip(clientId, postId);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        try
        {
            var client = await clientsService.GetClient(id);
            return Ok(client);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] ClientDTO client)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var id = await clientsService.AddClient(client);
        return Created($"api/clients/{id}", id);
    }
    
}