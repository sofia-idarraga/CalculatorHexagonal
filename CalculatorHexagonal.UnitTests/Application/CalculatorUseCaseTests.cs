using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;
using CalculatorHexagonal.Core.UseCases;
using Moq;

namespace CalculatorHexagonal.UnitTests.Application
{
    public class CalculatorUseCaseTests
    {
        private readonly Mock<IOperationUseCase> _mockOperationService;
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly Mock<IMathService> _mockMathService;
        private readonly CalculatorUseCase _calculatorService;

        public CalculatorUseCaseTests()
        {
            _mockOperationService = new Mock<IOperationUseCase>();
            _mockValidationService = new Mock<IValidationService>();
            _mockMathService = new Mock<IMathService>();
            _calculatorService = new CalculatorUseCase(_mockOperationService.Object, _mockValidationService.Object, _mockMathService.Object);
        }

        [Fact]
        public async Task Sum_ReturnsError_WhenOperandsAreNull()
        {
            Operand operand1 = new Operand(2);
            Operand operand2 = new Operand(null);

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Error("Operands cannot be null."));

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

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Error("Operands cannot be negative."));

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

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Create(true));

            _mockMathService.Setup(ms => ms.Sum(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<int>.Create(expectedSum));


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

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Create(true));

            _mockMathService.Setup(ms => ms.Sum(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<int>.Create(expectedSum));


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
