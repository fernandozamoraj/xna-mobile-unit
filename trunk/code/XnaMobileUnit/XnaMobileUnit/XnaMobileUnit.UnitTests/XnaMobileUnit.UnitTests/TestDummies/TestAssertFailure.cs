using XnaMobileUnit.FrameWork;

namespace XnaMobileUnit.UnitTests.TestDummies
{
    public class TestAssertFailure : XnaMobileUnit.FrameWork.TestFixture 
    {
        public override void Context()
        {
            
        }

        [TestMethod]
        public void TestAssertIsFalseFails()
        {
            Assert.IsFalse(true, "Assert.IsFalse(true)... if this failed then the test is valid");
        }

        [TestMethod]
        public void TestAssertIsTrueFails()
        {
            Assert.IsTrue(false, "Assert.IsTrue(false)... if this failed then the test is valid");
        }

        [TestMethod]
        public void TestAssertAreEqualFails()
        {
            Assert.AreEqual(0, 1, "Assert.AreEqual(0, 1)... should fail");
        }

        [TestMethod]
        public void TestAssertAreNotEqualFails()
        {
            Assert.AreNotEqual(0, 0, "Assert.AreNotEqual(0, 0)... should fail");
        }
    }
}
