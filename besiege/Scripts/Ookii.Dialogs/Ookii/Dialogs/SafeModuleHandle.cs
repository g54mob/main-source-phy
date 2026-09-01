using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	internal class SafeModuleHandle : SafeHandle
	{
		public override bool IsInvalid
		{
			get
			{
				return handle == IntPtr.Zero;
			}
		}

		public SafeModuleHandle()
			: base(IntPtr.Zero, true)
		{
		}

		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			return NativeMethods.FreeLibrary(handle);
		}
	}
}
