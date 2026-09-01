using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPresenceDeviceRecord
	{
		public XblPresenceDeviceType DeviceType { get; private set; }

		public XblPresenceTitleRecord[] TitleRecords { get; private set; }

		internal XblPresenceDeviceRecord(XGamingRuntime.Interop.XblPresenceDeviceRecord interopRecord)
		{
			DeviceType = interopRecord.deviceType;
			TitleRecords = interopRecord.GetTitleRecords((XGamingRuntime.Interop.XblPresenceTitleRecord tr) => new XblPresenceTitleRecord(tr));
		}
	}
}
