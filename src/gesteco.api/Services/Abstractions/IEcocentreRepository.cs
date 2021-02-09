using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Abstractions;

namespace gesteco.api.src.gesteco.WebApi.Domain.Forms {
    public interface IEcocentreRepository : IRepositoryBase<Ecocentre> {
        /// <summary>
        /// Retourne l'ecocentre avec les matieres associes grace a l'Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Ecocentre GetEcocentre(long id);
        /// <summary>
        /// Modifit l'eocentre et les matieres associes 
        /// </summary>
        /// <param name="ecocentre"></param>
        void Modifier(Ecocentre ecocentre);

    }
}
