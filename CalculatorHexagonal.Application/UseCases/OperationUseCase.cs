using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.UseCases;

namespace CalculatorHexagonal.Application.Services
{
    public class OperationUseCase : IOperationUseCase
    {

        private readonly IOperationAdapter _adapter;
        public OperationUseCase(IOperationAdapter adapter)
        {
            _adapter = adapter;
        }
        public async Task<Result<IEnumerable<Operation>>> FindByDate(DateTime initDate, DateTime endDate)
        {
            return await _adapter.FindByDate(initDate,endDate);
        }

        public async Task<Result<Operation>> Save(Operation operation)
        {
            return await _adapter.Save(operation);
        }
    }
}
