
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageApi.Data;
using VintageApi.Models;

namespace VintageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        // Recupera tutti i prodotti dal database.
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        // PATCH: api/Products/1
        // Modifica un prodotto esistente.
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(
            int id,
            [FromBody] Product updateProduct)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.Price = updateProduct.Price;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // POST: api/Products
        // Crea un nuovo prodotto nel database.
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return Created(
                $"/api/Products/{product.Id}",
                product
            );
        }
    }
}
