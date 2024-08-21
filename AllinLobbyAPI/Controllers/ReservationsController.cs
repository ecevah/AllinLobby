using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.ReservationDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IGenericService<Reservation> _reservationService, IMapper _mapper, ILogger<ReservationsController> _logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var values = _reservationService.TGetList();
                var response = new ApiResponse<IEnumerable<Reservation>>(true, "Data retrieved successfully", values);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving reservations.");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var value = _reservationService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Reservation>(false, "Reservation not found", null));
                }

                var successResponse = new ApiResponse<Reservation>(true, "Reservation retrieved successfully", value);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving reservation with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var value = _reservationService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Reservation>(false, "Reservation not found", null));
                }

                _reservationService.TDelete(id);
                return Ok(new ApiResponse<string>(true, "Reservation deleted successfully", null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting reservation with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while deleting data"));
            }
        }

        [HttpPost]
        public IActionResult Create(CreateReservationDto createReservationDto)
        {
            if (createReservationDto == null)
            {
                return BadRequest(new ApiResponse<Reservation>(false, "Invalid reservation data", null));
            }

            try
            {
                var newValue = _mapper.Map<Reservation>(createReservationDto);
                _reservationService.TCreate(newValue);

                var successResponse = new ApiResponse<Reservation>(true, "Reservation created successfully", newValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a reservation");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while creating data"));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateReservationDto updateReservationDto)
        {
            if (updateReservationDto == null)
            {
                return BadRequest(new ApiResponse<Reservation>(false, "Invalid reservation data", null));
            }

            try
            {
                var existingReservation = _reservationService.TGetById(id);
                if (existingReservation == null)
                {
                    return NotFound(new ApiResponse<Reservation>(false, "Reservation not found", null));
                }

                var updatedValue = _mapper.Map(updateReservationDto, existingReservation);
                _reservationService.TUpdate(updatedValue);

                var successResponse = new ApiResponse<Reservation>(true, "Reservation updated successfully", updatedValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating reservation with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while updating data"));
            }
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
                _logger.LogError(ex, $"An error occurred while checking active reservation for client ID {clientId}");
                return StatusCode(500, new { status = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
