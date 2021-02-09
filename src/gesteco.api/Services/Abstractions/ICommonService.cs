namespace gesteco.api.src.gesteco.WebApi.Domain.Forms {

    /// <summary>
    /// Permet d'encapsuler tous les interfaces
    /// </summary>
    public interface ICommonService {

        IQuotaRepository QuotaRepository { get;  }
        ITarificationRepository TarificationRepository { get; }
        IVisiteRepository VisiteRepository { get;  }
        IMatiereRepository MatiereRepository { get; }
        IModePaiementRepository ModePaiementRepository { get; }
        IEcocentreRepository EcocentreRepository { get; }

    }
}
