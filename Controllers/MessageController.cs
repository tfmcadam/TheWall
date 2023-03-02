using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

using TheWall.Models;
namespace TheWall.Controllers;

public class MessageController : Controller
{
    private readonly ILogger<MessageController> _logger;

    private MyContext _context;

    public MessageController(ILogger<MessageController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

// Messages Page
    [SessionCheck]
    [HttpGet("messages")]
    public IActionResult Messages()
    {
        MessageModel MyModel = new MessageModel
        {
            // all messages with their associated comments
            AllMessages = _context.Messages.Include(Mes => Mes.UserComments).Include(u => u.User).OrderByDescending(m => m.CreatedAt).ToList(),

            // a logged user info
            User = _context.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId")),

            UserComments =_context.Comments.Include(m => m.Message).Include(m => m.User).ToList()

            
        };
        return View("Messages", MyModel);
    }

    
// Create Message route
    [SessionCheck]
    [HttpPost("messages/create")]
    public IActionResult CreateMessage(Message newMessage)
    {
        foreach (KeyValuePair<string, ModelStateEntry> error in ModelState)
        {
            Console.WriteLine("********** ERROR ********");
            Console.WriteLine($"Field: {error.Key}");
            foreach (ModelError err in error.Value.Errors)
            {
                Console.WriteLine($"Error: {err.ErrorMessage}");
            }
        }
        if (ModelState.IsValid)
        {
            newMessage.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newMessage);
            _context.SaveChanges();
            return RedirectToAction("Messages");
        }
        return Messages();
    }

// Delete Message
    [SessionCheck]
    [HttpPost("messages/{messageId}/destroy")]
    public IActionResult DestroyMessage(int messageId)
    {
        Message? MessageToDestroy = _context.Messages.SingleOrDefault(m => m.MessageId == messageId);
        _context.Remove(MessageToDestroy);
        _context.SaveChanges();
        return Messages();
    }

// Add a comment
    [SessionCheck]
    [HttpPost("comments/create")]
    public IActionResult NewComment(Comment newComment)
    {
        if(ModelState.IsValid)
        {
            newComment.UserId =(int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("Messages");
        }
        return Messages();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

