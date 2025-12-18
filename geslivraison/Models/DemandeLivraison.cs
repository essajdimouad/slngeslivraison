namespace geslivraison.Models
{
    public class DemandeLivraison
    {
        public int Id { get; set; }

        public string AdresseDepart { get; set; } = string.Empty;
        public string AdresseArrivee { get; set; } = string.Empty;

        // ✅ MANQUANT AVANT
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public StatutDemande Statut { get; set; } = StatutDemande.EnAttente;

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? AdminId { get; set; }
        public Administrateur? Administrateur { get; set; }

        public ICollection<HistoriqueStatut>? Historiques { get; set; }
            = new List<HistoriqueStatut>();

        public void ChangerStatut(StatutDemande nouveauStatut)
        {
            var ancienStatut = this.Statut;
            this.Statut = nouveauStatut;

            Historiques?.Add(new HistoriqueStatut
            {
                DateChangement = DateTime.Now,
                AncienStatut = ancienStatut,
                NouveauStatut = nouveauStatut,
                DemandeLivraisonId = this.Id
            });
        }
    }

    public enum StatutDemande
    {
        EnAttente,
        Acceptee,
        EnCours,
        Livree,
        Annulee
    }
}
