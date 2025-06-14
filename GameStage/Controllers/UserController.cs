using System.Linq;
using System.Security.Claims;
using BCrypt.Net;
using GameStage.Data;
using GameStage.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStage.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles ="Developer")]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult AccessDenied()
        {

            return View();
        }

        [AllowAnonymous]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task <IActionResult> Login(User user)
        {
            var dados = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (dados == null)
            {
                ViewBag.Message = "usuario e/ou senha invalidos";
                return View();
            }

            bool senhaVerificada = BCrypt.Net.BCrypt.Verify(user.PasswordHash, dados.PasswordHash);

            if (senhaVerificada)
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dados.Username),
                    new Claim(ClaimTypes.NameIdentifier, dados.Id.ToString()),
                    new Claim(ClaimTypes.Role, dados.Role.ToString())
                };

                var userIdentity = new ClaimsIdentity(Claims, "Login");
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);

                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                    IsPersistent = true
                };

               await HttpContext.SignInAsync(userPrincipal, props);

                return Redirect("/");
            }
            else 
            {
                ViewBag.Messege = "usuario e/ou senha invalidos";
                return View();
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return View("Login");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public  IActionResult Create(User user)
        {
            if (user == null)
            {
                return NotFound();
            }

            bool existeEmail = _context.Users
                .Any(u=> u.Email == user.Email);

            if (existeEmail) 
            {
                return BadRequest("Este email já está cadastrado.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
