using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Adapters
{
    public interface IBaseAdapter<TEntity>
    {
        Task<Result<IEnumerable<TEntity>>> FindByDate(DateTime initDate, DateTime endDate);
        Task<Result<TEntity>> Save(TEntity entity);
    }
}
