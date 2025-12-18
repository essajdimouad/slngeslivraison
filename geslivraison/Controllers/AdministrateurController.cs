using geslivraison.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace geslivraison.Controllers
{
    public class AdministrateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministrateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =================================================
        // CRUD ADMINISTRATEUR
        // =================================================

        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrateurs.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Administrateur admin)
        {
            if (ModelState.IsValid)
            {
                _context.Administrateurs.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Administrateur admin)
        {
            if (ModelState.IsValid)
            {
                _context.Update(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin != null)
            {
                _context.Administrateurs.Remove(admin);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // =================================================
        // GESTION DES DEMANDES DE LIVRAISON
        // =================================================

        public async Task<IActionResult> GererDemandes()
        {
            var demandes = await _context.DemandesLivraison
                .Include(d => d.Client)
                .Include(d => d.Administrateur)
                .ToListAsync();

            ViewBag.Admins = await _context.Administrateurs.ToListAsync();

            return View(demandes);
        }

        public async Task<IActionResult> Assigner(int demandeId, int adminId)
        {
            var demande = await _context.DemandesLivraison.FindAsync(demandeId);
            if (demande == null) return NotFound();

            demande.AdminId = adminId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GererDemandes));
        }

        [HttpPost]
        public async Task<IActionResult> ChangerStatut(int demandeId, StatutDemande nouveauStatut)
        {
            var demande = await _context.DemandesLivraison
                .Include(d => d.Historiques)
                .FirstOrDefaultAsync(d => d.Id == demandeId);

            if (demande == null) return NotFound();

            demande.ChangerStatut(nouveauStatut);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GererDemandes));
        }

        // =================================================
        // GESTION DES UTILISATEURS (CLIENTS)
        // =================================================

        public async Task<IActionResult> GererUtilisateurs()
        {
            return View(await _context.Clients.ToListAsync());
        }
    }
}
