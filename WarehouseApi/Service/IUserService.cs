using WarehouseApi.DTO;
using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public interface IUserService
        {
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<string?> LoginAsync(LoginRequest loginRequest);
        Task<bool> UpdateUserAsync(int id, User user);
        }
    }