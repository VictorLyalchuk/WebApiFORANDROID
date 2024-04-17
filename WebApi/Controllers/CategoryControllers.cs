using Core.DTO.Category;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryControllers : Controller
    {
        private readonly ICategoryService _category;
        public CategoryControllers(ICategoryService category)
        {
            _category = category;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> CategoryGetByIdAsync(int id)
        {
            return Ok(await _category.GetByIdAsync(id));
        }

        [HttpGet("CategoryGetAsync")]
        public async Task<IActionResult> CategoryGetAllAsync()
        {
            return Ok(await _category.GetAllAsync());
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateProduct(CreateCategoryDTO createCategoryDTO)
        {
            await _category.CreateAsync(createCategoryDTO);
            return Ok();
        }

        [HttpDelete("DeleteCategoryByID/{id}")]
        public async Task<IActionResult> DeleteCategoryByIDAsync(int id)
        {
            await _category.DeleteCategoryByIDAsync(id);
            return Ok();
        }

        [HttpPut("EditCategory")]
        public async Task<IActionResult> EditProduct(EditCategoryDTO editCategoryDTO)
        {
            await _category.EditAsync(editCategoryDTO);
            return Ok();
        }

    }
}
