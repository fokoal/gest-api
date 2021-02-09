using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gesteco.api.Tests {
    public  class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<GestecoContext>));

                services.Remove(descriptor);

                services.AddDbContext<GestecoContext>(options =>
                {
                   options.UseInMemoryDatabase("InMemoryDbForTesting_" );
                   options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
                
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<GestecoContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {

                        if (!db.Quota_Standard.Any())
                        {
                            var quota_Standard = new Quota_Standard
                            {
                                Quantite = 20,
                                Quantite_Commerce = 20
                            };
                            db.Quota_Standard.Add(quota_Standard);
                            db.SaveChanges();
                        }

                        /// verifit s"il deja des donnee dans la table tarification
                        if (!db.Tarification.Any())
                        {
                            var tarification = new Tarification
                            {
                                Prix = 11,
                                Prix_Commerce = 32
                            };
                            db.Tarification.Add(tarification);
                            db.SaveChanges();
                        }

                        /// verifit s"il deja des donnee dans la table ModePaiement
                        if (!db.ModePaiement.Any())
                        {
                            var modepaiement = new List<ModePaiement>
                            {
                                new  ModePaiement  {  Nom = "Comptant" },
                                new  ModePaiement  {  Nom = "Debit" },
                                new  ModePaiement  {  Nom = "Credit" },
                                new  ModePaiement  {  Nom = "Autres" },
                            };
                            db.ModePaiement.AddRange(modepaiement);
                            db.SaveChanges();
                        }

                        /// verifit s"il deja des donnee dans la table ModePaiement
                        if (!db.Historique_Initialisation_Quota.Any())
                        {
                            var quotaInit = new Historique_Initialisation_Quota
                            {
                                DateEncours = true,
                                DateInit = DateTime.Now,
                                Description = "Initialisation de depart"
                            };

                            db.Historique_Initialisation_Quota.Add(quotaInit);
                            db.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

          
        
        
    }
}
