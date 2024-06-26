using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public class MathService : IMathService
    {
        public Result<T> Sum<T>(Operand operand1, Operand operand2)
        {
            try
            {
                var total = operand1.Value! + operand2.Value!;
                return Result<T>.Create(total);
            }
            catch (Exception ex)
            {
                return Result<T>.Error($"Failed to sum the operands. Error: {ex.Message}");
            }

        }
    }
}
