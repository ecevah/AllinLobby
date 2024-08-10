using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.SessionDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController(IGenericService<Session> _sessionService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _sessionService.TGetList();
            var response = new ApiResponse<IEnumerable<Session>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _sessionService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Session>(false, "Session not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Session>(true, "Session retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _sessionService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Session>(false, "Session not found", null);
                return NotFound(response);
            }

            _sessionService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Session deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateSessionDto createSessionDto)
        {
            if (createSessionDto == null)
            {
                var response = new ApiResponse<Session>(false, "Invalid session data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Session>(createSessionDto);
            _sessionService.TCreate(newValue);

            var successResponse = new ApiResponse<Session>(true, "Session created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateSessionDto updateSessionDto)
        {
            if (updateSessionDto == null)
            {
                var response = new ApiResponse<Session>(false, "Invalid session data", null);
                return BadRequest(response);
            }

            var existingSession = _sessionService.TGetById(id);
            if (existingSession == null)
            {
                var response = new ApiResponse<Session>(false, "Session not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateSessionDto, existingSession);
            _sessionService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Session>(true, "Session updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
