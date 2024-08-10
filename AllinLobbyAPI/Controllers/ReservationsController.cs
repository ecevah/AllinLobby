using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.ReservationDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IGenericService<Reservation> _reservationService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _reservationService.TGetList();
            var response = new ApiResponse<IEnumerable<Reservation>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _reservationService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Reservation>(false, "Reservation not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Reservation>(true, "Reservation retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _reservationService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Reservation>(false, "Reservation not found", null);
                return NotFound(response);
            }

            _reservationService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Reservation deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateReservationDto createReservationDto)
        {
            if (createReservationDto == null)
            {
                var response = new ApiResponse<Reservation>(false, "Invalid reservation data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Reservation>(createReservationDto);
            _reservationService.TCreate(newValue);

            var successResponse = new ApiResponse<Reservation>(true, "Reservation created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateReservationDto updateReservationDto)
        {
            if (updateReservationDto == null)
            {
                var response = new ApiResponse<Reservation>(false, "Invalid reservation data", null);
                return BadRequest(response);
            }

            var existingReservation = _reservationService.TGetById(id);
            if (existingReservation == null)
            {
                var response = new ApiResponse<Reservation>(false, "Reservation not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateReservationDto, existingReservation);
            _reservationService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Reservation>(true, "Reservation updated successfully", updatedValue);
            return Ok(successResponse);
        }

        [HttpGet("CheckActiveReservation/{clientId}")]
        public IActionResult CheckActiveReservation(int clientId)
        {
            try
            {
                var today = DateTime.UtcNow.Date;

                var reservation = _reservationService.TGetList()
                    .FirstOrDefault(r => r.ClientId == clientId && r.BookingEntryDate <= today && r.BookingExitTime >= today);

                if (reservation != null)
                {
                    var response = new
                    {
                        status = true,
                        message = "Reservation available",
                        reservation = true,
                        hotelId = reservation.HotelId
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "Reservation not available",
                        reservation = false,
                        hotelId = 0
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = false,
                    message = $"An error occurred: {ex.Message}"
                };
                return StatusCode(500, errorResponse); // 500 Internal Server Error
            }
        }

    }
}
