using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblAchievementProgression
	{
		public XblAchievementRequirement[] Requirements { get; private set; }

		public DateTime TimeUnlocked { get; private set; }

		internal XblAchievementProgression(XGamingRuntime.Interop.XblAchievementProgression interopProgression)
		{
			Requirements = interopProgression.GetRequirements((XGamingRuntime.Interop.XblAchievementRequirement r) => new XblAchievementRequirement(r));
			TimeUnlocked = interopProgression.timeUnlocked.DateTime;
		}
	}
}
