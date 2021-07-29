using LoadPagesSpeedTest.Context;
using LoadPagesSpeedTest.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Repository
{
    public class TestRepository : BaseEFRepository<Test>
    {
        private readonly DbSet<Test> tests;
        public TestRepository(AppDBContext context):base(context)
        {
            this.tests = context.Tests;
        }

        public override async Task<int> Add(Test entity)
        {
            tests.Add(entity);
            await db.SaveChangesAsync();
            return entity.TestId;
        }

        public override async Task Delete(Test entity)
        {
            var td = await db.TestDetails.Where(x => x.TestId == entity.TestId).ToListAsync();
            db.TestDetails.RemoveRange(td);
            await base.Delete(entity);
        }
    }
}