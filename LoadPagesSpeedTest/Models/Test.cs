using System;
using System.Collections.Generic;

namespace LoadPagesSpeedTest.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string MainUrl { get; set; }
        public DateTime TestDate { get; set; }

        public List<TestDetails> TestDetails { get; set; }

        public Test()
        {
            TestDetails = new List<TestDetails>();
        }
    }
}