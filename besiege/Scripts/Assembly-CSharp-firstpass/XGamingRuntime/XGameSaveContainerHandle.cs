using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XGameSaveContainerHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XGameSaveContainerHandle InteropHandle { get; private set; }

		internal XGameSaveContainerHandle(XGamingRuntime.Interop.XGameSaveContainerHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XGameSaveContainerHandle interopHandle, out XGameSaveContainerHandle userHandle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				userHandle = new XGameSaveContainerHandle(interopHandle);
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
