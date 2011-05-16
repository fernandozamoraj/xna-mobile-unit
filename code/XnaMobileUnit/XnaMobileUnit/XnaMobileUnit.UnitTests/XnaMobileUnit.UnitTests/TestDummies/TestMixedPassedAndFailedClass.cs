using XnaMobileUnit.FrameWork;

namespace XnaMobileUnit.UnitTests.TestDummies
{
    public class TestMixedPassedAndFailedClass : XnaMobileUnit.FrameWork.TestFixture
    {
        public override void Context()
        {

        }

        [TestMethod]
        public void TestAssertIsFalseFails()
        {
            Assert.IsFalse(true, "Assert.IsFalse(true)... should fail");
        }

        [TestMethod]
        public void TestAssertIsTrueFails()
        {
            Assert.IsTrue(false, "Assert.IsTrue(false)... should fail");
        }

        [TestMethod]
        public void TestAssertAreEqualPasses()
        {
            Assert.AreEqual(1, 1, "Assert.AreEqual(0, 1)... should pass");
        }

        [TestMethod]
        public void TestAssertAreNotEqualPasses()
        {
            Assert.AreNotEqual(0, 1, "Assert.AreNotEqual(0, 1)... should pass");
        }
    }
}