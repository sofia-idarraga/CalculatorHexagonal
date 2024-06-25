using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.UseCases
{
    public interface ICrudBaseUseCase<TEntity>
    {
        Task<Result<IEnumerable<TEntity>>> FindByDate(DateTime initDate, DateTime endDate);
        Task<Result<TEntity>> Save(TEntity entity);
    }
}
