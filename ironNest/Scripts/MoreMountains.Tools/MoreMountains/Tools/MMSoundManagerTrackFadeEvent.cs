namespace MoreMountains.Tools
{
	public struct MMSoundManagerTrackFadeEvent
	{
		public enum Modes
		{
			PlayFade = 0,
			StopFade = 1
		}

		public Modes Mode;

		public MMSoundManager.MMSoundManagerTracks Track;

		public float FadeDuration;

		public float FinalVolume;

		public MMTweenType FadeTween;

		private static MMSoundManagerTrackFadeEvent e;

		public MMSoundManagerTrackFadeEvent(Modes mode, MMSoundManager.MMSoundManagerTracks track, float fadeDuration, float finalVolume, MMTweenType fadeTween)
		{
			Mode = default(Modes);
			Track = default(MMSoundManager.MMSoundManagerTracks);
			FadeDuration = 0f;
			FinalVolume = 0f;
			FadeTween = null;
		}

		public static void Trigger(Modes mode, MMSoundManager.MMSoundManagerTracks track, float fadeDuration, float finalVolume, MMTweenType fadeTween)
		{
		}
	}
}
