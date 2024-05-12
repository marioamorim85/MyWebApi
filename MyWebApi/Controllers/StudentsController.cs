using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Threading.Tasks;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IValidator<Student> _validator;
    private readonly IStudentService _studentService; // Adicionado o serviço

    public StudentsController(IValidator<Student> validator, IStudentService studentService)
    {
        _validator = validator;
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        var students = _studentService.GetAllStudents();
        return Ok(students);
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _studentService.GetStudentById(id);
        if (student == null)
        {
            return NotFound($"Student with ID {id} not found.");
        }
        return Ok(student);
    }


    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> PostStudent([FromBody] Student student)
    {
        var validationResult = await _validator.ValidateAsync(student);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        _studentService.AddStudent(student);
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT: api/students/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> PutStudent(int id, [FromBody] Student student)
    {
        var validationResult = await _validator.ValidateAsync(student);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        try
        {
            _studentService.UpdateStudent(student);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteStudent(int id)
    {
        try
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
