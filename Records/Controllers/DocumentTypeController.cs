using Microsoft.AspNetCore.Mvc;
using RecordsRepositories.Interfaces;
using RecordsModels;
using AutoMapper;
using RecordsDTOs.DocumentTypeDTOs;


namespace Records.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeRepo _documentTypeRepo;
        private readonly IMapper _mapper;

        public DocumentTypeController(IDocumentTypeRepo documentTypeRepo, IMapper mapper)
        {
            _documentTypeRepo = documentTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentTypeReadDTO>>> GetDocumentTypes()
        {
            var documentTypes = await _documentTypeRepo.GetAllDocumentTypes();
            return Ok(_mapper.Map<IEnumerable<DocumentTypeReadDTO>>(documentTypes));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentTypeReadDTO>> GetDocumentTypeById(int id)
        {
            var documentTypeById = await _documentTypeRepo.GetAllDocumentTypesById(id);

            if (documentTypeById == null)
                return NotFound();

            return Ok(_mapper.Map<DocumentTypeReadDTO>(documentTypeById));
        }


        [HttpPost]
        public async Task<ActionResult<DocumentTypeReadDTO>> CreateDocumentType(DocumentTypeCreateDTO documentTypeCreateDto)
        {
            var documentTypeModel = _mapper.Map<DocumentType>(documentTypeCreateDto);
            await _documentTypeRepo.CreateDocumentType(documentTypeModel);
            await _documentTypeRepo.SaveChanges();
            var documentTypeReadDto = _mapper.Map<DocumentTypeReadDTO>(documentTypeModel);
            return CreatedAtAction(nameof(GetDocumentTypeById), new { id = documentTypeReadDto.Id }, documentTypeReadDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocumentType(int Id, DocumentTypeUpdateDTO documentTypeUpdateDto)
        {


            var dbDocumentType = await _documentTypeRepo.GetAllDocumentTypesById(Id);

            if (dbDocumentType == null)
                return NotFound();

            _mapper.Map(documentTypeUpdateDto, dbDocumentType);

            await _documentTypeRepo.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocumentType(int Id)
        {
            var dbDocumentType = await _documentTypeRepo.GetAllDocumentTypesById(Id);


            if (dbDocumentType == null)
                return NotFound();


            _documentTypeRepo.DeleteDocumentType(dbDocumentType);
            await _documentTypeRepo.SaveChanges();

            return Ok();

        }
    }
}
