using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Models;
using WarehouseApi.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using WarehouseApi.DTO;

namespace WarehouseApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
        {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            {
            _userService = userService;
            }

        // GET: api/Users
        [Authorize(Roles = "Admin")] // Tylko admin może pobrać listę użytkowników
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
            var users = await _userService.GetUsersAsync();

            // Konwersja User na UserDto
            var userDtos = users.Select(u => new User
                {
                Login = u.Login,
                Haslo = u.Haslo, // Można usunąć ten field z UserDto lub zmapować puste
                Data = u.Data,
                Email = u.Email,
                ImieNazwisko = u.ImieNazwisko,
                Ranga = u.Ranga
                });

            return Ok(userDtos);
            }

        // GET: api/Users/5
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
            {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                {
                return NotFound();
                }

            return Ok(user);
            }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
            {
            // Konwersja UserDto na User
            var user = new User
                {
                Login = userDto.Login,
                Haslo = userDto.Haslo,
                Email = userDto.Email,
                ImieNazwisko = userDto.ImieNazwisko,
                };

            var newUser = await _userService.CreateUserAsync(userDto);

            return Ok();
            }

        // PUT: api/Users/5
        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
            {
            // Konwersja UserDto na User
            var user = new User
                {
                Login = userDto.Login,
                Haslo = userDto.Haslo,
                Email = userDto.Email,
                ImieNazwisko = userDto.ImieNazwisko,
                };

            var result = await _userService.UpdateUserAsync(id, user);
            if (!result)
                {
                return NotFound();
                }

            return NoContent();
            }

        // DELETE: api/Users/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
            {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                {
                return NotFound();
                }

            return NoContent();
            }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
            {
            var token = await _userService.LoginAsync(loginRequest);
            if (token == null)
                {
                return Unauthorized("Invalid email or password");
                }

            return Ok(new { token });
            }
        }
    }
