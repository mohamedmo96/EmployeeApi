using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Employee.DTO;
using Employee.Model;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public EmployeeController(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmployees(int page = 1, int pageSize = 2)
        {
            try
            {
                // Calculate the number of items to skip based on the page number and page size
                int skip = (page - 1) * pageSize;

                // Retrieve employees from the database with pagination and include their addresses
                var employees = _context.Employees
                    .Include(e => e.Addresses)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                // Project the employees with addresses to DTOs
                var employeeDTOs = employees.Select(employee => new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Addresses = employee.Addresses.Select(address => new AddressDTO
                    {
                        Description = address.Description
                    }).ToList()
                }).ToList();

                return Ok(employeeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }






        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }
        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employe>(createEmployeeDTO);

                // Add the employee to the context and save changes
                _context.Employees.Add(employee);
                _context.SaveChanges();

                var createdEmployeeDTO = _mapper.Map<EmployeeDTO>(employee);

                return CreatedAtAction("GetEmployee", new { id = createdEmployeeDTO.Id }, createdEmployeeDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                if (id != employeeDTO.Id)
                {
                    return BadRequest("Invalid ID");
                }

                if (ModelState.IsValid)
                {
                    var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == id);

                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    _mapper.Map(employeeDTO, existingEmployee);
                    _context.SaveChanges(); // Save changes to the database

                    return NoContent();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {

            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);



            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();

        }




    }
}
