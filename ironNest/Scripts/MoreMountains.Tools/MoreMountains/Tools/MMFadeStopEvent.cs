namespace MoreMountains.Tools
{
	public struct MMFadeStopEvent
	{
		public int ID;

		public bool Restore;

		private static MMFadeStopEvent e;

		public MMFadeStopEvent(int id = 0, bool restore = false)
		{
			ID = 0;
			Restore = false;
		}

		public static void Trigger(int id = 0, bool restore = false)
		{
		}
	}
}
