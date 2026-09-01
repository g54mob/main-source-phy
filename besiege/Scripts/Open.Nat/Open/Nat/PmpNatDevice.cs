using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Nat
{
	internal sealed class PmpNatDevice : NatDevice
	{
		private readonly IPAddress _publicAddress;

		internal IPAddress LocalAddress { get; private set; }

		internal PmpNatDevice(IPAddress localAddress, IPAddress publicAddress)
		{
			LocalAddress = localAddress;
			_publicAddress = publicAddress;
		}

		public override Task CreatePortMapAsync(Mapping mapping, float timeout = 4f)
		{
			return InternalCreatePortMapAsync(mapping, true).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate
			{
				RegisterMapping(mapping);
			});
		}

		public override Task DeletePortMapAsync(Mapping mapping, float timeout = 4f)
		{
			return InternalCreatePortMapAsync(mapping, false).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate
			{
				UnregisterMapping(mapping);
			});
		}

		public override Task<IEnumerable<Mapping>> GetAllMappingsAsync(float timeout = 4f)
		{
			throw new NotSupportedException();
		}

		public override Task<IPAddress> GetExternalIPAsync(float timeout = 4f)
		{
			return Task.Factory.StartNew(() => _publicAddress).TimeoutAfter(TimeSpan.FromSeconds(timeout));
		}

		public override Task<Mapping> GetSpecificMappingAsync(Protocol protocol, int port, float timeout = 4f)
		{
			throw new NotSupportedException("NAT-PMP does not specify a way to get a specific port map");
		}

		private Task<Mapping> InternalCreatePortMapAsync(Mapping mapping, bool create)
		{
			List<byte> list = new List<byte>();
			list.Add(0);
			list.Add((byte)((mapping.Protocol != Protocol.Tcp) ? 1 : 2));
			list.Add(0);
			list.Add(0);
			list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(checked((short)mapping.PrivatePort))));
			list.AddRange(BitConverter.GetBytes((short)(create ? IPAddress.HostToNetworkOrder(checked((short)mapping.PublicPort)) : 0)));
			list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(mapping.Lifetime)));
			byte[] buffer = list.ToArray();
			int num = 0;
			int num2 = 250;
			UdpClient udpClient = new UdpClient();
			CreatePortMapListen(udpClient, mapping);
			Task task = Task.Factory.FromAsync((Func_<byte[], int, IPEndPoint, AsyncCallback, object, IAsyncResult>)udpClient.BeginSend, (Func<IAsyncResult, int>)udpClient.EndSend, buffer, buffer.Length, new IPEndPoint(LocalAddress, 5351), (object)null);
			checked
			{
				while (num < 8)
				{
					task = task.ContinueWith(delegate(Task t)
					{
						if (t.IsFaulted)
						{
							string arg = (create ? "create" : "delete");
							string text = string.Format("Failed to {0} portmap (protocol={1}, private port={2})", arg, mapping.Protocol, mapping.PrivatePort);
							NatDiscoverer.TraceSource.LogError(text);
							throw new MappingException(text, t.Exception);
						}
						return Task.Factory.FromAsync((Func_<byte[], int, IPEndPoint, AsyncCallback, object, IAsyncResult>)udpClient.BeginSend, (Func<IAsyncResult, int>)udpClient.EndSend, buffer, buffer.Length, new IPEndPoint(LocalAddress, 5351), (object)null);
					}).Unwrap();
					num++;
					num2 *= 2;
					Thread.Sleep(num2);
				}
				return task.ContinueWith(delegate
				{
					udpClient.Close();
					return mapping;
				});
			}
		}

		private void CreatePortMapListen(UdpClient udpClient, Mapping mapping)
		{
			IPEndPoint remoteEP = new IPEndPoint(LocalAddress, 5351);
			byte[] array;
			do
			{
				array = udpClient.Receive(ref remoteEP);
			}
			while (array.Length < 16 || array[0] != 0);
			checked
			{
				byte b = (byte)(array[1] & 0x7F);
				Protocol protocol = Protocol.Tcp;
				if (b == 1)
				{
					protocol = Protocol.Udp;
				}
				short num = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 2));
				int num2 = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 4));
				short num3 = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 8));
				short num4 = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 10));
				uint num5 = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 12));
				if (num3 < 0 || num4 < 0 || num != 0)
				{
					string[] array2 = new string[6] { "Success", "Unsupported Version", "Not Authorized/Refused (e.g. box supports mapping, but user has turned feature off)", "Network Failure (e.g. NAT box itself has not obtained a DHCP lease)", "Out of resources (NAT box cannot create any more mappings at this time)", "Unsupported opcode" };
					throw new MappingException(num, array2[num]);
				}
				if (num5 != 0)
				{
					mapping.PublicPort = num4;
					mapping.Protocol = protocol;
					mapping.Expiration = DateTime.Now.AddSeconds(num5);
				}
			}
		}

		public override string ToString()
		{
			return string.Format("Local Address: {0}\nPublic IP: {1}\nLast Seen: {2}", LocalAddress, _publicAddress, base.LastSeen);
		}
	}
}
