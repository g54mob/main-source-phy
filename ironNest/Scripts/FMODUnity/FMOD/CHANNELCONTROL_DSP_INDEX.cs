using System.Runtime.InteropServices;

namespace FMOD
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct CHANNELCONTROL_DSP_INDEX
	{
		public const int HEAD = -1;

		public const int FADER = -2;

		public const int TAIL = -3;
	}
}
