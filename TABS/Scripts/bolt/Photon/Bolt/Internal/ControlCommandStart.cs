using System;
using UdpKit;
using UdpKit.Platform;
using UnityEngine;

namespace Photon.Bolt.Internal
{
	internal class ControlCommandStart : ControlCommand
	{
		public BoltConfig Config;

		public BoltNetworkModes Mode;

		public UdpPlatform Platform;

		public UdpEndPoint EndPoint;

		private UdpPlatform DefaultPlatform => new PhotonPlatform();

		public override void Run()
		{
			if (BoltCore.IsRunning)
			{
				Debug.LogWarning("Bolt is already running, you must call BoltLauncher.Shutdown() before starting a new instance of Bolt.");
				State = ControlState.Failed;
				FinishedEvent.Set();
				return;
			}
			try
			{
				if (Mode == BoltNetworkModes.None)
				{
					Mode = BoltNetworkModes.Server;
					Platform = new NullPlatform();
				}
				if (!EndPoint.IPv6 && EndPoint.Address.IsLocalHost)
				{
					Platform = Platform ?? new DotNetPlatform();
				}
				if (Platform == null)
				{
					Platform = DefaultPlatform;
				}
			}
			catch (Exception)
			{
				throw;
			}
			BoltCore.BeginStart(this);
		}

		public override void Done()
		{
		}
	}
}
