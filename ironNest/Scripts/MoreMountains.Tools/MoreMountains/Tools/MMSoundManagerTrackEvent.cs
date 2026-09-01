namespace MoreMountains.Tools
{
	public struct MMSoundManagerTrackEvent
	{
		public MMSoundManagerTrackEventTypes TrackEventType;

		public MMSoundManager.MMSoundManagerTracks Track;

		public float Volume;

		private static MMSoundManagerTrackEvent e;

		public MMSoundManagerTrackEvent(MMSoundManagerTrackEventTypes trackEventType, MMSoundManager.MMSoundManagerTracks track = MMSoundManager.MMSoundManagerTracks.Master, float volume = 1f)
		{
			TrackEventType = default(MMSoundManagerTrackEventTypes);
			Track = default(MMSoundManager.MMSoundManagerTracks);
			Volume = 0f;
		}

		public static void Trigger(MMSoundManagerTrackEventTypes trackEventType, MMSoundManager.MMSoundManagerTracks track = MMSoundManager.MMSoundManagerTracks.Master, float volume = 1f)
		{
		}
	}
}
