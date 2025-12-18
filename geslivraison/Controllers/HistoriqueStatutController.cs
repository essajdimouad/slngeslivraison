using geslivraison.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace geslivraison.Controllers
{
    public class HistoriqueStatutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoriqueStatutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------
        // LISTE
        // ---------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var historiques = await _context.HistoriquesStatuts
                .Include(h => h.DemandeLivraison)
                .ToListAsync();

            return View(historiques);
        }

        // ---------------------------------------------------------
        // DETAILS
        // ---------------------------------------------------------
        public async Task<IActionResult> Details(int id)
        {
            var historique = await _context.HistoriquesStatuts
                .Include(h => h.DemandeLivraison)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historique == null) return NotFound();

            return View(historique);
        }

        // ---------------------------------------------------------
        // GET: CREATE
        // ---------------------------------------------------------
        public IActionResult Create()
        {
            ViewBag.Demandes = new SelectList(
                _context.DemandesLivraison,
                "Id",
                "AdresseDepart"
            );

            return View();
        }

        // ---------------------------------------------------------
        // POST: CREATE
        // ---------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(HistoriqueStatut historique)
        {
            if (ModelState.IsValid)
            {
                historique.DateChangement = DateTime.Now;

                _context.HistoriquesStatuts.Add(historique);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Demandes = new SelectList(
                _context.DemandesLivraison,
                "Id",
                "AdresseDepart",
                historique.DemandeLivraisonId
            );

            return View(historique);
        }

        // ---------------------------------------------------------
        // DELETE GET
        // ---------------------------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var historique = await _context.HistoriquesStatuts
                .Include(h => h.DemandeLivraison)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (historique == null) return NotFound();

            return View(historique);
        }

        // ---------------------------------------------------------
        // DELETE POST
        // ---------------------------------------------------------
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historique = await _context.HistoriquesStatuts.FindAsync(id);

            if (historique != null)
            {
                _context.HistoriquesStatuts.Remove(historique);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
