using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public interface IRaportDobowyService
        {
        Task<RaportDobowy> CreateRaportAsync(RaportDobowy raport);
        Task<bool> DeleteRaportAsync(int id);
        Task<IEnumerable<RaportDobowy>> GetAllRaportyAsync();
        Task<RaportDobowy?> GetRaportByIdAsync(int id);
        Task<bool> UpdateRaportAsync(int id, RaportDobowy raport);
        }
    }