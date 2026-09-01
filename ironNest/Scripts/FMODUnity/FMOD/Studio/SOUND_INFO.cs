using System;

namespace FMOD.Studio
{
	public struct SOUND_INFO
	{
		public IntPtr name_or_data;

		public MODE mode;

		public CREATESOUNDEXINFO exinfo;

		public int subsoundindex;

		public string name => null;
	}
}
