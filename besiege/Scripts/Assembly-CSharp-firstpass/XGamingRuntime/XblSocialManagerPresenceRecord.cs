using System.Linq;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblSocialManagerPresenceRecord
	{
		public XblSocialManagerPresenceTitleRecord[] PresenceTitleRecords;

		public XblPresenceUserState UserState { get; private set; }

		internal XblSocialManagerPresenceRecord(XGamingRuntime.Interop.XblSocialManagerPresenceRecord interopRecord)
		{
			UserState = interopRecord.userState;
			PresenceTitleRecords = (from r in interopRecord.presenceTitleRecords.Where((XGamingRuntime.Interop.XblSocialManagerPresenceTitleRecord r, int index) => (uint)index < interopRecord.presenceTitleCount)
				select new XblSocialManagerPresenceTitleRecord(r)).ToArray();
		}
	}
}
