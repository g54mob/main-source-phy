using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Nat
{
	public class NatDiscoverer
	{
		public static readonly TraceSource TraceSource = new TraceSource("Open.NAT");

		private static readonly Dictionary<string, NatDevice> Devices = new Dictionary<string, NatDevice>();

		private static readonly Finalizer Finalizer = new Finalizer();

		internal static readonly Timer RenewTimer = new Timer(RenewMappings, null, 5000, 2000);

		public Task<NatDevice> DiscoverDeviceAsync()
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.CancelAfter(3000);
			return DiscoverDeviceAsync(PortMapper.Pmp | PortMapper.Upnp, cancellationTokenSource);
		}

		public Task<NatDevice> DiscoverDeviceAsync(PortMapper portMapper, CancellationTokenSource cancellationTokenSource)
		{
			Guard.IsTrue(portMapper.HasFlag(PortMapper.Upnp) || portMapper.HasFlag(PortMapper.Pmp), "portMapper");
			Guard.IsNotNull(cancellationTokenSource, "cancellationTokenSource");
			return DiscoverAsync(portMapper, true, cancellationTokenSource).ContinueWith(delegate(Task<IEnumerable<NatDevice>> task)
			{
				IEnumerable<NatDevice> result = task.Result;
				NatDevice natDevice = result.FirstOrDefault();
				if (natDevice == null)
				{
					TraceSource.LogInfo("Device not found. Common reasons:");
					TraceSource.LogInfo("\t* No device is present or,");
					TraceSource.LogInfo("\t* Upnp is disabled in the router or");
					TraceSource.LogInfo("\t* Antivirus software is filtering SSDP (discovery protocol).");
					throw new NatDeviceNotFoundException();
				}
				return natDevice;
			});
		}

		public Task<IEnumerable<NatDevice>> DiscoverDevicesAsync(PortMapper portMapper, CancellationTokenSource cancellationTokenSource)
		{
			Guard.IsTrue(portMapper.HasFlag(PortMapper.Upnp) || portMapper.HasFlag(PortMapper.Pmp), "portMapper");
			Guard.IsNotNull(cancellationTokenSource, "cancellationTokenSource");
			return DiscoverAsync(portMapper, false, cancellationTokenSource).ContinueWith((Func<Task<IEnumerable<NatDevice>>, IEnumerable<NatDevice>>)((Task<IEnumerable<NatDevice>> t) => t.Result.ToArray()));
		}

		private Task<IEnumerable<NatDevice>> DiscoverAsync(PortMapper portMapper, bool onlyOne, CancellationTokenSource cts)
		{
			TraceSource.LogInfo("Start Discovery");
			List<Task<IEnumerable<NatDevice>>> searcherTasks = new List<Task<IEnumerable<NatDevice>>>();
			if (portMapper.HasFlag(PortMapper.Upnp))
			{
				UpnpSearcher upnpSearcher = new UpnpSearcher(new IPAddressesProvider());
				upnpSearcher.DeviceFound = (EventHandler<DeviceEventArgs>)Delegate.Combine(upnpSearcher.DeviceFound, (EventHandler<DeviceEventArgs>)delegate
				{
					if (onlyOne)
					{
						cts.Cancel();
					}
				});
				searcherTasks.Add(upnpSearcher.Search(cts.Token));
			}
			if (portMapper.HasFlag(PortMapper.Pmp))
			{
				PmpSearcher pmpSearcher = new PmpSearcher(new IPAddressesProvider());
				pmpSearcher.DeviceFound = (EventHandler<DeviceEventArgs>)Delegate.Combine(pmpSearcher.DeviceFound, (EventHandler<DeviceEventArgs>)delegate
				{
					if (onlyOne)
					{
						cts.Cancel();
					}
				});
				searcherTasks.Add(pmpSearcher.Search(cts.Token));
			}
			return TaskExtension.WhenAll(searcherTasks.ToArray()).ContinueWith(delegate
			{
				TraceSource.LogInfo("Stop Discovery");
				IEnumerable<NatDevice> enumerable = searcherTasks.SelectMany((Task<IEnumerable<NatDevice>> x) => x.Result);
				foreach (NatDevice item in enumerable)
				{
					string key = item.ToString();
					NatDevice value;
					if (Devices.TryGetValue(key, out value))
					{
						value.Touch();
					}
					else
					{
						Devices.Add(key, item);
					}
				}
				return enumerable;
			});
		}

		public static void ReleaseAll()
		{
			foreach (NatDevice value in Devices.Values)
			{
				value.ReleaseAll();
			}
		}

		internal static void ReleaseSessionMappings()
		{
			foreach (NatDevice value in Devices.Values)
			{
				value.ReleaseSessionMappings();
			}
		}

		private static void RenewMappings(object state)
		{
			Task.Factory.StartNew(delegate
			{
				Task task = null;
				foreach (NatDevice value in Devices.Values)
				{
					NatDevice d = value;
					task = ((task == null) ? value.RenewMappings() : task.ContinueWith((Task t) => d.RenewMappings()));
				}
			});
		}
	}
}
