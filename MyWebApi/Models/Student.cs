using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name must be under 100 characters.")]
    public string Name { get; set; } = string.Empty;  // Assegura que o valor padrão é uma string vazia, evitando nulos

    [Required(ErrorMessage = "Age is a required field.")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int Age { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Email must be under 100 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;  // Assegura que o valor padrão é uma string vazia, evitando nulos
}

