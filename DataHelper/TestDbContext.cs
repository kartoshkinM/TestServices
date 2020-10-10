using System.Data.Entity;

namespace DataHelper
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
            : base("name=TestDbContext")
        {
        }

        public virtual DbSet<MESSAGE> MESSAGES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}