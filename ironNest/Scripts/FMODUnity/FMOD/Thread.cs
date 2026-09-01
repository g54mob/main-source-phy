using System.Runtime.InteropServices;

namespace FMOD
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Thread
	{
		public static RESULT SetAttributes(THREAD_TYPE type, THREAD_AFFINITY affinity = THREAD_AFFINITY.GROUP_DEFAULT, THREAD_PRIORITY priority = THREAD_PRIORITY.DEFAULT, THREAD_STACK_SIZE stacksize = THREAD_STACK_SIZE.DEFAULT)
		{
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_Thread_SetAttributes(THREAD_TYPE type, THREAD_AFFINITY affinity, THREAD_PRIORITY priority, THREAD_STACK_SIZE stacksize);
	}
}
