using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public class MathService : IMathService
    {
        public Result<int> Sum(Operand operand1, Operand operand2)
        {
            return Result<int>.Create((int)operand1.Value! + (int)operand2.Value!);
        }
    }
}
