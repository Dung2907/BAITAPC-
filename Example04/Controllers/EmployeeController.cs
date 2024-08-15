using Microsoft.AspNetCore.Mvc;
using Example04.Models;
using Example04.Context;

namespace Example04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeContext companyContext;

        public EmployeeController(EmployeeContext companyContext)
        {
            this.companyContext = companyContext;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return this.companyContext.Employees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return this.companyContext.Employees.FirstOrDefault(s => s.Id == id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            this.companyContext.Employees.Add(value);
            this.companyContext.SaveChanges();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee value)
        {
            var employee = this.companyContext.Employees.FirstOrDefault(s => s.Id == id);
            if (employee != null)
            {
                this.companyContext.Entry(employee).CurrentValues.SetValues(value);
                this.companyContext.SaveChanges();
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}/delete")]
        public void Delete(int id)
        {
            var employee = this.companyContext.Employees.FirstOrDefault(s => s.Id == id);
            if (employee != null)
            {
                this.companyContext.Employees.Remove(employee);
                this.companyContext.SaveChanges();
            }
        }
    }
}