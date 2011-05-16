using System;

namespace XnaMobileUnit
{
    public class Assert
    {
        private string _className;
        private string _methodName;

        public event EventHandler<TestEventArgs> FailedTest;
        public event EventHandler<TestEventArgs> PassedTest;
        public void SetClassName(string className)
        {
            _className = className;
        }
        public void SetMethodName(string methodName)
        {
            _methodName = methodName;
        }

        public void IsTrue(bool test, string message)
        {
            if(!test)
            {
                FailedTest(this, new TestEventArgs {Message = message, TestClass = _className, TestMethod = _methodName});
            }
            else
            {
                PassedTest(this, new TestEventArgs{Message = message, TestClass = _className, TestMethod = _methodName});
            }
        }

        public void IsFalse(bool test, string message)
        {
            IsTrue(!test, message);
        }

        public void AreEqual<T>(T a, T b, string message)
        {
            IsTrue(a.Equals(b), message);
        }

        public void AreNotEqual<T>(T a, T b, string message)
        {
            IsFalse(a.Equals(b), message);
        }
    }
}