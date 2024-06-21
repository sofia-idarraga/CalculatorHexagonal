namespace CalculatorHexagonal.Core.Models
{
    public class Operation
    {
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public string OperationType { get; set; }
        public int Total { get; set; }
        public DateTime CreationDate { get; set; }

        private Operation()
        {
            
        }

        private Operation(int operand1, int operand2, int total, string operationType)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Total = total;
            CreationDate = DateTime.Now;
            OperationType = operationType;
        }

        public static Operation Create(int operand1, int operand2, int total, string operationType)
        {
            Operation operation = new Operation(operand1, operand2, total, operationType);
            return operation;
        }

        public override string ToString()
        {
            return $"Operation: {OperationType}, Operand1: {Operand1}, Operand2: {Operand2}, Total: {Total}, Creation Date: {CreationDate}";
        }
    }
}
