using System;

namespace XnaMobileUnit.FrameWork
{
    public abstract class TestFixture
    {
        protected Assert Assert = new Assert();
        public abstract void Context();
        public virtual void Setup()
        {
        }

        private TestResults _testResults = new TestResults();

        public TestResults TestResults
        {
            get { return _testResults; }
        }

        public string ClassName;

        public TestFixture()
        {
            Assert.FailedTest += Assert_FailedTest;
            Assert.PassedTest += Assert_PassedTest;
        }

        void Assert_PassedTest(object sender, TestEventArgs e)
        {
            _testResults.AddPassedTest(e.TestClass, e);
        }

        void Assert_FailedTest(object sender, TestEventArgs e)
        {
            _testResults.AddFailedTest(e.TestClass, e);
        }

        internal  void SetMethodName(string methodName)
        {
            Assert.SetMethodName(methodName);
        }

        internal void SetClassName(string className)
        {
            ClassName = className;
            Assert.SetClassName(className);
        }

        public virtual void TearDown()
        {
            
        }
    }
}
