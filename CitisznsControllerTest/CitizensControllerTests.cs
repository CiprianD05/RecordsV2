using Records.Controllers;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using RecordsModels;
using RecordsRepositories;
using RecordsDTOs.Profiles;
using Microsoft.AspNetCore.Mvc;
using RecordsDTOs.CitizensDTOs;

namespace CitisznsControllerTest
{
    public class CitizensControllerTests:IDisposable
    {
        Mock<RecordsRepositories.Interfaces.ICitizenRepo> mockRepo;
        RecordsDTOs.Profiles.CitizensProfiles.CitizenAutoMappingProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CitizensControllerTests()
        {
            mockRepo = new Mock<RecordsRepositories.Interfaces.ICitizenRepo>();
            realProfile = new RecordsDTOs.Profiles.CitizensProfiles.CitizenAutoMappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        

        [Fact]
        public void GetCitizenItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange            
            mockRepo.Setup(repo => repo.GetAllCitizens()).Returns(GetCitizens(0));            
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void GetAllCitizens_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizens()).Returns(GetCitizens(1));
            var controller= new CitizensController(mockRepo.Object, mapper);

            //Act
            var result=controller.Get();

            //Assert
            var okResult=result.Result as OkObjectResult;
            var citizens = okResult.Value as List<CitizenReadDTO>;
            Assert.Single(citizens);
        }


        [Fact]
        public void GetAllCitizens_RetrunsOk_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizens()).Returns(GetCitizens(1));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result= controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCitizens_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizens()).Returns(GetCitizens(1));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = controller.Get();
            
            //Assert
            Assert.IsType<ActionResult<IEnumerable<CitizenReadDTO>>>(result);
        }

        [Fact]
        public async void GetCitizenById_Returns404NotFound_WhenNoExistingIDProvidec()
        {
            //Arrange
            Citizen citizen = null;
            mockRepo.Setup(repo => repo.GetAllCitizenById(0)).
                Returns(Task.FromResult(citizen)); 
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetCitizenById(0);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetCitizenById_Returns200Ok_WhenExistingIdProvided() 
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id=1, SocialSecurityNumber="123",PassportNumber="123"}));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result= await controller.GetCitizenById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async void GetCitizenById_CitizenReadDto_WhenExistingIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1, SocialSecurityNumber = "123", PassportNumber = "123" }));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.GetCitizenById(1);

            //Assert
            Assert.IsType<ActionResult<CitizenReadDTO>>(result);
        }

        [Fact]
        public async void CreateCitizen_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1, SocialSecurityNumber = "123", PassportNumber = "123" }));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.CreateCitizen(new CitizenCreateDTO { });

            //Assert
            Assert.IsType<ActionResult<CitizenReadDTO>>(result);
        }

        [Fact]
        public async void CreateCitizen_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1, SocialSecurityNumber = "123", PassportNumber = "123" }));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.CreateCitizen(new CitizenCreateDTO { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public async void UpdateCitizen_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1, SocialSecurityNumber = "123", PassportNumber = "123" }));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.UpdateCitizen(1,new CitizenUpdateDTO { });

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void UpdateCitizen_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(0)).Returns(() => null);
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.UpdateCitizen(0, new CitizenUpdateDTO { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async void DeleteCitizen_Returns200Ok_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCitizenById(1)).
                Returns(Task.FromResult(new Citizen { Id = 1, SocialSecurityNumber = "123", PassportNumber = "123" }));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.DeleteCitizen(1);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public  async void DeleteCitizen_Returns404NotFound_WhenNonexistendIdProvided()
        {
            //Arrange
            Citizen citizen = null;
            mockRepo.Setup(repo => repo.GetAllCitizenById(0)).
                Returns(Task.FromResult(citizen));
            var controller = new CitizensController(mockRepo.Object, mapper);

            //Act
            var result = await controller.DeleteCitizen(0);

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

        private List<Citizen> GetCitizens(int num)
        {
            var citizens = new List<Citizen>();

            if (num > 0)
            {
                citizens.Add(
                    new Citizen
                    {
                        Id = 0,
                        Name="Jonny Bravo",
                        SocialSecurityNumber="1234"
                    }
                    );
            }
            return citizens;
        }
    }
}