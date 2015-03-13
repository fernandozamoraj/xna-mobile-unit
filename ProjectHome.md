This is a simple API that you can use to unit test your Windows Phone 7 Games

You can find an intro to setting up XnaMobileUnit at
http://fernandozamorajimenez.blogspot.com/2011/05/setting-up-xnamobileunit-to-run-unit.html

You can find a video here that demonstrates how to use the API
http://www.youtube.com/watch?v=Vs12D5T2dzc.

```
    //Sample Test
    public class FooTest : TestFixture
    {
        public override void Context()
        {
            
        }

        [TestMethod]
        public void Should_return_bar()
        {
            Foo foo = new Foo();
            Assert.AreEqual("bar", foo.Bar(), "Should have returned bar");
        }
    }
```