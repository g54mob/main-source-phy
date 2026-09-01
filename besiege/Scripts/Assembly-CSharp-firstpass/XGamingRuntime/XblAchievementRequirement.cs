using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblAchievementRequirement
	{
		public string Id { get; private set; }

		public string CurrentProgressValue { get; private set; }

		public string TargetProgressValue { get; private set; }

		internal XblAchievementRequirement(XGamingRuntime.Interop.XblAchievementRequirement interopRequirement)
		{
			Id = interopRequirement.id.GetString();
			CurrentProgressValue = interopRequirement.currentProgressValue.GetString();
			TargetProgressValue = interopRequirement.targetProgressValue.GetString();
		}
	}
}
