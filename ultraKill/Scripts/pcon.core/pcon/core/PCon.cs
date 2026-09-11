using System.Collections.Generic;
using pcon.core.Interfaces;
using pcon.core.Models;

namespace pcon.core
{
	public class PCon
	{
		private static IPConClient _mountedClient;

		private static List<string> _registeredFeatures = new List<string>();

		private static Handler _handler = null;

		public static void Internal_RegisterClient(IPConClient client)
		{
			_mountedClient = client;
			foreach (string registeredFeature in _registeredFeatures)
			{
				client.RegisterFeature(registeredFeature);
			}
			if (_handler != null)
			{
				client.MountHandler(_handler);
			}
		}

		public static void SendMessage(ISend message)
		{
			if (_mountedClient != null)
			{
				_mountedClient.SendMessage(message);
			}
		}

		public static void RegisterFeature(string feature)
		{
			feature = feature.ToLower();
			_registeredFeatures.Add(feature);
			if (_mountedClient != null)
			{
				_mountedClient.RegisterFeature(feature);
			}
		}

		public static void MountHandler(Handler handler)
		{
			_handler = handler;
			if (_mountedClient != null)
			{
				_mountedClient.MountHandler(handler);
			}
		}
	}
}
