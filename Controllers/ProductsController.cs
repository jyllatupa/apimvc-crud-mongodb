using API_CRUD_MongoDB.DTO;
using API_CRUD_MongoDB.Models;
using API_CRUD_MongoDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD_MongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> Get()
        {
            return await _productService.GetProducts();
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Products>> Get(string id)
        {
            var product = await _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Products>> Create(Products products)
        {
            var createProduct = await _productService.CreateProduct(products);

            return CreatedAtRoute("GetProduct", new { id = createProduct.Id }, createProduct);
        }

        [HttpPut ("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, ProductDTO productDTO)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.UpdateProduct(id, productDTO);

            return NoContent();
        }

        [HttpDelete ("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
