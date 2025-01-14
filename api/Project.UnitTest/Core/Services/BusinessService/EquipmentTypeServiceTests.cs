using Moq;
using NUnit.Framework;
using Project.Core.Entities;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.BusinessService;
using Microsoft.AspNetCore.Http;

namespace Project.UnitTest
{
    public class EquipmentTypeServiceTests
    {
        private Mock<IEquipmentTypeRepository> _repositoryMock;
        private Mock<IBaseMapper<AddEquipmentTypeDTO, EquipmentType>> _toModelMapperMock;
        private Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>> _toDTOMapperMock;
        private Mock<IFileService> _fileServiceMock;
        private EquipmentTypeService _equipmentTypeService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IEquipmentTypeRepository>();
            _toModelMapperMock = new Mock<IBaseMapper<AddEquipmentTypeDTO, EquipmentType>>();
            _toDTOMapperMock = new Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>>();
            _fileServiceMock = new Mock<IFileService>();
            _equipmentTypeService = new EquipmentTypeService(_repositoryMock.Object, _toModelMapperMock.Object, _toDTOMapperMock.Object, _fileServiceMock.Object);
        }

        [Test]
        public async Task Create_ValidDto_CallsUploadFileAndMap()
        {
            var addEquipmentTypeDTO = new AddEquipmentTypeDTO
            {
                TypeName = "Ponton czteroosobowy",
                MinParticipants = 4,
                MaxParticipants = 4,
                PricePerPerson = 200,
                Quantity = 8,
                file = new Mock<IFormFile>().Object
            };

            var newEquipmentType = new EquipmentType { Id = "1", TypeName = "Ponton czteroosobowy" };

            _fileServiceMock.Setup(fileService => fileService.Upload(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync("url");
            _toModelMapperMock.Setup(mapper => mapper.MapToModel(addEquipmentTypeDTO)).Returns(newEquipmentType);

            await _equipmentTypeService.Create(addEquipmentTypeDTO);

            _toModelMapperMock.Verify(mapper => mapper.MapToModel(addEquipmentTypeDTO), Times.Once);
        }

        [Test]
        public async Task GetAll_ReturnsMappedDtoList()
        {
            var equipmentTypes = new List<EquipmentType>
            {
                new EquipmentType { Id = "1", TypeName = "Ponton", PhotoUrl = "photo1.jpg" },
                new EquipmentType { Id = "2", TypeName = "Kajak", PhotoUrl = "photo2.jpg" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(equipmentTypes);

            _toDTOMapperMock.Setup(mapper => mapper.MapToList(equipmentTypes))
                .Returns(new List<GetEquipmentTypeDTO>
                {
                    new GetEquipmentTypeDTO { TypeName = "Ponton" },
                    new GetEquipmentTypeDTO { TypeName = "Kajak" }
                });

            var result = await _equipmentTypeService.GetAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TypeName, Is.EqualTo("ponton"));
            _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        
    }
}