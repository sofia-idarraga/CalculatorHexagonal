namespace CalculatorHexagonal.Core.Models
{
    public class Operation
    {
        public string Operand1 { get; set; }
        public string Operand2 { get; set; }
        public string OperationType { get; set; }
        public string Total { get; set; }
        public DateTime CreationDate { get; set; }

        private Operation()
        {
            
        }

        private Operation(string operand1, string operand2, string total, string operationType)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Total = total;
            CreationDate = DateTime.Now;
            OperationType = operationType;
        }

        public static Operation Create(string operand1, string operand2, string total, string operationType)
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
