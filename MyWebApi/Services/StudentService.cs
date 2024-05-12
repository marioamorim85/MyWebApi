using System.Collections.Generic;
using System.Linq;
using MyWebApi.Models;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;

namespace MyWebApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            
            var existing = _context.Students.Find(student.Id);
            if (existing == null)
                throw new KeyNotFoundException($"No student found with ID {student.Id}");

            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.Email = student.Email;
            _context.Students.Update(existing);
            _context.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                throw new KeyNotFoundException($"No student found with ID {id}");

            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}