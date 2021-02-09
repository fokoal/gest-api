using System.Collections.Generic;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Abstractions;

namespace gesteco.api.src.gesteco.WebApi.Domain.Forms {
    public  interface IQuotaRepository : IRepositoryBase<Quota> {

        /// <summary>
        /// Permet d'obtenir le quota d'une adresse  grace à  l'idcivique
        /// </summary>
        /// <param name="IdCivique"></param>
        /// <returns></returns>
        Quota GetCurrent(string IdCivique);
        /// <summary>
        /// Permet d'obtenir une liste d'historique des  quota selon certains critères   
        /// </summary>
        /// <param name="historique"></param>
        /// <returns></returns>
        public IEnumerable<Historique_Quota> GetHistoriqueQuota(QuotaCriteria historique);
        /// <summary>
        /// Permet de creer un quota par defaut 
        /// </summary>
        /// <param name="quota"></param>
        /// <returns></returns>
        Quota_Standard CreationQuota(Quota_Standard quota);
        /// <summary>
        /// Obtient le quota en cours 
        /// </summary>
        /// <returns></returns>
        Quota_Standard GetCurentQuota();

    }
}
