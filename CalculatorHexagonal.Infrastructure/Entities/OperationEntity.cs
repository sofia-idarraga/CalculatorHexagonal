using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CalculatorHexagonal.Infrastructure.Data.Entities
{
    [Table("operation")]
    public class OperationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("operand1")]
        public string Operand1 { get; set; }

        [Column("operand2")]
        public string Operand2 { get; set; }

        [Column("total")]
        public string Total { get; set; }

        [Column("operation_type")]
        public string OperationType { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }
    }
}
