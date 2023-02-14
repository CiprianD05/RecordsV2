using Microsoft.AspNetCore.Mvc;
using RecordsRepositories.Interfaces;
using RecordsModels;
using AutoMapper;
using RecordsDTOs.CitizensDTOs;

namespace Records.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenRepo _citizenRepo;
        private readonly IMapper _mapper;

        public CitizensController(ICitizenRepo citizenRepo, IMapper mapper)
        {
            _citizenRepo = citizenRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CitizenReadDTO>> Get()
        {
            var citizenList = _citizenRepo.GetAllCitizens();
            return Ok(_mapper.Map<IEnumerable<CitizenReadDTO>>(citizenList));
        }

        [HttpGet("{id}")]
        public ActionResult<Citizen> GetCitizenById(int id)
        {
            var citizenById = _citizenRepo.GetAllCitizenById(id);

            if (citizenById == null)
                return NotFound();

            return Ok(_mapper.Map<CitizenReadDTO>(citizenById));
        }

        [HttpPost]
        public ActionResult<Citizen> CreateCitizen(Citizen citizen)
        {
            var newCitizen= _citizenRepo.CreateCitizen(citizen);
            _citizenRepo.SaveChanges();

            return Ok(newCitizen);
            
        }

        [HttpPut]
        public ActionResult<Citizen> UpdateCitizen(Citizen citizen)
        {
            var dbCitizen = _citizenRepo.GetAllCitizenById(citizen.Id);
            if (citizen == null)
                return NotFound();

            dbCitizen.Result.Name=citizen.Name;
            dbCitizen.Result.SocialSecurityNumber=citizen.SocialSecurityNumber;
            dbCitizen.Result.PassportNumber = citizen.PassportNumber;

            _citizenRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCitizen(Citizen citizen)
        {
            var dbCitizen = await _citizenRepo.GetAllCitizenById(citizen.Id);
            
            if(dbCitizen==null)
                return NotFound();


            _citizenRepo.DeleteCitizen(dbCitizen);
            await _citizenRepo.SaveChanges();

            return Ok();

        }


    }
}
