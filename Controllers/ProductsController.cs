using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.Interfaces;
using Ecommerce.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;


namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository) { 
        
        _repository = repository;
        
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() { 
        
        var Products=await _repository.GetAllAsync();
            return Ok(Products);

        
        }

 [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto) {

            var product = new Product
            {

                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description

            };
            await _repository.AddAsync(product);
            return Ok(product);
 }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto) {

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.Description = dto.Description;

            await _repository.UpdateAsync(product);
            return Ok(product);
}


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id) {

            var product = await _repository.GetByIdAsync(id);
            if (product == null)

                return NotFound();

            await _repository.DeleteAsync(product);
            return NoContent();

         }


     



    }


}
