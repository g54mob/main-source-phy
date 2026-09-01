using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblSocialManagerPresenceTitleRecord
	{
		public uint TitleId { get; private set; }

		public bool IsTitleActive { get; private set; }

		public string PresenceText { get; private set; }

		public bool IsBroadcasting { get; private set; }

		public XblPresenceDeviceType DeviceType { get; private set; }

		public bool IsPrimary { get; private set; }

		internal XblSocialManagerPresenceTitleRecord(XGamingRuntime.Interop.XblSocialManagerPresenceTitleRecord interopRecord)
		{
			TitleId = interopRecord.titleId;
			IsTitleActive = interopRecord.isTitleActive;
			PresenceText = Converters.ByteArrayToString(interopRecord.presenceText);
			IsBroadcasting = interopRecord.isBroadcasting;
			DeviceType = interopRecord.deviceType;
			IsPrimary = interopRecord.isPrimary;
		}
	}
}
