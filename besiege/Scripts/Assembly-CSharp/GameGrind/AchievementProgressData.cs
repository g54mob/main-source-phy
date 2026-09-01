using System;

namespace GameGrind
{
	[Serializable]
	public class AchievementProgressData
	{
		public int id;

		public int value;

		public AchievementProgressData(int id, int value)
		{
			this.id = id;
			this.value = value;
		}
	}
}
