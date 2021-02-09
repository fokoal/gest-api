using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using AutoMapper;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using gesteco.api.src.gesteco.WebApi.CriteriaModels;
using System.Linq.Expressions;
using gesteco.api.OutputModels;

namespace gesteco.api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VisiteController : ControllerBase {
        private readonly GestecoContext _context;
        private readonly ICommonService _visiteRepository;
        private readonly IMapper _mapper;
        public VisiteController(GestecoContext context, IMapper mapper, ICommonService visiteRepository)
        {
            _context = context;
            _visiteRepository = visiteRepository;
            _mapper = mapper;
        }

        // GET: api/Visite
        [HttpGet]
        public  ActionResult<IEnumerable<VisiteDTO>>  GetVisite()
        {
            ServiceResponse<IEnumerable<VisiteDTO>> Data = new ServiceResponse<IEnumerable<VisiteDTO>>();
            try
            {
                var visites = _visiteRepository.VisiteRepository.GetAll().ToList();
                Data.Data = _mapper.Map<IEnumerable<VisiteDTO>>(visites);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }

        [HttpPost("GetHistorique")]
        public  ActionResult<IEnumerable<HistoriqueDTO>>  GetHistorique(HistoriqueCriteria historique)
        {
            ServiceResponse<IEnumerable<HistoriqueDTO>> Data = new ServiceResponse<IEnumerable<HistoriqueDTO>>();
            try
            {
                  var visites = _visiteRepository.VisiteRepository.GetHistorique(historique);
                  Data.Data = _mapper.Map<IEnumerable<HistoriqueDTO>>(visites);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }

        // GET: api/Visite/5
        [HttpGet("{id}")]
        public  ActionResult<VisiteDTO>  GetVisite(long id)
        {
            ServiceResponse<VisiteDTO> Data = new ServiceResponse<VisiteDTO>();

            try
            {
                var visite = _visiteRepository.VisiteRepository.GetVisite(id);

                if (visite == null)
                {
                    return NotFound();
                }

                Data.Data = _mapper.Map<VisiteDTO>(visite);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

        // POST: api/Visite
        // Le système doit permettre d’enregistrer une visite et de facturer le client
        [HttpPost]
        public ActionResult<VisiteDTO>  PostVisite(VisiteDTO visiteDTO)
        {

            ServiceResponse<VisiteDTO> Data = new ServiceResponse<VisiteDTO>();
            var visite = _mapper.Map<Visite>(visiteDTO);
            try
            {
                visite = _visiteRepository.VisiteRepository.CreateVisite(visite);
                Data.Data = _mapper.Map<VisiteDTO>(visite);
            }
            catch (DbUpdateException ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }


    }
}
