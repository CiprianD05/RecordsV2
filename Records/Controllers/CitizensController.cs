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
        public async Task<ActionResult<IEnumerable<CitizenReadDTO>>> Get()
        {
            var citizenList =await  _citizenRepo.GetAllCitizens();
            return Ok(_mapper.Map<IEnumerable<CitizenReadDTO>>(citizenList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenReadDTO>> GetCitizenById(int id)
        {
            var citizenById =await _citizenRepo.GetAllCitizenById(id);

            if (citizenById == null)
                return NotFound();

            return Ok(_mapper.Map<CitizenReadDTO>(citizenById));
        }

        [HttpPost]
        public async Task<ActionResult<CitizenReadDTO>> CreateCitizen(CitizenCreateDTO citizenCreateDto)
        {
            var citizenModel = _mapper.Map<Citizen>(citizenCreateDto);
            await _citizenRepo.CreateCitizen(citizenModel);
            await _citizenRepo.SaveChanges();
            var citizenReadDto = _mapper.Map<CitizenReadDTO>(citizenModel);
            return CreatedAtAction(nameof(GetCitizenById), new { id=citizenReadDto.Id},citizenReadDto);
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCitizen(int Id,CitizenUpdateDTO citizenUpdateDto)
        {


            var dbCitizen =await  _citizenRepo.GetAllCitizenById(Id);

            if (dbCitizen == null)
                return NotFound();
            
             _mapper.Map(citizenUpdateDto,dbCitizen);          

             await _citizenRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
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
