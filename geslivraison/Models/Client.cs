namespace geslivraison.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;

        // ?? POUR AUTHENTIFICATION CLIENT
        public string MotDePasse { get; set; } = string.Empty;

        // ?? Un client peut soumettre plusieurs demandes
        public ICollection<DemandeLivraison> Demandes { get; set; }
            = new List<DemandeLivraison>();
    }
}
