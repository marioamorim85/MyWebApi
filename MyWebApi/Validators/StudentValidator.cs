using FluentValidation;
using MyWebApi.Models;

namespace MyWebApi.Validators
{
    public sealed class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            // Validação do nome
            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
                .MaximumLength(100).WithMessage("Name can have at most 100 characters.");

            // Validação da idade
            RuleFor(student => student.Age)
                .InclusiveBetween(18, 40).WithMessage("Age must be between 18 and 40.");

            // Validação do email
            RuleFor(student => student.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");
        }
    }
}

