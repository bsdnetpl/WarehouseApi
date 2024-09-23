using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public interface IKontrahenciService
        {
        Task<Kontrahenci> CreateKontrahenciAsync(Kontrahenci kontrahent);
        Task<bool> DeleteKontrahenciAsync(int id);
        Task<IEnumerable<Kontrahenci>> GetAllKontrahenciAsync();
        Task<Kontrahenci?> GetKontrahenciByIdAsync(int id);
        Task<bool> UpdateKontrahenciAsync(int id, Kontrahenci kontrahent);
        }
    }