using System;

namespace TND.Upscaling.Framework
{
	public enum UpscalerName
	{
		None = 0,
		[Obsolete]
		FSR1 = 11000,
		FSR2 = 12000,
		FSR3 = 13000,
		FSR4 = 14000,
		ASR = 21000,
		DLSS3 = 33000,
		DLSS4 = 34000,
		SwitchDLSS = 39000,
		[Obsolete]
		XeSS1 = 41000,
		XeSS2 = 42000,
		XeSS3 = 43000,
		SGSR1 = 51000,
		SGSR2 = 52000,
		MetalFX = 61000,
		Reserved2 = 71000,
		Reserved3 = 81000,
		PSSR = 91000
	}
}
