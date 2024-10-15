using ApiSQLite.Data;
using ApiSQLite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        //Get all
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        //Get single
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee newEmployee)
        {
            if (newEmployee != null)
            {
                appDbContext.Employees.Add(newEmployee);
                await appDbContext.SaveChangesAsync();
                var employees = await appDbContext.Employees.ToListAsync();
                return Ok(employees);
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                appDbContext.Employees.Remove(employee);
                await appDbContext.SaveChangesAsync();
                var employees = await appDbContext.Employees.ToListAsync();
                return Ok(employees);
            }
            return BadRequest();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmployee)
        {
            if (updatedEmployee != null)
            {
                var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                if (employee != null)
                {
                    employee.Name = updatedEmployee.Name;
                    employee.Age = updatedEmployee.Age;
                    await appDbContext.SaveChangesAsync();
                    var employees = await appDbContext.Employees.ToListAsync();
                    return Ok(employees);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}