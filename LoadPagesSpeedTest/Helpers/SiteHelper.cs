using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using LoadPagesSpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Helpers
{
    public class SiteHelper : ISiteHelper
    {
        public List<string> GetSiteMap(string mainUrl)
        {
            Uri baseURI = new Uri(mainUrl);

            string html = DownloadString(mainUrl);
            List<string> res = new List<string>();

            IHtmlDocument angle = new HtmlParser().ParseDocument(html);
            string href = "";
            Uri newUri;
            foreach (IElement element in angle.QuerySelectorAll("a"))
            {
                href = element.GetAttribute("href");
                if (href == null) continue;

                if(Uri.TryCreate(baseURI, href, out newUri))
                {
                    if (newUri.IsFile || !baseURI.IsBaseOf(newUri) || !newUri.Scheme.Contains("http"))
                    {
                        continue;
                    }

                    if (!res.Contains(newUri.ToString()))
                    {
                        res.Add(newUri.ToString());
                    }
                }               
            }

            return res;
        }

        private string DownloadString(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                return httpClient.GetStringAsync(url).Result;
            }
            catch
            {
                return "";
            }
        }
        private byte[] DownloadBytes(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                return httpClient.GetByteArrayAsync(url).Result;
            }
            catch
            {
                return null;
            }
        }

        private int GetDownloadSpeed(string url)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var s = DownloadBytes(url);
            stopWatch.Stop();
            return s != null ? stopWatch.Elapsed.Milliseconds : -1;
        }

        private async Task<List<int>> GetTestResult(string url, int requestsCount=3)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < requestsCount; i++)
            {
                var ms = await Task.Run(() => GetDownloadSpeed(url));
                res.Add(ms);
                if (ms == -1) break;
            }
            return res;
        }

        public async Task<TestDetails> GetResultBySitemapItem(string url, int id)
        {
            var res = await GetTestResult(url);
            return new TestDetails() { Url = url, ResponseMinTime = res.Min(), ResponseMaxTime = res.Max(), ResponseAvgTime = Convert.ToInt32(res.Average()), TestId = id };
        }
    }
}