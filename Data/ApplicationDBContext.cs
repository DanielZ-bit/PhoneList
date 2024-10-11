using Microsoft.EntityFrameworkCore;
using PhoneList2.Models;

namespace PhoneList2.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<PhoneList> phoneLists { get; set; }
       
    }
}
