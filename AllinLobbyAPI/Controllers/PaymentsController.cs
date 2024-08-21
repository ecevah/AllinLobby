using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.PaymentDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IGenericService<Payment> _paymentService, IMapper _mapper, ILogger<PaymentsController> _logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var values = _paymentService.TGetList();
                var response = new ApiResponse<IEnumerable<Payment>>(true, "Data retrieved successfully", values);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving payments");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var value = _paymentService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Payment>(false, "Payment not found"));
                }

                var successResponse = new ApiResponse<Payment>(true, "Payment retrieved successfully", value);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving payment with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while retrieving data"));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var value = _paymentService.TGetById(id);
                if (value == null)
                {
                    return NotFound(new ApiResponse<Payment>(false, "Payment not found"));
                }

                _paymentService.TDelete(id);
                return Ok(new ApiResponse<string>(true, "Payment deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting payment with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while deleting data"));
            }
        }

        [HttpPost]
        public IActionResult Create(CreatePaymentDto createPaymentDto)
        {
            if (createPaymentDto == null)
            {
                return BadRequest(new ApiResponse<Payment>(false, "Invalid payment data"));
            }

            try
            {
                var newValue = _mapper.Map<Payment>(createPaymentDto);
                _paymentService.TCreate(newValue);

                var successResponse = new ApiResponse<Payment>(true, "Payment created successfully", newValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new payment");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while creating data"));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePaymentDto updatePaymentDto)
        {
            if (updatePaymentDto == null)
            {
                return BadRequest(new ApiResponse<Payment>(false, "Invalid payment data"));
            }

            try
            {
                var existingPayment = _paymentService.TGetById(id);
                if (existingPayment == null)
                {
                    return NotFound(new ApiResponse<Payment>(false, "Payment not found"));
                }

                var updatedValue = _mapper.Map(updatePaymentDto, existingPayment);
                _paymentService.TUpdate(updatedValue);

                var successResponse = new ApiResponse<Payment>(true, "Payment updated successfully", updatedValue);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating payment with ID {id}");
                return StatusCode(500, new ApiResponse<string>(false, "An error occurred while updating data"));
            }
        }
    }
}
