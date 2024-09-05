using Microsoft.AspNetCore.Mvc;
using System.Net;
using TranAnhDung.API.Domain;
using TranAnhDung.API.Services.Interface;

namespace TranAnhDung.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<Employee>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _employeeService.GetAll();
            return Ok(response);
        }

        [HttpGet]
        [Route("Get/{employeeId}")]
        public async Task<ActionResult> Get(int employeeId)
        {
            var response = await _employeeService.GetById(employeeId);
            return Ok(response);
        }

        // [HttpPost]
        // [Route("Add")]
        // [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        // [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        // public async Task<ActionResult> Add([FromBody] Employee employee)
        // {
        //     var response = await _employeeService.Add(employee);
        //     if (response.IsSuccess)
        //     {
        //         return Ok(response);
        //     }
        //     return BadRequest(response);
        // }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add([FromForm] Employee employeeDto, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Lưu hình ảnh vào thư mục

                var filePath = Path.Combine("wwwroot/images", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                employeeDto.imageUrl = file.FileName; // Lưu tên file hoặc đường dẫn
            }

            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Mobile = employeeDto.Mobile,
                IsPermanent = employeeDto.IsPermanent,
                Gender = employeeDto.Gender,
                DepartmentId = employeeDto.DepartmentId,
                DateOfBirth = employeeDto.DateOfBirth,
                imageUrl = employeeDto.imageUrl
            };

            var response = await _employeeService.Add(employee, file);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // [HttpPut]
        // [Route("Update")]
        // [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        // [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        // public async Task<ActionResult> Update([FromBody] Employee employee)
        // {
        //     var response = await _employeeService.Update(employee);
        //     if (response.IsSuccess)
        //     {
        //         return Ok(response);
        //     }
        //     return BadRequest(response);
        // }
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromForm] Employee employeeDto, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                employeeDto.imageUrl = file.FileName;
            }

            var response = await _employeeService.Update(employeeDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpDelete]
        [Route("Delete/{employeeId}")]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int employeeId)
        {
            var response = await _employeeService.Delete(employeeId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}