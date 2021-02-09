using System.Collections.Generic;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Abstractions;

namespace gesteco.api.src.gesteco.WebApi.Domain.Forms {
    public interface IVisiteRepository:IRepositoryBase<Visite> {

        public IEnumerable<Visite> GetAll();

        /// <summary>
        /// Retourne une liste des visites en fonction des critères 
        /// </summary>
        /// <param name="historique"></param>
        /// <returns></returns>
        public IEnumerable<Visite> GetHistorique(HistoriqueCriteria historique);

        /// <summary>
        /// Retourne une visite grace a l'Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Visite GetVisite(long id);
        /// <summary>
        ///Permet d’enregistrer une visite et de facturer le client
        /// en créant le client s'il n'exite pas
        /// en créant la provenance ,  la transaction et la liste des matieres de la visite 
        /// et la fait la mise a jour des quota 
        /// </summary>
        /// <param name="visite"></param>
        /// <returns></returns>
        public Visite CreateVisite(Visite visite);

    }
}
