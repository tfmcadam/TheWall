#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models;

public class EmailExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var owner = validationContext.ObjectInstance as User;
        if (owner == null) return new ValidationResult("Model is empty");
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        var user = _context.Users.FirstOrDefault(u => u.Email == (string)value && u.UserId != owner.UserId);

        if (user == null)
            return ValidationResult.Success;
        else
            return new ValidationResult("Mail already exists");
    }
}