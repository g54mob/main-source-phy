using System.Runtime.InteropServices;

namespace FMOD
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Debug
	{
		public static RESULT Initialize(DEBUG_FLAGS flags, DEBUG_MODE mode = DEBUG_MODE.TTY, DEBUG_CALLBACK callback = null, string filename = null)
		{
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_Debug_Initialize(DEBUG_FLAGS flags, DEBUG_MODE mode, DEBUG_CALLBACK callback, byte[] filename);
	}
}
