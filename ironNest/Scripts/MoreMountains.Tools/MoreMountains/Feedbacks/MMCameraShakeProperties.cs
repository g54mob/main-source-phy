using System;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public struct MMCameraShakeProperties
	{
		public float Duration;

		public float Amplitude;

		public float Frequency;

		public float AmplitudeX;

		public float AmplitudeY;

		public float AmplitudeZ;

		public MMCameraShakeProperties(float duration, float amplitude, float frequency, float amplitudeX = 0f, float amplitudeY = 0f, float amplitudeZ = 0f)
		{
			Duration = 0f;
			Amplitude = 0f;
			Frequency = 0f;
			AmplitudeX = 0f;
			AmplitudeY = 0f;
			AmplitudeZ = 0f;
		}
	}
}
