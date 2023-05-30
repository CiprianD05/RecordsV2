using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Records.Controllers;
using RecordsDTOs.CitizensDTOs;
using RecordsDTOs.PsychologicalProfileDTOs;
using RecordsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsControllerTests
{
    public class PsychologicalProfilesControllerTests : IDisposable
    {
        Mock<RecordsRepositories.Interfaces.IPsychologicalProfileRepo> mockRepo;
        RecordsDTOs.Profiles.PsychologicalProfileProfiles.PsychologicalProfileAutomappingProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;


        public PsychologicalProfilesControllerTests()
        {
            mockRepo = new Mock<RecordsRepositories.Interfaces.IPsychologicalProfileRepo>();
            realProfile = new RecordsDTOs.Profiles.PsychologicalProfileProfiles.PsychologicalProfileAutomappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        [Fact]
        public void GetPsychologicalProfilesItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange            
            mockRepo.Setup(repo => repo.GetAllPsychologicalProfiles(0)).Returns(GetPsychologicalProfiles(0));
            var controller = new PsychologicalProfilesController(mapper,mockRepo.Object);

            //Act
            var result = controller.GetPsychProfilesByCitizenId(0);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);

        }


        [Fact]
        public void GetAllPsychologicalProfiles_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllPsychologicalProfiles(1)).Returns(GetPsychologicalProfiles(1));
            var controller = new PsychologicalProfilesController(mapper,mockRepo.Object);

            //Act
            var result = controller.GetPsychProfilesByCitizenId(1);

            //Assert
            var okResult = result.Result.Result as OkObjectResult;
            var psychProfiles = okResult.Value as List<PsychologicalProfileReadDTO>;
            Assert.Single(psychProfiles);
        }

        [Fact]
        public void GetAllPsychologicalProfiles_RetrunsOk_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllPsychologicalProfiles(1)).Returns(GetPsychologicalProfiles(1));
            var controller = new PsychologicalProfilesController( mapper, mockRepo.Object);

            //Act
            var result = controller.GetPsychProfilesByCitizenId(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);
        }

        [Fact]
        public void GetAllPsychologicalProfiles_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllPsychologicalProfiles(1)).Returns(GetPsychologicalProfiles(1));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = controller.GetPsychProfilesByCitizenId(1);

            //Assert
            Assert.IsType<Task<ActionResult<IEnumerable<PsychologicalProfileReadDTO>>>>(result);
        }

        [Fact]
        public async void GetPsychologicalProfileById_Returns404NotFound_WhenNoExistingIDProvidec()
        {
            //Arrange
            PsychologicalProfile psychProfule= null;
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(0)).
                Returns(Task.FromResult(psychProfule));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.GetPsychProfilesById(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetPsychologicalProfileById_Returns200Ok_WhenExistingIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile {
                    Id = 0,
                    CitizenId = 0,
                    
                    Summary = "asd"
                    
                }));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.GetPsychProfilesById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetPsychologicalProfileById_PsychologicalProfileReadDto_WhenExistingIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile
                {
                    Id = 0,
                    CitizenId = 0,

                    Summary = "asd"
                }));

            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.GetPsychProfilesById(1);

            //Assert
            Assert.IsType<ActionResult<PsychologicalProfileReadDTO>>(result);
        }

        [Fact]
        public async void CreatePsychologicalProfile_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile
                {
                    Id = 0,
                    CitizenId = 0,
                   
                    Summary = "asd"
                   
                }));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.CreatePsychologicalProfile(new PsychologicalProfileCreateDTO { });

            //Assert
            Assert.IsType<ActionResult<PsychologicalProfileReadDTO>>(result);
        }

        [Fact]
        public async void CreatePsychologicalProfile_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile
                {
                    Id = 0,
                    CitizenId = 0,
                   
                    Summary = "asd"
                }));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.CreatePsychologicalProfile(new PsychologicalProfileCreateDTO { });

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async void UpdatePsychologicalProfile_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile
                {
                    Id = 0,
                    CitizenId = 0,
                    
                    Summary = "asd"
                   
                }));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.UpdatePsychProfile(1,new PsychologicalProfileUpdateDTO { });

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void UpdatePsychologicalProfile_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange

            PsychologicalProfile psychProfile= null;
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(0)).
                Returns(Task.FromResult(psychProfile));

            //mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.UpdatePsychProfile(0, new PsychologicalProfileUpdateDTO{ });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeletePsychologicalProfile_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(1)).
                Returns(Task.FromResult(new PsychologicalProfile
                {
                    Id = 0,
                    CitizenId = 0,
                    
                    Summary = "asd"
                   
                }));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.DeletePsychologicalProfile(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeletePsychologicalProfile_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange
            PsychologicalProfile psychProfile = null;
            mockRepo.Setup(repo => repo.GetPsychologicalProfileById(0)).
                Returns(Task.FromResult(psychProfile));
            var controller = new PsychologicalProfilesController(mapper, mockRepo.Object);

            //Act
            var result = await controller.DeletePsychologicalProfile(0);

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
        private async Task<IEnumerable<PsychologicalProfile>> GetPsychologicalProfiles(int num)
        {
            var psychologicalProfiles = new List<PsychologicalProfile>();

            if (num > 0)
            {
                psychologicalProfiles.Add(
                    new PsychologicalProfile
                    {
                        Id = 0,
                        CitizenId= 0,
                        
                        Summary="asd"
                    }
                    );
            }
            return psychologicalProfiles;
        }
    }
}
