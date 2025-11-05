using HW_Azure_3.Data;
using HW_Azure_3.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HW_Azure_3.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentsController(DataContext context) => _context = context;

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

