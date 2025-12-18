CREATE TABLE HistoriqueStatut (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateChangement DATETIME NOT NULL DEFAULT GETDATE(),
    AncienStatut NVARCHAR(50) NOT NULL,
    NouveauStatut NVARCHAR(50) NOT NULL,

    DemandeId INT NOT NULL,

    CONSTRAINT FK_Historique_Demande FOREIGN KEY (DemandeId)
        REFERENCES DemandeLivraison(Id)
);
