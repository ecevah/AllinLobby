using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.CommentDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(IGenericService<Comment> _commentService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _commentService.TGetList();
            var response = new ApiResponse<IEnumerable<Comment>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _commentService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Comment>(false, "Comment not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Comment>(true, "Comment retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _commentService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Comment>(false, "Comment not found", null);
                return NotFound(response);
            }

            _commentService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Comment deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateCommentDto createCommentDto)
        {
            if (createCommentDto == null)
            {
                var response = new ApiResponse<Comment>(false, "Invalid comment data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Comment>(createCommentDto);
            _commentService.TCreate(newValue);

            var successResponse = new ApiResponse<Comment>(true, "Comment created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCommentDto updateCommentDto)
        {
            if (updateCommentDto == null)
            {
                var response = new ApiResponse<Comment>(false, "Invalid comment data", null);
                return BadRequest(response);
            }

            var existingComment = _commentService.TGetById(id);
            if (existingComment == null)
            {
                var response = new ApiResponse<Comment>(false, "Comment not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateCommentDto, existingComment);
            _commentService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Comment>(true, "Comment updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
