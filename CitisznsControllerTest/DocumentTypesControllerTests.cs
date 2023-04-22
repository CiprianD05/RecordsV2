using Records.Controllers;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using RecordsModels;
using RecordsRepositories;
using RecordsDTOs.Profiles;
using Microsoft.AspNetCore.Mvc;
using RecordsDTOs.CocumentTypeDTOs;


namespace CitisznsControllerTest
{
    public class DocumentTypesControllerTests : IDisposable
    {

        Mock<RecordsRepositories.Interfaces.IDocumentTypeRepo> mockRepo;
        RecordsDTOs.Profiles.DocumentTypesProfiles.DocumentTypeAutoMappingProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;


        public DocumentTypesControllerTests()
        {
            mockRepo = new Mock<RecordsRepositories.Interfaces.IDocumentTypeRepo>();
            realProfile = new RecordsDTOs.Profiles.DocumentTypesProfiles.DocumentTypeAutoMappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }


        [Fact]
        public void GetDocumentTypesItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange            
            mockRepo.Setup(repo => repo.GetAllDocumentTypes()).Returns(GetDocumentTypes(0));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetDocumentTypes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);

        }
        [Fact]
        public void GetAllDocumentTypes_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypes()).Returns(GetDocumentTypes(1));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetDocumentTypes();

            //Assert
            var okResult = result.Result.Result as OkObjectResult;
            var documentTypes = okResult.Value as List<DocumentTypeReadDTO>;
            Assert.Single(documentTypes);
        }


        [Fact]
        public void GetAllDocumentTypes_RetrunsOk_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypes()).Returns(GetDocumentTypes(1));

            var controller = new DocumentTypeController(mockRepo.Object, mapper);


            //Act
            var result = controller.GetDocumentTypes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);
        }

        [Fact]
        public void GetAllDocumentTypes_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypes()).Returns(GetDocumentTypes(1));

            var controller = new DocumentTypeController(mockRepo.Object, mapper);


            //Act
            var result = controller.GetDocumentTypes();

            //Assert
            Assert.IsType<Task<ActionResult<IEnumerable<DocumentTypeReadDTO>>>>(result);
        }

        [Fact]
        public async void GetDocumentTypeById_Returns404NotFound_WhenNoExistingIDProvidec()
        {
            //Arrange
            DocumentType documentType= null;
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(0)).
                Returns(Task.FromResult(documentType));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetDocumentTypeById(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async void GetDocumentTypeById_Returns200Ok_WhenExistingIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType { Id = 1, Name= "Mandat Supraveghere Thenica"}));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetDocumentTypeById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetDocumentTypeById_CitizenReadDto_WhenExistingIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType { Id = 1, Name = "Mandat Supraveghere Thenica" }));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetDocumentTypeById(1);

            //Assert
            Assert.IsType<ActionResult<DocumentTypeReadDTO>>(result);
        }


        [Fact]
        public async void CreateDocumentType_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType { Id = 1, Name = "Mandat Supraveghere Thenica" }));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.CreateDocumentType(new DocumentTypeCreateDTO { });

            //Assert
            Assert.IsType<ActionResult<DocumentTypeReadDTO>>(result);
        }


        [Fact]
        public async void UpdateDocumentType_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType { Id = 1, Name = "Mandat Supraveghere Thenica" }));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.UpdateDocumentType(1,new DocumentTypeUpdateDTO { });

            //Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async void UpdateDocumentType_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange

            DocumentType documentType = null;
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(0)).
                Returns(Task.FromResult(documentType));

            //mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.UpdateDocumentType(0, new DocumentTypeUpdateDTO{ });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteDocumentType_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType { Id = 1, Name= "Mandat Supraveghere Tehnica"}));
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.DeleteDocumentType(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async void DeleteDocumentType_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange

            DocumentType documentType = null;
            mockRepo.Setup(repo => repo.GetAllDocumentTypesById(0)).
                Returns(Task.FromResult(documentType));

            //mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new DocumentTypeController(mockRepo.Object, mapper);

            //Act
            var result = await controller.DeleteDocumentType(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }




        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }


        private async Task<IEnumerable<DocumentType>> GetDocumentTypes(int num)
        {
            var documentTypes = new List<DocumentType>();

            if (num > 0)
            {
                documentTypes.Add(
                    new DocumentType
                    {
                        Id = 0,
                        Name = "Mandat supraveghere tehnica"
                    }
                    );
            }
            return documentTypes;
        }
    }
}
