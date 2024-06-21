namespace CalculatorHexagonal.Core.Models
{
    public class Operand
    {
        public int? Value { get; }

        public Operand(int? value)
        {
            Value = value;
        }

        public bool IsNull()
        {
            return Value == null;
        }

        public bool IsGreaterThan(int threshold)
        {
            return Value > threshold;
        }
    }
}
