using System;
using System.Diagnostics;

namespace Photon.Bolt.Exceptions
{
	internal static class Assert
	{
		[Conditional("DEBUG")]
		internal static void Fail(string message = "")
		{
			throw new BoltAssertFailedException(message);
		}

		[Conditional("DEBUG")]
		internal static void Same(object a, object b, string error = "")
		{
		}

		[Conditional("DEBUG")]
		internal static void NotSame(object a, object b)
		{
		}

		[Conditional("DEBUG")]
		internal static void Null(object a, string msg = "Object was not null")
		{
		}

		[Conditional("DEBUG")]
		internal static void NotNull(object a, string msg = "Object was null")
		{
		}

		[Conditional("DEBUG")]
		internal static void Equal(object a, object b)
		{
		}

		[Conditional("DEBUG")]
		internal static void Equal<T>(T a, T b) where T : IEquatable<T>
		{
		}

		[Conditional("DEBUG")]
		internal static void NotEqual(object a, object b)
		{
		}

		[Conditional("DEBUG")]
		internal static void NotEqual<T>(T a, T b) where T : IEquatable<T>
		{
		}

		[Conditional("DEBUG")]
		internal static void False(bool condition, string message = "")
		{
			if (condition)
			{
				throw new BoltAssertFailedException(message);
			}
		}

		[Conditional("DEBUG")]
		internal static void True(bool condition, string message = "")
		{
			if (!condition)
			{
				throw new BoltAssertFailedException(message);
			}
		}

		[Conditional("DEBUG")]
		internal static void True(bool condition, string message, params object[] args)
		{
			if (!condition)
			{
				throw new BoltAssertFailedException(string.Format(message, args));
			}
		}

		internal static void False<BE>(bool condition, object extraInfo = null) where BE : BoltException, new()
		{
			if (condition)
			{
				throw new BE
				{
					ExtraInfo = extraInfo
				};
			}
		}

		internal static void True<BE>(bool condition, object extraInfo = null) where BE : BoltException, new()
		{
			if (!condition)
			{
				throw new BE
				{
					ExtraInfo = extraInfo
				};
			}
		}
	}
}
