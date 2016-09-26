using FallingCatGame.Test.Tests;
using System.Collections;

namespace FallingCatGame.Test
{
    /// <summary>
    /// This object is initialized in GameMain.cs Initialize().
    /// All the tests below are also run in GameMain's Initialize() method as the tests contained in this Runner do not need
    /// access to Update() or Delete(). UpdatingTestRunner does this.
    /// </summary>
    public class TestRunner
    {
        private Queue _tests;
        private TestObject _current;

        /// <summary>
        /// Any Monogame or Android dependencies can be passed through the constructor and then through SetUp() into your tests.
        /// For example, ContentManager.
        /// </summary>
        public TestRunner()
        {
            SetUp();
        }

        private void SetUp()
        {
            _tests = new Queue();

            // Enqueue all of your tests that do not require XNA's Update() or Draw() here.
            _tests.Enqueue(new ScaleTest());

            // Leave this.
            _tests.Enqueue(null);
            _current = (TestObject)_tests.Dequeue();
        }

        /// <summary>
        /// Will run through all the tests in the queue.
        /// </summary>
        public void RunTests()
        {
            while(_current != null)
            {
                _current.Run();
                _current = (TestObject)_tests.Dequeue();
            }
        }
    }
}