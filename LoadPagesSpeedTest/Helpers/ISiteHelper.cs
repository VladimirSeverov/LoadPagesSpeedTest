using LoadPagesSpeedTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Helpers
{
    public interface ISiteHelper
    {
        List<string> GetSiteMap(string mainUrl);
        Task<TestDetails> GetResultBySitemapItem(string url, int id);
    }
}
