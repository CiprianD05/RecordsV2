using Microsoft.AspNetCore.Mvc;
using RecordsRepositories.Interfaces;
using RecordsModels;
using AutoMapper;
using RecordsDTOs.PsychologicalProfileDTOs;
using RecordsDTOs.CitizensDTOs;

namespace Records.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PsychologicalProfilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPsychologicalProfileRepo _psycRepo;

        public PsychologicalProfilesController(IMapper mapper, IPsychologicalProfileRepo psycRepo)
        {
            _mapper = mapper;
            _psycRepo = psycRepo;
        }

        [HttpGet("citizenId/{citizenId}")]
        public async Task<ActionResult<IEnumerable<PsychologicalProfileReadDTO>>> GetPsychProfilesByCitizenId(int citizenId)
        {
            var psychProfilesList = await _psycRepo.GetAllPsychologicalProfiles(citizenId);
            return Ok(_mapper.Map<IEnumerable<PsychologicalProfileReadDTO>>(psychProfilesList));
        }

        [HttpGet("{psychId}")]
        public async Task<ActionResult<PsychologicalProfileReadDTO>> GetPsychProfilesById(int psychId)
        {
            var psychProfile = await _psycRepo.GetPsychologicalProfileById(psychId);

            if (psychProfile == null)
                return NotFound();
            return Ok(_mapper.Map<PsychologicalProfileReadDTO>(psychProfile));
        }

        [HttpPost]
        public async Task<ActionResult<PsychologicalProfileReadDTO>> CreatePsychologicalProfile(PsychologicalProfileCreateDTO psychologicalProfileCreateDto)
        {
            var psychologicalProfileModel = _mapper.Map<PsychologicalProfile>(psychologicalProfileCreateDto);
            await _psycRepo.CreatePsychologicalProfile(psychologicalProfileModel);
            await _psycRepo.SaveChanges();
            var psychologicalProfileReadDto = _mapper.Map<PsychologicalProfileReadDTO>(psychologicalProfileModel);
            return CreatedAtAction(nameof(GetPsychProfilesById), new { id = psychologicalProfileReadDto.Id }, psychologicalProfileReadDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePsychProfile(int Id, PsychologicalProfileUpdateDTO psychProfileUpdateDto)
        {


            var dbPychProfile = await _psycRepo.GetPsychologicalProfileById(Id);

            if (dbPychProfile == null)
                return NotFound();

            _mapper.Map(psychProfileUpdateDto, dbPychProfile);

            await _psycRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePsychologicalProfile(int Id)
        {
            var dbPychProfile = await _psycRepo.GetPsychologicalProfileById(Id);

            if (dbPychProfile == null)
                return NotFound();


            _psycRepo.DeletePsychologicalProfile(dbPychProfile);
            await _psycRepo.SaveChanges();

            return Ok();

        }
    }
}
