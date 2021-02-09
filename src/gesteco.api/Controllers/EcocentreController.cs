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
 //  [Authorize]
    public class EcocentreController : ControllerBase
    {
        private readonly GestecoContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonService _icommonService;
        public EcocentreController(GestecoContext context, IMapper mapper, ICommonService commonService)
        {
            _context = context;
            _mapper = mapper;
            _icommonService = commonService;
        }

        [HttpGet]
        // TODO  : Ne pas utiliser le mot "async" s'il n'y a pas d'appel asynchrone avec le mot "await". "async" est toujours utilisé conjointement avec "await".
        public ActionResult<IEnumerable<EcocentreDTO>> GetEcocentre()
        {
            var data = new ServiceResponse<IEnumerable<EcocentreDTO>>();

            try
            {
                var ecocentre = _icommonService.EcocentreRepository.FindAll().ToList();
                // BUG : Le contrôler retourne toujours un élément, même si aucun écocentre existe.
                data.Data = _mapper.Map<IEnumerable<EcocentreDTO>>(ecocentre);
            }
            catch (Exception ex)
            {
                data.Success = false;
                data.Messages = ex.Message;
            }

            return Ok(data);
        }


        [HttpGet("{id}")]
        public  ActionResult<EcocentreDTO>  GetEcocentre(long id)
        {
            ServiceResponse<EcocentreDTO> Data = new ServiceResponse<EcocentreDTO>();

            try
            {
                var ecocentre = _icommonService.EcocentreRepository.GetEcocentre(id);

                if (ecocentre == null)
                {
                    return NotFound();
                }
                Data.Data = _mapper.Map<EcocentreDTO>(ecocentre);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

        /// <summary>
        /// Permet de retourner l'ecocentre  avec la liste des 
        /// matieres standard en cochant ceux qui sont rattaches a l'ecocentre encours
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetEcocentreMatiere/{id}")]
        public ActionResult<EcocentreDTO>  GetEcocentreMatiere(long id)
        {
            ServiceResponse<EcocentreDTO> Data = new ServiceResponse<EcocentreDTO>();

            try
            {
                var ecocentre = _icommonService.EcocentreRepository.GetEcocentre(id);

                if (ecocentre == null)
                {
                    return NotFound();
                }

                Data.Data = _mapper.Map<EcocentreDTO>(ecocentre);
                var listOfMatiereStandard = _context.Matiere.Where(p => p.IdMatiere != -1).ToList();
                var matiereEcocentre = Data.Data.Matieres. ToList();
                var listOfNewMatiereCocentre = new List<Ecocentre_MatiereDTO>();

                var matieredisplay = matiereEcocentre.Where(el2 => listOfMatiereStandard.Any(el1 => el1.   Description == el2.  Description)).ToList();
               
                matieredisplay.ForEach(em =>
                {
                    em.Selected = true;
                });

                listOfMatiereStandard.ForEach(m =>
                {
                    var newMatiere = matieredisplay.FirstOrDefault(t => t. Description == m. Description);
                    if (newMatiere==null)
                    {
                        Ecocentre_MatiereDTO ecm = new Ecocentre_MatiereDTO
                        {
                            Comptable = m.Comptable,
                            Description = m.Description,
                            Id = m.IdMatiere,
                            Selected = false,
                        };
                        listOfNewMatiereCocentre.Add(ecm);
                    }
                });
                matieredisplay.AddRange(listOfNewMatiereCocentre);
                Data.Data.Matieres = matieredisplay;
               
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

        [HttpPut("{id}")]
        public IActionResult  PutEcocentre(long id, EcocentreDTO ecocentre)
        {
            ServiceResponse<EcocentreDTO> Data = new ServiceResponse<EcocentreDTO>();

            if (id != ecocentre.IdEcocentre)
            {
                return BadRequest();
            }

            var _ecocentre = _mapper.Map<Ecocentre>(ecocentre);


            try
            {
                _icommonService.EcocentreRepository.Modifier(_ecocentre);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EcocentreExists(id))
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
        public  ActionResult<EcocentreDTO>  PostEcocentre(EcocentreDTO ecocentre)
        {
            ServiceResponse<EcocentreDTO> Data = new ServiceResponse<EcocentreDTO>();

            try
            {
                var _ecocentre = _mapper.Map<Ecocentre>(ecocentre);
                _ecocentre = _icommonService.EcocentreRepository.Create(_ecocentre);
                Data.Data = _mapper.Map<EcocentreDTO>(_ecocentre);

            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }

        [HttpDelete("{id}")]
        public  ActionResult<EcocentreDTO>  DeleteEcocentre(long id)
        {
            ServiceResponse<EcocentreDTO> Data = new ServiceResponse<EcocentreDTO>();
            try
            {
                var ecocentre = _icommonService.EcocentreRepository.Find((int)id);

                if (ecocentre == null)
                {
                    return NotFound();
                }
                var _ecocentre = _mapper.Map<EcocentreDTO>(ecocentre);
                _icommonService.EcocentreRepository.Delete(ecocentre);
                Data.Data = _ecocentre;
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }

        private bool EcocentreExists(long id)
        {
            return _context.Ecocentre.Any(e => e.IdEcocentre == id);
        }
    }
}
