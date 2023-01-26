#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall;
public class UpdateUser
{

    [MinLength(2)]
    [Display(Name = "First Name")]
    public string UFirstName { get; set; }

    
    [MinLength(2)]
    [Display(Name = "Last Name")]
    public string ULastName { get; set; }


    [Display(Name = "User Image")]
    public string UUserImg { get; set; }

    
    [EmailAddress]
    [Display(Name = "Email")]
    public string UEmail { get; set; }
    
}