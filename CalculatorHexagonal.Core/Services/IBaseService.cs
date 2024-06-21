using CalculatorHexagonal.Core.Models;

namespace CalculatorHexagonal.Core.Services
{
    public interface IBaseService<TEntity>
    {
        Task<Result<IEnumerable<TEntity>>> FindByDate(DateTime initDate, DateTime endDate);
        Task<Result<TEntity>> Save(TEntity entity);
    }
}
