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
    public class MatiereController : ControllerBase {
        private readonly GestecoContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonService _matiereRepository;

        public MatiereController(GestecoContext context, IMapper mapper, ICommonService matiereRepository)
        {
            _context = context;
            _mapper = mapper;
            _matiereRepository = matiereRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MatiereDTO>> GetMatiere()
        {
            ServiceResponse<IEnumerable<MatiereDTO>> Data = new ServiceResponse<IEnumerable<MatiereDTO>>();
            try
            {
                var matiere = _matiereRepository.MatiereRepository.FindAll().ToList();
                Data.Data = _mapper.Map<IEnumerable<MatiereDTO>>(matiere);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }


        [HttpGet("{id}")]
        public  ActionResult<MatiereDTO>  GetMatiere(long id)
        {
            ServiceResponse<MatiereDTO> Data = new ServiceResponse<MatiereDTO>();

            try
            {
                var matiere = _matiereRepository.MatiereRepository.Find(id, x => x.IdMatiere);

                if (matiere == null)
                {
                    return NotFound();
                }
                Data.Data = _mapper.Map<MatiereDTO>(matiere);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }


        [HttpPut("{id}")]
        public  IActionResult  PutMatiere(long id, MatiereDTO matiere)
        {
            ServiceResponse<MatiereDTO> Data = new ServiceResponse<MatiereDTO>();

            if (id != matiere.IdMatiere)
            {
                return BadRequest();
            }

            var _matiere = _mapper.Map<Matiere>(matiere);

            try
            {
                _matiereRepository.MatiereRepository.Update(_matiere);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MatiereExists(id))
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
        public  ActionResult<MatiereDTO>  PostMatiere(MatiereDTO matiere)
        {
            ServiceResponse<MatiereDTO> Data = new ServiceResponse<MatiereDTO>();

            try
            {
                var _matiere = _mapper.Map<Matiere>(matiere);
                _matiere = _matiereRepository.MatiereRepository.Create(_matiere);
                Data.Data = _mapper.Map<MatiereDTO>(_matiere);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data); //Created($"api/matiere/{Data.Data.IdMatiere}", new { Data = Data });
        }


        [HttpDelete("{id}")]
        public  ActionResult<Matiere>  DeleteMatiere(long id)
        {
            ServiceResponse<MatiereDTO> Data = new ServiceResponse<MatiereDTO>();
            try
            {
                var matiere = _matiereRepository.MatiereRepository.Find((int)id);

                if (matiere == null)
                {
                    return NotFound();
                }

                var _matiere = _mapper.Map<MatiereDTO>(matiere);
                _matiereRepository.MatiereRepository.Delete(matiere);
                Data.Data = _matiere;
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }

        private bool MatiereExists(long id)
        {
            return _context.Matiere.Any(e => e.IdMatiere == id);
        }
    }
}
