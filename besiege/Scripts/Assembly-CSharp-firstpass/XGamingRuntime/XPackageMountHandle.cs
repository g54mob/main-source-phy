using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XPackageMountHandle
	{
		internal XGamingRuntime.Interop.XPackageMountHandle Handle;

		internal XPackageMountHandle(XGamingRuntime.Interop.XPackageMountHandle rawHandle)
		{
			Handle = rawHandle;
		}
	}
}
