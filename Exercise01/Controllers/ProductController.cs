using Exercise01.Context;
using Exercise01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise01.InputModels;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly Exercise01Context _context;

    public ProductController(Exercise01Context context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts(int page = 1, int pageSize = 10)
    {
        var products = _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return products;
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }
    // POST: api/products
    [HttpPost]
public async Task<IActionResult> PostProduct([FromBody] ProductInputModel productInput)
{
    try
    {
        if (ModelState.IsValid)
        {
            if (!_context.Categories.Any(c => c.CategoryId == productInput.CategoryId))
            {
                return BadRequest("Invalid categoryId");
            }

            var newProduct = new Product
            {
                ProductTitle = productInput.ProductTitle,
                ImageUrl = productInput.ImageUrl,
                Sku = productInput.Sku,
                PriceUnit = productInput.PriceUnit,
                Quantity = productInput.Quantity,
                CategoryId = productInput.CategoryId,
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }

        return BadRequest(ModelState);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}


    // PUT: api/products/{id}
 [HttpPut("{id}")]
public async Task<IActionResult> PutProduct(int id, ProductInputModel productInput)
{
    try
    {
        

        try
        {
            var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }
        // Kiểm tra xem categoryId có tồn tại không
        if (!_context.Categories.Any(c => c.CategoryId == productInput.CategoryId))
        {
            return BadRequest("Invalid categoryId");
        }

        // Cập nhật thông tin của đối tượng Product từ dữ liệu nhập
        existingProduct.ProductTitle = productInput.ProductTitle;
        existingProduct.ImageUrl = productInput.ImageUrl;
        existingProduct.Sku = productInput.Sku;
        existingProduct.PriceUnit = productInput.PriceUnit;
        existingProduct.Quantity = productInput.Quantity;
        existingProduct.CategoryId = productInput.CategoryId;

        _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(existingProduct);
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

    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}


    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
            public async Task<ActionResult<Category>> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Product with ID {id} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}
