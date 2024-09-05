using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;

namespace TranAnhDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, Route("GetAll")]
        [ProducesResponseType(typeof(List<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _categoryService.GetAll();
            return Ok(response);
        }

        [HttpGet, Route("Get/{CategoryId}")]
        public async Task<ActionResult> Get(long CategoryId)
        {
            var response = await _categoryService.GetById(CategoryId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost, Route("Add")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add([FromBody] Category category)
        {
            var response = await _categoryService.Add(category);
            return Ok(response);
        }
        [Authorize]
        [HttpPut, Route("Update")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody] Category category)
        {
            var response = await _categoryService.Update(category);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete, Route("Delete/{categoryId}")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(long CategoryId)
        {
            var response = await _categoryService.Delete(CategoryId);
            return Ok(response);
        }
    }
}
