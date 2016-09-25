using Android.Util;

namespace FallingCatGame
{
	public class Assert
	{
        // The tag in which all unit test results are found under.
        // This tag does not have to be a constant. Upon creation of a test class, a custom tag could be passed into the assert object.
        // But by default it's to group all unit tests together under one tab.
        protected const string Tag = "FALLINGCAT";

        // Counts the number of pass and fail.
        private int _nSuccess = 0;
        private int _nFail = 0;

        public Assert()
		{

		}

		public void Equal(object expected, object actual, string message)
		{
            if (!expected.Equals(actual))
                Fail(message + " " + expected + " != " + actual);
            else
                Success();
		}

        public void NotEqual(object expected, object actual, string message)
        {
            if (expected.Equals(actual))
                Fail(message + " " + expected + " == " + actual);
            else
                Success();
        }

        public void False(bool val, string message)
		{
            if (val)
                Fail(message);
            else
                Success();
		}

		public void True(bool val, string message)
		{
            if (!val)
                Fail(message);
            else
                Success();
        }


		public void Null(object obj, string message)
		{
            if (obj != null)
                Fail(message);
            else
                Success();
        }

		public void NotNull(object obj, string message)
		{
            if (obj == null)
                Fail(message);
            else
                Success();
        }

        /// <summary>
        /// Called at the end of a test run if the debugger wants to know how many tests passed and failed.
        /// The name of the test is past in to identify the numbers for each test.
        /// </summary>
        /// <param name="testName"></param>
        public void TestResults(string testName)
        {
            Log.Debug(Tag, testName + ": " + _nFail + " failed, " + _nSuccess + " passed.");
        }

        private void Success()
        {
            _nSuccess++;
        }

        /// <summary>
        /// If an assert fails, the message given to the assert will be logged.
        /// In this case it is best to give an assert a message that will identify where and why the error occured.
        /// </summary>
        /// <param name="message"></param>
        private void Fail(string message)
        {
            _nFail++;
            Log.Debug(Tag, message);
        }
	}
}