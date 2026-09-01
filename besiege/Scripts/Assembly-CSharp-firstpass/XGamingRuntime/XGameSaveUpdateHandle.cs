using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XGameSaveUpdateHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XGameSaveUpdateHandle InteropHandle { get; private set; }

		internal XGameSaveUpdateHandle(XGamingRuntime.Interop.XGameSaveUpdateHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XGameSaveUpdateHandle interopHandle, out XGameSaveUpdateHandle userHandle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				userHandle = new XGameSaveUpdateHandle(interopHandle);
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
