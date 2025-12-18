using geslivraison.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace slngeslivraison.Controllers
{
    public class DemandeLivraisonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemandeLivraisonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------
        // LIST
        // ---------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var demandes = await _context.DemandesLivraison
                .Include(d => d.Client)
                .Include(d => d.Administrateur)
                .ToListAsync();

            return View(demandes);
        }

        // ---------------------------------------------------------
        // DETAILS
        // ---------------------------------------------------------
        public async Task<IActionResult> Details(int id)
        {
            var demande = await _context.DemandesLivraison
                .Include(d => d.Client)
                .Include(d => d.Administrateur)
                .Include(d => d.Historiques)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (demande == null) return NotFound();

            return View(demande);
        }

        // ---------------------------------------------------------
        // CREATE GET
        // ---------------------------------------------------------
        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");
            ViewBag.Admins = new SelectList(_context.Administrateurs, "Id", "Nom");
            ViewBag.Statuts = new SelectList(Enum.GetValues(typeof(StatutDemande)));

            return View();
        }

        // ---------------------------------------------------------
        // CREATE POST
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(DemandeLivraison demande)
        {
            if (ModelState.IsValid)
            {
                // Ajout première entrée historique
                demande.Historiques.Add(new HistoriqueStatut
                {
                    DateChangement = DateTime.Now,
                    AncienStatut = StatutDemande.EnAttente,
                    NouveauStatut = demande.Statut,
                    DemandeLivraisonId = demande.Id
                });

                _context.DemandesLivraison.Add(demande);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom", demande.ClientId);
            ViewBag.Admins = new SelectList(_context.Administrateurs, "Id", "Nom", demande.AdminId);
            ViewBag.Statuts = new SelectList(Enum.GetValues(typeof(StatutDemande)), demande.Statut);

            return View(demande);
        }

        // ---------------------------------------------------------
        // EDIT GET
        // ---------------------------------------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var demande = await _context.DemandesLivraison.FindAsync(id);
            if (demande == null) return NotFound();

            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom", demande.ClientId);
            ViewBag.Admins = new SelectList(_context.Administrateurs, "Id", "Nom", demande.AdminId);
            ViewBag.Statuts = new SelectList(Enum.GetValues(typeof(StatutDemande)), demande.Statut);

            return View(demande);
        }

        // ---------------------------------------------------------
        // EDIT POST (avec historique auto)
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Edit(DemandeLivraison demande)
        {
            if (!ModelState.IsValid)
                return View(demande);

            var existing = await _context.DemandesLivraison
                .Include(d => d.Historiques)
                .FirstOrDefaultAsync(d => d.Id == demande.Id);

            if (existing == null) return NotFound();

            // détecter changement de statut
            if (existing.Statut != demande.Statut)
            {
                existing.ChangerStatut(demande.Statut);
            }

            // mettre à jour champs simples
            existing.AdresseDepart = demande.AdresseDepart;
            existing.AdresseArrivee = demande.AdresseArrivee;
            existing.ClientId = demande.ClientId;
            existing.AdminId = demande.AdminId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ---------------------------------------------------------
        // DELETE GET
        // ---------------------------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var demande = await _context.DemandesLivraison
                .Include(d => d.Client)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (demande == null) return NotFound();

            return View(demande);
        }

        // ---------------------------------------------------------
        // DELETE POST
        // ---------------------------------------------------------
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demande = await _context.DemandesLivraison.FindAsync(id);

            if (demande != null)
            {
                _context.DemandesLivraison.Remove(demande);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ---------------------------------------------------------
        // ACTION SPÉCIALE : CHANGER STATUT
        // ---------------------------------------------------------
        public async Task<IActionResult> ChangerStatut(int id, StatutDemande nouveauStatut)
        {
            var demande = await _context.DemandesLivraison
                .Include(d => d.Historiques)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (demande == null) return NotFound();

            demande.ChangerStatut(nouveauStatut);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
