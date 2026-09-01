using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD
{
	internal static class StringHelper
	{
		public class ThreadSafeEncoding : IDisposable
		{
			private UTF8Encoding encoding;

			private byte[] encodedBuffer;

			private char[] decodedBuffer;

			private bool inUse;

			private GCHandle gcHandle;

			public bool InUse()
			{
				return false;
			}

			public void SetInUse()
			{
			}

			private int roundUpPowerTwo(int number)
			{
				return 0;
			}

			public byte[] byteFromStringUTF8(string s)
			{
				return null;
			}

			public IntPtr intptrFromStringUTF8(string s)
			{
				return (IntPtr)0;
			}

			public string stringFromNative(IntPtr nativePtr)
			{
				return null;
			}

			public void Dispose()
			{
			}
		}

		private static List<ThreadSafeEncoding> encoders;

		public static ThreadSafeEncoding GetFreeHelper()
		{
			return null;
		}
	}
}
