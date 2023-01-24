#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Comment
{
    [Key]
    public int CommentId {get;set;}

    [Required(ErrorMessage ="Comment is Required")]
    public string CommentText {get;set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int UserId {get;set;}
    public int MessageId {get;set;}
    
    public Message? Message {get;set;}
    public User? User {get;set;}


}