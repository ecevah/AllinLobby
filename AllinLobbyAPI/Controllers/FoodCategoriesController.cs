using AllinLobby.Bussiness.Abstract;
using AllinLobby.DataAccess.Context;
using AllinLobby.DTO.DTOs.FoodCategoryDtos;
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
    public class FoodCategoriesController(IGenericService<FoodCategory> _foodCategoryService, IMapper _mapper, AllinLobbyContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var foodCategories = _context.FoodCategories
                                         .Include(fc => fc.Foods)  // Include related Foods
                                         .ToList();

            var response = new ApiResponse<IEnumerable<FoodCategory>>(true, "Data retrieved successfully", foodCategories);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var foodCategory = _context.FoodCategories
                                       .Include(fc => fc.Foods)  // Include related Foods
                                       .FirstOrDefault(fc => fc.FoodCategoryId == id);

            if (foodCategory == null)
            {
                var response = new ApiResponse<FoodCategory>(false, "Food category not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<FoodCategory>(true, "Food category retrieved successfully", foodCategory);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var foodCategory = _foodCategoryService.TGetById(id);
            if (foodCategory == null)
            {
                var response = new ApiResponse<FoodCategory>(false, "Food category not found", null);
                return NotFound(response);
            }

            _foodCategoryService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Food category deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateFoodCategoryDto createFoodCategoryDto)
        {
            if (createFoodCategoryDto == null)
            {
                var response = new ApiResponse<FoodCategory>(false, "Invalid food category data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<FoodCategory>(createFoodCategoryDto);
            _foodCategoryService.TCreate(newValue);

            var successResponse = new ApiResponse<FoodCategory>(true, "Food category created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateFoodCategoryDto updateFoodCategoryDto)
        {
            if (updateFoodCategoryDto == null)
            {
                var response = new ApiResponse<FoodCategory>(false, "Invalid food category data", null);
                return BadRequest(response);
            }

            var existingFoodCategory = _foodCategoryService.TGetById(id);
            if (existingFoodCategory == null)
            {
                var response = new ApiResponse<FoodCategory>(false, "Food category not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateFoodCategoryDto, existingFoodCategory);
            _foodCategoryService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<FoodCategory>(true, "Food category updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
