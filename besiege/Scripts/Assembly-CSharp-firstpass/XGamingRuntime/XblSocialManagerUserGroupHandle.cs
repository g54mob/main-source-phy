using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblSocialManagerUserGroupHandle : EquatableHandle
	{
		internal XGamingRuntime.Interop.XblSocialManagerUserGroupHandle InteropHandle { get; set; }

		internal XblSocialManagerUserGroupHandle(XGamingRuntime.Interop.XblSocialManagerUserGroupHandle interopHandle)
		{
			InteropHandle = interopHandle;
		}

		internal static int WrapAndReturnHResult(int hresult, XGamingRuntime.Interop.XblSocialManagerUserGroupHandle interopHandle, out XblSocialManagerUserGroupHandle handle)
		{
			if (XGamingRuntime.Interop.HR.SUCCEEDED(hresult))
			{
				handle = new XblSocialManagerUserGroupHandle(interopHandle);
			}
			else
			{
				handle = null;
			}
			return hresult;
		}

		internal void ClearInteropHandle()
		{
			InteropHandle = default(XGamingRuntime.Interop.XblSocialManagerUserGroupHandle);
		}

		internal override IntPtr GetInternalPtr()
		{
			return InteropHandle.Handle;
		}
	}
}
