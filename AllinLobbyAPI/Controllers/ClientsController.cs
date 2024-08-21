using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.ClientDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllinLobby.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(IGenericService<Client> _clientService, IMapper _mapper, UserManager<Client> _userManager, SignInManager<Client> _signInManager, IConfiguration _configuration, AllinLobbyContext _context) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateClientDto registerClientDto)
        {
            if (registerClientDto == null)
            {
                var response = new ApiResponse<Client>(false, "Invalid registration data", null);
                return BadRequest(response);
            }

            var client = _mapper.Map<Client>(registerClientDto);
            var result = await _userManager.CreateAsync(client, registerClientDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                var response = new ApiResponse<string>(false, $"Registration failed: {errorMessages}");
                return BadRequest(response);
            }

            var successResponse = new ApiResponse<Client>(true, "Client registered successfully", client);
            return Ok(successResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginClientDto loginClientDto)
        {
            if (loginClientDto == null)
            {
                var response = new ApiResponse<string>(false, "Invalid login data", null);
                return BadRequest(response);
            }

            var user = await _userManager.FindByEmailAsync(loginClientDto.Email);
            if (user == null)
            {
                var response = new ApiResponse<string>(false, "User not found", null);
                return NotFound(response);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginClientDto.Password, false, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var response = new ApiResponse<string>(false, "Account locked out", null);
                return Unauthorized(response);
            }

            if (!result.Succeeded)
            {
                var response = new ApiResponse<string>(false, "Login failed", null);
                return Unauthorized(response);
            }

            var token = GenerateJWT(user);
            var successResponse = new ApiResponse<object>(true, "Login successful", token);
            return Ok(successResponse);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var values = _context.Clients
                                .Include(c => c.BookingEvents)
                                .Include(c => c.Comments)
                                .Include(c => c.Complaints)
                                .Include(c => c.Guests)
                                .Include(c => c.Orders)
                                .Include(c => c.Reservations)
                                .Include(c => c.Sessions)
                                .ToList();

            var response = new ApiResponse<IEnumerable<Client>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _context.Clients
                                .Include(c => c.BookingEvents)
                                .Include(c => c.Comments)
                                .Include(c => c.Complaints)
                                .Include(c => c.Guests)
                                .Include(c => c.Orders)
                                .Include(c => c.Reservations)
                                .Include(c => c.Sessions)
                                .FirstOrDefault(c => c.ClientId == id);

            if (value == null)
            {
                var response = new ApiResponse<Client>(false, "Client not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Client>(true, "Client retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _clientService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Client>(false, "Client not found", null);
                return NotFound(response);
            }

            _clientService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Client deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateClientDto createClientDto)
        {
            if (createClientDto == null)
            {
                var response = new ApiResponse<Client>(false, "Invalid client data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Client>(createClientDto);
            _clientService.TCreate(newValue);

            var successResponse = new ApiResponse<Client>(true, "Client created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateClientDto updateClientDto)
        {
            if (updateClientDto == null)
            {
                var response = new ApiResponse<Client>(false, "Invalid client data", null);
                return BadRequest(response);
            }

            var existingClient = _clientService.TGetById(id);
            if (existingClient == null)
            {
                var response = new ApiResponse<Client>(false, "Client not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateClientDto, existingClient);
            _clientService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Client>(true, "Client updated successfully", updatedValue);
            return Ok(successResponse);
        }

        private object GenerateJWT(Client user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ?? "")
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "ahmetecevit.com"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
