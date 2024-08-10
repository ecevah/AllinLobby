using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.RoomDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController(IGenericService<Room> _roomService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _roomService.TGetList();
            var response = new ApiResponse<IEnumerable<Room>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _roomService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Room>(false, "Room not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Room>(true, "Room retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _roomService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Room>(false, "Room not found", null);
                return NotFound(response);
            }

            _roomService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Room deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            if (createRoomDto == null)
            {
                var response = new ApiResponse<Room>(false, "Invalid room data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Room>(createRoomDto);
            _roomService.TCreate(newValue);

            var successResponse = new ApiResponse<Room>(true, "Room created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRoomDto updateRoomDto)
        {
            if (updateRoomDto == null)
            {
                var response = new ApiResponse<Room>(false, "Invalid room data", null);
                return BadRequest(response);
            }

            var existingRoom = _roomService.TGetById(id);
            if (existingRoom == null)
            {
                var response = new ApiResponse<Room>(false, "Room not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateRoomDto, existingRoom);
            _roomService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Room>(true, "Room updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
