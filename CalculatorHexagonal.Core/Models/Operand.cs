namespace CalculatorHexagonal.Core.Models
{
    public class Operand
    {
        public dynamic? Value { get; set; }

        public Operand(dynamic? value)
        {
            Value = value;
        }

        public bool IsNull()
        {
            return Value == null;
        }

        public bool IsGreaterThan(dynamic threshold)
        {
            return Value > threshold;
        }
    }
}
