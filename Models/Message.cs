#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Message
{
    [Key]
    public int MessageId {get;set;}

    [Required]
    [Display(Name="Message")]
    public string MessageText {get;set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // One to Many
    public User? User {get;set;}

    // Many to Many
    public int UserId {get;set;}
    public List<Comment> UserComments {get;set;} = new List<Comment>();

}
