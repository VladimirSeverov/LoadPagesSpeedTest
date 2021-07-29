using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace LoadPagesSpeedTest.Models
{
    public class SiteWorker
    {
        public List<string> GetSiteMap(string mainUrl)
        {
            Uri baseURI = new Uri(mainUrl);

            string html = DownloadString(mainUrl);
            List<string> res = new List<string>();
            
            IHtmlDocument angle = new HtmlParser().ParseDocument(html);
            string href = "";
            foreach (IElement element in angle.QuerySelectorAll("a"))
            {
                href = element.GetAttribute("href");
                if (href == null) continue;
                if (href.Length <= 1) continue;
                if (!href.Contains("http"))
                {
                    Uri newURI = new Uri(baseURI, href);
                    href = newURI.ToString();
                }
                if (!res.Contains(href))
                {
                    res.Add(href);
                }
            }
                
            return res;
        }

        private string DownloadString(string url)
        {
            HttpClient httpClient = new HttpClient();
            return httpClient.GetStringAsync(url).Result;
        }
        private byte[] DownloadBytes(string url)
        {
            HttpClient httpClient = new HttpClient();
            return httpClient.GetByteArrayAsync(url).Result;
        }

        private int GetDownloadSpeed(string url)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var s = DownloadBytes(url);
            stopWatch.Stop();
            return stopWatch.Elapsed.Milliseconds;
        }

        private async Task<List<int>> GetTestResult(string url)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                var ms = await Task.Run(() => GetDownloadSpeed(url));
                res.Add(ms);
            }
            return res;
        }

        public async Task<TestDetails> GetResultBySitemapItem(string url, int id)
        {
            var res = await GetTestResult(url);

            return new TestDetails() { Url = url, ResponseMinTime = res.Min(), ResponseMaxTime = res.Max(), ResponseAvgTime = Convert.ToInt32(res.Average()), TestId=id };
        }
    }
}