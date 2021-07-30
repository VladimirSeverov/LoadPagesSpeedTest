using LoadPagesSpeedTest.Models;
using LoadPagesSpeedTest.Repository;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoadPagesSpeedTest.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IRepository<Test> tests;
        private readonly IRepository<TestDetails> testDetails;
        public HistoryController(IRepository<Test> tests, IRepository<TestDetails> testDetails)
        {
            this.tests = tests;
            this.testDetails = testDetails;
        }

        public async Task<ActionResult> Index()
        {
            var tt = await tests.GetAll();
            return View(tt.OrderByDescending(x => x.TestDate).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            TestViewModel tw = new TestViewModel();
            tw.Test = await tests.FindById(id);
            tw.TestDetails = await testDetails.GetEntitiesByParentId(id);

            if(tw.TestDetails == null)
            {
                return HttpNotFound();
            }

            ViewBag.Labels = JsonConvert.SerializeObject(tw.TestDetails.Where(z => z.ResponseMinTime > -1).Select(x => x.Url).ToArray());
            ViewBag.DataPoints = JsonConvert.SerializeObject(tw.TestDetails.Where(z => z.ResponseMinTime > -1).Select(x => x.ResponseMaxTime).ToArray());

            return View(tw);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id) 
        {
            var tt = await tests.FindById(id);
            if (tt == null)
            {
                return HttpNotFound();
            }

            return View(tt);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int TestId)
        {
            var tt = await tests.FindById(TestId);
            await tests.Delete(tt);

            return RedirectToAction(nameof(Index));
        }
    }
}