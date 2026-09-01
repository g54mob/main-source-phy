using System;

namespace BitCode.Platform
{
	public delegate void AchievementEventHandler(IAchievement achievement, float progress, bool awarded, Exception exception);
}
