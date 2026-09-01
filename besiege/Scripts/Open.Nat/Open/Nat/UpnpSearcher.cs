using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;

namespace Open.Nat
{
	internal class UpnpSearcher : Searcher
	{
		private readonly IIPAddressesProvider _ipprovider;

		private readonly IDictionary<Uri, NatDevice> _devices;

		private readonly Dictionary<IPAddress, DateTime> _lastFetched;

		private static readonly string[] ServiceTypes = new string[4] { "WANIPConnection:2", "WANPPPConnection:2", "WANIPConnection:1", "WANPPPConnection:1" };

		internal UpnpSearcher(IIPAddressesProvider ipprovider)
		{
			_ipprovider = ipprovider;
			Sockets = CreateSockets();
			_devices = new Dictionary<Uri, NatDevice>();
			_lastFetched = new Dictionary<IPAddress, DateTime>();
		}

		private List<UdpClient> CreateSockets()
		{
			List<UdpClient> list = new List<UdpClient>();
			try
			{
				IEnumerable<IPAddress> enumerable = _ipprovider.UnicastAddresses();
				foreach (IPAddress item in enumerable)
				{
					try
					{
						list.Add(new UdpClient(new IPEndPoint(item, 0)));
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
				list.Add(new UdpClient(0));
			}
			return list;
		}

		protected override void Discover(UdpClient client, CancellationToken cancelationToken)
		{
			NextSearch = DateTime.UtcNow.AddSeconds(1.0);
			IPEndPoint endPoint = new IPEndPoint(WellKnownConstants.IPv4MulticastAddress, 1900);
			string[] serviceTypes = ServiceTypes;
			foreach (string serviceType in serviceTypes)
			{
				string s = DiscoverDeviceMessage.Encode(serviceType);
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				for (int j = 0; j < 2; j = checked(j + 1))
				{
					if (cancelationToken.IsCancellationRequested)
					{
						return;
					}
					client.Send(bytes, bytes.Length, endPoint);
				}
			}
		}

		public override NatDevice AnalyseReceivedResponse(IPAddress localAddress, byte[] response, IPEndPoint endpoint)
		{
			string text = null;
			try
			{
				text = Encoding.UTF8.GetString(response);
				DiscoveryResponseMessage discoveryResponseMessage = new DiscoveryResponseMessage(text);
				string text2 = discoveryResponseMessage["ST"];
				if (!IsValidControllerService(text2))
				{
					NatDiscoverer.TraceSource.LogWarn("Invalid controller service. Ignoring.");
					return null;
				}
				NatDiscoverer.TraceSource.LogInfo("UPnP Response: Router advertised a '{0}' service!!!", text2);
				string uriString = discoveryResponseMessage["Location"] ?? discoveryResponseMessage["AL"];
				Uri uri = new Uri(uriString);
				NatDiscoverer.TraceSource.LogInfo("Found device at: {0}", uri.ToString());
				if (_devices.ContainsKey(uri))
				{
					NatDiscoverer.TraceSource.LogInfo("Already found - Ignored");
					_devices[uri].Touch();
					return null;
				}
				if (_lastFetched.ContainsKey(endpoint.Address))
				{
					DateTime dateTime = _lastFetched[endpoint.Address];
					if (DateTime.Now - dateTime < TimeSpan.FromSeconds(20.0))
					{
						return null;
					}
				}
				_lastFetched[endpoint.Address] = DateTime.Now;
				NatDiscoverer.TraceSource.LogInfo("{0}:{1}: Fetching service list", uri.Host, uri.Port);
				UpnpNatDeviceInfo deviceInfo = BuildUpnpNatDeviceInfo(localAddress, uri);
				UpnpNatDevice upnpNatDevice;
				lock (_devices)
				{
					upnpNatDevice = new UpnpNatDevice(deviceInfo);
					if (!_devices.ContainsKey(uri))
					{
						_devices.Add(uri, upnpNatDevice);
					}
				}
				return upnpNatDevice;
			}
			catch (Exception ex)
			{
				NatDiscoverer.TraceSource.LogError("Unhandled exception when trying to decode a device's response. ");
				NatDiscoverer.TraceSource.LogError("Report the issue in https://github.com/lontivero/Open.Nat/issues");
				NatDiscoverer.TraceSource.LogError("Also copy and paste the following info:");
				NatDiscoverer.TraceSource.LogError("-- beging ---------------------------------");
				NatDiscoverer.TraceSource.LogError(ex.Message);
				NatDiscoverer.TraceSource.LogError("Data string:");
				NatDiscoverer.TraceSource.LogError(text ?? "No data available");
				NatDiscoverer.TraceSource.LogError("-- end ------------------------------------");
			}
			return null;
		}

		private static bool IsValidControllerService(string serviceType)
		{
			var source = from serviceName in ServiceTypes
				let serviceUrn = string.Format("urn:schemas-upnp-org:service:{0}", serviceName)
				where serviceType.ContainsIgnoreCase(serviceUrn)
				select new
				{
					ServiceName = serviceName,
					ServiceUrn = serviceUrn
				};
			return source.Any();
		}

		private UpnpNatDeviceInfo BuildUpnpNatDeviceInfo(IPAddress localAddress, Uri location)
		{
			NatDiscoverer.TraceSource.LogInfo("Found device at: {0}", location.ToString());
			IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(location.Host), location.Port);
			WebResponse webResponse = null;
			try
			{
				WebRequest webRequest = WebRequest.Create(location);
				webRequest.Headers.Add("ACCEPT-LANGUAGE", "en");
				webRequest.Method = "GET";
				webResponse = webRequest.GetResponse();
				HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
				if (httpWebResponse != null && httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					string message = string.Format("Couldn't get services list: {0} {1}", httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
					throw new Exception(message);
				}
				XmlDocument xmlDocument = ReadXmlResponse(webResponse);
				NatDiscoverer.TraceSource.LogInfo("{0}: Parsed services list", iPEndPoint);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("ns", "urn:schemas-upnp-org:device-1-0");
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//ns:service", xmlNamespaceManager);
				foreach (XmlNode item in xmlNodeList)
				{
					string xmlElementText = item.GetXmlElementText("serviceType");
					if (IsValidControllerService(xmlElementText))
					{
						NatDiscoverer.TraceSource.LogInfo("{0}: Found service: {1}", iPEndPoint, xmlElementText);
						string xmlElementText2 = item.GetXmlElementText("controlURL");
						NatDiscoverer.TraceSource.LogInfo("{0}: Found upnp service at: {1}", iPEndPoint, xmlElementText2);
						NatDiscoverer.TraceSource.LogInfo("{0}: Handshake Complete", iPEndPoint);
						return new UpnpNatDeviceInfo(localAddress, location, xmlElementText2, xmlElementText);
					}
				}
				throw new Exception("No valid control service was found in the service descriptor document");
			}
			catch (WebException ex)
			{
				NatDiscoverer.TraceSource.LogError("{0}: Device denied the connection attempt: {1}", iPEndPoint, ex);
				SocketException ex2 = ex.InnerException as SocketException;
				if (ex2 != null)
				{
					NatDiscoverer.TraceSource.LogError("{0}: ErrorCode:{1}", iPEndPoint, ex2.ErrorCode);
					NatDiscoverer.TraceSource.LogError("Go to http://msdn.microsoft.com/en-us/library/system.net.sockets.socketerror.aspx");
					NatDiscoverer.TraceSource.LogError("Usually this happens. Try resetting the device and try again. If you are in a VPN, disconnect and try again.");
				}
				throw;
			}
			finally
			{
				if (webResponse != null)
				{
					webResponse.Close();
				}
			}
		}

		private static XmlDocument ReadXmlResponse(WebResponse response)
		{
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
			{
				string xml = streamReader.ReadToEnd();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				return xmlDocument;
			}
		}
	}
}
