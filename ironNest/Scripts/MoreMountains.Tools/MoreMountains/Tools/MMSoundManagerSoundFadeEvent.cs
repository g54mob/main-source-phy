namespace MoreMountains.Tools
{
	public struct MMSoundManagerSoundFadeEvent
	{
		public enum Modes
		{
			PlayFade = 0,
			StopFade = 1
		}

		public Modes Mode;

		public int SoundID;

		public float FadeDuration;

		public float FinalVolume;

		public MMTweenType FadeTween;

		private static MMSoundManagerSoundFadeEvent e;

		public MMSoundManagerSoundFadeEvent(Modes mode, int soundID, float fadeDuration, float finalVolume, MMTweenType fadeTween)
		{
			Mode = default(Modes);
			SoundID = 0;
			FadeDuration = 0f;
			FinalVolume = 0f;
			FadeTween = null;
		}

		public static void Trigger(Modes mode, int soundID, float fadeDuration, float finalVolume, MMTweenType fadeTween)
		{
		}
	}
}
