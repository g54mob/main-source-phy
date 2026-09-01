using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreContext : EquatableHandle
	{
		internal XStoreContextHandle handle { get; set; }

		internal override IntPtr GetInternalPtr()
		{
			return handle.intPtr;
		}
	}
}
