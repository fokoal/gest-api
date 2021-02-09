using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gesteco.api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ModePaiementController : ControllerBase {
        private readonly GestecoContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonService _modePaiementRepository;
        public ModePaiementController(GestecoContext context, IMapper mapper, ICommonService modePaiementRepository)
        {
            _context = context;
            _mapper = mapper;
            _modePaiementRepository = modePaiementRepository;
        }


        [HttpGet]
        public  ActionResult<IEnumerable<ModePaiementDTO>>  GetModePaiement()
        {
            ServiceResponse<IEnumerable<ModePaiementDTO>> Data = new ServiceResponse<IEnumerable<ModePaiementDTO>>();
            try
            {
                var tarifications = _modePaiementRepository.ModePaiementRepository.FindAll().ToList();
                Data.Data = _mapper.Map<IEnumerable<ModePaiementDTO>>(tarifications);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

        // GET: api/ModePaiement/5
        [HttpGet("{id}")]
        public  ActionResult<ModePaiementDTO>  GetModePaiement(long id)
        {
            ServiceResponse<ModePaiementDTO> Data = new ServiceResponse<ModePaiementDTO>();

            try
            {
                var modePaiement = _modePaiementRepository.ModePaiementRepository.Find(id, x => x.IdModePaiement);

                if (modePaiement == null)
                {
                    return NotFound();
                }
                Data.Data = _mapper.Map<ModePaiementDTO>(modePaiement);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }


        [HttpPut("{id}")]
        public  IActionResult PutModePaiement(long id, ModePaiementDTO modePaiement)
        {
            ServiceResponse<ModePaiementDTO> Data = new ServiceResponse<ModePaiementDTO>();

            if (id != modePaiement.IdModePaiement)
            {
                return BadRequest();
            }
            var _modePaiement = _mapper.Map<ModePaiement>(modePaiement);
            try
            {
                _modePaiementRepository.ModePaiementRepository.Update(_modePaiement);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ModePaiementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Data.Success = false;
                    Data.Messages = ex.Message;
                }
            }

            return Ok(Data);
        }


        [HttpPost]
        public  ActionResult<ModePaiementDTO>  PostModePaiement(ModePaiementDTO modePaiement)
        {

            ServiceResponse<ModePaiementDTO> Data = new ServiceResponse<ModePaiementDTO>();

            try
            {
                var _modePaiement = _mapper.Map<ModePaiement>(modePaiement);

                _modePaiement = _modePaiementRepository.ModePaiementRepository.Create(_modePaiement);

                Data.Data = _mapper.Map<ModePaiementDTO>(_modePaiement);

            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

    
        [HttpDelete("{id}")]
        public  ActionResult<ModePaiementDTO>  DeleteModePaiement(long id)
        {
            ServiceResponse<ModePaiementDTO> Data = new ServiceResponse<ModePaiementDTO>();
            try
            {
                var modePaiement = _modePaiementRepository.ModePaiementRepository.Find((int)id);

                if (modePaiement == null)
                {
                    return NotFound();
                }

                var _modePaiement = _mapper.Map<ModePaiementDTO>(modePaiement);
                _modePaiementRepository.ModePaiementRepository.Delete(modePaiement);
                Data.Data = _modePaiement;
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }

        private bool ModePaiementExists(long id)
        {
            return _context.ModePaiement.Any(e => e.IdModePaiement == id);
        }
    }
}
