namespace CalculatorHexagonal.Core.Models
{
    public class Result<TEntity> 
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TEntity? Value { get; set; }

        private Result()
        {
            
        }

        private Result(bool success, string? message, TEntity? entity)
        {
            Success = success;
            Message = message;
            Value = entity;
        }

        public static Result<TEntity> Create(TEntity? entity)
        {
            var result = new Result<TEntity>(true, "", entity);
            return result;
        }

        public static Result<TEntity> Create(bool success, string? message, TEntity? entity)
        {
            var result = new Result<TEntity>(success,message,entity);
            return result;
        }

        public static Result<TEntity> Error(string? message)
        {
            var result = new Result<TEntity>(false, message, default);
            return result;
        }

    }
}
