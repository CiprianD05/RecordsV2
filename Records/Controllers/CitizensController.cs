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
        public ActionResult<Citizen> CreateCitizen(CitizenCreateDTO citizenCreateDto)
        {
            var citizenModel = _mapper.Map<Citizen>(citizenCreateDto);
            _citizenRepo.CreateCitizen(citizenModel);
            _citizenRepo.SaveChanges();
            var commandReadDto = _mapper.Map<CitizenReadDTO>(citizenModel);
            return CreatedAtRoute(nameof(GetCitizenById), new { Id=commandReadDto.Id},commandReadDto);
            
        }

        [HttpPut]
        public ActionResult<Citizen> UpdateCitizen(int Id,CitizenUpdateDTO citizenUpdateDto)
        {


            var dbCitizen = _citizenRepo.GetAllCitizenById(Id);
            if (dbCitizen == null)
                return NotFound();
            
            _mapper.Map(citizenUpdateDto,dbCitizen);          

            _citizenRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCitizen(int Id)
        {
            var dbCitizen = await _citizenRepo.GetAllCitizenById(Id);
            
            if(dbCitizen==null)
                return NotFound();


            _citizenRepo.DeleteCitizen(dbCitizen);
            await _citizenRepo.SaveChanges();

            return Ok();

        }


    }
}
