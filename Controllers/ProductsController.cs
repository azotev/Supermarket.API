using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Resources;

namespace Supermarket.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            var resources = 
                _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResource>> GetProductAsync(int id)
        {
            var result = await _productService.GetAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            return Ok(productResource);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.SaveAsync(product);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);

            return Ok(productResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResource>> PutAsync(int id, [FromBody] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.UpdateAsync(id, product);

            if (!result.Success)
                return NotFound(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);

            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);

            return Ok(productResource);
        }


    }
}
