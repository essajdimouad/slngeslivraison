using geslivraison.Models;
using geslivraison.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace geslivraison.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // 🔎 Admin
            var admin = await _context.Administrateurs
                .FirstOrDefaultAsync(a =>
                    a.Email == model.Email &&
                    a.MotDePasse == model.MotDePasse);

            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", admin.Email);
                return RedirectToAction("Index", "Administrateur");
            }

            // 🔎 Client
            var client = await _context.Clients
                .FirstOrDefaultAsync(c =>
                    c.Email == model.Email &&
                    c.MotDePasse == model.MotDePasse);

            if (client != null)
            {
                HttpContext.Session.SetString("Client", client.Email);
                return RedirectToAction("Index", "Client");
            }

            ViewBag.Error = "Email ou mot de passe incorrect";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
