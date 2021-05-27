using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventSource;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Resources;
using Supermarket.API.Extensions;

namespace Supermarket.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public CategoriesController(ICategoryService categoryService, IMapper mapper, 
                                        ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            _logger.LogInformation("Getting all categories");
            var categories = await _categoryService.ListAsync();
            var resources =
                _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
            // return categories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResource>> GetCategoryAsync(int id)
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState.GetErrorMessages());
            _logger.LogInformation("Getting category {Id}", id);

            var result = await _categoryService.GetAsync(id);

            if (!result.Success)
            {
                _logger.LogWarning("Category {Id} not found", id);
                return NotFound(result.Message);
            }

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);
            // return categoryResource;
        }
        

        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            // if (!ModelState.IsValid)
            // {
            //     Console.WriteLine("Bad model state\n");
            //     return BadRequest(ModelState.GetErrorMessages());
            // }
                
            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.SaveAsync(category);
            
            if (!result.Success)
                return BadRequest(result.Message);
            
            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            
            return Ok(categoryResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState.GetErrorMessages());
        
            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.UpdateAsync(id, category);
        
            if (!result.Success)
                return NotFound(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);
        }
        
        
    }
}
