namespace MoreMountains.Tools
{
	public static class EventRegister
	{
		public delegate void Delegate<T>(T eventType);

		public static void MMEventStartListening<EventType>(this MMEventListener<EventType> caller) where EventType : struct
		{
		}

		public static void MMEventStopListening<EventType>(this MMEventListener<EventType> caller) where EventType : struct
		{
		}
	}
}
