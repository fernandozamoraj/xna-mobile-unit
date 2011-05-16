using XnaMobileUnit.FrameWork;

namespace XnaMobileUnit.UnitTests.TestDummies
{
    public class TestOnlyValidMethodsAreCalled : XnaMobileUnit.FrameWork.TestFixture
    {
        public override void Context()
        {

        }

        [TestMethod]
        private void ThisNotAValidTestMethodBecauseItIsPrivate()
        {
            Assert.IsFalse(true, "Assert.IsFalse(true) should fail");
        }

        [TestMethod]
        protected void NotValidBecauseItIsProtected()
        {
            Assert.IsTrue(false, "This method is protected and should not be called");
        }

        public void TestMethodHasNoTestMethodAttribute()
        {
            Assert.AreEqual(0, 1, "This method is not decorated with TestMethodAttribute and should not be called");
        }

        [TestMethod]
        public void ThisIsTheOnlyValidMethod()
        {
            Assert.AreNotEqual(0, 0, "This method is vali an should be called");
        }
    }
}