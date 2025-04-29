using APBD8.Models.DTOs;
using APBD8.Repositories;

namespace APBD8.Services;

public class ClientService(IClientsRepository clientsRepository) : IClientsService
{
    public async Task<int> AddClient(ClientDTO client)
    {
        return await clientsRepository.AddClient(client);
    }
}