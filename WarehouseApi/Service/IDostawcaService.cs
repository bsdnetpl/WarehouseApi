using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public interface IDostawcaService
        {
        Task<Dostawca> CreateDostawcaAsync(Dostawca dostawca);
        Task<bool> DeleteDostawcaAsync(int id);
        Task<IEnumerable<Dostawca>> GetAllDostawcyAsync();
        Task<Dostawca?> GetDostawcaByIdAsync(int id);
        Task<bool> UpdateDostawcaAsync(int id, Dostawca dostawca);
        }
    }