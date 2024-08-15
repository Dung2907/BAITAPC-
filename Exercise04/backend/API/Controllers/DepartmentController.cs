using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Domain;
using API.Services.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _departmentService;

        public DepartmentController(IDepartment departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<Department>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _departmentService.GetAll();
            return Ok(response);
        }

        [HttpGet]
        [Route("Get/{departmentId}")]
        public async Task<ActionResult> Get(int departmentId)
        {
            var response = await _departmentService.GetById(departmentId);
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Invalid data."
                });
            }

            var response = await _departmentService.Add(department);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("Delete/{departmentId}")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int departmentId)
        {
            var response = await _departmentService.Delete(departmentId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
