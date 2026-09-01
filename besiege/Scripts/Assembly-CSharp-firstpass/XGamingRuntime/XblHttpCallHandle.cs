using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblHttpCallHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XblHttpCallHandle InteropHandle { get; set; }

		internal XblHttpCallHandle(XGamingRuntime.Interop.XblHttpCallHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XblHttpCallHandle interopHandle, out XblHttpCallHandle handle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				handle = new XblHttpCallHandle(interopHandle);
			}
			else
			{
				handle = null;
			}
			return hresult;
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.handle;
		}
	}
}
