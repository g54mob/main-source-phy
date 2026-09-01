using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public class DisposableBuffer : IDisposable
	{
		public IntPtr IntPtr { get; private set; }

		public DisposableBuffer()
		{
			IntPtr = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		public DisposableBuffer(int size)
		{
			IntPtr = Marshal.AllocHGlobal(size);
			byte[] source = new byte[size];
			Marshal.Copy(source, 0, IntPtr, size);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
			if (IntPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(IntPtr);
				IntPtr = IntPtr.Zero;
			}
		}

		~DisposableBuffer()
		{
			Dispose(false);
		}
	}
}
