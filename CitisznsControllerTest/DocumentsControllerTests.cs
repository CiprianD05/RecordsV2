using Records.Controllers;
using Moq;
using AutoMapper;
using RecordsModels;
using Microsoft.AspNetCore.Mvc;
using RecordsDTOs.DocumentDTOs;
using Records.Functionalities.Interfaces;
using Records.Functionalities.ConcreteImpl;
using Microsoft.AspNetCore.Hosting;
using RecordsDTOs.DocumentTypeDTOs;




//_mapper = mapper;
//_documentRepo = repo;
//_documentTypeRepo = documentTypeRepo;
//_citizenRepo = citizenRepo;
//_folderManipulation = folderManipulation;

namespace RecordsControllerTests
{
    public class DocumentsControllerTests
    {
        Mock<RecordsRepositories.Interfaces.IDocumentRepo> documentMockRepo;
        Mock<RecordsRepositories.Interfaces.IDocumentTypeRepo> documentTypeMockRepo;
        Mock<RecordsRepositories.Interfaces.ICitizenRepo> citizenMockRepo;
        Mock<IWebHostEnvironment> mockEnviroment;
        RecordsDTOs.Profiles.DocumentsProfiles.DocumentsAutoMappingProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        Mock<IFolderManipulation> _folderManipulation;
        

        public DocumentsControllerTests()
        {
            documentMockRepo = new Mock<RecordsRepositories.Interfaces.IDocumentRepo>();
            documentTypeMockRepo = new Mock<RecordsRepositories.Interfaces.IDocumentTypeRepo>();
            citizenMockRepo = new Mock<RecordsRepositories.Interfaces.ICitizenRepo>();
            mockEnviroment = new Mock<IWebHostEnvironment>();
            mockEnviroment.Setup(m => m.EnvironmentName).Returns("Test");
            realProfile = new RecordsDTOs.Profiles.DocumentsProfiles.DocumentsAutoMappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            
            mapper = new Mapper(configuration);
            
            _folderManipulation = new Mock<IFolderManipulation>();
        }

        [Fact]
        public void GetDocumentItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange            
            documentMockRepo.Setup(repo => repo.GetAllDocuments(1)).Returns(GetDocument(0));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,               
               _folderManipulation.Object);

