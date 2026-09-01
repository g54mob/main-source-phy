using System;

namespace FMOD
{
	public struct DSP_BUFFER_ARRAY
	{
		public int numbuffers;

		public IntPtr buffernumchannels;

		public IntPtr bufferchannelmask;

		public IntPtr buffers;

		public SPEAKERMODE speakermode;

		public int numchannels
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public IntPtr buffer
		{
			get
			{
				return (IntPtr)0;
			}
			set
			{
			}
		}
	}
}
