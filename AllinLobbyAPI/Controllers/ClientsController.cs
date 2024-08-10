using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.ClientDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(IGenericService<Client> _clientService, IMapper _mapper, UserManager<Client> _userManager, SignInManager<Client> _signInManager) : ControllerBase
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
                var response = new ApiResponse<string>(false, "Registration failed");
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

            var result = await _signInManager.PasswordSignInAsync(loginClientDto.Email, loginClientDto.Password, false, false);

            if (!result.Succeeded)
            {
                var response = new ApiResponse<string>(false, "Login failed", null);
                return Unauthorized(response);
            }

            var successResponse = new ApiResponse<string>(true, "Login successful", null);
            return Ok(successResponse);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var values = _clientService.TGetList();
            var response = new ApiResponse<IEnumerable<Client>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _clientService.TGetById(id);
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
    }
}
