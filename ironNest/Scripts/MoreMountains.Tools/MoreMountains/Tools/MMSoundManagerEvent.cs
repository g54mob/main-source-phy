namespace MoreMountains.Tools
{
	public struct MMSoundManagerEvent
	{
		public MMSoundManagerEventTypes EventType;

		private static MMSoundManagerEvent e;

		public MMSoundManagerEvent(MMSoundManagerEventTypes eventType)
		{
			EventType = default(MMSoundManagerEventTypes);
		}

		public static void Trigger(MMSoundManagerEventTypes eventType)
		{
		}
	}
}
