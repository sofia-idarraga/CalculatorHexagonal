using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.UnitTests.Utils;
using Moq;

namespace CalculatorHexagonal.UnitTests.Application
{
    public class OperationServiceTests
    {
        private readonly Mock<IOperationAdapter> _mockAdapter;
        private readonly OperationService _operationService;

        public OperationServiceTests()
        {
            
            _mockAdapter = new Mock<IOperationAdapter>();
            _operationService = new OperationService(_mockAdapter.Object);
        }

        [Fact]
        public async Task FindByDate_ReturnsOperations()
        {

            DateTime initDate = new DateTime(2024, 06, 18, 10, 0, 0);
            DateTime endDate = new DateTime(2024, 06, 20, 10, 0, 0);

            IEnumerable<Operation> testOperations = OperationFixtures.GetTestOperations();
            
            var expectedResult = Result<IEnumerable<Operation>>.Create(true, "found", testOperations);
            _mockAdapter
                .Setup(adapter => adapter.FindByDate(initDate, endDate))
                .ReturnsAsync(expectedResult);

            var result = await _operationService.FindByDate(initDate, endDate);
            
            Assert.Equal(expectedResult.Success, result.Success);
            Assert.Equal(expectedResult.Message, result.Message);

        }

        [Fact]
        public async Task Save_ReturnsResultWithOperation()
        {
            var operation = OperationFixtures.GetOperation();

            var expectedResult = Result<Operation>.Create(true, "saved", operation);

            _mockAdapter.Setup(a => a.Save(operation)).ReturnsAsync(expectedResult);

            var result = await _operationService.Save(operation);

            Assert.Equal(expectedResult.Success, result.Success);
            Assert.Equal(expectedResult.Message, result.Message);
        }
    }
}
