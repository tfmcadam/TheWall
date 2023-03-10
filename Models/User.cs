#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models;

public class User
{
    [Key]
    public int UserId {get;set;}

    [Required]
    [MinLength(2)]
    public string FirstName {get;set;}
    
    [Required]
    [MinLength(2)]
    public string LastName {get;set;}
    
    [Image]
    public string UserImg {get;set;}
    
    [Required]
    [EmailAddress]
    [EmailExists]
    public string Email {get;set;}

    [DataType(DataType.Password)]
    [Required]
    [MinLength(8)]
    public string Password {get;set;}


    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public List<Comment> Comments {get;set;} = new List<Comment>();
    public List<Message> Messages {get;set;} = new List<Message>();

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Required]
    public string Confirm {get;set;}
}



