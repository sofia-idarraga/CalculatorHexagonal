using CalculatorHexagonal.Application.Services;
using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;
using CalculatorHexagonal.Core.UseCases;
using Moq;
using System.Numerics;

namespace CalculatorHexagonal.UnitTests.Application
{
    public class CalculatorUseCaseTests
    {
        private readonly Mock<IOperationUseCase> _mockOperationUseCase;
        private readonly Mock<IValidationService> _mockValidationService;
        private readonly Mock<IMathService> _mockMathService;
        private readonly CalculatorUseCase _calculatorUseCase;

        public CalculatorUseCaseTests()
        {
            _mockOperationUseCase = new Mock<IOperationUseCase>();
            _mockValidationService = new Mock<IValidationService>();
            _mockMathService = new Mock<IMathService>();
            _calculatorUseCase = new CalculatorUseCase(_mockOperationUseCase.Object, _mockValidationService.Object, _mockMathService.Object);
        }

        [Fact]
        public async Task Sum_ReturnsError_WhenOperandsAreNull()
        {
            Operand operand1 = new Operand(2);
            Operand operand2 = new Operand(null);

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Error("Operands cannot be null."));

            var result = await _calculatorUseCase.Sum<int>(operand1, operand2);
                    
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

            var result = await _calculatorUseCase.Sum<int>(operand1, operand2);

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

            _mockMathService.Setup(ms => ms.Sum<int>(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<int>.Create(expectedSum));


            Operation operation = Operation.Create(Convert.ToString(operand1.Value), Convert.ToString(operand2.Value), Convert.ToString(expectedSum), "sum");

            _mockOperationUseCase.Setup(os => os.Save(It.IsAny<Operation>()))
                 .ReturnsAsync(Result<Operation>.Create(operation));

            var result = await _calculatorUseCase.Sum<int>(operand1, operand2);

            Assert.True(result.Success);
            Assert.Equal($"The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(Convert.ToString(expectedSum), result.Value.Total);
        }

        [Fact]
        public async Task Sum_ReturnsPartialSuccess_WhenSumIsCorrect_ButOperationSaveFails()
        {
            var operand1 = new Operand(5);
            var operand2 = new Operand(3);
            var expectedSum = 8;

            _mockValidationService.Setup(vs => vs.ValidateSum(It.IsAny<Operand>(), It.IsAny<Operand>()))
               .Returns(Result<bool>.Create(true));

            _mockMathService.Setup(ms => ms.Sum<int>(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<int>.Create(expectedSum));


            Operation operation = Operation.Create(Convert.ToString(operand1.Value), Convert.ToString(operand2.Value), Convert.ToString(expectedSum), "sum");

            _mockOperationUseCase.Setup(os => os.Save(It.IsAny<Operation>()))
                 .ReturnsAsync(Result<Operation>.Create(false, "Operation save failed.", operation));

            var result = await _calculatorUseCase.Sum<int>(operand1, operand2);         


            Assert.False(result.Success);
            Assert.Equal($"Operation saving failed.The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(Convert.ToString(expectedSum), result.Value.Total);
        }

        [Fact]
        public async Task SumComplex_ReturnsSuccess_WhenSumIsCorrect_AndOperationIsSaved()
        {
            var operand1 = new Operand(new Complex(1, 1));
            var operand2 = new Operand(new Complex(2, 2));
            var expectedSum = new Complex(3, 3);

            _mockValidationService.Setup(vs => vs.ValidateSumComplex(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<bool>.Create(true));

            _mockMathService.Setup(ms => ms.Sum<Complex>(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<Complex>.Create(expectedSum));

            Operation operation = Operation.Create(Convert.ToString(operand1.Value), Convert.ToString(operand2.Value), Convert.ToString(expectedSum), "sum complex");

            _mockOperationUseCase.Setup(os => os.Save(It.IsAny<Operation>()))
                .ReturnsAsync(Result<Operation>.Create(operation));

            var result = await _calculatorUseCase.SumComplex<Complex>(operand1, operand2);

            Assert.True(result.Success);
            Assert.Equal($"The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(Convert.ToString(expectedSum), result.Value.Total);
        }

        [Fact]
        public async Task SumComplex_ReturnsError_WhenOperandsAreInvalid()
        {
            var operand1 = new Operand(new Complex(1, 1));
            var operand2 = new Operand(new Complex(2, 2));

            _mockValidationService.Setup(vs => vs.ValidateSumComplex(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<bool>.Error("Invalid operands for complex sum."));

            var result = await _calculatorUseCase.SumComplex<Complex>(operand1, operand2);

            Assert.False(result.Success);
            Assert.Equal("Invalid operands for complex sum.", result.Message);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task SumComplex_ReturnsError_WhenOperationSavingFails()
        {
            var operand1 = new Operand(new Complex(1, 1));
            var operand2 = new Operand(new Complex(2, 2));
            var expectedSum = new Complex(3, 3);

            _mockValidationService.Setup(vs => vs.ValidateSumComplex(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<bool>.Create(true));

            _mockMathService.Setup(ms => ms.Sum<Complex>(It.IsAny<Operand>(), It.IsAny<Operand>()))
                .Returns(Result<Complex>.Create(expectedSum));


            _mockOperationUseCase.Setup(os => os.Save(It.IsAny<Operation>()))
                .ReturnsAsync(Result<Operation>.Error("Failed to save operation."));

            var result = await _calculatorUseCase.SumComplex<Complex>(operand1, operand2);

            Assert.False(result.Success);
            Assert.Equal($"Operation saving failed. The result of the sum is: {expectedSum}", result.Message);
            Assert.NotNull(result.Value);
            Assert.Equal(Convert.ToString(expectedSum), result.Value.Total);
        }
    }

    
}
