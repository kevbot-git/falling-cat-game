namespace FallingCatGame.Test.Tests
{
    public abstract class TestObject
    {
        // The assert object to be used in TestObject test classes.
        protected MonoAssert assert = new MonoAssert();

        // Runs the test code contained in this method. Called by the Runner.
        public abstract void Run();
    }
}