using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using Microsoft.AspNetCore.Mvc;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

    public HomeController(ILogger<HomeController> logger, ICT272_Assignment_3_Online_Tourism_PlatformContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var featuredTours = _context.FeaturedGuidedTours
            .Include(f => f.GuidedTour)
            .ToList();

        return View(featuredTours);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    
    // GET: /Home/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    // POST: /Home/Register
    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            user.Password = GetMd5Hash(user.Password);
            _context.User.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    
    private string GetMd5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }

    
    public IActionResult Login()
    {
        return View();
    }
    
    // POST: /Home/Login
    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        var hashedPassword = GetMd5Hash(user.Password);
        var existingUser = _context.User.FirstOrDefault(u => u.Email == user.Email && u.Password == hashedPassword);
        if (existingUser != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, existingUser.Role), // Add the role claim
                new Claim("UserId", existingUser.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }
    
    // GET: /Home/Logout
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}