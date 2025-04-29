using APBD8.Models.DTOs;

namespace APBD8.Repositories;

public interface IClientsRepository
{
    public Task<int> AddClient(ClientDTO client);
}