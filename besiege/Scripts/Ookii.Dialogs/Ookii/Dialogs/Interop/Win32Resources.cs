using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Ookii.Dialogs.Interop
{
	internal class Win32Resources : IDisposable
	{
		private SafeModuleHandle _moduleHandle;

		private const int _bufferSize = 500;

		public Win32Resources(string module)
		{
			_moduleHandle = NativeMethods.LoadLibraryEx(module, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			if (_moduleHandle.IsInvalid)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		public string LoadString(uint id)
		{
			CheckDisposed();
			StringBuilder stringBuilder = new StringBuilder(500);
			if (NativeMethods.LoadString(_moduleHandle, id, stringBuilder, stringBuilder.Capacity + 1) == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return stringBuilder.ToString();
		}

		public string FormatString(uint id, params string[] args)
		{
			CheckDisposed();
			IntPtr lpBuffer = IntPtr.Zero;
			string s = LoadString(id);
			NativeMethods.FormatMessageFlags dwFlags = NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY;
			IntPtr intPtr = Marshal.StringToHGlobalAuto(s);
			try
			{
				if (NativeMethods.FormatMessage(dwFlags, intPtr, id, 0u, ref lpBuffer, 0u, args) == 0)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			string result = Marshal.PtrToStringAuto(lpBuffer);
			Marshal.FreeHGlobal(lpBuffer);
			return result;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_moduleHandle.Dispose();
			}
		}

		private void CheckDisposed()
		{
			if (_moduleHandle.IsClosed)
			{
				throw new ObjectDisposedException("Win32Resources");
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
