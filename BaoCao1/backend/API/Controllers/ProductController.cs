using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        public ProductController(IProduct productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [HttpGet, Route("GetAll")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _productService.GetAll();
            return Ok(response);
        }
        [HttpGet, Route("Get/{ProductId}")]
        public async Task<ActionResult> Get(long ProductId)
        {

            var response = await _productService.GetById(ProductId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost, Route("Add")]
        [ProducesResponseType(typeof(List<Response>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add([FromBody] Product product)
        {
            var response = await _productService.Add(product);
            return Ok(response);
        }

        [Authorize]
        [HttpPut, Route("Update")]
        [ProducesResponseType(typeof(List<Response>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            var response = await _productService.Update(product);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete, Route("Delete/{productId}")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(long ProductId)
        {
            var response = await _productService.Delete(ProductId);
            return Ok(response);
        }
    }
}