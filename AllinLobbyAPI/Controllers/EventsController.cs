﻿using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.EventDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IGenericService<Event> _eventService, IMapper _mapper, IWebHostEnvironment _env) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _eventService.TGetList();
            var response = new ApiResponse<IEnumerable<Event>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _eventService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Event>(false, "Event not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Event>(true, "Event retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _eventService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Event>(false, "Event not found", null);
                return NotFound(response);
            }

            _eventService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Event deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateEventDto createEventDto)
        {
            if (createEventDto == null)
            {
                var response = new ApiResponse<Event>(false, "Invalid event data", null);
                return BadRequest(response);
            }

            var newEvent = _mapper.Map<Event>(createEventDto);

            if (createEventDto.Photo != null)
            {
                var photoPath = SavePhoto(createEventDto.Photo);
                newEvent.PhotoPath = photoPath;
            }

            _eventService.TCreate(newEvent);

            var successResponse = new ApiResponse<Event>(true, "Event created successfully", newEvent);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateEventDto updateEventDto)
        {
            if (updateEventDto == null)
            {
                var response = new ApiResponse<Event>(false, "Invalid event data", null);
                return BadRequest(response);
            }

            var existingEvent = _eventService.TGetById(id);
            if (existingEvent == null)
            {
                var response = new ApiResponse<Event>(false, "Event not found", null);
                return NotFound(response);
            }

            _mapper.Map(updateEventDto, existingEvent);

            if (updateEventDto.Photo != null)
            {
                var photoPath = SavePhoto(updateEventDto.Photo);
                existingEvent.PhotoPath = photoPath;
            }

            _eventService.TUpdate(existingEvent);

            var successResponse = new ApiResponse<Event>(true, "Event updated successfully", existingEvent);
            return Ok(successResponse);
        }

        [HttpGet("GetEventsByHotel/{hotelId}")]
        public IActionResult GetEventsByHotel(int hotelId)
        {
            try
            {
                var events = _eventService.TGetList()
                    .Where(e => e.HotelId == hotelId)
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
                        message = "No events found for this hotel",
                        events = new List<Event>() // Boş bir liste döndürülüyor
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


        private string SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return "/uploads/" + uniqueFileName; // Return the relative path to the file
        }
    }
}