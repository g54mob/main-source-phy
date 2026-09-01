using System;
using UnityEngine;

namespace UdpKit
{
	[Serializable]
	public class UdpConfig
	{
		public int PacketWindow = 256;

		public int PacketDatagramSize = 1200;

		public int StreamWindow = 1024;

		public int StreamDatagramSize = 4096;

		public bool NatPunchEnabled;

		public float RoomCreateTimeout = 10f;

		public float RoomJoinTimeout = 10f;

		public uint BroadcastInterval = 2000u;

		public bool IPv6;

		public float DefaultNetworkPing = 0.1f;

		public float DefaultAliasedPing = 0.15f;

		public bool AllowPacketOverflow;

		public uint ConnectRequestTimeout = 1000u;

		public uint ConnectRequestAttempts = 5u;

		public uint ConnectRequestLANAttempts = 5u;

		public uint ConnectionTimeout = 5000u;

		public uint PingTimeout = 100u;

		public uint RecvWithoutAckLimit = 8u;

		public int ConnectionLimit = 64;

		public bool AllowIncommingConnections = true;

		public bool AutoAcceptIncommingConnections = true;

		public bool AllowImplicitAccept = true;

		public float SimulatedLoss;

		public int SimulatedPingMin;

		public int SimulatedPingMax;

		public Func<float> NoiseFunction;

		public bool IsBuildMono;

		public bool IsBuildDotNet;

		public bool IsBuildIL2CPP;

		public RuntimePlatform CurrentPlatform;

		internal UdpConfig Duplicate()
		{
			return (UdpConfig)MemberwiseClone();
		}
	}
}
