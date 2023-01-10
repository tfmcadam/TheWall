using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // Message Page
    [SessionCheck]
    [HttpGet("messages")]
    public IActionResult Messages()
    {
        MessageModel MyModel = new MessageModel
        {
            // all messages with their associated comments
            AllMessages = _context.Messages.Include(Mes => Mes.UserComments).ToList(),

            // a logged user info
            User = _context.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"))
        };
        return View("Messages", MyModel);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

