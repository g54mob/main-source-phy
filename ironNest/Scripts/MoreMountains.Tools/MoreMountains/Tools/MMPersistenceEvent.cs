namespace MoreMountains.Tools
{
	public struct MMPersistenceEvent
	{
		public MMPersistenceEventType PersistenceEventType;

		public string PersistenceID;

		private static MMPersistenceEvent e;

		public MMPersistenceEvent(MMPersistenceEventType eventType, string persistenceID)
		{
			PersistenceEventType = default(MMPersistenceEventType);
			PersistenceID = null;
		}

		public static void Trigger(MMPersistenceEventType eventType, string persistencyID)
		{
		}
	}
}
