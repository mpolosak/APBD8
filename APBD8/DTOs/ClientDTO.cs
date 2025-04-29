using System.ComponentModel.DataAnnotations;

namespace APBD8.Models.DTOs;
public class ClientDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string? Telephone { get; set; }
    [MinLength(11)]
    [RegularExpression(@"\d{11}")]
    public string? PESEL { get; set; }
}