using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.GuestDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController(IGenericService<Guest> _guestService, IMapper _mapper, AllinLobbyContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var guests = _context.Guests
                                 .Include(g => g.Client)       // Include related Client
                                 .Include(g => g.Reservation)  // Include related Reservation
                                 .ToList();

            var response = new ApiResponse<IEnumerable<Guest>>(true, "Data retrieved successfully", guests);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var guest = _context.Guests
                                .Include(g => g.Client)       // Include related Client
                                .Include(g => g.Reservation)  // Include related Reservation
                                .FirstOrDefault(g => g.GuestId == id);

            if (guest == null)
            {
                var response = new ApiResponse<Guest>(false, "Guest not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Guest>(true, "Guest retrieved successfully", guest);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var guest = _guestService.TGetById(id);
            if (guest == null)
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

            var newGuest = _mapper.Map<Guest>(createGuestDto);
            _guestService.TCreate(newGuest);

            var successResponse = new ApiResponse<Guest>(true, "Guest created successfully", newGuest);
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

            var updatedGuest = _mapper.Map(updateGuestDto, existingGuest);
            _guestService.TUpdate(updatedGuest);

            var successResponse = new ApiResponse<Guest>(true, "Guest updated successfully", updatedGuest);
            return Ok(successResponse);
        }
    }
}
