#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Email is Required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if (_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email Must be Unique");
        }
        return ValidationResult.Success;
    }
}