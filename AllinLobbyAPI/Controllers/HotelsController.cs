using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.HotelDtos;
using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController(IGenericService<Hotel> _hotelService, IMapper _mapper, IWebHostEnvironment _env) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _hotelService.TGetList();
            var response = new ApiResponse<IEnumerable<Hotel>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _hotelService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Hotel>(false, "Hotel not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Hotel>(true, "Hotel retrieved successfully", value);
            return Ok(successResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _hotelService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Hotel>(false, "Hotel not found", null);
                return NotFound(response);
            }

            _hotelService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Hotel deleted successfully", null);
            return Ok(successResponse);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromForm] CreateHotelDto createHotelDto)
        {
            if (createHotelDto == null)
            {
                var response = new ApiResponse<Hotel>(false, "Invalid hotel data", null);
                return BadRequest(response);
            }

            var newHotel = _mapper.Map<Hotel>(createHotelDto);

            if (createHotelDto.Photo != null)
            {
                var photoPath = SavePhoto(createHotelDto.Photo);
                newHotel.PhotoPath = photoPath;
            }

            _hotelService.TCreate(newHotel);

            var successResponse = new ApiResponse<Hotel>(true, "Hotel created successfully", newHotel);
            return Ok(successResponse);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateHotelDto updateHotelDto)
        {
            if (updateHotelDto == null)
            {
                var response = new ApiResponse<Hotel>(false, "Invalid hotel data", null);
                return BadRequest(response);
            }

            var existingHotel = _hotelService.TGetById(id);
            if (existingHotel == null)
            {
                var response = new ApiResponse<Hotel>(false, "Hotel not found", null);
                return NotFound(response);
            }

            _mapper.Map(updateHotelDto, existingHotel);

            if (updateHotelDto.Photo != null)
            {
                var photoPath = SavePhoto(updateHotelDto.Photo);
                existingHotel.PhotoPath = photoPath;
            }

            _hotelService.TUpdate(existingHotel);

            var successResponse = new ApiResponse<Hotel>(true, "Hotel updated successfully", existingHotel);
            return Ok(successResponse);
        }

        [HttpGet("GetHotelsByCountry/{country}")]
        public IActionResult GetHotelsByCountry(Country country)
        {
            try
            {
                var hotels = _hotelService.TGetList()
                    .Where(h => h.Country == country)
                    .ToList();

                if (hotels.Any())
                {
                    var response = new
                    {
                        status = true,
                        message = "Hotels found",
                        hotels = hotels
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "No hotels found in this country",
                        hotels = new List<Hotel>() 
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
