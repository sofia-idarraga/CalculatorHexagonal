using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Infrastructure.Data.Adapters;
using CalculatorHexagonal.Infrastructure.Data.Contexts;
using CalculatorHexagonal.Infrastructure.Data.Entities;
using CalculatorHexagonal.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace CalculatorHexagonal.UnitTests.Infrastructure
{
    public class OperationAdapterTests
    {
        private Mock<OperationDbContext> _mockDbContext;
        private OperationAdapter _operationAdapter;

        public OperationAdapterTests()
        {
            _mockDbContext = new Mock<OperationDbContext>(new DbContextOptions<OperationDbContext>());
            _operationAdapter = new OperationAdapter(_mockDbContext.Object);
        }

        [Fact]
        public async Task FindByDate_ReturnsOperationsBetweenDates()
        {
            DateTime initDate = new DateTime(2024, 6, 18, 10, 0, 0);
            DateTime endDate = new DateTime(2024, 6, 20, 10, 0, 0);

            var operationsList = OperationFixtures.GetTestOperations();
            var entityList = OperationFixtures.GetTestOperationEntities().AsQueryable();

            var mockDbSet = entityList.BuildMockDbSet();

            var expectedResult = Result<IEnumerable<Operation>>.Create(true, $"Found {operationsList.Count} operations between {initDate} and {endDate}.", operationsList);
           
            _mockDbContext.Setup(db => db.Operations).Returns(mockDbSet.Object);
            var result = await _operationAdapter.FindByDate(initDate, endDate);
            
            Assert.True(result.Success);
            Assert.Equal(expectedResult.Message, result.Message);
            Assert.Equal(expectedResult.Value.Count(), result.Value.Count());

            _mockDbContext.Verify(db => db.Operations, Times.Once);
        }

        [Fact]
        public async Task Save_SavesOperationSuccessfully()
        {
            var model = OperationFixtures.GetOperation();
            var entityList = OperationFixtures.GetTestOperationEntities().AsQueryable();

            var mockDbSet = entityList.BuildMockDbSet();
            _mockDbContext.Setup(db => db.Operations).Returns(mockDbSet.Object);

            var result = await _operationAdapter.Save(model);

            Assert.True(result.Success);
            Assert.Equal("Operation saved successfully.", result.Message);
            Assert.Equal(model, result.Value); 

            _mockDbContext.Verify(db => db.Operations.Add(It.IsAny<OperationEntity>()), Times.Once);
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task FindByDate_ReturnsErrorOnException()
        {
            DateTime initDate = new DateTime(2024, 6, 18, 10, 0, 0);
            DateTime endDate = new DateTime(2024, 6, 20, 10, 0, 0);

            _mockDbContext.Setup(db => db.Operations).Throws<InvalidOperationException>();
                     ;
            var result = await _operationAdapter.FindByDate(initDate, endDate);

            Assert.False(result.Success);
            Assert.Null(result.Value);

            _mockDbContext.Verify(db => db.Operations, Times.Once);
        }

        [Fact]
        public async Task Save_ReturnsErrorOnException()
        {
            var model = OperationFixtures.GetOperation();
            
            _mockDbContext.Setup(db => db.Operations).Throws<InvalidOperationException>();

            var result = await _operationAdapter.Save(model);

            Assert.False(result.Success);
            Assert.Null(result.Value);

            _mockDbContext.Verify(db => db.Operations.Add(It.IsAny<OperationEntity>()), Times.Never);
            _mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Never);
        }
    }
}
