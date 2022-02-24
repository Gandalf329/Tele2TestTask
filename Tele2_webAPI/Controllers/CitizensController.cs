using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tele2API.Data;
using Tele2API.Models;
using AutoMapper;
using Tele2API.DTO;

namespace Tele2API.Controllers
{   
    [ApiController]
    [Route("api/citizen")]
    public class CitizensController : Controller
    {
        private readonly ICitizen _repository;
        private readonly IMapper _mapper;
        public CitizensController(ICitizen repository, IMapper mapper)
        {
            _repository = repository;
            _repository.PersonCitizens();
            _repository.SaveChanges();
            _mapper = mapper;
        }

        //GET api/citizen
        [HttpGet("{sex?}/{lowerAgeLimit?}/{upperAgeLimit?}/{pageNumber?}/{pageSize?}")]
        public ActionResult<IEnumerable<CitizenDTO>> GetAllResidents([FromQuery] string sex=null, [FromQuery] int lowAge=-1, [FromQuery] int upAge=-1, [FromQuery] int pageNum=-1, [FromQuery] int pageSize=-1)
        {
            IEnumerable<Citizen> citizens;

            if (sex==null && lowAge == -1 && upAge == -1 && pageNum == -1 && pageSize ==-1)
            {
                citizens = _repository.GetCitizens();
            }
            else
            {
                citizens = _repository.GetCitizensByParams(sex, lowAge, upAge, pageNum, pageSize);
            }
            
            if (citizens != null)
            {
                return Ok(_mapper.Map<List<CitizenDTO>>(citizens));
            }
            return NotFound();
        }


        //GET api/citizen/{id}
        [HttpGet("{id}")]
        public ActionResult<CitizenDTO> GetResidentById(string id)
        {
            var resident = _repository.GetCitizenById(id);
            if (resident != null)
            {
                return Ok(_mapper.Map<CitizenDTO>(resident));
            }
            return NotFound();
        }

    }
}
