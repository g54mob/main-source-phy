using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal class SafeDeviceHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		internal SafeDeviceHandle()
			: base(true)
		{
		}

		internal SafeDeviceHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			SetHandle(existingHandle);
		}

		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteDC(handle);
		}
	}
}
