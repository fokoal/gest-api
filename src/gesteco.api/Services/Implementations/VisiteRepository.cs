using System;
using System.Collections.Generic;
using System.Linq;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Implementations;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace gesteco.api.src.gesteco.WebApi.Services {

    public class VisiteRepository : RepositoryBase<Visite>,IVisiteRepository {
        
        private readonly GestecoContext _context;

        public VisiteRepository(GestecoContext context)
            :base(context)
        {
            _context = context;
        }
        /// <summary>
        ///Le système doit permettre d’enregistrer une visite et de facturer le client
        /// en créant le client s'il n'exite pas
        /// en créant la provenance ,  la transaction et la liste des matieres de la visite 
        /// et la fait la mise a jour des quota 
        /// </summary>
        /// <param name="visite"></param>
        /// <returns></returns>
        public Visite CreateVisite(Visite visite)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /// le client s'il n'exite pas 
                    visite.DateCreation = DateTime.Now;
                    bool Iscommerce = string.IsNullOrEmpty(visite.Client.NomCommerce);
                    if ((visite.IClient == 0) || (!_context.Client.Any(c => c.IdClient == visite.IClient)))
                    {
                        /// Creation du Client et entreprise  
                        visite.Client.IdClient = 0;
                        visite.Client. DateCreation =DateTime.Now;
                        _context.Client.Add(visite.Client);
                        _context.SaveChanges();
                    }
                    else
                    {
                        visite.Client = null;
                    }


                    /// Creation de l'adresse et  Quota 
                    if (!string.IsNullOrEmpty(visite.Provenance.IdCivique))
                    {
                        /// Verifit si l'adresse existe 
                        var adresse = _context.Adresse.FirstOrDefault(a => a.IdCivique == visite.Provenance.IdCivique);

                        if (adresse == null)
                        {
                            var _adresse = new Adresse
                            {
                                IdCivique = visite.Provenance.IdCivique,
                                Nom = visite.Provenance.Adresse
                            };
                            _context.Adresse.Add(_adresse);
                            _context.SaveChanges();

                            /// Quota  
                            var dtdebut = new DateTime(DateTime.Now.Year, 1, 1);
                            var dtdefin = new DateTime(DateTime.Now.Year, 12, 31);

                            var quotaStandard = _context.Quota_Standard.First();

                            var _quota = new Quota
                            {

                                IdCivique = visite.Provenance.IdCivique,
                                DateDebut = dtdebut,
                                DateFin = dtdefin,
                                Quantite_Disponible = quotaStandard.Quantite,
                                Quantite_Initiale = quotaStandard.Quantite,
                                Quantite_Commerce = quotaStandard.Quantite_Commerce
                            };
                            _context.Quota.Add(_quota);
                            _context.SaveChanges();

                            visite.Provenance.Quantite_Disponible =  string.IsNullOrWhiteSpace(visite.Client.NomCommerce)  ?_quota.Quantite_Disponible: _quota.Quantite_Commerce;
                        }
                    }

                    // Creation de la Provenance 
                    /// Verification des Quota 
                    /// Si une adresse officielle n’existe pas dans les bases de données  de la ville
                    visite.Provenance.Quantite_Initiale = visite.Provenance.Quantite_Disponible;

                    if (string.IsNullOrEmpty(visite.Provenance.IdCivique))
                    {
                        /// si le client ne montre pas une preuve de résidence   
                        if (visite.Provenance.Quantite_Disponible > 0)
                        {
                            if (visite.Provenance.Quantite_Disponible > visite.Transaction.Volume)
                            {
                                visite.Provenance.Quantite_Disponible -= (long)visite.Transaction.Volume;
                                visite.Transaction.Quantite_Utilisee = (long)visite.Transaction.Volume;
                                visite.Transaction.Volume = 0;
                            }
                            else
                            {
                                visite.Transaction.Volume -= visite.Provenance.Quantite_Disponible;
                                visite.Provenance.Quantite_Disponible = 0;
                                visite.Transaction.Quantite_Utilisee = (long)visite.Transaction.Volume;
                            }
                        }
                    }
                    else
                    {
                        if (visite.Provenance.Quantite_Disponible > visite.Transaction.Volume)
                        {
                            visite.Provenance.Quantite_Disponible -= (long)visite.Transaction.Volume;
                        }
                        else
                        {
                            visite.Provenance.Quantite_Disponible = 0;
                        }
                    }

                    _context.Provenance.Add(visite.Provenance);
                    _context.SaveChanges();

                    /// Verification des Quota Cas Adressse Officielle
                    if (!string.IsNullOrEmpty(visite.Provenance.IdCivique))
                    {
                        var _quota = _context.Quota.FirstOrDefault(p => p.IdCivique == visite.Provenance.IdCivique);

                        if (_quota != null && _quota.DateFin >= DateTime.Now.Date)
                        {
                            long _qauntiteUtilise = 0;
                            /// Calcul du Volume facturable 

                            if (!string.IsNullOrWhiteSpace(visite.Client.NomCommerce))
                            {
                                if (_quota. Quantite_Commerce > visite.Transaction.Volume)
                                {
                                    _quota.Quantite_Commerce -= (long)visite.Transaction.Volume;
                                    _qauntiteUtilise = (long)visite.Transaction.Volume;
                                    visite.Transaction.Volume = 0;
                                }
                                else
                                {
                                    visite.Transaction.Volume -= _quota.Quantite_Commerce;
                                    _qauntiteUtilise = (long)visite.Transaction.Volume;
                                    _quota.Quantite_Commerce = 0;
                                }
                            }
                            else
                            {
                                if (_quota.Quantite_Disponible > visite.Transaction.Volume)
                                {
                                    _quota.Quantite_Disponible -= (long)visite.Transaction.Volume;
                                    _qauntiteUtilise = (long)visite.Transaction.Volume;
                                    visite.Transaction.Volume = 0;
                                }
                                else
                                {
                                    visite.Transaction.Volume -= _quota.Quantite_Disponible;
                                    _qauntiteUtilise = (long)visite.Transaction.Volume;
                                    _quota.Quantite_Disponible = 0;
                                }
                            }
                            visite.Transaction.Quantite_Utilisee = _qauntiteUtilise;
                            _context.SaveChanges();

                            /// Enregistrement de l'historique des quota
                            var historiqueQuota = new Historique_Quota
                            {
                                DateHistorique = DateTime.Now,
                                DateDebut = _quota.DateDebut,
                                DateFin = _quota.DateFin,
                                IdCivique = _quota.IdCivique,
                                IdQuota = _quota.IdQuota,
                                Quantite_Utilisee = _qauntiteUtilise,
                                Quantite_Initiale = visite.Provenance.Quantite_Initiale

                            };
                            _context.Historique_Quota.Add(historiqueQuota);
                            _context.SaveChanges();
                        }
                    }

                    /// Facturation  Chargement de la tarification 
                    var tarif = _context.Tarification.First();
                    visite.Transaction.Total = Iscommerce ? visite.Transaction.Volume * tarif.Prix : visite.Transaction.Volume * tarif.Prix_Commerce;

                    /// On ne facture pas   lorsqu'il s'agit d'une nouvelle adresse
                    
                    visite.Transaction.Total = string.IsNullOrEmpty(visite.Provenance.IdCivique) ? 0 : visite.Transaction.Total;
                    // Creation de la visite , Transaction et des Matiere de la visite 
                    visite.IClient = visite.Client == null ? visite.IClient : visite.Client.IdClient;
                    visite.IdProvenance = visite.Provenance.IdProvenance;
                    visite.Ecocentre = null;
                    visite.Transaction.ModePaiement = null;

                    _context.Visite.Add(visite);
                    visite.Transaction.IdVisite = visite.IdVisite;
                    _context.Transaction.Add(visite.Transaction);

                    _context.SaveChanges();
                    transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    transaction.RollbackAsync();
                    throw ex;
                }
            }

            return GetVisite(visite.IdVisite);
        }

        public IEnumerable<Visite> GetAll()
        {
            var Data = _context.Visite.Where(v=>v.IdVisite !=-1)
                .ToList();
            return Data;
        }

        public IEnumerable<Visite> GetHistorique(HistoriqueCriteria historique)
        {
            DateTime deteDebut;
            DateTime deteFin;
            int skipPage = (historique.PageNo - 1) * historique.RowsPerPage;
            historique.DefaultNumber = historique.DefaultNumber == 0 ? 5000 : historique.DefaultNumber;

            if((historique.DateDebut.HasValue && historique.DateFin.HasValue))
            {
                  deteDebut = new DateTime(historique.DateDebut.Value.Year, historique.DateDebut.Value.Month, historique.DateDebut.Value.Day, 0, 0, 0);
                  deteFin = new DateTime(historique.DateFin.Value.Year, historique.DateFin.Value.Month, historique.DateFin.Value.Day, 23, 59, 59);
            }
            else
            {
                deteDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                deteFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }

            var Data = _context.Visite.Include(c => c.Client).Include(p => p.Provenance)
               .Include(e => e.Ecocentre)
               .Include(t => t.Transaction).ThenInclude(c => c.ModePaiement)
               .Include(m => m.Matieres).Where(v => v.DateCreation >= deteDebut.Date && v.DateCreation <= deteFin).OrderByDescending(s => s.DateCreation).ToList();


            if (!string.IsNullOrWhiteSpace(historique.Adresse))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Provenance.Adresse) && x.Provenance.Adresse.ToLower().Contains(historique.Adresse.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique. Employe))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Employe) && x.Employe.ToLower().Contains(historique. Employe.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Commerce))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Client.NomCommerce) && x.Client.NomCommerce.ToLower().Contains(historique.Commerce.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Courriel))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Client.Courriel) && x.Client.Courriel.ToLower().Contains(historique.Courriel.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Plaque))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Client.Immaticulation) && x.Client.Immaticulation.ToLower().Contains(historique.Plaque.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Tel))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Client.Telephone) && x.Client.Telephone.ToLower().Contains(historique.Tel.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.ClientNom))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Client.Nom) && x.Client.Nom.ToLower().Contains(historique.ClientNom.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Ecocentre))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Ecocentre.Nom) && x.Ecocentre.Nom.ToLower().Contains(historique.Ecocentre.ToLower())).ToList();
            }

            return Data.OrderByDescending(p=>p. IdVisite).Skip(skipPage).Take(historique.DefaultNumber);
        }


        public Visite GetVisite(long id)
        {
            var Data = _context.Visite
              .Include(c => c.Client).ThenInclude(c => c.Entreprises)
              .Include(p => p.Provenance)
              .Include(e => e.Ecocentre)
              .Include(t => t.Transaction).ThenInclude(c => c.ModePaiement)
              .Include(m => m.Matieres)
              .FirstOrDefault(p => p.IdVisite == id);

            return Data;
        }
    }
}
