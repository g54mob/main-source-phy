using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode.Platform
{
	public interface IAchievementManager : IPlatformService
	{
		void UpdateAchievementAsync([NotNull] IAchievement achievement, [NotNull] ILocalAccount account, float progress, AchievementEventHandler eventHandler = null);

		void GetAchievementAsync([NotNull] IAchievement achievement, [NotNull] ILocalAccount account, AchievementEventHandler eventHandler);
	}
}