            //Act
            var result = controller.GetDocuments(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);
        }


        [Fact]
        public void GetAllDocuments_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocuments(1)).Returns(GetDocument(1));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);

            //Act
            var result = controller.GetDocuments(1);

            //Assert
            var okResult = result.Result.Result as OkObjectResult;
            var documents = okResult.Value as List<DocumentReadDTO>;
            Assert.Single(documents);
        }

        [Fact]
        public void GetAllDocuments_RetrunsOk_WhenDBHasOneResource()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocuments(1)).Returns(GetDocument(1));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);


            //Act
            var result = controller.GetDocuments(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);
        }

        [Fact]
        public void GetAllDocuments_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocuments(1)).Returns(GetDocument(1));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);


            //Act
            var result = controller.GetDocuments(1);

            //Assert
            Assert.IsType<Task<ActionResult<IEnumerable<DocumentReadDTO>>>>(result);
        }

        [Fact]
        public async void GetDocumentById_Returns404NotFound_WhenNoExistingIDProvidec()
        {
            //Arrange
            Document document = null;
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(0)).
                Returns(Task.FromResult(document));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);

            //Act
            var result = await controller.GetDocumentById(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetDocumentById_Returns200Ok_WhenExistingIdProvided()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(1)).
                Returns(Task.FromResult(new Document { Id = 1, Name = "Random" }));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);

            //Act
            var result = await controller.GetDocumentById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetDocumentById_CitizenReadDto_WhenExistingIdProvided()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(1)).
                Returns(Task.FromResult(new Document { Id = 1, Name = "Random" }));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
               documentTypeMockRepo.Object,
               citizenMockRepo.Object,
               _folderManipulation.Object);

            //Act
            var result = await controller.GetDocumentById(1);

            //Assert
            Assert.IsType<ActionResult<DocumentReadDTO>>(result);
        }


        [Fact]
        public async void CreateDocument_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(1)).
                Returns(Task.FromResult(new Document { Id = 1, Name = "Mandat Supraveghere Thenica" }));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
              documentTypeMockRepo.Object,
              citizenMockRepo.Object,
              _folderManipulation.Object);


            //Act
            var result = await controller.CreateDocument(1,1,new DocumentCreateDTO { });

            //Assert
            Assert.IsType<ActionResult<DocumentReadDTO>>(result);
        }


        [Fact]
        public async void UpdateDocument_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(1)).
                Returns(Task.FromResult(new Document { Id = 1, 
                Name = "Random",
                Citizen= new Citizen {Id=1, FirstName="asda",LastName="asdas"} ,
                DocumentType= new DocumentType { Id = 1,Name="Asdas",Abbreviation="asd"},
                DateAdded=DateTime.Now
                }));


            documentTypeMockRepo.Setup(repo => repo.GetAllDocumentTypesById(1)).
                Returns(Task.FromResult(new DocumentType
                {
                    Id = 1,
                    Name = "Random",
                    Abbreviation="asd"
                   
                }));

            citizenMockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1,FirstName="Jhonny", LastName="Bravo", SocialSecurityNumber = "123", PassportNumber = "123" }));

            _folderManipulation.Setup(fM => fM.CreateFoldersForDocument(new Document())).Returns("Ok");

            var controller = new DocumentsController(mapper, documentMockRepo.Object,
              documentTypeMockRepo.Object,
              citizenMockRepo.Object,
              _folderManipulation.Object);


            //Act
            var result = await controller.UpdateDocument(1, new DocumentUpdateDTO {Citizen=await citizenMockRepo.Object.GetAllCitizenById(1)
                ,DocumentType=await documentTypeMockRepo.Object.GetAllDocumentTypesById(1)}   );

            //Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async void UpdateDocument_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange

            Document document = null;
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(0)).
                Returns(Task.FromResult(document));

            //mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
              documentTypeMockRepo.Object,
              citizenMockRepo.Object,
              _folderManipulation.Object);


            //Act
            var result = await controller.UpdateDocument(0, new DocumentUpdateDTO { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteDocument_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(1)).
                Returns(Task.FromResult(new Document { Id = 1, Name = "Random" }));
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
              documentTypeMockRepo.Object,
              citizenMockRepo.Object,
              _folderManipulation.Object);


            //Act
            var result = await controller.DeleteDocument(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async void DeleteDocument_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange

            Document document = null;
            documentMockRepo.Setup(repo => repo.GetAllDocumentById(0)).
                Returns(Task.FromResult(document));

            //mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new DocumentsController(mapper, documentMockRepo.Object,
              documentTypeMockRepo.Object,
              citizenMockRepo.Object,
              _folderManipulation.Object);


            //Act
            var result = await controller.DeleteDocument(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private async Task<IEnumerable<Document>> GetDocument(int num)
        {
            var document = new List<Document>();

            if (num > 0)
            {
                document.Add(
                new Document
                {
                    Id = 1,
                    Name = "Random",
                    Citizen = new Citizen { Id = 1, FirstName = "asda", LastName = "asdas" },
                    DocumentType = new DocumentType { Id = 1, Name = "Asdas" }

                });
            }
            return document;
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
        private async Task<IEnumerable<Citizen>> GetCitizens(int num)
        {
            var citizens = new List<Citizen>();

            if (num > 0)
            {
                citizens.Add(
                    new Citizen
                    {
                        Id = 0,
                        FirstName = "Jonny",
                        LastName = " Bravo",
                        SocialSecurityNumber = "1234"
                    }
                    );
            }
            return citizens;
        }


        public void Dispose()
        {
            documentMockRepo = null;
            documentTypeMockRepo = null;
            citizenMockRepo = null;
            mockEnviroment = null;
            configuration = null;
            realProfile = null;
            mapper = null;
            _folderManipulation = null;
        }
    }
}
