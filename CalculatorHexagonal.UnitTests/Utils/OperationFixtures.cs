using CalculatorHexagonal.Core.Models;
using CalculatorHexagonal.Infrastructure.Data.Entities;

namespace CalculatorHexagonal.UnitTests.Utils
{
    internal class OperationFixtures
    {

        public static List<Operation> GetTestOperations()
        {
            return new List<Operation>()
            {
                 Operation.Create(1,2,3,"sum"),
                 Operation.Create(5,2,7,"sum"),
                 Operation.Create(123,0,124,"sum")
            };
        }

        public static Operation GetOperation()
        {
            return Operation.Create(1, 2, 3, "sum");
        }

        public static OperationEntity GetOperationEntity()
        {
            return ToEntity( Operation.Create(1, 2, 3, "sum"));
        }

        public static List<OperationEntity> GetTestOperationEntities()
        {
            var operations = GetTestOperations();
            return operations.Select(ToEntity).ToList();            
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
