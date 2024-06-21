using CalculatorHexagonal.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculatorHexagonal.Infrastructure.Data.Contexts
{
    public class OperationDbContext : DbContext
    {
        
        public OperationDbContext(DbContextOptions<OperationDbContext> options) : base(options) { }

        public virtual DbSet<OperationEntity> Operations { get; set; }

    }
}
