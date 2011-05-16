using System.Collections.Generic;

namespace XnaMobileUnit
{
    public class TestResults
    {
        public string TestClass;
        public Dictionary<string, TestEventArgs> PassedTests = new Dictionary<string, TestEventArgs>();
        public Dictionary<string, TestEventArgs> FailedTests = new Dictionary<string, TestEventArgs>();

        public void AddFailedTest(string testClass, TestEventArgs testEventArgs)
        {
            FailedTests.Add(testEventArgs.TestMethod, testEventArgs);
        }

        public void AddPassedTest(string testClass, TestEventArgs testEventArgs)
        {
            PassedTests.Add(testEventArgs.TestMethod, testEventArgs);
        }
    }
}