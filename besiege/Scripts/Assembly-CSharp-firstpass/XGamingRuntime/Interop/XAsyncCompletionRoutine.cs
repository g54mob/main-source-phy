using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void XAsyncCompletionRoutine(XAsyncBlockPtr asyncBlock);
}
