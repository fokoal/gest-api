using AutoMapper;
using gesteco.api.OutputModels;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.OutputModels;

namespace gesteco.api.src.gesteco.WebApi.Profiles {
    public class GestecoProfile : Profile {

        public GestecoProfile()
        {

            CreateMap<Matiere_Visite, Matiere_VisiteDTO>();
            CreateMap<Matiere_VisiteDTO, Matiere_Visite>();

            CreateMap<Quota_StandardDTO, Quota_Standard>();
            CreateMap<Quota_Standard, Quota_StandardDTO>();

            CreateMap<Ecocentre_MatiereDTO, Ecocentre_Matiere>();
            CreateMap<Ecocentre_Matiere, Ecocentre_MatiereDTO>();

            CreateMap<Entreprise, EntrepriseDTO>();
            CreateMap<EntrepriseDTO, Entreprise>();

            CreateMap<Historique_Quota, Historique_QuotaDTO>();

            CreateMap<Visite, VisiteDTO>()
           .ForMember(dt => dt.Provenance, c => c.MapFrom(v => v.Provenance))
           .ForMember(dt => dt.Transaction, c => c.MapFrom(v => v.Transaction))
           .ForMember(dt => dt.Ecocentre, c => c.MapFrom(v => v.Ecocentre))
           .ForMember(dt => dt.Client, c => c.MapFrom(v => v.Client))
           .ForMember(dest => dest.Matieres, opt => opt.MapFrom(src => src.Matieres));


            CreateMap<VisiteDTO, Visite>().ForMember(dt => dt.Matieres, c => c.MapFrom(v => v.Matieres))
                   .ForMember(dt => dt.Provenance, c => c.MapFrom(v => v.Provenance))
                   .ForMember(dt => dt.Transaction, c => c.MapFrom(v => v.Transaction))
                   .ForMember(dt => dt.Ecocentre, c => c.MapFrom(v => v.Ecocentre))
                   .ForMember(dt => dt.Client, c => c.MapFrom(v => v.Client));
                 

            CreateMap<Adresse, AdresseDTO>();
            CreateMap<AdresseDTO, Adresse>();

            CreateMap<Matiere, MatiereDTO>();
            CreateMap<MatiereDTO, Matiere>();

            CreateMap<Tarification, TarificationDTO>();
            CreateMap<TarificationDTO, Tarification>();


            CreateMap<ModePaiement, ModePaiementDTO>().ForMember(dt => dt.Transactions, c => c.MapFrom(v => v.Transactions));
            CreateMap<ModePaiementDTO, ModePaiement>().ForMember(dt => dt. Transactions, c => c.MapFrom(v => v. Transactions));

            CreateMap<Provenance, ProvenanceDTO>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites));
            CreateMap<ProvenanceDTO, Provenance>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites));

            CreateMap<Transaction, TransactionDTO>().ForMember(dt => dt.ModePaiement, c => c.MapFrom(v => v.ModePaiement));
            CreateMap<TransactionDTO, Transaction>().ForMember(dt => dt.ModePaiement, c => c.MapFrom(v => v.ModePaiement));

            CreateMap<Client, ClientDTO>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites))
                 .ForMember(dt => dt. Entreprises, c => c.MapFrom(v => v.Entreprises));

            CreateMap<ClientDTO, Client>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites))
                 .ForMember(dt => dt.Entreprises, c => c.MapFrom(v => v.Entreprises)); 

            CreateMap<Ecocentre, EcocentreDTO>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites))
                .ForMember(dt => dt. Matieres, c => c.MapFrom(v => v. Matieres));

            CreateMap<EcocentreDTO, Ecocentre>().ForMember(dt => dt.Visites, c => c.MapFrom(v => v.Visites))
                .ForMember(dt => dt. Matieres, c => c.MapFrom(v => v. Matieres));

            CreateMap<Quota, QuotaDTO>().ForMember(dt => dt.Historiques, c => c.MapFrom(v => v.Historiques));
            CreateMap<QuotaDTO, Quota>().ForMember(dt => dt.Historiques, c => c.MapFrom(v => v.Historiques));

            CreateMap<Visite, HistoriqueDTO>()
                .ForMember(x => x. Adresse, opt => opt.MapFrom(src => src. Provenance.Adresse))
                .ForMember(x => x. Avoir, opt => opt.MapFrom(src => src. Provenance.Quantite_Disponible))
                .ForMember(x => x. Commerce, opt => opt.MapFrom(src => src. Client. NomCommerce))
                .ForMember(x => x.Ecocentre, opt => opt.MapFrom(src => src.  Ecocentre.Nom))
                .ForMember(x => x. DateVisite, opt => opt.MapFrom(src => src.DateCreation ))
                .ForMember(x => x. Momtant, opt => opt.MapFrom(src => src. Transaction.Total))
                .ForMember(x => x. Id, opt => opt.MapFrom(src => src. IdVisite))
                .ForMember(x => x. Nom, opt => opt.MapFrom(src => src.  Client.Nom))
                .ForMember(x => x. Prenom, opt => opt.MapFrom(src => src.Client. Prenom))
                .ForMember(x => x. Tel, opt => opt.MapFrom(src => src.Client. Telephone))
                .ForMember(x => x.Employe, opt => opt.MapFrom(src => src. Employe));

        }

    }


}
