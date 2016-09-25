namespace FallingCatGame.Test.Tests
{
    public class BasicTest : TestObject
    {
        public BasicTest()
        {

        }

        public override void Run()
        {
            assert.False(true, "This should fail.");
        }
    }
}