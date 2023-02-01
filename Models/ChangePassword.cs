#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;


namespace TheWall.Models;

public class ChangePassword
{

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string CurrentPassword {get;set;}

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword {get;set;}

    [Required]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage ="The New Password must match with Confirm Password")]
    public string ConfirmPassword {get;set;}
}

    