using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.Vacant;
namespace StudentTransfer.Dal;

public class StudentTransferContext : IdentityDbContext<AppUser, AppRole, Guid>
{
        /// <summary>
        /// Actual vacant list, sometimes needed to be updated (post to ../api/vacant/update)
        /// </summary>
        public DbSet<VacantDirection> VacantList { get; set; } = null!;
        
        public DbSet<ApplicationEntity> Applications { get; set; } = null!;
        public DbSet<FileEntity> Files { get; set; } = null!;

        public StudentTransferContext(DbContextOptions<StudentTransferContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

}