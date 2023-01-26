using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


using TheWall.Models;
namespace TheWall.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View("Index");
    }


    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
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

            // hash the password prior to saving
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

            // Once Hashed Add the user and Save changes
            _context.Add(newUser);
            _context.SaveChanges();

            // Add Session to begin after user added
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Messages", "Message");
        }
        else
        {
            return View("Index");
        }
    }

    // LoginUser
    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser loginUser)
    {
        if (ModelState.IsValid)
        {
            User? UserInDb = _context.Users.FirstOrDefault(user => user.Email == loginUser.LEmail);

            // Verify the user exists
            if (UserInDb == null)
            {
                ModelState.TryAddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(loginUser, UserInDb.Password, loginUser.LPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", UserInDb.UserId);
            return RedirectToAction("Messages", "Message");
        }
        return View("Index");
    }
    [HttpGet("user/{userId}")]
    public IActionResult UserPage(int userId)
    {
        User? OneUser = _context.Users.Include(user => user.Messages)
                        .Include(user => user.Comments)
                        .FirstOrDefault(user =>user.UserId == userId);
        return View("UserPage", OneUser );
    }

    // Edit User page
    [HttpGet("user/{userId}/edit")]
    public IActionResult EditUser(int userId)
    {
        
            User? UserToEdit = _context.Users.FirstOrDefault(u => u.UserId == userId);

        return View(UserToEdit);
    }

// Update the user
    [HttpPost("user/{userId}/update")]
    public IActionResult UpdateUser(int userId, User UpdatedUser)
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
        User? UserToUpdate = _context.Users.FirstOrDefault(u => u.UserId == userId);
        {
            User? UserToEdit = _context.Users.FirstOrDefault(a => a.UserId == userId);
            if (UserToUpdate == null)
            {
                return RedirectToAction("Messages", "Message");
            }
            if (ModelState.IsValid)
            {
                UserToUpdate.FirstName = UpdatedUser.FirstName;
                UserToUpdate.LastName = UpdatedUser.LastName;
                UserToUpdate.Email = UpdatedUser.Email;
                UserToUpdate.Password = UpdatedUser.Password;
                UserToUpdate.UserImg = UpdatedUser.UserImg;
                UserToUpdate.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Messages", "Message", new { userId = userId });
            }
            else
            {
                return View("EditUser", UserToUpdate);
            }

        }
    }

    // Logout
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    // user image trials
    [HttpGet("user/images")]
    public IActionResult Images()
    {
        return View("Images");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? UserId = context.HttpContext.Session.GetInt32("UserId");
        if (UserId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}