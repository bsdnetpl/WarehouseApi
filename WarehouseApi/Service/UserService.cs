using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseApi.DTO;
using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public class UserService : IUserService
        {
        private readonly BsdnetphMatgazynMainContext _context;
        private readonly IConfiguration _configuration;


        public UserService(BsdnetphMatgazynMainContext context, IConfiguration configuration)
            {
            _context = context;
            _configuration = configuration;
            }

        // Pobranie wszystkich użytkowników
        public async Task<IEnumerable<User>> GetUsersAsync()
            {
            return await _context.Users.ToListAsync();
            }

        // Pobranie użytkownika po ID
        public async Task<User?> GetUserByIdAsync(int id)
            {
            return await _context.Users.FindAsync(id);
            }

        // Tworzenie nowego użytkownika z hashowaniem hasła
        public async Task<UserDto> CreateUserAsync(UserDto user)
            {

            var newUser = new User
                {
                Login = user.Login,
                Email = user.Email,
                ImieNazwisko = user.ImieNazwisko,
                Ranga = "User", // Domyślna rola użytkownika
                Data = DateTime.Now.ToString()
                };

            // Hashowanie hasła za pomocą BCrypt
            newUser.Haslo = BCrypt.Net.BCrypt.HashPassword(user.Haslo);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Usuń hasło przed zwróceniem DTO do klienta
            user.Haslo = null;

            return user;
            }

        // Aktualizacja użytkownika
        public async Task<bool> UpdateUserAsync(int id, User user)
            {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                {
                return false;
                }

            // Hashowanie hasła tylko, jeśli zostało zmienione
            if (!BCrypt.Net.BCrypt.Verify(user.Haslo, existingUser.Haslo))
                {
                // Jeśli hasło zostało zmienione, hashujemy nowe hasło za pomocą BCrypt
                existingUser.Haslo = BCrypt.Net.BCrypt.HashPassword(user.Haslo);
                }

            existingUser.Login = user.Login;

            existingUser.Data = user.Data;
            existingUser.Email = user.Email;
            existingUser.ImieNazwisko = user.ImieNazwisko;
            existingUser.Ranga = user.Ranga;

            await _context.SaveChangesAsync();
            return true;
            }

        // Usuwanie użytkownika
        public async Task<bool> DeleteUserAsync(int id)
            {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                {
                return false;
                }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
            }
        public async Task<string?> LoginAsync(LoginRequest loginRequest)
            {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null)
                {
                return null; // Zwróć null, jeśli użytkownik nie istnieje
                }

            // Weryfikacja hasła za pomocą BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Haslo);
            if (!isPasswordValid)
                {
                return null; // Zwróć null, jeśli hasło jest nieprawidłowe
                }

            // Generowanie tokenu JWT, jeśli dane logowania są poprawne
            return GenerateJwtToken(user);
            }

        private string GenerateJwtToken(User user)
            {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
                {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.ImieNazwisko),
                new Claim(ClaimTypes.Role, user.Ranga)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }
        }
    }
