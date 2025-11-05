using HW_Azure_3.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW_Azure_3.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
