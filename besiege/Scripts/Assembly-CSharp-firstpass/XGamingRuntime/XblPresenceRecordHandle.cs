using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPresenceRecordHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XblPresenceRecordHandle InteropHandle { get; private set; }

		internal XblPresenceRecordHandle(XGamingRuntime.Interop.XblPresenceRecordHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapInteropHandleAndReturnHResult(int hresult, XGamingRuntime.Interop.XblPresenceRecordHandle interopHandle, out XblPresenceRecordHandle handle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				handle = new XblPresenceRecordHandle(interopHandle);
			}
			else
			{
				handle = null;
			}
			return hresult;
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.intPtr;
		}
	}
}
