using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzSapkaTShirt.Data;
using OzSapkaTShirt.Models;

namespace OzSapkaTShirt2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsAPIController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public ProductsAPIController(ApplicationContext context)
		{
			_context = context;
		}

		// GET: api/ProductsAPI

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			if(_context.Products == null)
			{
				return NotFound();
			}
			return await _context.Products.ToListAsync();
		}

		// GET: api/ProductsAPI/id

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(long id)
		{
			if(_context.Products == null)
			{
				return NotFound();
			}
			else
			{
				var result = await _context.Products.FindAsync(id);
				if(result == null)
				{
					return NotFound();
				}
				return result;
			}
		}

		// POST: api/ProductsAPI

		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			if(_context.Products == null)
			{
				return Problem("ApplicationContext.Products null");
			}
			else
			{
				_context.Products.Add(product);
			    await _context.SaveChangesAsync();
				return CreatedAtAction("GetProduct", new { id = product.Id }, product);
			}
		}

		// DELETE: api/ProductsAPI/id

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(long id)
		{
			if(_context.Products == null)
			{
				return NotFound();
			}
			else
			{
				var result = await _context.Products.FindAsync(id);
				if(result == null)
				{
					return NotFound();
				}
				else
				{
					_context.Products.Remove(result);
					await _context.SaveChangesAsync();
					
					return NoContent();
				}

			}
		}

        // PUT: api/ProductsAPI/id
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
	
