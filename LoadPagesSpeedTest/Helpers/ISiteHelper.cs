using LoadPagesSpeedTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Helpers
{
    public interface ISiteHelper
    {
        List<string> GetSiteMap(string mainUrl);
        //string DownloadString(string url);
        //byte[] DownloadBytes(string url);
        //int GetDownloadSpeed(string url);

        //Task<List<int>> GetTestResult(string url);
        Task<TestDetails> GetResultBySitemapItem(string url, int id);
    }
}
