using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gesteco.api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class QuotaController : ControllerBase {
        private readonly GestecoContext _context;
        private readonly ICommonService _quotaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<QuotaController> _logger;

        public QuotaController(GestecoContext context, ICommonService quota, IMapper mapper, ILogger<QuotaController> logger)
        {
            _context = context;
            _quotaRepository = quota;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetCurrentQuota")]
        public  ActionResult<Quota_StandardDTO>  GetCurrentQuota()
        {
            ServiceResponse<Quota_StandardDTO> Data = new ServiceResponse<Quota_StandardDTO>();
            try
            {
                var quota = _quotaRepository.QuotaRepository.GetCurentQuota();
                Data.Data = _mapper.Map<Quota_StandardDTO>(quota);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
                _logger.LogDebug(typeof(QuotaController).Name + " GetCurrentQuota Fails {0}", ex.Message);
            }
            return Ok(Data);
        }
        // GET: api/Quota
        [HttpGet]
        public  ActionResult<IEnumerable<Historique_QuotaDTO>>  GetHistoriqueQuota(QuotaCriteria historique)
        {
            ServiceResponse<IEnumerable<Historique_QuotaDTO>> Data = new ServiceResponse<IEnumerable<Historique_QuotaDTO>>();
            try
            {
                if (historique.DateDebut == null || historique.DateFin == null)
                {
                    Data.Success = false;
                    Data.Messages = "Veuillez renseigner la date Debut et Fin ";

                    return Ok(Data);
                }

                var historiqueQuota = _quotaRepository.QuotaRepository.GetHistoriqueQuota(historique).ToList();
                Data.Data = _mapper.Map<IEnumerable<Historique_QuotaDTO>>(historiqueQuota);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
                _logger.LogDebug(typeof(QuotaController).Name + " GetHistoriqueQuota Fails {0}", ex.Message);
            }

            return Ok(Data);
        }

        
        [HttpGet("{IdCivique}")]
        public  ActionResult<QuotaDTO>  GetQuota(string IdCivique)
        {
            ServiceResponse<QuotaDTO> Data = new ServiceResponse<QuotaDTO>();
            try
            {
                var quota = _quotaRepository.QuotaRepository.GetCurrent(IdCivique);

               

                Data.Data = _mapper.Map<QuotaDTO>(quota);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
                _logger.LogDebug(typeof(QuotaController).Name + " GetQuota Fails {0}", ex.Message);
            }
            return Ok(Data);
        }


        [HttpPost]
        public ActionResult<Quota_StandardDTO>  PostQuota(Quota_StandardDTO quota)
        {
            ServiceResponse<Quota_StandardDTO> Data = new ServiceResponse<Quota_StandardDTO>();
            try
            {
                if (quota.Quantite == 0|| quota.Quantite_Commerce ==0 )
                {
                    Data.Success = false;
                    Data.Messages = "Veuillez renseigner les quantités résidentiel ou non résidentiel";
                    return Ok(Data);
                }

                var _quota = _mapper.Map<Quota_Standard>(quota);
                _quota = _quotaRepository.QuotaRepository.CreationQuota(_quota);
                Data.Data = _mapper.Map<Quota_StandardDTO>(_quota);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
               _logger.LogDebug(typeof(QuotaController).Name +" PostQuota Fails {0}", ex.Message);
            }

            return Ok(Data);
        }



    }
}
