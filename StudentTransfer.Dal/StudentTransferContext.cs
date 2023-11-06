using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal.Models.Vacant;

namespace StudentTransfer.Dal;

public class StudentTransferContext : DbContext
{

        public StudentTransferContext(DbContextOptions<StudentTransferContext> options) : base(options)
        {
        }
        
        
}