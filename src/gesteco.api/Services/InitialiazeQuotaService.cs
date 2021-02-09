using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace gesteco.api.Services {
    public class InitialiazeQuotaService : BackgroundService  {


        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<InitialiazeQuotaService> _logger;
      
        public InitialiazeQuotaService(IServiceScopeFactory serviceScopeFactory, ILogger<InitialiazeQuotaService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        /// <summary>
        /// Verifit si la l'annee en cours est superieure a l'annee de la derniere initialisation , si oui 
        /// on execute la reinitialisation des quota 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            using (var scope = _serviceScopeFactory.CreateScope())
                {

                    try
                    {
                        do
                        {
                          
                           var now = DateTime.Now;
                           var dbcontext = scope.ServiceProvider.GetRequiredService<GestecoContext>();
                           var quotaIni = dbcontext.Historique_Initialisation_Quota.FirstOrDefault(p => p.DateEncours);
                           if(quotaIni !=null && now.Date.Year > quotaIni.DateInit.Year)
                            {
                              InitializeQuota(dbcontext, quotaIni);
                            }
                            await Task.Delay(1800000, stoppingToken); // tous les 30mn
                        }
                        while (!stoppingToken.IsCancellationRequested); 
                    
                    }
                    catch (Exception ex)
                    {
                      _logger.LogDebug("Quota initialization  Fails {0}", ex.Message);
                    }
                }
        }

        /// <summary>
        /// Reinitialisation des quota 
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="currentDate"></param>
        public void InitializeQuota(GestecoContext dbcontext, Historique_Initialisation_Quota currentDate)
        {
            using (var transaction = dbcontext.Database.BeginTransaction())
            {
                try
                {
                        var listQuota = dbcontext.Quota.Where(p => p.IdQuota != -1);
                        var dtdebut = new DateTime(DateTime.Now.Year, 1, 1);
                        var dtdefin = new DateTime(DateTime.Now.Year, 12, 31);
                        var _quotaStandard = dbcontext.Quota_Standard.First();

                        foreach (var _quota in listQuota)
                        {
                            _quota.Quantite_Commerce = _quotaStandard.Quantite_Commerce;
                            _quota.Quantite_Disponible = _quotaStandard.Quantite;
                            _quota.DateDebut = dtdebut;
                            _quota.DateFin = dtdefin;
                        }


                    currentDate = dbcontext.Historique_Initialisation_Quota.FirstOrDefault(p => p.DateEncours);
                  
                    currentDate.DateEncours = false;
                   
                    var quotaInit = new Historique_Initialisation_Quota
                     {
                        DateEncours = true,
                        DateInit = DateTime.Now,
                        Description =   string.Format("Quota initialization started on {0}", DateTime.Now) 
                     };
                    dbcontext.Historique_Initialisation_Quota.Add(quotaInit);

                    dbcontext.SaveChanges();
                    transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.RollbackAsync();
                    _logger.LogDebug("Quota initialization  Fails {0}", ex.Message);
                }
            }
        }
    }
}
