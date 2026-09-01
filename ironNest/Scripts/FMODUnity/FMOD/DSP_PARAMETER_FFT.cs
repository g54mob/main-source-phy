using System;

namespace FMOD
{
	public struct DSP_PARAMETER_FFT
	{
		public int length;

		public int numchannels;

		private IntPtr[] spectrum_internal;

		public float[][] spectrum => null;

		public void getSpectrum(ref float[][] buffer)
		{
		}

		public void getSpectrum(int channel, ref float[] buffer)
		{
		}
	}
}
