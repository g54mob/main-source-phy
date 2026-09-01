using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPresenceRichPresenceIds
	{
		public string ServiceConfigurationId { get; private set; }

		public string PresenceId { get; private set; }

		public string[] PresenceTokenIds { get; private set; }

		private XblPresenceRichPresenceIds(string serviceConfigurationId, string presenceId, string[] presenceTokenIds)
		{
			ServiceConfigurationId = serviceConfigurationId;
			PresenceId = presenceId;
			PresenceTokenIds = presenceTokenIds;
		}

		public static int Create(string serviceConfigurationId, string presenceId, string[] presenceTokenIds, out XblPresenceRichPresenceIds richPresenceIds)
		{
			if (!XblPresenceRichPresenceIdsRef.ValidateFields(serviceConfigurationId))
			{
				richPresenceIds = null;
				return -2147024809;
			}
			richPresenceIds = new XblPresenceRichPresenceIds(serviceConfigurationId, presenceId, presenceTokenIds);
			return 0;
		}
	}
}
