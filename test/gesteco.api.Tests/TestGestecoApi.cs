using gesteco.api.OutputModels;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace gesteco.api.Tests {
    public class TestGestecoApi : IClassFixture<CustomWebApplicationFactory<gesteco.api.Startup>> {

        private readonly HttpClient httpClient;
        private readonly string baseUrl = "https://gesteco-api-dev-app.azurewebsites.net/api/"; //http://localhost:52229/api/
        private readonly IConfigurationSection auth0Settings;
        public TestGestecoApi(CustomWebApplicationFactory<gesteco.api.Startup> factory)
        {
            httpClient = factory.CreateClient();
            auth0Settings = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build()
           .GetSection("Auth0");
        }

        [Fact()]
        public async Task AddMatiere()
        {
           var req = new HttpRequestMessage(HttpMethod.Post, baseUrl+ "matiere");

            var matiere = new MatiereDTO
            {
                Comptable = true,
                Description = "T" + DateTime.Now.ToFileTime().ToString()
            };

            req .Content = new StringContent(JsonConvert.SerializeObject(matiere), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(req.RequestUri, req.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<MatiereDTO>>();

            Assert.Equal(matiere.Description, result.Data.Description);
        }

        [Fact()]
        public async Task Update_Matiere()
        {
          
            var reqPost = new HttpRequestMessage(HttpMethod.Post, baseUrl + "matiere");

            var matiere = new MatiereDTO
            {
                Comptable = true,
                Description = Guid.NewGuid().ToString()
            };

            reqPost.Content = new StringContent(JsonConvert.SerializeObject(matiere), Encoding.UTF8, "application/json");
            var _response = await httpClient.PostAsync(reqPost.RequestUri, reqPost.Content);
            var _result = await _response.Content.ReadAsAsync<ServiceResponse<MatiereDTO>>();
             
            if (_response.IsSuccessStatusCode)
            {
                _result.Data.Description = "Update";
            }

            var req = new HttpRequestMessage(HttpMethod.Put, baseUrl + "matiere/"+ _result.Data.IdMatiere);
            req.Content = new StringContent(JsonConvert.SerializeObject(_result.Data), Encoding.UTF8, "application/json");
            _response = await httpClient.PutAsync(req.RequestUri, req.Content);

            var res = await httpClient.GetAsync(reqPost.RequestUri);
            var find_All_result = await res.Content.ReadAsAsync<ServiceResponse<IEnumerable<MatiereDTO>>>();
            List<MatiereDTO> ms = find_All_result.Data as List<MatiereDTO>;

            var m = ms.FirstOrDefault(p => p.Description == "Update");

            Assert. NotEqual(matiere.Description, m.Description);
        }

        [Fact()]
        public async Task Delete_Matiere()
        {
            var req = new HttpRequestMessage(HttpMethod.Post, baseUrl + "matiere");
            var test = true;
            var matiere = new MatiereDTO
            {
                Comptable = true,
                Description = Guid.NewGuid().ToString()
            };
            req.Content = new StringContent(JsonConvert.SerializeObject(matiere), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(req.RequestUri, req.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<MatiereDTO>>();

            req = new HttpRequestMessage(HttpMethod.Delete, baseUrl + "matiere/" + result.Data.IdMatiere);
            var res = await httpClient.DeleteAsync(req.RequestUri);
            var rlt = await res.Content.ReadAsAsync<ServiceResponse<MatiereDTO>>();

            Assert.Equal(test, rlt.Success);
        }

        [Fact()]
        public async Task Add_Ecocentre()
        {
            var req = new HttpRequestMessage(HttpMethod.Post, baseUrl + "Ecocentre");
            var ecocentre = new EcocentreDTO
            {
                Adresse = "Coaticook, QC",
                Codepostal="J1J3V3",
                Nom= Guid.NewGuid().ToString(),
                Rue="75 Rue",
                Ville="Laval",
                IdEcocentre=0,
            };

             var m = new List<Ecocentre_MatiereDTO> 
             {
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = false,
                      Description = Guid.NewGuid().ToString(),
                  },
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = true,
                      Description = Guid.NewGuid().ToString(),
                  },
             };

            ecocentre.Matieres = m;

            req.Content = new StringContent(JsonConvert.SerializeObject(ecocentre), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(req.RequestUri, req.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();


            Assert.NotEqual(ecocentre.IdEcocentre, result. Data.IdEcocentre);
        }

        [Fact()]
        public async Task Update_Ecocentre()
        {
            var req = new HttpRequestMessage(HttpMethod.Post, baseUrl + "Ecocentre");
           
            var ecocentre = new EcocentreDTO
            {
                Adresse = "Notre-Dame Est, bur. R.134 QC",
                Codepostal = "H2Y1C6",
                Nom = Guid.NewGuid().ToString(),
                Rue = "280 Rue",
                Ville = "Montreal",
                IdEcocentre = 0,
            };

            var m = new List<Ecocentre_MatiereDTO>
             {
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = false,
                      Description =  Guid.NewGuid().ToString(),
                  },
             };
            ecocentre.Matieres = m;

            req.Content = new StringContent(JsonConvert.SerializeObject(ecocentre), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(req.RequestUri, req.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();

            req = new HttpRequestMessage(HttpMethod.Get, baseUrl + "Ecocentre/GetEcocentreMatiere/" + result.Data. IdEcocentre);
            var res = await httpClient.GetAsync(req.RequestUri);
            var rlt = await res.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();

            ecocentre = rlt.Data;
            ecocentre.Nom = "Ecocentre-Update";

            req = new HttpRequestMessage(HttpMethod.Put, baseUrl + "Ecocentre/" + ecocentre. IdEcocentre);
            req.Content = new StringContent(JsonConvert.SerializeObject(ecocentre), Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(req.RequestUri, req.Content);

            req = new HttpRequestMessage(HttpMethod.Get, baseUrl + "Ecocentre/GetEcocentreMatiere/" + result.Data.IdEcocentre);
            res = await httpClient.GetAsync(req.RequestUri);
            rlt = await res.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();

            Assert.Equal(ecocentre.Nom, rlt.Data.Nom);
        }

        [Fact()]
        public async Task AddVisite()
        {
           
            // Arrange
            VisiteDTO visiteDTO = new VisiteDTO();
            visiteDTO.Client = new ClientDTO
            {
                Courriel = "pepe@yahoo.fr",
                DateCreation = DateTime.Now,
                Immaticulation = "025122",
                Nom = "Charle ",
                NomCommerce = "Couche tart",
                Prenom = "Le grand",
                Telephone = "5143691245"
            };

            var  entrep = new List<EntrepriseDTO> 
            {
                new EntrepriseDTO
                {
                    Nom = "SheWeb3"
                }
            };

            visiteDTO.Client.Entreprises = entrep;

            visiteDTO.Provenance = new ProvenanceDTO
            {
                Adresse = "9658 Rue albert skinner Laval",
                Quantite_Disponible = 1000,
            };

            visiteDTO.Transaction = new TransactionDTO
            {
                Hauteur = 2,
                Largeur = 2,
                Longueur = 12,
                IdModePaiement = 2,
                Quantite_Utilisee = 0,
                Volume = 48
            };

            var mt = new List<Matiere_VisiteDTO> {

                new Matiere_VisiteDTO
                {
                    Comptable = false,
                    Description = "Vetement et textiles"
                }
            };

            visiteDTO.Matieres = mt;

            //// Creation de l'ecocentre
            var ecocentre = new EcocentreDTO
            {
                Adresse = "Coaticook, QC",
                Codepostal = "J1J3V3",
                Nom = "Coaticook Test"  +DateTime.Now.ToFileTime().ToString(),
                Rue = "755 Rue",
                Ville = "Laval",
                IdEcocentre = 0,
            };

            var m = new List<Ecocentre_MatiereDTO>
             {
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = false,
                      Description = "Vetement et textiles",
                  },
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = true,
                      Description = "Chaussures",
                  },
             };
            ecocentre.Matieres = m;

            var request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "ecocentre");

            request.Content = new StringContent(JsonConvert.SerializeObject(ecocentre), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(request.RequestUri, request.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();
            visiteDTO.IdEcocentre = result.Data.IdEcocentre ;
            visiteDTO.Employe = "Alain";

            request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "visite");
            request.Content = new StringContent(JsonConvert.SerializeObject(visiteDTO), Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(request.RequestUri, request.Content);
            var _result = await response.Content.ReadAsAsync<ServiceResponse<VisiteDTO>>();

            Assert.NotEqual(visiteDTO.IdVisite, _result. Data.IdVisite);
        }


        [Fact()]
        public async Task  getHistorique_Visite()
        {
            // Arrange
            VisiteDTO visiteDTO = new VisiteDTO();
            var es = new List<EntrepriseDTO>();

            visiteDTO.Client = new ClientDTO
            {
                Courriel = "test@gmail.com",
                DateCreation = DateTime.Now,
                Immaticulation = "789654",
                Nom = Guid.NewGuid().ToString(),
                Prenom = "Talla",
                Telephone = "8197891878",
                Entreprises = es
            };

            visiteDTO.Provenance = new ProvenanceDTO
            {
                Adresse = "96 Rue Notre-Dame Est, bur. R.134 QC",
                Quantite_Disponible = 1500,
            };

            visiteDTO.Transaction = new TransactionDTO
            {
                Hauteur = 25,
                Largeur = 50,
                Longueur= 12,
                IdModePaiement = 2,
                Quantite_Utilisee = 0,
                Volume = 1500
            };

            var  mt = new List<Matiere_VisiteDTO> {
                new Matiere_VisiteDTO
                {
                    Comptable = false,
                    Description = "Vetement et textiles"
                },
                new Matiere_VisiteDTO
                {
                    Comptable = true,
                    Description = "Chaussures"
                }
            };
            visiteDTO.Matieres = mt;

            //// Creation de l'ecocentre
            var ecocentre = new EcocentreDTO
            {
                Adresse = "Coaticook, QC",
                Codepostal = "J1J3V3",
                Nom = "Ecocentre Test",
                Rue = "75 Rue",
                Ville = "Laval",
                IdEcocentre = 0,
            };

            var m = new List<Ecocentre_MatiereDTO>
             {
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = false,
                      Description = "Vetement et textiles",
                  },
                  new Ecocentre_MatiereDTO
                  {
                      Comptable = true,
                      Description = "Chaussures",
                  },
             };
            ecocentre.Matieres = m;

            var request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "ecocentre");

            request.Content = new StringContent(JsonConvert.SerializeObject(ecocentre), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(request.RequestUri, request.Content);
            var result = await response.Content.ReadAsAsync<ServiceResponse<EcocentreDTO>>();


            visiteDTO.IdEcocentre = result.Data.IdEcocentre;
            visiteDTO.Employe = "Test Fact22";

            request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "Visite");
            request.Content = new StringContent(JsonConvert.SerializeObject(visiteDTO), Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(request.RequestUri, request.Content);
            var  result_visite = await response.Content.ReadAsAsync<ServiceResponse<VisiteDTO>>();

            var h = new HistoriqueCriteria
            {
                Employe = result_visite.Data.Employe,
                ClientNom = result_visite.Data.Client.Nom,
                Courriel = result_visite.Data.Client.Courriel,
                Ecocentre = result_visite.Data. Ecocentre. Nom,
            };

            request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "Visite/GetHistorique");
            request.Content = new StringContent(JsonConvert.SerializeObject(h), Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(request.RequestUri, request.Content);
            var  result_hist = await response.Content.ReadAsAsync<ServiceResponse<IEnumerable<HistoriqueDTO>>>();
            var hist = result_hist.Data as List<HistoriqueDTO>;
            int cpt = 1;
          
            Assert.Equal(cpt, hist.Count);
        }
    }
}
