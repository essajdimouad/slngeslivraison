using Microsoft.EntityFrameworkCore;

namespace geslivraison.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<DemandeLivraison> DemandesLivraison { get; set; }
        public DbSet<HistoriqueStatut> HistoriquesStatuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------------------
            // 🔗 Relation Client (1) → (n) Demandes
            // -------------------------------------
            modelBuilder.Entity<DemandeLivraison>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Demandes)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------------------------------------
            // 🔗 Relation Admin (1) → (n) Demandes
            // Admin est nullable
            // -------------------------------------
            modelBuilder.Entity<DemandeLivraison>()
                .HasOne(d => d.Administrateur)
                .WithMany(a => a.Demandes)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------------------------------------
            // 🔗 Relation Historique (n) → (1) Demande
            // -------------------------------------
            modelBuilder.Entity<HistoriqueStatut>()
                .HasOne(h => h.DemandeLivraison)
                .WithMany(d => d.Historiques)
                .HasForeignKey(h => h.DemandeLivraisonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
