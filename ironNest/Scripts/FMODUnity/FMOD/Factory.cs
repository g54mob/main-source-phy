using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Factory
	{
		public static RESULT System_Create(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_System_Create(out IntPtr system, uint headerversion);
	}
}
