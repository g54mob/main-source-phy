using System;

namespace FMOD
{
	public struct StringWrapper
	{
		private IntPtr nativeUtf8Ptr;

		public StringWrapper(IntPtr ptr)
		{
			nativeUtf8Ptr = (IntPtr)0;
		}

		public static implicit operator string(StringWrapper fstring)
		{
			return null;
		}

		public bool StartsWith(byte[] prefix)
		{
			return false;
		}

		public bool Equals(byte[] comparison)
		{
			return false;
		}
	}
}
