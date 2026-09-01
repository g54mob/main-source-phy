using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct DSPConnection
	{
		public IntPtr handle;

		public RESULT getInput(out DSP input)
		{
			input = default(DSP);
			return default(RESULT);
		}

		public RESULT getOutput(out DSP output)
		{
			output = default(DSP);
			return default(RESULT);
		}

		public RESULT setMix(float volume)
		{
			return default(RESULT);
		}

		public RESULT getMix(out float volume)
		{
			volume = default(float);
			return default(RESULT);
		}

		public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop = 0)
		{
			return default(RESULT);
		}

		public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop = 0)
		{
			outchannels = default(int);
			inchannels = default(int);
			return default(RESULT);
		}

		public RESULT getType(out DSPCONNECTION_TYPE type)
		{
			type = default(DSPCONNECTION_TYPE);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetInput(IntPtr dspconnection, out IntPtr input);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetOutput(IntPtr dspconnection, out IntPtr output);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_SetMix(IntPtr dspconnection, float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetMix(IntPtr dspconnection, out float volume);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_SetMixMatrix(IntPtr dspconnection, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetMixMatrix(IntPtr dspconnection, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetType(IntPtr dspconnection, out DSPCONNECTION_TYPE type);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_SetUserData(IntPtr dspconnection, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_DSPConnection_GetUserData(IntPtr dspconnection, out IntPtr userdata);

		public DSPConnection(IntPtr ptr)
		{
			handle = (IntPtr)0;
		}

		public bool hasHandle()
		{
			return false;
		}

		public void clearHandle()
		{
		}
	}
}
