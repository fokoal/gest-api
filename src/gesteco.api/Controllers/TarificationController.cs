using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Database.Models;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using gesteco.api.src.gesteco.WebApi.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace gesteco.api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TarificationController : ControllerBase {
        private readonly GestecoContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonService _icommonservice;
        public TarificationController(GestecoContext context, IMapper mapper, ICommonService tarificationRepository)
        {
            _context = context;
            _mapper = mapper;
            _icommonservice = tarificationRepository;
        }
        

        [HttpGet]
        public  ActionResult<TarificationDTO>  GetTarification()
        {
            ServiceResponse<TarificationDTO> Data = new ServiceResponse<TarificationDTO>();

            try
            {
                var tarification = _context.Tarification.First();

                if (tarification == null)
                {
                    return NotFound();
                }

                Data.Data = _mapper.Map<TarificationDTO>(tarification);
            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }

            return Ok(Data);
        }


        [HttpPost]
        public  ActionResult<TarificationDTO>  PostTarification(TarificationDTO tarification)
        {
            ServiceResponse<TarificationDTO> Data = new ServiceResponse<TarificationDTO>();

            try
            {
                if (tarification. Prix == 0 || tarification. Prix_Commerce == 0)
                {
                    Data.Success = false;
                    Data.Messages = "Veuillez renseigner le tarif résidentiel ou non résidentiel";
                    return Ok(Data);
                }
                var _tarification = _mapper.Map<Tarification>(tarification);

                _tarification = _context.Tarification.First();
                _tarification.Prix = tarification.Prix;
                _tarification.Prix_Commerce = tarification.Prix_Commerce;
                _icommonservice.TarificationRepository.Update(_tarification);

                Data.Data = _mapper.Map<TarificationDTO>(_tarification);

            }
            catch (Exception ex)
            {
                Data.Success = false;
                Data.Messages = ex.Message;
            }
            return Ok(Data);
        }


    }
}
