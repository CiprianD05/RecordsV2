using Microsoft.AspNetCore.Mvc;
using RecordsRepositories.Interfaces;
using RecordsModels;

namespace Records.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenRepo _citizenRepo;

        public CitizensController(ICitizenRepo citizenRepo)
        {
            _citizenRepo = citizenRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Citizen>> Get()
        {
            var citizenList = _citizenRepo.GetAllCitizens();
            return Ok(citizenList);
        }

        [HttpGet("{id}")]
        public   ActionResult<Citizen> GetCitizenById(int id)
        {
            var citizenById =  _citizenRepo.GetAllCitizenById(id);
            
            if(citizenById==null)
                return NotFound();
            
            return Ok(citizenById);
        }
        
        
    }
}
