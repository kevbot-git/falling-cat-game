using Android.Util;

namespace FallingCatGame
{
	public class Assert
	{
        protected const string Tag = "FALLINGCAT";
        private int _nSuccess = 0;
        private int _nFail = 0;

        public Assert()
		{

		}

		public void Equal(object expected, object actual, string message)
		{
            if (!expected.Equals(actual))
                Fail(message);
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

        private void Success()
        {
            _nSuccess++;
        }

        private void Fail(string message)
        {
            _nFail++;
            Log.Debug(Tag, message);
        }
	}
}