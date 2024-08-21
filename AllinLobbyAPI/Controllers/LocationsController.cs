using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.LocationDtos;
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
    public class LocationsController(IGenericService<Location> _locationService, IMapper _mapper, IWebHostEnvironment _env, AllinLobbyContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var locations = _context.Locations
                                     .Include(l => l.Hotel) // Include related Hotel
                                     .ToList();

            var response = new ApiResponse<IEnumerable<Location>>(true, "Data retrieved successfully", locations);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _context.Locations
                                    .Include(l => l.Hotel) // Include related Hotel
                                    .FirstOrDefault(l => l.LocationId == id);

            if (location == null)
            {
                var response = new ApiResponse<Location>(false, "Location not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Location>(true, "Location retrieved successfully", location);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _locationService.TGetById(id);
            if (location == null)
            {
                var response = new ApiResponse<Location>(false, "Location not found", null);
                return NotFound(response);
            }

            _locationService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Location deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateLocationDto createLocationDto)
        {
            if (createLocationDto == null)
            {
                var response = new ApiResponse<Location>(false, "Invalid location data", null);
                return BadRequest(response);
            }

            var newLocation = _mapper.Map<Location>(createLocationDto);

            if (createLocationDto.Photo != null)
            {
                var photoPath = SavePhoto(createLocationDto.Photo);
                newLocation.PhotoPath = photoPath;
            }

            _locationService.TCreate(newLocation);

            var successResponse = new ApiResponse<Location>(true, "Location created successfully", newLocation);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateLocationDto updateLocationDto)
        {
            if (updateLocationDto == null)
            {
                var response = new ApiResponse<Location>(false, "Invalid location data", null);
                return BadRequest(response);
            }

            var existingLocation = _locationService.TGetById(id);
            if (existingLocation == null)
            {
                var response = new ApiResponse<Location>(false, "Location not found", null);
                return NotFound(response);
            }

            _mapper.Map(updateLocationDto, existingLocation);

            if (updateLocationDto.Photo != null)
            {
                var photoPath = SavePhoto(updateLocationDto.Photo);
                existingLocation.PhotoPath = photoPath;
            }

            _locationService.TUpdate(existingLocation);

            var successResponse = new ApiResponse<Location>(true, "Location updated successfully", existingLocation);
            return Ok(successResponse);
        }

        [HttpGet("GetLocationsByHotel/{hotelId}")]
        public IActionResult GetLocationsByHotel(int hotelId)
        {
            try
            {
                var locations = _context.Locations
                                        .Include(l => l.Hotel) // Include related Hotel
                                        .Where(l => l.HotelId == hotelId)
                                        .ToList();

                if (locations.Any())
                {
                    var response = new
                    {
                        status = true,
                        message = "Locations found",
                        locations = locations
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "No locations found for this hotel",
                        locations = new List<Location>() // Return an empty list
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
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return "/uploads/" + uniqueFileName;
        }
    }
}
