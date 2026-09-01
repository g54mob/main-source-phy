namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerConnectionAddressDeviceTokenPair
	{
		internal readonly UTF8StringPtr connectionAddress;

		internal readonly XblDeviceToken deviceToken;

		internal XblMultiplayerConnectionAddressDeviceTokenPair(XGamingRuntime.XblMultiplayerConnectionAddressDeviceTokenPair publicObject, DisposableCollection disposableCollection)
		{
			connectionAddress = new UTF8StringPtr(publicObject.ConnectionAddress, disposableCollection);
			deviceToken = new XblDeviceToken(publicObject.DeviceToken);
		}
	}
}
