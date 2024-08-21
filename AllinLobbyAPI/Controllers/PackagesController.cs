using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.PackageDtos;
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
    public class PackagesController(IGenericService<Package> _packageService, IMapper _mapper, ILogger<PackagesController> _logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var values = _packageService.TGetList();
                var response = new ApiResponse<IEnumerable<Package>>(true, "Data retrieved successfully", values);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving packages");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var value = _packageService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Package>(false, "Package not found"));
                }

                var successResponse = new ApiResponse<Package>(true, "Package retrieved successfully", value);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving package with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var value = _packageService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Package>(false, "Package not found"));
                }

                _packageService.TDelete(id);
                return Ok(new ApiResponse<string>(true, "Package deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting package with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while deleting data"));
            }
        }

        [HttpPost]
        public IActionResult Create(CreatePackageDto createPackageDto)
        {
            if (createPackageDto == null)
            {
                return BadRequest(new ApiResponse<Package>(false, "Invalid package data"));
            }

            try
            {
                var newValue = _mapper.Map<Package>(createPackageDto);
                _packageService.TCreate(newValue);

                var successResponse = new ApiResponse<Package>(true, "Package created successfully", newValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new package");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while creating data"));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePackageDto updatePackageDto)
        {
            if (updatePackageDto == null)
            {
                return BadRequest(new ApiResponse<Package>(false, "Invalid package data"));
            }

            try
            {
                var existingPackage = _packageService.TGetById(id);
                if (existingPackage == null)
                {
                    return NotFound(new ApiResponse<Package>(false, "Package not found"));
                }

                var updatedValue = _mapper.Map(updatePackageDto, existingPackage);
                _packageService.TUpdate(updatedValue);

                var successResponse = new ApiResponse<Package>(true, "Package updated successfully", updatedValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating package with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while updating data"));
            }
        }

        [HttpGet("GetPackagesByHotel/{hotelId}")]
        public IActionResult GetPackagesByHotel(int hotelId)
        {
            try
            {
                var packages = _packageService.TGetList()
                    .Where(p => p.HotelId == hotelId)
                    .ToList();

                var response = packages.Any()
                    ? new ApiResponse<IEnumerable<Package>>(true, "Packages found", packages)
                    : new ApiResponse<IEnumerable<Package>>(true, "No packages found for this hotel", new List<Package>());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving packages for hotel ID {hotelId}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }
    }
}
