using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HW_Azure_3.Data;
using HW_Azure_3.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HW_Azure_3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private const string connectionString = "AzureSql";
        public StudentsController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("secret")]
        public async Task<IActionResult> GetSecret()
        {
            try
            {
                var connection = _configuration[$"ConnectionStrings:{connectionString}"];
                return Ok(connection);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Student> students = await _context.Students.ToListAsync();
                if (students.Count == 0) return Ok("Table 'Students' is empty");
                return Ok(students); 
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return Ok(student);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Student? student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}

