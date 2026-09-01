using System;

namespace FMOD
{
	public struct DSP_STATE
	{
		public IntPtr instance;

		public IntPtr plugindata;

		public uint channelmask;

		public int source_speakermode;

		public IntPtr sidechaindata;

		public int sidechainchannels;

		private IntPtr functions_internal;

		public int systemobject;

		public DSP_STATE_FUNCTIONS functions => default(DSP_STATE_FUNCTIONS);
	}
}
