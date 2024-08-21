using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.OrderDetailDtos;
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
    public class OrderDetailsController(IGenericService<OrderDetail> _orderDetailService, IMapper _mapper, AllinLobbyContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _context.OrderDetails
                                 .Include(od => od.Order)   // Eager load related Order entity
                                 .Include(od => od.Food)    // Eager load related Food entity
                                 .ToList();

            var response = new ApiResponse<IEnumerable<OrderDetail>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _context.OrderDetails
                                .Include(od => od.Order)   // Eager load related Order entity
                                .Include(od => od.Food)    // Eager load related Food entity
                                .FirstOrDefault(od => od.OrderDetailId == id);

            if (value == null)
            {
                var response = new ApiResponse<OrderDetail>(false, "Order detail not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<OrderDetail>(true, "Order detail retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _orderDetailService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<OrderDetail>(false, "Order detail not found", null);
                return NotFound(response);
            }

            _orderDetailService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Order detail deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderDetailDto createOrderDetailDto)
        {
            if (createOrderDetailDto == null)
            {
                var response = new ApiResponse<OrderDetail>(false, "Invalid order detail data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<OrderDetail>(createOrderDetailDto);
            _orderDetailService.TCreate(newValue);

            var successResponse = new ApiResponse<OrderDetail>(true, "Order detail created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOrderDetailDto updateOrderDetailDto)
        {
            if (updateOrderDetailDto == null)
            {
                var response = new ApiResponse<OrderDetail>(false, "Invalid order detail data", null);
                return BadRequest(response);
            }

            var existingOrderDetail = _orderDetailService.TGetById(id);
            if (existingOrderDetail == null)
            {
                var response = new ApiResponse<OrderDetail>(false, "Order detail not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateOrderDetailDto, existingOrderDetail);
            _orderDetailService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<OrderDetail>(true, "Order detail updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
