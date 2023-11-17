using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal.Entities.Vacant;
namespace StudentTransfer.Dal;

public class StudentTransferContext : DbContext
{
        public DbSet<EducationDirection> VacantList { get; set; } = null!;

        public StudentTransferContext(DbContextOptions<StudentTransferContext> options) : base(options)
        {
        }
        
        
}