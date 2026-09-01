using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XUserHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XUserHandle InteropHandle { get; private set; }

		internal XUserHandle(XGamingRuntime.Interop.XUserHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapAndReturnHResult(int hresult, XGamingRuntime.Interop.XUserHandle interopHandle, out XUserHandle handle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				handle = new XUserHandle(interopHandle);
			}
			else
			{
				handle = null;
			}
			return hresult;
		}

		internal void ClearInteropHandle()
		{
			InteropHandle = default(XGamingRuntime.Interop.XUserHandle);
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.Ptr;
		}
	}
}
