using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.BookingEventDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingEventsController(IGenericService<BookingEvent> _bookingEventService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _bookingEventService.TGetList();
            var response = new ApiResponse<IEnumerable<BookingEvent>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _bookingEventService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<BookingEvent>(false, "Booking event not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<BookingEvent>(true, "Booking event retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _bookingEventService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<BookingEvent>(false, "Booking event not found", null);
                return NotFound(response);
            }

            _bookingEventService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Booking event deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateBookingEventDto createBookingEventDto)
        {
            if (createBookingEventDto == null)
            {
                var response = new ApiResponse<BookingEvent>(false, "Invalid booking event data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<BookingEvent>(createBookingEventDto);
            _bookingEventService.TCreate(newValue);

            var successResponse = new ApiResponse<BookingEvent>(true, "Booking event created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookingEventDto updateBookingEventDto)
        {
            if (updateBookingEventDto == null)
            {
                var response = new ApiResponse<BookingEvent>(false, "Invalid booking event data", null);
                return BadRequest(response);
            }

            var existingBookingEvent = _bookingEventService.TGetById(id);
            if (existingBookingEvent == null)
            {
                var response = new ApiResponse<BookingEvent>(false, "Booking event not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateBookingEventDto, existingBookingEvent);
            _bookingEventService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<BookingEvent>(true, "Booking event updated successfully", updatedValue);
            return Ok(successResponse);
        }

        [HttpGet("GetEventsByClient/{clientId}")]
        public IActionResult GetEventsByClient(int clientId)
        {
            try
            {
                var events = _bookingEventService.TGetList()
                    .Where(e => e.ClientId == clientId)
                    .ToList();

                if (events.Any())
                {
                    var response = new
                    {
                        status = true,
                        message = "Events found",
                        events = events
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "No events found for this client",
                        events = new List<BookingEvent>()
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
                return StatusCode(500, errorResponse);
            }
        }
        

    }
}
