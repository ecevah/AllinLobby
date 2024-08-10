using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.MessageDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IGenericService<Message> _messageService, IMapper _mapper, IGenericService<Session> _sessionService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _messageService.TGetList();
            var response = new ApiResponse<IEnumerable<Message>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _messageService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Message>(false, "Message not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Message>(true, "Message retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _messageService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Message>(false, "Message not found", null);
                return NotFound(response);
            }

            _messageService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Message deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateMessageDto createMessageDto)
        {
            if (createMessageDto == null)
            {
                var response = new ApiResponse<Message>(false, "Invalid message data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Message>(createMessageDto);
            _messageService.TCreate(newValue);

            var successResponse = new ApiResponse<Message>(true, "Message created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateMessageDto updateMessageDto)
        {
            if (updateMessageDto == null)
            {
                var response = new ApiResponse<Message>(false, "Invalid message data", null);
                return BadRequest(response);
            }

            var existingMessage = _messageService.TGetById(id);
            if (existingMessage == null)
            {
                var response = new ApiResponse<Message>(false, "Message not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateMessageDto, existingMessage);
            _messageService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Message>(true, "Message updated successfully", updatedValue);
            return Ok(successResponse);
        }

        [HttpGet("GetMessagesByReservationAndClient/{reservationId}/{clientId}")]
        public IActionResult GetMessagesByReservationAndClient(int reservationId, int clientId)
        {
            try
            {
                var session = _sessionService.TGetList()
                    .FirstOrDefault(s => s.ReservationId == reservationId && s.ClientId == clientId);

                if (session == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Session not found"
                    });
                }

                var sessionId = session.SessionId;

                var messages = _messageService.TGetList()
                    .Where(m => m.SessionId == sessionId)
                    .ToList();

                if (messages.Any())
                {
                    var response = new
                    {
                        status = true,
                        message = "Messages found",
                        sessionId = sessionId,
                        messages = messages
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = true,
                        message = "No messages found for this reservation and session",
                        sessionId = sessionId,
                        messages = new List<Message>() 
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
