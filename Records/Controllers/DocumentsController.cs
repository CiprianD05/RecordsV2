﻿using Microsoft.AspNetCore.Mvc;
using RecordsRepositories.Interfaces;
using RecordsModels;
using AutoMapper;
using RecordsDTOs.DocumentDTOs;
using System.IO;
using Records.Functionalities.Interfaces;
using RecordsDTOs.CitizensDTOs;

namespace Records.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepo _documentRepo;
        private readonly IDocumentTypeRepo _documentTypeRepo;
        private readonly ICitizenRepo _citizenRepo;
        
        private IFolderManipulation _folderManipulation;

        public DocumentsController(IMapper mapper,IDocumentRepo repo, IDocumentTypeRepo documentTypeRepo,ICitizenRepo citizenRepo,IFolderManipulation folderManipulation)
        {
            _mapper = mapper;
            _documentRepo = repo;
            _documentTypeRepo = documentTypeRepo;
            _citizenRepo = citizenRepo;            
            _folderManipulation = folderManipulation;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentReadDTO>>> GetDocuments()
        {
            var document = await _documentRepo.GetAllDocuments();
            return Ok(_mapper.Map<IEnumerable<DocumentReadDTO>>(document));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentReadDTO>> GetDocumentById(int id)
        {
            var documentById = await _documentRepo.GetAllDocumentById(id);

            if (documentById == null)
                return NotFound();

            return Ok(_mapper.Map<DocumentReadDTO>(documentById));
        }


        [HttpPost]
        public async Task<ActionResult<DocumentReadDTO>> CreateDocument(DocumentCreateDTO documentCreateDto)
        {            
            var documentModel = _mapper.Map<Document>(documentCreateDto);

            documentModel.DateAdded = DateTime.Now;
            
            var documentTypeById = await _documentTypeRepo.GetAllDocumentTypesById(documentModel.DocumentTypeId);
            if (documentTypeById == null)
                return NotFound();

            var citizenById = await _citizenRepo.GetAllCitizenById(documentModel.CitizenId);
            if (citizenById == null)
                return NotFound();

            if (documentCreateDto.File.ContentType != "application/pdf")
            {
                return BadRequest("File must be a PDF.");
            }


            documentModel.Name = documentModel.Citizen.LastName +
                documentModel.Citizen.FirstName +
                documentModel.DocumentType.Abbreviation + "-" +
                documentModel.DateAdded.ToString("ddMMyyyymmss");

            documentModel.Path = _folderManipulation.CreateFoldersForDocument(documentModel)+documentModel.Name+".pdf";

            using (var stream = new FileStream(documentModel.Path, FileMode.Create))
            {
                await documentCreateDto.File.CopyToAsync(stream);
            }

            await _documentRepo.CreateDocument(documentModel);
            await _documentRepo.SaveChanges();
            var documentReadDto = _mapper.Map<DocumentReadDTO>(documentModel);
            return CreatedAtAction(nameof(GetDocumentById), new { id = documentReadDto.Id }, documentReadDto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocument(int Id, DocumentUpdateDTO documentUpdateDto)
        {


            var dbDocument = await _documentRepo.GetAllDocumentById(Id);

            if (dbDocument == null)
                return NotFound();

            _mapper.Map(documentUpdateDto, dbDocument);



            dbDocument.Name = dbDocument.Citizen.LastName +
                dbDocument.Citizen.FirstName +
                dbDocument.DocumentType.Abbreviation + "-" +
                dbDocument.DateAdded.ToString("ddMMyyyymmss");

            

            try
            {
                System.IO.File.Delete(dbDocument.Path);
                dbDocument.Path = _folderManipulation.CreateFoldersForDocument(dbDocument) + dbDocument.Name + ".pdf";
                using (var stream = new FileStream(dbDocument.Path, FileMode.Create))
                {
                    await documentUpdateDto.File.CopyToAsync(stream);
                }
            }catch (Exception ex) { }



            await _documentRepo.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(int Id)
        {
            var dbDocument = await _documentRepo.GetAllDocumentById(Id);


            if (dbDocument == null)
                return NotFound();


            _documentRepo.DeleteDocument(dbDocument);
            await _documentRepo.SaveChanges();

            return Ok();

        }

    }
}