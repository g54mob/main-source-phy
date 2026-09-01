using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal class SafeGDIHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		internal SafeGDIHandle()
			: base(true)
		{
		}

		internal SafeGDIHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			SetHandle(existingHandle);
		}

		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteObject(handle);
		}
	}
}
