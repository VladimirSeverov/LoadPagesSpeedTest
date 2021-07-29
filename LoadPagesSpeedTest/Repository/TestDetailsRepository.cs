using LoadPagesSpeedTest.Context;
using LoadPagesSpeedTest.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Repository
{
    public class TestDetailsRepository : BaseEFRepository<TestDetails>
    {
        public TestDetailsRepository(AppDBContext context):base(context)
        {
        }

        public override async Task<List<TestDetails>> GetEntitiesByParentId(int id)
        {
            return await dbSet.Where(x => x.TestId == id).OrderByDescending(z=>z.ResponseMaxTime).ToListAsync();
        }
    }
}