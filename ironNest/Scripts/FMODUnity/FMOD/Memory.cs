using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Memory
	{
		public static RESULT Initialize(IntPtr poolmem, int poollen, MEMORY_ALLOC_CALLBACK useralloc, MEMORY_REALLOC_CALLBACK userrealloc, MEMORY_FREE_CALLBACK userfree, MEMORY_TYPE memtypeflags = MEMORY_TYPE.ALL)
		{
			return default(RESULT);
		}

		public static RESULT GetStats(out int currentalloced, out int maxalloced, bool blocking = true)
		{
			currentalloced = default(int);
			maxalloced = default(int);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_Memory_Initialize(IntPtr poolmem, int poollen, MEMORY_ALLOC_CALLBACK useralloc, MEMORY_REALLOC_CALLBACK userrealloc, MEMORY_FREE_CALLBACK userfree, MEMORY_TYPE memtypeflags);

		[PreserveSig]
		private static extern RESULT FMOD5_Memory_GetStats(out int currentalloced, out int maxalloced, bool blocking);
	}
}
