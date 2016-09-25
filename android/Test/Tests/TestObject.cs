namespace FallingCatGame.Test.Tests
{
    public abstract class TestObject
    {
        // The assert object to be used in TestObject test classes.
        protected Assert assert = new Assert();

        // Runs the test code contained in this method. Called by the Runner.
        public abstract void Run();
    }
}