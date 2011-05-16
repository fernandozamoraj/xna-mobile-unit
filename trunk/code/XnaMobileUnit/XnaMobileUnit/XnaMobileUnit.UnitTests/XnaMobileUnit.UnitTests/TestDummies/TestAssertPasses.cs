using XnaMobileUnit.FrameWork;

namespace XnaMobileUnit.UnitTests.TestDummies
{
    public class TestAssertPasses : XnaMobileUnit.FrameWork.TestFixture
    {
        public override void Context()
        {

        }

        [TestMethod]
        public void TestAssertIsFalsePasses()
        {
            Assert.IsFalse(false, "Assert.IsFalse(false)... should pass");
        }

        [TestMethod]
        public void TestAssertIsTruePasses()
        {
            Assert.IsTrue(true, "Assert.IsTrue(true)... should pass");
        }

        [TestMethod]
        public void TestAssertAreEqualPasses()
        {
            Assert.AreEqual(1, 1, "Assert.AreEqual(1, 1)... should pass");
        }

        [TestMethod]
        public void TestAssertAreEqualStringsPasses()
        {
            Assert.AreEqual("A", "A", "Assert.AreEqual(\"A\", \"A\")... should pass");
        }

        [TestMethod]
        public void TestAssertAreNotEqualStringsPasses()
        {
            Assert.AreNotEqual("A", "B", "Assert.AreNotEqual(\"A\", \"B\")... should pass");
        }


        [TestMethod]
        public void TestAssertAreNotEqualFailure()
        {
            Assert.AreNotEqual(1, 0, "Assert.AreNotEqual(1, 0)... should pass");
        }
    }
}