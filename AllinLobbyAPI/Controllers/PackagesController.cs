using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.PackageDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController(IGenericService<Package> _packageService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _packageService.TGetList();
            var response = new ApiResponse<IEnumerable<Package>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _packageService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Package>(false, "Package not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Package>(true, "Package retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _packageService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Package>(false, "Package not found", null);
                return NotFound(response);
            }

            _packageService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Package deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreatePackageDto createPackageDto)
        {
            if (createPackageDto == null)
            {
                var response = new ApiResponse<Package>(false, "Invalid package data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Package>(createPackageDto);
            _packageService.TCreate(newValue);

            var successResponse = new ApiResponse<Package>(true, "Package created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePackageDto updatePackageDto)
        {
            if (updatePackageDto == null)
            {
                var response = new ApiResponse<Package>(false, "Invalid package data", null);
                return BadRequest(response);
            }

            var existingPackage = _packageService.TGetById(id);
            if (existingPackage == null)
            {
                var response = new ApiResponse<Package>(false, "Package not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updatePackageDto, existingPackage);
            _packageService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Package>(true, "Package updated successfully", updatedValue);
            return Ok(successResponse);
        }

        [HttpGet("GetPackagesByHotel/{hotelId}")]
        public IActionResult GetPackagesByHotel(int hotelId)
        {
            try
            {
                var packages = _packageService.TGetList()
                    .Where(p => p.HotelId == hotelId)
                    .ToList();

                if (packages.Any())
                {
                    var response = new
                    {
                        status = true,
                        message = "Packages found",
                        packages = packages
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "No packages found for this hotel",
                        packages = new List<Package>() 
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
