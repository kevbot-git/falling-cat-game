using System;
namespace FallingCatGame
{
	public class Assert
	{

		public Assert()
		{
		}

		public bool Equal(object expected, object actual, string message)
		{
			if (expected.Equals(actual))
				return true;
			// do something with failure..
			return false;
		}

		public bool False(bool val, string message)
		{
			if (val != false)
				// do something with failure..
				return false;
			return true;
		}

		public bool True(bool val, string message)
		{
			if (val != true)
				// do something with failure..
				return false;
			return true;
		}


		public bool Null(object obj, string message)
		{
			if (obj == null)
				return true;
			// do something with failure..
			return false;

		}

		public bool NotNull(object obj, string message)
		{
			if (obj != null)
				return true;
			// do something with failure..
			return false;
		}
	}
}