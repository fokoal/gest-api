using System;
using System.Collections.Generic;
using System.Linq;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Implementations;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace gesteco.api.src.gesteco.WebApi.Services {
    public class QuotaRepository : RepositoryBase<Quota>,IQuotaRepository {

        private readonly GestecoContext _context;

        public QuotaRepository(GestecoContext context)
            :base(context)
        {
            _context = context;
        }

        public Quota_Standard CreationQuota(Quota_Standard quota)
        {

            try
            {
                /// Verifit si un Quota par defaut existe deja 
                if (_context.Quota_Standard.Any())
                {
                    var _quota = _context.Quota_Standard.First();
                    _quota.Quantite = quota.Quantite;
                    _quota. Quantite_Commerce = quota. Quantite_Commerce;
                    _context.SaveChanges();
                    return _quota;
                }

                _context.Quota_Standard.Add(quota);
                _context.SaveChanges();
                return quota;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Quota_Standard GetCurentQuota()
        {
          return _context.Quota_Standard.FirstOrDefault();
        }

        public Quota GetCurrent(string IdCivique)
        {
            return _context.Quota.Include(p=>p.Adresse)
                .FirstOrDefault(q => q.IdCivique == IdCivique);
        }

        public IEnumerable<Historique_Quota> GetHistoriqueQuota(QuotaCriteria historique)
        {
            DateTime deteDebut = new DateTime(historique.DateDebut.Value.Year, historique.DateDebut.Value.Month, historique.DateDebut.Value.Day, 0, 0, 0);
            DateTime deteFin = new DateTime(historique.DateFin.Value.Year, historique.DateFin.Value.Month, historique.DateFin.Value.Day, 23, 59, 59);

            var Data = _context.Historique_Quota.Include(p => p.Quota).ThenInclude(s=>s.Adresse)
                .Where(h => h.DateHistorique >= deteDebut && h.DateHistorique <= deteFin).OrderByDescending(s => s.DateHistorique).ToList();


            
            if (!string.IsNullOrWhiteSpace(historique.IdCivique))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.IdCivique) && x.IdCivique.ToLower().Contains(historique.IdCivique.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(historique.Adresse))
            {
                Data = Data.Where(x => !string.IsNullOrEmpty(x.Quota.Adresse.Nom) && x.Quota.Adresse.Nom.ToLower().Contains(historique.Adresse.ToLower())).ToList();
            }

            return Data.Take(historique.DefaultNumber);
        }
    }
}
