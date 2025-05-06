using APBD8.Models.DTOs;

namespace APBD8.Services;

public interface IClientsService
{
    public Task<int> AddClient(ClientDTO client);
    public Task<ClientDTO> GetClient(int id);
}