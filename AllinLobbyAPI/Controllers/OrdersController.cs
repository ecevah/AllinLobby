using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.OrderDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IGenericService<Order> _orderService, IMapper _mapper, IGenericService<OrderDetail> _orderDetailService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _orderService.TGetList();
            var response = new ApiResponse<IEnumerable<Order>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _orderService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Order>(false, "Order not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Order>(true, "Order retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _orderService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Order>(false, "Order not found", null);
                return NotFound(response);
            }

            _orderService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Order deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                var response = new ApiResponse<Order>(false, "Invalid order data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Order>(createOrderDto);
            _orderService.TCreate(newValue);

            var successResponse = new ApiResponse<Order>(true, "Order created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOrderDto updateOrderDto)
        {
            if (updateOrderDto == null)
            {
                var response = new ApiResponse<Order>(false, "Invalid order data", null);
                return BadRequest(response);
            }

            var existingOrder = _orderService.TGetById(id);
            if (existingOrder == null)
            {
                var response = new ApiResponse<Order>(false, "Order not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateOrderDto, existingOrder);
            _orderService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Order>(true, "Order updated successfully", updatedValue);
            return Ok(successResponse);
        }

        [HttpPost("CreateOrderWithDetails")]
        public IActionResult CreateOrderWithDetails([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null || createOrderDto.OrderDetails == null || !createOrderDto.OrderDetails.Any())
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Invalid order data or empty order details"
                });
            }

            try
            {
                // Order'ı oluştur
                var newOrder = _mapper.Map<Order>(createOrderDto);
                newOrder.CreateAt = DateTime.UtcNow.AddHours(3);
                newOrder.UpdateAt = DateTime.UtcNow.AddHours(3);

                _orderService.TCreate(newOrder);

                // Order ID'yi kullanarak OrderDetail'ları oluştur
                foreach (var orderDetailDto in createOrderDto.OrderDetails)
                {
                    var newOrderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
                    newOrderDetail.OrderId = newOrder.OrderId; // Oluşturulan Order ID'sini atıyoruz
                    newOrderDetail.CreateAt = DateTime.UtcNow.AddHours(3);
                    newOrderDetail.UpdateAt = DateTime.UtcNow.AddHours(3);

                    _orderDetailService.TCreate(newOrderDetail);
                }

                var response = new
                {
                    status = true,
                    message = "Order and order details created successfully",
                    orderId = newOrder.OrderId
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = false,
                    message = $"An error occurred: {ex.Message}"
                };
                return StatusCode(500, errorResponse); // 500 Internal Server Error
            }
        }

    }
}
