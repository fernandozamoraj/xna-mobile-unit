using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XnaMobileUnit.FrameWork;

namespace XnaMobileUnit.Api
{
    public class TestFixtureRunner
    {
        private IList<TestFixture> _testFixtures = new List<TestFixture>();
        private TimeSpan _testDuration;

        public IList<TestFixture> TestFixtures
        {
            get { return _testFixtures; }
        }

        public List<string> GetTestExecutionTree()
        {
            List<string> testExecutionTree = new List<string>();

            int testsFailed = 0;
            int testsPassed = 0;

            foreach (var testFixture in TestFixtures)
            {
                testExecutionTree.Add(string.Format("{0} {1}", testFixture.ClassName, GetPassedOrFailed(testFixture)));

                foreach (var testPassed in testFixture.TestResults.PassedTests)
                {
                    testExecutionTree.Add(string.Format("   {0} Passed.", testPassed.Key));
                    testsPassed++;
                }

                foreach (var testFailed in testFixture.TestResults.FailedTests)
                {
                    testExecutionTree.Add(string.Format("   {0} Failed: {1}", testFailed.Value.TestMethod, testFailed.Value.Message));
                    testsFailed++;
                }
            }

            testExecutionTree.Insert(0, string.Format("Test took {0}{1}{2} to execute", GetMinutes((int)_testDuration.TotalMinutes), GetSeconds((int)_testDuration.Seconds), GetMilliseconds((int)_testDuration.Milliseconds)));
            testExecutionTree.Insert(1, string.Format("{0} tests passed", testsPassed));
            testExecutionTree.Insert(2, string.Format("{0} tests failed", testsFailed));

            return testExecutionTree;
        }

        private string GetPassedOrFailed(TestFixture testFixture)
        {
            string value = "Passed";

            if (testFixture.TestResults.FailedTests.Count > 0)
                value = "Failed";

            return value;
        }

        private string GetMinutes(int totalMinutes)
        {
            string minutes = string.Empty;

            if(totalMinutes > 0)
            {
                minutes = string.Format(" {0} minutes ", totalMinutes);
            }

            return minutes;
        }

        private string GetSeconds(int totalSeconds)
        {
            string seconds = string.Empty;

            if (totalSeconds > 0)
            {
                seconds = string.Format(" {0} seconds ", totalSeconds);
            }

            return seconds;
        }

        private string GetMilliseconds(int totalMilliseconds)
        {
            string milliSeconds = string.Empty;

            if (totalMilliseconds > 0)
            {
                milliSeconds = string.Format(" {0} miliseconds ", totalMilliseconds);
            }

            return milliSeconds;
        }

        public void RunTests(Assembly assembly)
        {
            DateTime started = DateTime.Now;

            foreach (TestFixture testFixture in GetTestFixtures(assembly))
            {
                _testFixtures.Add(testFixture);

                testFixture.Context();

                foreach (MethodInfo methodInfo in GetTestMethods(testFixture))
                {
                    testFixture.SetClassName(testFixture.GetType().Name);
                    testFixture.SetMethodName(methodInfo.Name);
                    testFixture.Setup();
                    methodInfo.Invoke(testFixture, null);
                    testFixture.TearDown();
                }
            }

            DateTime finished = DateTime.Now;

            _testDuration = finished - started;
        }

        private IEnumerable<MethodInfo> GetTestMethods(TestFixture testFixture)
        {
            Type t = testFixture.GetType();
            List<MethodInfo> _testMethods = new List<MethodInfo>();

            foreach (MethodInfo methodInfo in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                object[] testMethodAttributes =
                    methodInfo.GetCustomAttributes(typeof(TestMethodAttribute), true);

                if (testMethodAttributes.Count() > 0)
                {
                    _testMethods.Add(methodInfo);
                }
            }

            return _testMethods;
        }

        private IEnumerable<TestFixture> GetTestFixtures(Assembly assembly)
        {
            List<TestFixture> testFixtures = new List<TestFixture>();

            foreach (Type type in assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(TestFixture))))
            {
                object instance = Activator.CreateInstance(type) as TestFixture;
                
                if(instance != null)
                    testFixtures.Add((TestFixture)instance);
            }

            return testFixtures;
        }
    }
}