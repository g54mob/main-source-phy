using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	internal static class NativeMethods
	{
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, HandleRef hSourceHandle, HandleRef hTargetProcess, out SafeProcessHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			targetHandle = null;
			return false;
		}

		public static IntPtr GetCurrentProcess()
		{
			return (IntPtr)0;
		}

		public static bool GetExitCodeProcess(IntPtr processHandle, out int exitCode)
		{
			exitCode = default(int);
			return false;
		}

		public static bool GetExitCodeProcess(SafeProcessHandle processHandle, out int exitCode)
		{
			exitCode = default(int);
			return false;
		}

		public static int GetCurrentProcessId()
		{
			return 0;
		}

		public static bool CloseProcess(IntPtr handle)
		{
			return false;
		}
	}
}
