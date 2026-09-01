using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace Open.Nat
{
	internal sealed class UpnpNatDevice : NatDevice
	{
		internal readonly UpnpNatDeviceInfo DeviceInfo;

		private readonly SoapClient _soapClient;

		internal UpnpNatDevice(UpnpNatDeviceInfo deviceInfo)
		{
			Touch();
			DeviceInfo = deviceInfo;
			_soapClient = new SoapClient(DeviceInfo.ServiceControlUri, DeviceInfo.ServiceType);
		}

		public override Task<IPAddress> GetExternalIPAsync(float timeout = 4f)
		{
			NatDiscoverer.TraceSource.LogInfo("GetExternalIPAsync - Getting external IP address");
			GetExternalIPAddressRequestMessage getExternalIPAddressRequestMessage = new GetExternalIPAddressRequestMessage();
			return _soapClient.InvokeAsync("GetExternalIPAddress", getExternalIPAddressRequestMessage.ToXml()).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate(Task<XmlDocument> task)
			{
				XmlDocument result = task.Result;
				GetExternalIPAddressResponseMessage getExternalIPAddressResponseMessage = new GetExternalIPAddressResponseMessage(result, DeviceInfo.ServiceType);
				return getExternalIPAddressResponseMessage.ExternalIPAddress;
			});
		}

		public override Task CreatePortMapAsync(Mapping mapping, float timeout = 4f)
		{
			Guard.IsNotNull(mapping, "mapping");
			if (mapping.PrivateIP.Equals(IPAddress.None))
			{
				mapping.PrivateIP = DeviceInfo.LocalAddress;
			}
			NatDiscoverer.TraceSource.LogInfo("CreatePortMapAsync - Creating port mapping {0}", mapping);
			CreatePortMappingRequestMessage createPortMappingRequestMessage = new CreatePortMappingRequestMessage(mapping);
			return _soapClient.InvokeAsync("AddPortMapping", createPortMappingRequestMessage.ToXml()).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate(Task<XmlDocument> task)
			{
				if (!task.IsFaulted)
				{
					RegisterMapping(mapping);
				}
				else
				{
					MappingException ex = task.Exception.InnerException as MappingException;
					if (ex == null)
					{
						throw task.Exception.InnerException;
					}
					switch (ex.ErrorCode)
					{
					case 725:
						NatDiscoverer.TraceSource.LogWarn("Only Permanent Leases Supported - There is no warranty it will be closed");
						mapping.Lifetime = 0;
						mapping.LifetimeType = MappingLifetime.ForcedSession;
						CreatePortMapAsync(mapping);
						break;
					case 724:
						NatDiscoverer.TraceSource.LogWarn("Same Port Values Required - Using internal port {0}", mapping.PrivatePort);
						mapping.PublicPort = mapping.PrivatePort;
						CreatePortMapAsync(mapping);
						break;
					case 726:
						NatDiscoverer.TraceSource.LogWarn("Remote Host Only Supports Wildcard");
						mapping.PublicIP = IPAddress.None;
						CreatePortMapAsync(mapping);
						break;
					case 727:
						NatDiscoverer.TraceSource.LogWarn("External Port Only Supports Wildcard");
						throw ex;
					case 718:
						NatDiscoverer.TraceSource.LogWarn("Conflict with an already existing mapping");
						throw ex;
					default:
						throw ex;
					}
				}
			});
		}

		public override Task DeletePortMapAsync(Mapping mapping, float timeout = 4f)
		{
			Guard.IsNotNull(mapping, "mapping");
			if (mapping.PrivateIP.Equals(IPAddress.None))
			{
				mapping.PrivateIP = DeviceInfo.LocalAddress;
			}
			NatDiscoverer.TraceSource.LogInfo("DeletePortMapAsync - Deleteing port mapping {0}", mapping);
			DeletePortMappingRequestMessage deletePortMappingRequestMessage = new DeletePortMappingRequestMessage(mapping);
			return _soapClient.InvokeAsync("DeletePortMapping", deletePortMappingRequestMessage.ToXml()).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate(Task<XmlDocument> task)
			{
				if (!task.IsFaulted)
				{
					UnregisterMapping(mapping);
				}
				else
				{
					MappingException ex = task.Exception.InnerException as MappingException;
					if (ex != null && ex.ErrorCode != 714)
					{
						throw ex;
					}
				}
			});
		}

		public void GetGenericMappingAsync(int index, List<Mapping> mappings, TaskCompletionSource<IEnumerable<Mapping>> taskCompletionSource, float timeout = 4f)
		{
			GetGenericPortMappingEntry getGenericPortMappingEntry = new GetGenericPortMappingEntry(index);
			_soapClient.InvokeAsync("GetGenericPortMappingEntry", getGenericPortMappingEntry.ToXml()).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate(Task<XmlDocument> task)
			{
				if (!task.IsFaulted)
				{
					XmlDocument result = task.Result;
					GetPortMappingEntryResponseMessage getPortMappingEntryResponseMessage = new GetPortMappingEntryResponseMessage(result, DeviceInfo.ServiceType, true);
					IPAddress address;
					if (!IPAddress.TryParse(getPortMappingEntryResponseMessage.InternalClient, out address))
					{
						NatDiscoverer.TraceSource.LogWarn("InternalClient is not an IP address. Mapping ignored!");
					}
					else
					{
						Mapping item = new Mapping(getPortMappingEntryResponseMessage.Protocol, address, getPortMappingEntryResponseMessage.InternalPort, getPortMappingEntryResponseMessage.ExternalPort, getPortMappingEntryResponseMessage.LeaseDuration, getPortMappingEntryResponseMessage.PortMappingDescription);
						mappings.Add(item);
					}
					GetGenericMappingAsync(checked(index + 1), mappings, taskCompletionSource);
				}
				else
				{
					MappingException ex = task.Exception.InnerException as MappingException;
					if (ex == null)
					{
						throw task.Exception.InnerException;
					}
					if (ex.ErrorCode == 713 || ex.ErrorCode == 714)
					{
						taskCompletionSource.SetResult(mappings);
					}
					else
					{
						if (ex.ErrorCode != 402)
						{
							throw task.Exception.InnerException;
						}
						NatDiscoverer.TraceSource.LogWarn("Router failed with 402-InvalidArgument. No more mappings is assumed.");
						taskCompletionSource.SetResult(mappings);
					}
				}
			});
		}

		public override Task<IEnumerable<Mapping>> GetAllMappingsAsync(float timeout = 4f)
		{
			TaskCompletionSource<IEnumerable<Mapping>> taskCompletionSource = new TaskCompletionSource<IEnumerable<Mapping>>();
			NatDiscoverer.TraceSource.LogInfo("GetAllMappingsAsync - Getting all mappings");
			GetGenericMappingAsync(0, new List<Mapping>(), taskCompletionSource, timeout);
			return taskCompletionSource.Task;
		}

		public override Task<Mapping> GetSpecificMappingAsync(Protocol protocol, int port, float timeout = 4f)
		{
			Guard.IsTrue(protocol == Protocol.Tcp || protocol == Protocol.Udp, "protocol");
			Guard.IsInRange(port, 0, 65535, "port");
			NatDiscoverer.TraceSource.LogInfo("GetSpecificMappingAsync - Getting mapping for protocol: {0} port: {1}", Enum.GetName(typeof(Protocol), protocol), port);
			GetSpecificPortMappingEntryRequestMessage getSpecificPortMappingEntryRequestMessage = new GetSpecificPortMappingEntryRequestMessage(protocol, port);
			return _soapClient.InvokeAsync("GetSpecificPortMappingEntry", getSpecificPortMappingEntryRequestMessage.ToXml()).TimeoutAfter(TimeSpan.FromSeconds(timeout)).ContinueWith(delegate(Task<XmlDocument> task)
			{
				if (!task.IsFaulted)
				{
					XmlDocument result = task.Result;
					GetPortMappingEntryResponseMessage getPortMappingEntryResponseMessage = new GetPortMappingEntryResponseMessage(result, DeviceInfo.ServiceType, false);
					return new Mapping(getPortMappingEntryResponseMessage.Protocol, IPAddress.Parse(getPortMappingEntryResponseMessage.InternalClient), getPortMappingEntryResponseMessage.InternalPort, getPortMappingEntryResponseMessage.ExternalPort, getPortMappingEntryResponseMessage.LeaseDuration, getPortMappingEntryResponseMessage.PortMappingDescription);
				}
				MappingException ex = task.Exception.InnerException as MappingException;
				if (ex != null && ex.ErrorCode == 714)
				{
					return (Mapping)null;
				}
				if (ex != null && ex.ErrorCode == 402)
				{
					NatDiscoverer.TraceSource.LogWarn("Router failed with 402-InvalidArgument. Mapping not found is assumed.");
					return (Mapping)null;
				}
				throw task.Exception.InnerException;
			});
		}

		public override string ToString()
		{
			return string.Format("EndPoint: {0}\nControl Url: {1}\nService Type: {2}\nLast Seen: {3}", DeviceInfo.HostEndPoint, DeviceInfo.ServiceControlUri, DeviceInfo.ServiceType, base.LastSeen);
		}
	}
}
