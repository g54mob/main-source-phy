using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XUserSignOutDeferralHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XUserSignOutDeferralHandle InteropHandle { get; private set; }

		internal XUserSignOutDeferralHandle(XGamingRuntime.Interop.XUserSignOutDeferralHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.Ptr;
		}
	}
}
