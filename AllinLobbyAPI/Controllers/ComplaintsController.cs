using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.ComplaintDtos;
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
    public class ComplaintsController(IGenericService<Complaint> _complaintService, IMapper _mapper, AllinLobbyContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _context.Complaints
                                 .Include(c => c.Client)
                                 .Include(c => c.Hotel)
                                 .Include(c => c.Room)
                                 .ToList();

            var response = new ApiResponse<IEnumerable<Complaint>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _context.Complaints
                                .Include(c => c.Client)
                                .Include(c => c.Hotel)
                                .Include(c => c.Room)
                                .FirstOrDefault(c => c.ComplaintID == id);

            if (value == null)
            {
                var response = new ApiResponse<Complaint>(false, "Complaint not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Complaint>(true, "Complaint retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _complaintService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Complaint>(false, "Complaint not found", null);
                return NotFound(response);
            }

            _complaintService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Complaint deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateComplaintDto createComplaintDto)
        {
            if (createComplaintDto == null)
            {
                var response = new ApiResponse<Complaint>(false, "Invalid complaint data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Complaint>(createComplaintDto);
            _complaintService.TCreate(newValue);

            var successResponse = new ApiResponse<Complaint>(true, "Complaint created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateComplaintDto updateComplaintDto)
        {
            if (updateComplaintDto == null)
            {
                var response = new ApiResponse<Complaint>(false, "Invalid complaint data", null);
                return BadRequest(response);
            }

            var existingComplaint = _complaintService.TGetById(id);
            if (existingComplaint == null)
            {
                var response = new ApiResponse<Complaint>(false, "Complaint not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateComplaintDto, existingComplaint);
            _complaintService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Complaint>(true, "Complaint updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
