using FallingCatGame.Test.Tests;
using System.Collections;

namespace FallingCatGame.Test
{
    public class TestRunner
    {
        private Queue _tests;
        private TestObject _current;

        public TestRunner()
        {
            SetUp();
        }

        private void SetUp()
        {
            _tests = new Queue();

            _tests.Enqueue(new BasicTest());

            _tests.Enqueue(null);
            _current = (TestObject)_tests.Dequeue();
        }

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