using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal.Entities.Vacant;
namespace StudentTransfer.Dal;

public class StudentTransferContext : DbContext
{
        /// <summary>
        /// Actual vacant list, sometimes needed to be updated (post to ../api/vacant/update)
        /// </summary>
        public DbSet<VacantDirection> VacantList { get; set; } = null!;

        public StudentTransferContext(DbContextOptions<StudentTransferContext> options) : base(options)
        {
        }
}