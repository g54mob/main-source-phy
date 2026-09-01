using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct UTF8StringPtr
	{
		private IntPtr pointer;

		internal UTF8StringPtr(string str, DisposableCollection disposableCollection)
		{
			if (str == null)
			{
				pointer = IntPtr.Zero;
				return;
			}
			byte[] array = Converters.StringToNullTerminatedUTF8ByteArray(str);
			DisposableBuffer disposableBuffer = new DisposableBuffer(array.Length);
			Marshal.Copy(array, 0, disposableBuffer.IntPtr, array.Length);
			disposableCollection.Add(disposableBuffer);
			pointer = disposableBuffer.IntPtr;
		}

		internal string GetString()
		{
			if (pointer == IntPtr.Zero)
			{
				return null;
			}
			return Converters.PtrToStringUTF8(pointer);
		}

		internal IntPtr ToPointer()
		{
			return pointer;
		}
	}
}
