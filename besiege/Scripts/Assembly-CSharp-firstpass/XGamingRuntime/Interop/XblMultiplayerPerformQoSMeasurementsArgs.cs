using System;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerPerformQoSMeasurementsArgs
	{
		private unsafe readonly XblMultiplayerConnectionAddressDeviceTokenPair* remoteClients;

		internal readonly SizeT remoteClientsSize;

		internal unsafe XblMultiplayerPerformQoSMeasurementsArgs(XGamingRuntime.XblMultiplayerPerformQoSMeasurementsArgs publicObject, DisposableCollection disposableCollection)
		{
			remoteClients = (XblMultiplayerConnectionAddressDeviceTokenPair*)(void*)Converters.ClassArrayToPtr(publicObject.RemoteClients, (Func<XGamingRuntime.XblMultiplayerConnectionAddressDeviceTokenPair, DisposableCollection, XblMultiplayerConnectionAddressDeviceTokenPair>)((XGamingRuntime.XblMultiplayerConnectionAddressDeviceTokenPair x, DisposableCollection dc) => new XblMultiplayerConnectionAddressDeviceTokenPair(x, dc)), disposableCollection, out remoteClientsSize);
		}

		internal unsafe T[] GetRemoteClients<T>(Func<XblMultiplayerConnectionAddressDeviceTokenPair, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)remoteClients, remoteClientsSize, ctor);
		}
	}
}
