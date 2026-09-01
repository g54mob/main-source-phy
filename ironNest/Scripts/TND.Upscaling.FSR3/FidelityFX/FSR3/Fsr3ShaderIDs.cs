namespace FidelityFX.FSR3
{
	public static class Fsr3ShaderIDs
	{
		public static readonly int SrvInputColor;

		public static readonly int SrvOpaqueOnly;

		public static readonly int SrvInputMotionVectors;

		public static readonly int SrvInputDepth;

		public static readonly int SrvInputExposure;

		public static readonly int SrvFrameInfo;

		public static readonly int SrvReactiveMask;

		public static readonly int SrvTransparencyAndCompositionMask;

		public static readonly int SrvReconstructedPrevNearestDepth;

		public static readonly int SrvDilatedMotionVectors;

		public static readonly int SrvDilatedDepth;

		public static readonly int SrvInternalUpscaled;

		public static readonly int SrvAccumulation;

		public static readonly int SrvLumaHistory;

		public static readonly int SrvRcasInput;

		public static readonly int SrvLanczosLut;

		public static readonly int SrvSpdMips;

		public static readonly int SrvDilatedReactiveMasks;

		public static readonly int SrvNewLocks;

		public static readonly int SrvFarthestDepth;

		public static readonly int SrvFarthestDepthMip1;

		public static readonly int SrvShadingChange;

		public static readonly int SrvCurrentLuma;

		public static readonly int SrvPreviousLuma;

		public static readonly int SrvLumaInstability;

		public static readonly int SrvPrevColorPreAlpha;

		public static readonly int SrvPrevColorPostAlpha;

		public static readonly int UavReconstructedPrevNearestDepth;

		public static readonly int UavDilatedMotionVectors;

		public static readonly int UavDilatedDepth;

		public static readonly int UavInternalUpscaled;

		public static readonly int UavAccumulation;

		public static readonly int UavLumaHistory;

		public static readonly int UavUpscaledOutput;

		public static readonly int UavDilatedReactiveMasks;

		public static readonly int UavFrameInfo;

		public static readonly int UavSpdAtomicCount;

		public static readonly int UavNewLocks;

		public static readonly int UavAutoReactive;

		public static readonly int UavShadingChange;

		public static readonly int UavFarthestDepth;

		public static readonly int UavFarthestDepthMip1;

		public static readonly int UavCurrentLuma;

		public static readonly int UavLumaInstability;

		public static readonly int UavIntermediate;

		public static readonly int UavSpdMip0;

		public static readonly int UavSpdMip1;

		public static readonly int UavSpdMip2;

		public static readonly int UavSpdMip3;

		public static readonly int UavSpdMip4;

		public static readonly int UavSpdMip5;

		public static readonly int UavAutoComposition;

		public static readonly int UavPrevColorPreAlpha;

		public static readonly int UavPrevColorPostAlpha;

		public static readonly int CbFsr3Upscaler;

		public static readonly int CbSpd;

		public static readonly int CbRcas;

		public static readonly int CbGenReactive;
	}
}
