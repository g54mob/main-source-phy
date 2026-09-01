using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblAchievementTitleAssociation
	{
		public string Name { get; private set; }

		public uint TitleId { get; private set; }

		internal XblAchievementTitleAssociation(XGamingRuntime.Interop.XblAchievementTitleAssociation interopTitleAssociation)
		{
			Name = interopTitleAssociation.name.GetString();
			TitleId = interopTitleAssociation.titleId;
		}
	}
}
