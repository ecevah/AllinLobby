using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.SubscriptionDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController(IGenericService<Subscription> _subscriptionService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _subscriptionService.TGetList();
            var response = new ApiResponse<IEnumerable<Subscription>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _subscriptionService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Subscription>(false, "Subscription not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Subscription>(true, "Subscription retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _subscriptionService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Subscription>(false, "Subscription not found", null);
                return NotFound(response);
            }

            _subscriptionService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Subscription deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateSubscriptionDto createSubscriptionDto)
        {
            if (createSubscriptionDto == null)
            {
                var response = new ApiResponse<Subscription>(false, "Invalid subscription data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Subscription>(createSubscriptionDto);
            _subscriptionService.TCreate(newValue);

            var successResponse = new ApiResponse<Subscription>(true, "Subscription created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateSubscriptionDto updateSubscriptionDto)
        {
            if (updateSubscriptionDto == null)
            {
                var response = new ApiResponse<Subscription>(false, "Invalid subscription data", null);
                return BadRequest(response);
            }

            var existingSubscription = _subscriptionService.TGetById(id);
            if (existingSubscription == null)
            {
                var response = new ApiResponse<Subscription>(false, "Subscription not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateSubscriptionDto, existingSubscription);
            _subscriptionService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Subscription>(true, "Subscription updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
