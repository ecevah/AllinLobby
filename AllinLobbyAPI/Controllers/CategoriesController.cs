using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.CategoryDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IGenericService<Category> _categoryService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _categoryService.TGetList();
            var response = new ApiResponse<IEnumerable<Category>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _categoryService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Category>(false, "Category not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Category>(true, "Category retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _categoryService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Category>(false, "Category not found", null);
                return NotFound(response);
            }

            _categoryService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Category deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                var response = new ApiResponse<Category>(false, "Invalid category data", null);
                return BadRequest(response);
            }

            var newValue = _mapper.Map<Category>(createCategoryDto);
            _categoryService.TCreate(newValue);

            var successResponse = new ApiResponse<Category>(true, "Category created successfully", newValue);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null)
            {
                var response = new ApiResponse<Category>(false, "Invalid category data", null);
                return BadRequest(response);
            }

            var existingCategory = _categoryService.TGetById(id);
            if (existingCategory == null)
            {
                var response = new ApiResponse<Category>(false, "Category not found", null);
                return NotFound(response);
            }

            var updatedValue = _mapper.Map(updateCategoryDto, existingCategory);
            _categoryService.TUpdate(updatedValue);

            var successResponse = new ApiResponse<Category>(true, "Category updated successfully", updatedValue);
            return Ok(successResponse);
        }
    }
}
