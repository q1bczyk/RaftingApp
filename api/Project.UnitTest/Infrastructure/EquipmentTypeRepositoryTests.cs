using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using Project.Core.Entities;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repositories;
using api;

namespace Project.UnitTest.Infrastructure
{
    public class EquipmentTypeRepositoryTests
    {
        private Mock<DataContext> _dbContextMock;
        private EquipmentTypeRepository _equipmentTypeRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<DataContext>(new DbContextOptions<DataContext>());
            _equipmentTypeRepository = new EquipmentTypeRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task AddAsync_ValidEquipmentType_ReturnAddedEquipmentType()
        {
            var newEquipmentType = new EquipmentType
            {
                TypeName = "Kajak dwuosobowy",
                MinParticipants = 2,
                MaxParticipants = 2,
                PricePerPerson = 100,
                Quantity = 10,
                PhotoUrl = "url",
            };

            var equipmentTypeDbSetMock = new Mock<DbSet<EquipmentType>>();

            _dbContextMock.Setup(db => db.Set<EquipmentType>())
                .Returns(equipmentTypeDbSetMock.Object);

            equipmentTypeDbSetMock.Setup(dbSet => dbSet.AddAsync(newEquipmentType, default))
                .ReturnsAsync((EntityEntry<EquipmentType>)null);

            var result = await _equipmentTypeRepository.Create(newEquipmentType);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(newEquipmentType));
        }

        [Test]
        public async Task GetByIdAsync_ReturnEquipmentType()
        {
            var equipmentTypeId = "testId";

            var equipmentType = new EquipmentType
            {
                Id = equipmentTypeId,
                TypeName = "Kajak dwuosobowy",
                MinParticipants = 2,
                MaxParticipants = 2,
                PricePerPerson = 100,
                Quantity = 10,
                PhotoUrl = "url",
            };

            var equipmentTypeDbSetMock = new Mock<DbSet<EquipmentType>>();

            var equipmentTypes = new List<EquipmentType> { equipmentType }.AsQueryable();
            equipmentTypeDbSetMock.As<IQueryable<EquipmentType>>().Setup(m => m.Provider).Returns(equipmentTypes.Provider);
            equipmentTypeDbSetMock.As<IQueryable<EquipmentType>>().Setup(m => m.Expression).Returns(equipmentTypes.Expression);
            equipmentTypeDbSetMock.As<IQueryable<EquipmentType>>().Setup(m => m.ElementType).Returns(equipmentTypes.ElementType);
            equipmentTypeDbSetMock.As<IQueryable<EquipmentType>>().Setup(m => m.GetEnumerator()).Returns(equipmentTypes.GetEnumerator());

            _dbContextMock.Setup(db => db.Set<EquipmentType>()).Returns(equipmentTypeDbSetMock.Object);

            _dbContextMock.Setup(m => m.Set<EquipmentType>().FindAsync(equipmentTypeId))
                          .ReturnsAsync(equipmentType);

            var result = await _equipmentTypeRepository.GetById(equipmentTypeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(equipmentTypeId));
        }

        [Test]
        public async Task GetByIdAsync_ThrowsNotFoundException_WhenIdDoesNotExist()
        {
            var nonExistentId = "nonExistentId";

            var equipmentTypeDbSetMock = new Mock<DbSet<EquipmentType>>();
            _dbContextMock.Setup(db => db.Set<EquipmentType>())
                        .Returns(equipmentTypeDbSetMock.Object);

            _dbContextMock.Setup(m => m.Set<EquipmentType>()
                          .FindAsync(nonExistentId))
                          .ReturnsAsync((EquipmentType)null);

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
                await _equipmentTypeRepository.GetById(nonExistentId));

            Assert.That(ex.Message, Is.EqualTo("Not found!"));
        }

        [Test]
        public async Task Update_ValidEquipmentType_ReturnUpdatedEquipmentType()
        {
            var equipmentTypeId = "testId";
            var existingEquipmentType = new EquipmentType
            {
                Id = equipmentTypeId,
                TypeName = "Kajak dwuosobowy",
                MinParticipants = 2,
                MaxParticipants = 2,
                PricePerPerson = 100,
                Quantity = 10,
                PhotoUrl = "url",
            };

            var updatedEquipmentType = new EquipmentType
            {
                Id = equipmentTypeId,
                TypeName = "Kajak trzyosobowy",
                MinParticipants = 3,
                MaxParticipants = 3,
                PricePerPerson = 150,
                Quantity = 5,
                PhotoUrl = "newUrl",
            };

            var equipmentTypeDbSetMock = new Mock<DbSet<EquipmentType>>();

            _dbContextMock.Setup(db => db.Set<EquipmentType>()).Returns(equipmentTypeDbSetMock.Object);

            _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            var result = await _equipmentTypeRepository.Update(updatedEquipmentType);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(equipmentTypeId));
            Assert.That(result.TypeName, Is.EqualTo(updatedEquipmentType.TypeName));
            Assert.That(result.PricePerPerson, Is.EqualTo(updatedEquipmentType.PricePerPerson));
        }

        [Test]
        public async Task Delete_ValidEquipmentType_ReturnsDeletedEquipmentType()
        {
            var equipmentTypeId = "testId";
            var equipmentTypeToDelete = new EquipmentType
            {
                Id = equipmentTypeId,
                TypeName = "Ponton czteroosobowy",
                MinParticipants = 4,
                MaxParticipants = 4,
                PricePerPerson = 200,
                Quantity = 8,
                PhotoUrl = "url3",
            };

            var equipmentTypeDbSetMock = new Mock<DbSet<EquipmentType>>();

            _dbContextMock.Setup(db => db.Set<EquipmentType>()).Returns(equipmentTypeDbSetMock.Object);

            equipmentTypeDbSetMock.Setup(dbSet => dbSet.Remove(equipmentTypeToDelete));

            _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            var result = await _equipmentTypeRepository.Delete(equipmentTypeToDelete);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(equipmentTypeId));

            equipmentTypeDbSetMock.Verify(dbSet => dbSet.Remove(equipmentTypeToDelete), Times.Once);

            _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Save_ReturnsFalse_WhenNoChangesAreSaved()
        {
            _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(0);

            var result = await _equipmentTypeRepository.Save();

            Assert.That(result, Is.False);
            _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Save_ReturnsTrue_WhenChangesAreSaved()
        {
            _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            var result = await _equipmentTypeRepository.Save();

            Assert.That(result, Is.True);
            _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}