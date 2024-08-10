﻿using AllinLobby.Bussiness.Abstract;
using AllinLobby.DTO.DTOs.FoodDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllinLobby.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController(IGenericService<Food> _foodService, IMapper _mapper, IWebHostEnvironment _env) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var values = _foodService.TGetList();
            var response = new ApiResponse<IEnumerable<Food>>(true, "Data retrieved successfully", values);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _foodService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Food>(false, "Food not found", null);
                return NotFound(response);
            }

            var successResponse = new ApiResponse<Food>(true, "Food retrieved successfully", value);
            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _foodService.TGetById(id);
            if (value == null)
            {
                var response = new ApiResponse<Food>(false, "Food not found", null);
                return NotFound(response);
            }

            _foodService.TDelete(id);
            var successResponse = new ApiResponse<string>(true, "Food deleted successfully", null);
            return Ok(successResponse);
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateFoodDto createFoodDto)
        {
            if (createFoodDto == null)
            {
                var response = new ApiResponse<Food>(false, "Invalid food data", null);
                return BadRequest(response);
            }

            var newFood = _mapper.Map<Food>(createFoodDto);

            if (createFoodDto.Photo != null)
            {
                var photoPath = SavePhoto(createFoodDto.Photo);
                newFood.PhotoPath = photoPath;
            }

            _foodService.TCreate(newFood);

            var successResponse = new ApiResponse<Food>(true, "Food created successfully", newFood);
            return Ok(successResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateFoodDto updateFoodDto)
        {
            if (updateFoodDto == null)
            {
                var response = new ApiResponse<Food>(false, "Invalid food data", null);
                return BadRequest(response);
            }

            var existingFood = _foodService.TGetById(id);
            if (existingFood == null)
            {
                var response = new ApiResponse<Food>(false, "Food not found", null);
                return NotFound(response);
            }

            _mapper.Map(updateFoodDto, existingFood);

            if (updateFoodDto.Photo != null)
            {
                var photoPath = SavePhoto(updateFoodDto.Photo);
                existingFood.PhotoPath = photoPath;
            }


            _foodService.TUpdate(existingFood);

            var successResponse = new ApiResponse<Food>(true, "Food updated successfully", existingFood);
            return Ok(successResponse);
        }

        private string SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return "/uploads/" + uniqueFileName;
        }
    }
}
