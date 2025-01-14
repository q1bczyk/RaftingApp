using Moq;
using NUnit.Framework;
using Project.Core.Entities;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.BusinessService;
using Project.Core.DTO.ReservationsDTO;

namespace Project.UnitTest.Core
{
    public class ReservationEquipmentServiceTests
    {
        private Mock<IReservationEquipmentRepository> _repositoryMock;
        private Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>> _equipmentTypeMapperMock;
        private Mock<IFileService> _fileServiceMock;
        private ReservationEquipmentService _reservationEquipmentService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IReservationEquipmentRepository>();
            _equipmentTypeMapperMock = new Mock<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>>();
            _fileServiceMock = new Mock<IFileService>();
            _reservationEquipmentService = new ReservationEquipmentService(_repositoryMock.Object, _equipmentTypeMapperMock.Object, _fileServiceMock.Object);
        }

        [Test]
        public async Task FetchAvaiableEquipment_ValidDto_ReturnsMappedEquipmentList()
        {
            var reservationDetailsDTO = new ReservationDetailsDTO
            {

            };

            var availableEquipment = new List<EquipmentType>
            {
                new EquipmentType { Id = "1", TypeName = "Ponton", PhotoUrl = "photo1.jpg" },
                new EquipmentType { Id = "2", TypeName = "Kajak", PhotoUrl = "photo2.jpg" }
            };

            _repositoryMock.Setup(repo => repo.GetAvaiableEquipmentAsync(reservationDetailsDTO))
                .ReturnsAsync(availableEquipment);

            var mappedEquipmentTypes = new List<GetEquipmentTypeDTO>
            {
                new GetEquipmentTypeDTO { Id = "1", TypeName = "Ponton", PhotoUrl = "photo1.jpg" },
                new GetEquipmentTypeDTO { Id = "2", TypeName = "Kajak", PhotoUrl = "photo2.jpg" }
            };

            _equipmentTypeMapperMock.Setup(mapper => mapper.MapToList(availableEquipment)).Returns(mappedEquipmentTypes);

            _fileServiceMock.Setup(fileService => fileService.GeneratePublicLink(It.IsAny<string>()))
                .ReturnsAsync((string photoUrl) => $"https://publiclink.com/{photoUrl}");

            var result = await _reservationEquipmentService.FetchAvaiableEquipment(reservationDetailsDTO);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].TypeName, Is.EqualTo("ponton"));
            Assert.That(result[0].PhotoUrl, Is.EqualTo("https://publiclink.com/photo1.jpg"));
            Assert.That(result[1].TypeName, Is.EqualTo("kajak"));
            Assert.That(result[1].PhotoUrl, Is.EqualTo("https://publiclink.com/photo2.jpg"));

            _repositoryMock.Verify(repo => repo.GetAvaiableEquipmentAsync(reservationDetailsDTO), Times.Once);
            _equipmentTypeMapperMock.Verify(mapper => mapper.MapToList(availableEquipment), Times.Once);
            _fileServiceMock.Verify(fileService => fileService.GeneratePublicLink("photo1.jpg"), Times.Once);
            _fileServiceMock.Verify(fileService => fileService.GeneratePublicLink("photo2.jpg"), Times.Once);
        }

    }
}