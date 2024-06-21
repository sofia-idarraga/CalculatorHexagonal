using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Core.Services;

namespace CalculatorHexagonal.Application.Services
{
    public class OperationService : IOperationService
    {

        private readonly IOperationAdapter _adapter;
        public OperationService(IOperationAdapter adapter)
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
