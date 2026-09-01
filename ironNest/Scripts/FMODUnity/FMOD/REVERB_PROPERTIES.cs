namespace FMOD
{
	public struct REVERB_PROPERTIES
	{
		public float DecayTime;

		public float EarlyDelay;

		public float LateDelay;

		public float HFReference;

		public float HFDecayRatio;

		public float Diffusion;

		public float Density;

		public float LowShelfFrequency;

		public float LowShelfGain;

		public float HighCut;

		public float EarlyLateMix;

		public float WetLevel;

		public REVERB_PROPERTIES(float decayTime, float earlyDelay, float lateDelay, float hfReference, float hfDecayRatio, float diffusion, float density, float lowShelfFrequency, float lowShelfGain, float highCut, float earlyLateMix, float wetLevel)
		{
			DecayTime = 0f;
			EarlyDelay = 0f;
			LateDelay = 0f;
			HFReference = 0f;
			HFDecayRatio = 0f;
			Diffusion = 0f;
			Density = 0f;
			LowShelfFrequency = 0f;
			LowShelfGain = 0f;
			HighCut = 0f;
			EarlyLateMix = 0f;
			WetLevel = 0f;
		}
	}
}
