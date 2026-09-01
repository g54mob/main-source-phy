using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XGameSaveProviderHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XGameSaveProviderHandle InteropHandle { get; private set; }

		internal XGameSaveProviderHandle(XGamingRuntime.Interop.XGameSaveProviderHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XGameSaveProviderHandle interopHandle, out XGameSaveProviderHandle userHandle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				userHandle = new XGameSaveProviderHandle(interopHandle);
			}
			else
			{
				userHandle = null;
			}
			return hresult;
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.Ptr;
		}
	}
}
