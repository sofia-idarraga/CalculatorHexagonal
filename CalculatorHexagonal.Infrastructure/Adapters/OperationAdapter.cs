using CalculatorHexagonal.Core.Adapters;
using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Infrastructure.Data.Contexts;
using CalculatorHexagonal.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculatorHexagonal.Infrastructure.Data.Adapters
{
    public class OperationAdapter : IOperationAdapter
    {
        private readonly OperationDbContext dbContext;    

        public OperationAdapter(OperationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<Operation>>> FindByDate(DateTime initDate, DateTime endDate)
        {
            try
            {
                var operations = await dbContext.Operations
                    .Where(o => o.CreationDate >= initDate && o.CreationDate <= endDate)
                    .ToListAsync();

                return Result<IEnumerable<Operation>>.Create(true, $"Found {operations.Count} operations between {initDate} and {endDate}.", operations.Select(toModel));
            }
            catch (Exception ex)
            {
            
                return Result<IEnumerable<Operation>>.Error($"Failed to retrieve operations between {initDate} and {endDate}. Error: {ex.Message}");
            }
        }

        public async Task<Result<Operation>> Save(Operation model)
        {
            try
            {
                var operationEntity = new OperationEntity
                {
                    Operand1 = model.Operand1,
                    Operand2 = model.Operand2,
                    Total = model.Total,
                    OperationType = model.OperationType,
                    CreationDate = model.CreationDate
                };

                dbContext.Operations.Add(operationEntity);
                await dbContext.SaveChangesAsync();

                return Result<Operation>.Create(true, "Operation saved successfully.", model);
            }
            catch (Exception ex)
            {               
                return Result<Operation>.Error($"Failed to save the operation. Error: {ex.Message}");
            }
        }

        private static Operation toModel(OperationEntity entity)
        {
            Operation model = Operation.Create(entity.Operand1, entity.Operand2, entity.Total, entity.OperationType);
            model.CreationDate = entity.CreationDate;
            return model;
        }

        private static OperationEntity ToEntity(Operation model)
        {
            return new OperationEntity
            {
                Operand1 = model.Operand1,
                Operand2 = model.Operand2,
                Total = model.Total,
                OperationType = model.OperationType,
                CreationDate = model.CreationDate
            };
        }
    }
}
