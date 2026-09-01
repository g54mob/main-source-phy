using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Nat
{
	internal abstract class Searcher
	{
		private readonly List<NatDevice> _devices = new List<NatDevice>();

		protected List<UdpClient> Sockets;

		public EventHandler<DeviceEventArgs> DeviceFound;

		internal DateTime NextSearch = DateTime.UtcNow;

		public Task<IEnumerable<NatDevice>> Search(CancellationToken cancelationToken)
		{
			return Task.Factory.StartNew((Action<object>)delegate
			{
				NatDiscoverer.TraceSource.LogInfo("Searching for: {0}", GetType().Name);
				while (!cancelationToken.IsCancellationRequested)
				{
					Discover(cancelationToken);
					Receive(cancelationToken);
				}
				CloseSockets();
			}, (object)cancelationToken).ContinueWith((Func<Task, IEnumerable<NatDevice>>)((Task task) => _devices));
		}

		private void Discover(CancellationToken cancelationToken)
		{
			if (DateTime.UtcNow < NextSearch)
			{
				return;
			}
			foreach (UdpClient socket in Sockets)
			{
				try
				{
					Discover(socket, cancelationToken);
				}
				catch (Exception ex)
				{
					NatDiscoverer.TraceSource.LogError("Error searching {0} - Details:", GetType().Name);
					NatDiscoverer.TraceSource.LogError(ex.ToString());
				}
			}
		}

		private void Receive(CancellationToken cancelationToken)
		{
			foreach (UdpClient item in Sockets.Where((UdpClient x) => x.Available > 0))
			{
				if (cancelationToken.IsCancellationRequested)
				{
					break;
				}
				IPAddress address = ((IPEndPoint)item.Client.LocalEndPoint).Address;
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.None, 0);
				byte[] response = item.Receive(ref remoteEP);
				NatDevice natDevice = AnalyseReceivedResponse(address, response, remoteEP);
				if (natDevice != null)
				{
					RaiseDeviceFound(natDevice);
				}
			}
		}

		protected abstract void Discover(UdpClient client, CancellationToken cancelationToken);

		public abstract NatDevice AnalyseReceivedResponse(IPAddress localAddress, byte[] response, IPEndPoint endpoint);

		public void CloseSockets()
		{
			foreach (UdpClient socket in Sockets)
			{
				socket.Close();
			}
		}

		private void RaiseDeviceFound(NatDevice device)
		{
			_devices.Add(device);
			EventHandler<DeviceEventArgs> deviceFound = DeviceFound;
			if (deviceFound != null)
			{
				deviceFound(this, new DeviceEventArgs(device));
			}
		}
	}
}
