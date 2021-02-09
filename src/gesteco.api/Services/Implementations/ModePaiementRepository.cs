using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.DataLayer.Implementations;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;

namespace gesteco.api.src.gesteco.WebApi.Services {
    public class ModePaiementRepository : RepositoryBase<ModePaiement>, IModePaiementRepository {

        private readonly GestecoContext _context;

        public ModePaiementRepository(GestecoContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
