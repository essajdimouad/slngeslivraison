namespace geslivraison.Models
{
    public class Administrateur
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // 🔐 POUR L’AUTHENTIFICATION
        public string MotDePasse { get; set; } = string.Empty;

        // 🔗 L’administrateur gère plusieurs demandes
        public ICollection<DemandeLivraison> Demandes { get; set; }
            = new List<DemandeLivraison>();

        // 🔥 Logique métier issue de l’UML
        public void AssignerDemande(DemandeLivraison demande)
        {
            demande.AdminId = this.Id;
            demande.Administrateur = this;
        }

        public void ChangerStatut(DemandeLivraison demande, StatutDemande nouveauStatut)
        {
            demande.ChangerStatut(nouveauStatut);
        }
    }
}
