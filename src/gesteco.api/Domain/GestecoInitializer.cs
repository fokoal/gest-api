using System;
using System.Collections.Generic;
using System.Linq;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;

namespace gesteco.api.Domain
{
    public class GestecoInitializer
    {
        public static void Seed(GestecoContext context)
        {
            /// verifit s"il deja des donnee dans la table quota standard
            if (!context.Quota_Standard.Any())
            {
                var quota_Standard = new Quota_Standard
                {
                    Quantite = 20,
                    Quantite_Commerce = 20
                };
                context.Quota_Standard.Add(quota_Standard);
                context.SaveChanges();
            }

            /// verifit s"il deja des donnee dans la table tarification
            if (!context.Tarification.Any())
            {
                var tarification = new Tarification
                {
                    Prix = 11,
                    Prix_Commerce = 32
                };
                context.Tarification.Add(tarification);
                context.SaveChanges();
            }

            /// verifit s"il deja des donnee dans la table ModePaiement
            if (!context.ModePaiement.Any())
            {
                var modepaiement = new List<ModePaiement>
                {
                    new  ModePaiement  {  Nom = "Comptant" },
                    new  ModePaiement  {  Nom = "Debit" },
                    new  ModePaiement  {  Nom = "Credit" },
                    new  ModePaiement  {  Nom = "Autres" },
                };
                context.ModePaiement.AddRange(modepaiement);
                context.SaveChanges();
            }

            /// verifit s"il deja des donnee dans la table ModePaiement
            if (!context. Historique_Initialisation_Quota.Any())
            {
                var quotaInit = new Historique_Initialisation_Quota
                {
                    DateEncours = true,
                    DateInit = DateTime.Now,
                    Description =  "Initialisation de depart"
                };

                context.Historique_Initialisation_Quota.Add(quotaInit);
                context.SaveChanges();
            }
        }
    }
}
