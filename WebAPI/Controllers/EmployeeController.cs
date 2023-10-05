using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.BussinessEntities.Model;
using WebAPI.Repository.LoginRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound(); // Return a 404 response if the employee is not found.
            }

            return Ok(employee);
        }


        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee newEmployee)
        {
            try
            {
                if (newEmployee == null)
                {
                    return BadRequest(); // Return a 400 response if the request body is invalid.
                }

                _employeeRepository.Add(newEmployee);

                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                return BadRequest("Error occured while creating Employee data:" + ex.Message + "");
            }
           
        }

        [HttpPost]
        [Route("Update/{Id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee == null || id != updatedEmployee.Id)
            {
                return BadRequest("Error occured while updating Employee data"); // Return a 400 response if the request body is invalid or the ID doesn't match.
            }

            var existingEmployee = _employeeRepository.GetById(id);

            if (existingEmployee == null)
            {
                return NotFound("Employee Data not found"); // Return a 404 response if the employee is not found.
            }

            _employeeRepository.Update(updatedEmployee);

            return NoContent(); // Return a 204 response for successful updates.
        }

        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                Response response = new Response();

                var existingEmployee = _employeeRepository.GetById(id);

                if (existingEmployee == null)
                {
                    return NotFound("Employee Data not found");  // Return a 404 response if the employee is not found.
                }

                var result = await _employeeRepository.Delete(id);
                if (!result)
                {
                    response.StatusCode = 500;
                    response.Message = "Something Went Wrong";
                }
                else
                {
                    response.StatusCode = 200;
                    response.Message = "Delete Successfully";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                return BadRequest("Something Went Wrong: " + ex.Message + "");
            }

        }

    }
}
