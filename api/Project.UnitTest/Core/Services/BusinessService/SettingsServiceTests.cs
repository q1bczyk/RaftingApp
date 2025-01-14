using Moq;
using NUnit.Framework;
using Project.Core.DTO.SettingsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Services.BusinessService;

namespace Project.UnitTest.Core
{
    public class SettingsServiceTests
    {
        private Mock<ISettingsRepository> _settingsRepositoryMock;
        private Mock<IBaseMapper<BaseSettingsDTO, Settings>> _toModelMapperMock;
        private Mock<IBaseMapper<Settings, GetSettingsDTO>> _toDTOMapperMock;
        private SettingsService _settingsService;

        [SetUp]
        public void Setup()
        {
            _settingsRepositoryMock = new Mock<ISettingsRepository>();
            _toModelMapperMock = new Mock<IBaseMapper<BaseSettingsDTO, Settings>>();
            _toDTOMapperMock = new Mock<IBaseMapper<Settings, GetSettingsDTO>>();

            _settingsService = new SettingsService(
                _settingsRepositoryMock.Object,
                _toModelMapperMock.Object,
                _toDTOMapperMock.Object
            );
        }

        [Test]
        public async Task GetSettings_ShouldReturnGetSettingsDTO_WhenSettingsExist()
        {
            // Arrange
            var settings = new Settings { /* initialize properties */ };
            var expectedDTO = new GetSettingsDTO { /* initialize properties */ };

            _settingsRepositoryMock.Setup(repo => repo.GetSettingsAsync()).ReturnsAsync(settings);
            _toDTOMapperMock.Setup(mapper => mapper.MapToModel(settings)).Returns(expectedDTO);

            // Act
            var result = await _settingsService.GetSettings();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedDTO));
            _settingsRepositoryMock.Verify(repo => repo.GetSettingsAsync(), Times.Once);
            _toDTOMapperMock.Verify(mapper => mapper.MapToModel(settings), Times.Once);
        }

        [Test]
        public async Task GetSettings_ShouldReturnNull_WhenSettingsDoNotExist()
        {
            // Arrange
            Settings settings = null; 
            GetSettingsDTO expectedDTO = null; 

            _settingsRepositoryMock.Setup(repo => repo.GetSettingsAsync()).ReturnsAsync(settings);
            _toDTOMapperMock.Setup(mapper => mapper.MapToModel(settings)).Returns(expectedDTO);

            // Act
            var result = await _settingsService.GetSettings();

            // Assert
            Assert.That(result, Is.Null);
            _settingsRepositoryMock.Verify(repo => repo.GetSettingsAsync(), Times.Once);
            _toDTOMapperMock.Verify(mapper => mapper.MapToModel(settings), Times.Once);
        }
    }
}