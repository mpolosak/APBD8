using APBD8.Exceptions;
using APBD8.Models.DTOs;
using APBD8.Repositories;

namespace APBD8.Services;

public class ClientService(IClientsRepository clientsRepository) : IClientsService
{
    public async Task<int> AddClient(ClientDTO client)
    {
        return await clientsRepository.AddClient(client);
    }

    public async Task<ClientDTO> GetClient(int id)
    {
        var client = await clientsRepository.GetClient(id);
        if (client == null)
            throw new NotFoundException($"Client not found");
        return client;
    }
}