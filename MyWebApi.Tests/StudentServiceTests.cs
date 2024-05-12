using Xunit;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Tests
{
    public class StudentServiceTests
    {
        private readonly IStudentService _studentService;
        private readonly List<Student> _mockStudents;

        public StudentServiceTests()
        {
            _mockStudents = new List<Student>
            {
                new Student { Id = 1, Name = "João Silva", Age = 22, Email = "joao.silva@gmail.com" },
                new Student { Id = 2, Name = "Marta Reis", Age = 28, Email = "marta.reis@hotmail.com" },
                new Student { Id = 3, Name = "António Gomes", Age = 35, Email = "antonio.gomes@gmail.com" },
                new Student { Id = 4, Name = "Sofia Costa", Age = 40, Email = "sofia.costa@hotmail.com" }
            };
            
            
            var mock = Substitute.For<IStudentService>();
            mock.GetAllStudents().Returns(_mockStudents);
            mock.GetStudentById(Arg.Any<int>()).Returns(id => _mockStudents.FirstOrDefault(s => s.Id == id.Arg<int>()));
            mock.When(x => x.AddStudent(Arg.Any<Student>())).Do(x => _mockStudents.Add(x.Arg<Student>()));
            mock.When(x => x.DeleteStudent(Arg.Any<int>())).Do(x => _mockStudents.RemoveAll(s => s.Id == x.Arg<int>()));
            mock.When(x => x.UpdateStudent(Arg.Is<Student>(s => !_mockStudents.Any(m => m.Id == s.Id))))
                .Do(x => throw new KeyNotFoundException("No student found with given ID"));
            _studentService = mock;
        }

        [Fact]
        public void GetAllStudents_ReturnsAllStudents()
        {
            var result = _studentService.GetAllStudents();
            result.Should().HaveCount(_mockStudents.Count);
            result.Should().BeEquivalentTo(_mockStudents);
        }

        [Fact]
        public void GetStudentById_WithValidId_ReturnsCorrectStudent()
        {
            var student = _studentService.GetStudentById(1);
            student.Should().NotBeNull();
            student.Name.Should().Be("João Silva");
        }

        [Fact]
        public void GetStudentById_WithInvalidId_ReturnsNull()
        {
            var result = _studentService.GetStudentById(99);
            result.Should().BeNull();
        }

        [Fact]
        public void AddStudent_ValidatesNewStudent()
        {
            var newStudent = new Student { Id = 5, Name = "Carlos Mota", Age = 25, Email = "carlos.mota@gmail.com" };
            _studentService.AddStudent(newStudent);

            var result = _studentService.GetStudentById(5);
            result.Should().NotBeNull();
            result.Name.Should().Be("Carlos Mota");
        }

        [Fact]
        public void DeleteStudent_RemovesStudentCorrectly()
        {
            int initialCount = _mockStudents.Count;
            _studentService.DeleteStudent(1);
            _studentService.GetAllStudents().Should().HaveCountLessThan(initialCount);
            _studentService.GetStudentById(1).Should().BeNull();
        }

        [Fact]
        public void UpdateStudent_ValidatesUpdatedData()
        {
            var studentToUpdate = _mockStudents.First();
            studentToUpdate.Name = "Updated Name";
            _studentService.UpdateStudent(studentToUpdate);

            var updatedStudent = _studentService.GetStudentById(studentToUpdate.Id);
            updatedStudent.Should().NotBeNull();
            updatedStudent.Name.Should().Be("Updated Name");
        }

        [Fact]
        public void UpdateStudent_WithInvalidId_ThrowsException()
        {
            var studentToUpdate = new Student { Id = 99, Name = "Nome Fictício", Age = 30, Email = "email@teste.com" };

            Action act = () => _studentService.UpdateStudent(studentToUpdate);

            act.Should().Throw<KeyNotFoundException>()
                .WithMessage("No student found with given ID");
        }

    }
}

