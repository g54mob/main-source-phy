using System;

namespace BitCode.Platform
{
	public static class AchievementManagerDelegateExtensions
	{
		public static void SafelyInvoke<T>(this AchievementEventHandler self, T achievement, float percent, bool hasBeenAwarded, Exception exception) where T : IAchievement
		{
			try
			{
				self(achievement, percent, hasBeenAwarded, exception);
			}
			catch (Exception)
			{
			}
		}
	}
}
