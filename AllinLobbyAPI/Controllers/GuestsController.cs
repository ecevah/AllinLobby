using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.GuestDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController(IGenericService<Guest> _guestService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _guestService.TGetList();
            var response = new ApiResponse<IEnumerable<Guest>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _guestService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Guest>(false, "Guest not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Guest>(true, "Guest retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _guestService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Guest>(false, "Guest not found", null);
                return NotFound(response);
            }

            _guestService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Guest deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateGuestDto createGuestDto)
        {
            if (createGuestDto == null)
            {
                var response = new ApiResponse<Guest>(false, "Invalid guest data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Guest>(createGuestDto);
            _guestService.TCreate(newValue);

            var successResponse = new ApiResponse<Guest>(true, "Guest created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateGuestDto updateGuestDto)
        {
            if (updateGuestDto == null)
            {
                var response = new ApiResponse<Guest>(false, "Invalid guest data", null);
                return BadRequest(response);
            }

            var existingGuest = _guestService.TGetById(id);
            if (existingGuest == null)
            {
                var response = new ApiResponse<Guest>(false, "Guest not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateGuestDto, existingGuest);
            _guestService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Guest>(true, "Guest updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
