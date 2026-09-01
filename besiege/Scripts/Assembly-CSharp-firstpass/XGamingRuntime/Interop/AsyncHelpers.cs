using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public class AsyncHelpers
	{
		public static XAsyncBlockPtr WrapAsyncBlock(XTaskQueueHandle queue, XAsyncCompletionRoutine callback)
		{
			UnmanagedCallback<XAsyncCompletionRoutine, XAsyncCompletionRoutine> unmanagedCallback = new UnmanagedCallback<XAsyncCompletionRoutine, XAsyncCompletionRoutine>();
			unmanagedCallback.directCallback = AsyncBlockCallback;
			unmanagedCallback.userCallback = callback;
			UnmanagedCallback<XAsyncCompletionRoutine, XAsyncCompletionRoutine> unmanagedCallback2 = unmanagedCallback;
			GCHandle value = GCHandle.Alloc(unmanagedCallback2);
			XAsyncBlock xAsyncBlock = new XAsyncBlock
			{
				queue = queue,
				context = GCHandle.ToIntPtr(value),
				callback = Marshal.GetFunctionPointerForDelegate(unmanagedCallback2.directCallback)
			};
			int cb = Marshal.SizeOf(xAsyncBlock);
			IntPtr intPtr = Marshal.AllocHGlobal(cb);
			Marshal.StructureToPtr(xAsyncBlock, intPtr, false);
			return new XAsyncBlockPtr(intPtr);
		}

		internal static void CleanupAsyncBlock(XAsyncBlockPtr block)
		{
			GCHandle.FromIntPtr(((XAsyncBlock)Marshal.PtrToStructure(block.IntPtr, typeof(XAsyncBlock))).context).Free();
			Marshal.FreeHGlobal(block.IntPtr);
		}

		[MonoPInvokeCallback]
		private static void AsyncBlockCallback(XAsyncBlockPtr block)
		{
			GCHandle gCHandle = GCHandle.FromIntPtr(((XAsyncBlock)Marshal.PtrToStructure(block.IntPtr, typeof(XAsyncBlock))).context);
			UnmanagedCallback<XAsyncCompletionRoutine, XAsyncCompletionRoutine> unmanagedCallback = gCHandle.Target as UnmanagedCallback<XAsyncCompletionRoutine, XAsyncCompletionRoutine>;
			unmanagedCallback.userCallback(block);
			gCHandle.Free();
			Marshal.FreeHGlobal(block.IntPtr);
		}
	}
}
