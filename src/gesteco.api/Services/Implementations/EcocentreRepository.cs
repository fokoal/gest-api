using System;
using System.Collections.Generic;
using System.Linq;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Implementations;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace gesteco.api.src.gesteco.WebApi.Services {
    public class EcocentreRepository : RepositoryBase<Ecocentre>, IEcocentreRepository {

        private readonly GestecoContext _context;

        public EcocentreRepository(GestecoContext context)
            : base(context)
        {
            _context = context;
        }

        public Ecocentre GetEcocentre(long id)
        {
         
            var  ecocentre = _context.Ecocentre.Include(c => c.Matieres)
                .FirstOrDefault(e => e.IdEcocentre == id);

            return ecocentre;
        }

         

        public void Modifier(Ecocentre ecocentre)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var tmpEcocentre = _context.Ecocentre.Include(c => c.Matieres).First(s => s.IdEcocentre == ecocentre.IdEcocentre);
                    var listmatiere = tmpEcocentre.Matieres.ToList();
                    var listMatiereDistint = ecocentre.Matieres.ToList();

                    listMatiereDistint = listMatiereDistint.Distinct(new MatiereComparer()).ToList();
                   
                    foreach (var _m in listMatiereDistint)
                    {
                        if (listmatiere.FirstOrDefault(t=>t.Description == _m.Description)==null)
                        {
                            _m.IdEcocentre = ecocentre.IdEcocentre;
                            _m.Id = 0;
                            _context.Ecocentre_Matiere.Add(_m);
                        }
                    }

                    tmpEcocentre.Codepostal = ecocentre.Codepostal;
                    tmpEcocentre.Adresse = ecocentre.Adresse;
                    tmpEcocentre.Nom = ecocentre.Nom;
                    tmpEcocentre.Rue = ecocentre.Rue;
                    tmpEcocentre.Ville = ecocentre.Ville;
                    
                    _context.SaveChanges();

                    var listmatiereEncours = listMatiereDistint;
                    
                    var matiereDelete = listmatiere.Where(el2 => !listmatiereEncours.Any(el1 => el1.Description == el2.Description)).ToList();

                    foreach (var _m in matiereDelete)
                    {
                        _context.Ecocentre_Matiere.Remove(_m);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Supprimer les doublons dans une liste matieres 
        /// </summary>
        public class MatiereComparer : IEqualityComparer<Ecocentre_Matiere> {
            public int GetHashCode(Ecocentre_Matiere co)
            {
                if (co == null)
                {
                    return 0;
                }
                return co. Description.GetHashCode();
            }

            public bool Equals(Ecocentre_Matiere x1, Ecocentre_Matiere x2)
            {
                if (object.ReferenceEquals(x1.Description, x2.Description))
                {
                    return true;
                }
                if (object.ReferenceEquals(x1.Description, null) ||
                    object.ReferenceEquals(x2.Description, null))
                {
                    return false;
                }
                return x1. Description == x2.Description;
            }
        }

    }
}
