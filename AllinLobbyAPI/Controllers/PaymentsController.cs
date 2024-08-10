using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.PaymentDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IGenericService<Payment> _paymentService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _paymentService.TGetList();
            var response = new ApiResponse<IEnumerable<Payment>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _paymentService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Payment>(false, "Payment not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Payment>(true, "Payment retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _paymentService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Payment>(false, "Payment not found", null);
                return NotFound(response);
            }

            _paymentService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Payment deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreatePaymentDto createPaymentDto)
        {
            if (createPaymentDto == null)
            {
                var response = new ApiResponse<Payment>(false, "Invalid payment data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Payment>(createPaymentDto);
            _paymentService.TCreate(newValue);

            var successResponse = new ApiResponse<Payment>(true, "Payment created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePaymentDto updatePaymentDto)
        {
            if (updatePaymentDto == null)
            {
                var response = new ApiResponse<Payment>(false, "Invalid payment data", null);
                return BadRequest(response);
            }

            var existingPayment = _paymentService.TGetById(id);
            if (existingPayment == null)
            {
                var response = new ApiResponse<Payment>(false, "Payment not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updatePaymentDto, existingPayment);
            _paymentService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Payment>(true, "Payment updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
