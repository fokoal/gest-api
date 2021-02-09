using gesteco.api.Services.Implementations;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace gesteco.api.src.gesteco.WebApi.Database.Data {
    public class GestecoContext : DbContext
    {
        public GestecoContext(DbContextOptions<GestecoContext> options, AzureSqlDatabaseTokenProvider azureSqlDatabaseTokenProvider)
         : base(options)
        {
            var providerName = Database.ProviderName;
            if (!providerName.Equals("Microsoft.EntityFrameworkCore.InMemory")) {
                azureSqlDatabaseTokenProvider.AddAccessTokenIfNotLocal(Database.GetDbConnection()); 
            }
          
        }

        public DbSet<Quota_Standard> Quota_Standard { get; set; }
        public DbSet<Adresse> Adresse { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Ecocentre> Ecocentre { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }
        public DbSet<Historique_Quota> Historique_Quota { get; set; }
        public DbSet<Matiere> Matiere { get; set; }
        public DbSet<Matiere_Visite> Matiere_Visite { get; set; }
        public DbSet<ModePaiement> ModePaiement { get; set; }
        public DbSet<Provenance> Provenance { get; set; }
        public DbSet<Quota> Quota { get; set; }
        public DbSet<Tarification> Tarification { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Visite> Visite { get; set; }
        public DbSet<Ecocentre_Matiere> Ecocentre_Matiere { get; set; }
        public DbSet<Historique_Initialisation_Quota> Historique_Initialisation_Quota { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Historique_Quota>()
           .HasOne(p => p.Quota)
           .WithMany(b => b.Historiques)
           .HasForeignKey(p => p.IdQuota);

            builder.Entity<Matiere_Visite>()
           .HasOne(p => p.Visite)
           .WithMany(b => b.Matieres)
           .HasForeignKey(p => p.IdVisite);

            builder.Entity<Visite>()
           .HasOne(p => p.Provenance)
           .WithMany(b => b.Visites)
           .HasForeignKey(p => p.IdProvenance);

            builder.Entity<Visite>()
           .HasOne(p => p.Ecocentre)
           .WithMany(b => b.Visites)
           .HasForeignKey(p => p.IdEcocentre);

            builder.Entity<Visite>()
           .HasOne(p => p.Client)
           .WithMany(b => b.Visites)
           .HasForeignKey(p => p.IClient);

            builder.Entity<Entreprise>()
           .HasOne(p => p.Client)
           .WithMany(b => b.Entreprises)
           .HasForeignKey(p => p.IdClient);

            builder.Entity<Transaction>()
           .HasOne(p => p.ModePaiement)
           .WithMany(b => b.Transactions)
           .HasForeignKey(p => p.IdModePaiement);


            builder.Entity<Visite>()
           .HasOne(p => p.Transaction)
           .WithOne(b => b.Visite)
           .HasForeignKey<Transaction>(b => b.IdVisite);

            builder.Entity<Ecocentre_Matiere>()
           .HasOne(p => p.Ecocentre)
           .WithMany(b => b.Matieres)
           .HasForeignKey(p => p.IdEcocentre);


        }

    }
}
