using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.CleaningRoomDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningRoomsController(IGenericService<CleaningRoom> _cleaningRoomService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _cleaningRoomService.TGetList();
            var response = new ApiResponse<IEnumerable<CleaningRoom>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _cleaningRoomService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<CleaningRoom>(false, "Cleaning room not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<CleaningRoom>(true, "Cleaning room retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _cleaningRoomService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<CleaningRoom>(false, "Cleaning room not found", null);
                return NotFound(response);
            }

            _cleaningRoomService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Cleaning room deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateCleaningRoomDto createCleaningRoomDto)
        {
            if (createCleaningRoomDto == null)
            {
                var response = new ApiResponse<CleaningRoom>(false, "Invalid cleaning room data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<CleaningRoom>(createCleaningRoomDto);
            _cleaningRoomService.TCreate(newValue);

            var successResponse = new ApiResponse<CleaningRoom>(true, "Cleaning room created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCleaningRoomDto updateCleaningRoomDto)
        {
            if (updateCleaningRoomDto == null)
            {
                var response = new ApiResponse<CleaningRoom>(false, "Invalid cleaning room data", null);
                return BadRequest(response);
            }

            var existingCleaningRoom = _cleaningRoomService.TGetById(id);
            if (existingCleaningRoom == null)
            {
                var response = new ApiResponse<CleaningRoom>(false, "Cleaning room not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateCleaningRoomDto, existingCleaningRoom);
            _cleaningRoomService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<CleaningRoom>(true, "Cleaning room updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
