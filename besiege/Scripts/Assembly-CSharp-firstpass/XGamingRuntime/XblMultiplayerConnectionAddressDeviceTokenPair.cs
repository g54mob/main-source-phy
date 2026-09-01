using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerConnectionAddressDeviceTokenPair
	{
		public string ConnectionAddress { get; private set; }

		public XblDeviceToken DeviceToken { get; private set; }

		internal XblMultiplayerConnectionAddressDeviceTokenPair(XGamingRuntime.Interop.XblMultiplayerConnectionAddressDeviceTokenPair interopStruct)
		{
			ConnectionAddress = interopStruct.connectionAddress.GetString();
			DeviceToken = new XblDeviceToken(interopStruct.deviceToken);
		}
	}
}
