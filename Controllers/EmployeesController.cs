using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDBContext dBContext;

        public EmployeesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
           var allEmployees = dBContext.Employees.ToList();

            return Ok(allEmployees);

        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id)
        {
            var employee = dBContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
       
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };



            dBContext.Employees.Add(employeeEntity);
            dBContext.SaveChanges();

            return Ok(employeeEntity);
        }
        [HttpPut]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dBContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();

            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dBContext.SaveChanges();

            return Ok(employee);

        }
        [HttpDelete]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dBContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            dBContext.Employees.Remove(employee);
            dBContext.SaveChanges();

            return Ok();
        }
    }
}
