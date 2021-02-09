using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;

namespace gesteco.api.src.gesteco.WebApi.Services {
    public class CommonService : ICommonService {

        private readonly GestecoContext _context;

        private IQuotaRepository _QuotaRepository;
        private ITarificationRepository _TarificationRepository;
        private IVisiteRepository _VisiteRepository;
        private IMatiereRepository _MatiereRepository;
        private IModePaiementRepository _ModePaiementRepository;
        private IEcocentreRepository _EcocentreRepository;

        public CommonService(GestecoContext context)
        {
            _context = context;
        }
        public IQuotaRepository QuotaRepository
        {

            get
            {
                if (_QuotaRepository == null)
                {
                    _QuotaRepository = new QuotaRepository(_context);
                }
                return _QuotaRepository;
            }
        }
        public ITarificationRepository TarificationRepository
        {

            get
            {
                if (_TarificationRepository == null)
                {
                    _TarificationRepository = new TarificationRepository(_context);
                }
                return _TarificationRepository;
            }
        }

        public IVisiteRepository VisiteRepository
        {

            get
            {
                if (_VisiteRepository == null)
                {
                    _VisiteRepository = new VisiteRepository(_context);
                }
                return _VisiteRepository;
            }
        }

        public IMatiereRepository MatiereRepository
        {

            get
            {
                if (_MatiereRepository == null)
                {
                    _MatiereRepository = new MatiereRepository(_context);
                }
                return _MatiereRepository;
            }
        }

        public IModePaiementRepository ModePaiementRepository
        {

            get
            {
                if (_ModePaiementRepository == null)
                {
                    _ModePaiementRepository = new ModePaiementRepository(_context);
                }
                return _ModePaiementRepository;
            }
        }
        public IEcocentreRepository EcocentreRepository
        {

            get
            {
                if (_EcocentreRepository == null)
                {
                    _EcocentreRepository= new EcocentreRepository(_context);
                }
                return _EcocentreRepository;
            }
        }
    }
}
