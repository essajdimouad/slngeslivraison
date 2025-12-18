namespace geslivraison.Models
{
    public class HistoriqueStatut
    {
        public int Id { get; set; }

        // 🔥 Statuts avant et après
        public StatutDemande AncienStatut { get; set; }
        public StatutDemande NouveauStatut { get; set; }

        public DateTime DateChangement { get; set; }

        // 🔗 Relation vers DemandeLivraison
        public int DemandeLivraisonId { get; set; }
        public DemandeLivraison DemandeLivraison { get; set; }
    }
}
