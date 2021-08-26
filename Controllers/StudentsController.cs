using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context) {
            _context = context;
        }

        //Get All Students: /api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll() {
            return await _context.Students.ToListAsync();
        }

        //Get One Student: /api/student/{}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int Id) {
            var StudentItem = await _context.Students.FindAsync(Id);

            if (StudentItem == null) {
                return NotFound();
            }
            return StudentItem;
        }

        //Insert One Student /api/student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student) {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        //Update One Student /api/student/{}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int Id, Student student) {
            if (Id != student.Id) {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Delete One Student /api/student/{}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int Id) {
            var Student = await _context.Students.FindAsync(Id);
            if (Student == null) {
                return NotFound();
            }
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}