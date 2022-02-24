using AutoMapper;
using Moq;
using Tele2API.Data;
using Tele2API.Models;
using Tele2API.Profiles;
using Tele2API.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Tele2API.UnitTest
{
    public class CitizentsControllerTest
    {
        private readonly Mock<ICitizen> repositoryStub = new Mock<ICitizen>();
        private static MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CitizensProfile());
        });
        private IMapper mapper = new Mapper(mockMapper);

        [Fact]
        public void GetCitizenById_NonExistentElement_ReturnNotFound()
        {
            repositoryStub.Setup(repo => repo.GetCitizenById(It.IsAny<string>())).Returns((Citizen)null);

            var controller = new CitizensController(repositoryStub.Object, mapper);

            var result = controller.GetResidentById(It.IsAny<string>());

            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void GetCitizenById_Item_ReturnOk()
        {
            var expectedItem = new Citizen() { Id = "id1", Name = "John", Age = 19, Id_num = 1, Sex = "male" };

            repositoryStub.Setup(repo => repo.GetCitizenById(It.IsAny<string>())).Returns(expectedItem);

            var controller = new CitizensController(repositoryStub.Object, mapper);

            var result = controller.GetResidentById("id1");

            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetAllCitizens_NoParams_ReturnNotFound()
        {
            repositoryStub.Setup(repo => repo.GetCitizens()).Returns((List<Citizen>)null);

            var controller = new CitizensController(repositoryStub.Object, mapper);

            var result = controller.GetAllResidents();

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAllCitizens_NoParams_ReturnOk()
        {
            var expectedItem = this.GetAllCitizens();

            repositoryStub.Setup(repo => repo.GetCitizens()).Returns(expectedItem);

            var controller = new CitizensController(repositoryStub.Object, mapper);

            var result = controller.GetAllResidents();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Citizen> GetAllCitizens()
        {
            List <Citizen> citizens = new List<Citizen>();
            citizens.Add(new Citizen() { Id = "1", Name = "John", Age = 19, Id_num = 1, Sex = "male" });
            citizens.Add(new Citizen() { Id = "2", Name = "Maria", Age = 24, Id_num = 2, Sex = "female" });
            citizens.Add(new Citizen() { Id = "3", Name = "Nikita", Age = 19, Id_num = 3, Sex = "male" });
            citizens.Add(new Citizen() { Id = "4", Name = "Anna", Age = 34, Id_num = 4, Sex = "female" });
            citizens.Add(new Citizen() { Id = "5", Name = "Lebron", Age = 37, Id_num = 5, Sex = "male" });
            citizens.Add(new Citizen() { Id = "6", Name = "Polina", Age = 43, Id_num = 6, Sex = "female" });
            citizens.Add(new Citizen() { Id = "7", Name = "Lando", Age = 22, Id_num = 7, Sex = "male" });
            citizens.Add(new Citizen() { Id = "8", Name = "Gavi", Age = 19, Id_num = 8, Sex = "male" });

            return citizens;
        }
    }
}
