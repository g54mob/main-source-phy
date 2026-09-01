using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerEventArgsHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XblMultiplayerEventArgsHandle InteropHandle { get; set; }

		internal XblMultiplayerEventArgsHandle(XGamingRuntime.Interop.XblMultiplayerEventArgsHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XblMultiplayerEventArgsHandle interopHandle, out XblMultiplayerEventArgsHandle handle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				handle = new XblMultiplayerEventArgsHandle(interopHandle);
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
