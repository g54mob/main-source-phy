namespace MoreMountains.Tools
{
	public struct MMSoundManagerAllSoundsControlEvent
	{
		public MMSoundManagerAllSoundsControlEventTypes EventType;

		private static MMSoundManagerAllSoundsControlEvent e;

		public MMSoundManagerAllSoundsControlEvent(MMSoundManagerAllSoundsControlEventTypes eventType)
		{
			EventType = default(MMSoundManagerAllSoundsControlEventTypes);
		}

		public static void Trigger(MMSoundManagerAllSoundsControlEventTypes eventType)
		{
		}
	}
}
