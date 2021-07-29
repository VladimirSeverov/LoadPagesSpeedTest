

namespace LoadPagesSpeedTest.Models
{
    public class TestDetails
    {
        public int Id { get; set; }
        public int? TestId { get; set; }
        public Test Test { get; set; }
        public string Url { get; set; }
        public int ResponseMinTime { get; set; }
        public int ResponseMaxTime { get; set; }
        public int ResponseAvgTime { get; set; }
    }
}