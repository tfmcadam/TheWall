using System.ComponentModel.DataAnnotations;

public class ImageAttribute : ValidationAttribute
{
    // Call upon the protected IsValid method
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("words"); 
        }
        
        if (((string)value).Contains(".jpeg") || ((string)value).Contains(".png") || ((string)value).Contains(".jpg"))
        {
            // we return an error message in ValidationResult we want to render    
            return ValidationResult.Success;
        }
        else
        {
            // Otherwise, we were successful and can report our success  
            return new ValidationResult("You must have an Image");
        }
    }
}