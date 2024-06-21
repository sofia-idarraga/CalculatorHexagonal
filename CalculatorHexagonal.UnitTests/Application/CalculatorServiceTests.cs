using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;
using Moq;

namespace CalculatorHexagonal.UnitTests.Application
{
    public class CalculatorServiceTests
    {
        private readonly Mock<IOperationService> _mockOperationService;
        private readonly CalculatorService _calculatorService;

        public CalculatorServiceTests()
        {
            _mockOperationService = new Mock<IOperationService>();
            _calculatorService = new CalculatorService(_mockOperationService.Object);
        }

        [Fact]
        public async Task Sum_ReturnsError_WhenOperandsAreNull()
        {
            Operand operand1 = new Operand(2);
            Operand operand2 = new Operand(null);
            var result = await _calculatorService.Sum(operand1, operand2);

            Assert.False(result.Success);
            Assert.Equal("Operands cannot be null.", result.Message);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Sum_ReturnsError_WhenOperandsAreNegative()
        {
            var operand1 = new Operand(-5);
            var operand2 = new Operand(-3);

            var result = await _calculatorService.Sum(operand1, operand2);

            Assert.False(result.Success);
            Assert.Equal("Operands cannot be negative.", result.Message);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Sum_ReturnsSuccess_WhenSumIsCorrect_AndOperationIsSaved()
        {
            var operand1 = new Operand(5);
            var operand2 = new Operand(3);
            var expectedSum = 8;

            var operation = Operation.Create((int)operand1.Value, (int)operand2.Value, expectedSum, "sum");

            _mockOperationService.Setup(os => os.Save(It.IsAny<Operation>()))
                .ReturnsAsync(Result<Operation>.Create(true, "Operation saved successfully.", operation));

            var result = await _calculatorService.Sum(operand1, operand2);

            Assert.True(result.Success);
            Assert.Equal($"The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(expectedSum, result.Value.Total);
        }

        [Fact]
        public async Task Sum_ReturnsPartialSuccess_WhenSumIsCorrect_ButOperationSaveFails()
        {
            var operand1 = new Operand(5);
            var operand2 = new Operand(3);
            var expectedSum = 8;

            var operation = Operation.Create((int)operand1.Value, (int)operand2.Value, expectedSum, "sum");

            _mockOperationService.Setup(os => os.Save(It.IsAny<Operation>()))
                .ReturnsAsync(Result<Operation>.Create(false, "Operation save failed.", operation));

            var result = await _calculatorService.Sum(operand1, operand2);

            Assert.False(result.Success);
            Assert.Equal($"Operation saving failed.The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(expectedSum, result.Value.Total);
        }
    }

    
}
