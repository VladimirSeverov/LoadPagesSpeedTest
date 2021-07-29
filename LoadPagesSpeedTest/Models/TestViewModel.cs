using System.Collections.Generic;

namespace LoadPagesSpeedTest.Models
{
    public class TestViewModel
    {
        public Test Test { get; set; }
        //public string MainUrl { get; set; }
        public List<TestDetails> TestDetails { get; set; }
       
        public TestViewModel()
        {
            TestDetails = new List<TestDetails>();
            Test = new Test();
        }
    }
}