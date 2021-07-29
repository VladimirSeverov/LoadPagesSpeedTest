using LoadPagesSpeedTest.Hubs;
using LoadPagesSpeedTest.Models;
using LoadPagesSpeedTest.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoadPagesSpeedTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Test> tests;
        private readonly IRepository<TestDetails> testDetails;
        private string UserConnectionId { get; set; }

        public HomeController(IRepository<Test> tests, IRepository<TestDetails> testDetails)
        {
            this.tests = tests;
            this.testDetails = testDetails;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new TestViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(string MainUrl, string UserConnectionId)
        {
            try
            {
                this.UserConnectionId = UserConnectionId;
                TestViewModel twm = new TestViewModel();
                twm.Test.MainUrl = MainUrl;

                await StartTestSite(twm);
                twm.TestDetails = twm.TestDetails.OrderByDescending(x => x.ResponseMaxTime).ToList();
                ViewBag.Labels = JsonConvert.SerializeObject(twm.TestDetails.Select(x => x.Url).ToArray());
                ViewBag.DataPoints = JsonConvert.SerializeObject(twm.TestDetails.Select(x => x.ResponseMaxTime).ToArray());

                return View(twm);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        private async Task StartTestSite(TestViewModel twm)
        {

            SiteWorker sw = new SiteWorker();

            List<string> siteMapUrls = sw.GetSiteMap(twm.Test.MainUrl);
            twm.Test.TestDate = DateTime.Now;
            twm.Test.TestId = await tests.Add(twm.Test);

            for (int i = 0; i < siteMapUrls.Count; i++)
            {
                var res = await sw.GetResultBySitemapItem(siteMapUrls[i], twm.Test.TestId);
                await testDetails.Add(res);
                twm.TestDetails.Add(res);
                SendItemResToView(res);
            }
        }

        private void SendItemResToView(TestDetails td)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<ViewHub>();
            context.Clients.Client(this.UserConnectionId).addItemResult(td);
        }
    }
}