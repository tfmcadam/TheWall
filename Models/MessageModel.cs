#pragma warning disable CS8618

namespace TheWall.Models;

public class MessageModel
{
    public User? User {get;set;}
    public Message? Message {get;set;}
    public LogUser? LogUser {get;set;}

    public List<Message> AllMessages {get;set;}
    public List<Comment> UserComments {get;set;}

    public Comment Comment {get;set;}
}