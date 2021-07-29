using LoadPagesSpeedTest.Models;
using System.Data.Entity;

namespace LoadPagesSpeedTest.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<TestDetails> TestDetails { get; set; }
    }
}