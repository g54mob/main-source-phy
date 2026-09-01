using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;
using RakNet;
using UnityEngine;
using UnityEngine.Networking;

namespace Besiege.Networking
{
	public class NATHelper : MonoBehaviour
	{
		private class PortMappingDoneInfo
		{
			public Mapping mapping;

			public bool success;

			public Exception exception;

			private object handle = new object();

			private bool _isDone;

			public bool isDone
			{
				get
				{
					lock (handle)
					{
						return _isDone;
					}
				}
				set
				{
					lock (handle)
					{
						_isDone = value;
					}
				}
			}

			public event Action<Mapping, bool, Exception> onPortMappingDone;

			public void fireEvent()
			{
				if (this.onPortMappingDone != null)
				{
					this.onPortMappingDone(mapping, success, exception);
				}
			}
		}

		private enum PunchthroughStateClient
		{
			PUNCHING_THROUGH_TO_SERVER = 0,
			CONNECTING_TO_SERVER = 1,
			WAITING_FOR_SERVER_TO_DISCONNECT = 2,
			WAITING_FOR_FACILITATOR_TO_DISCONNECT = 3,
			DONE = 4
		}

		private enum PunchthroughStateServer
		{
			WAITING_FOR_CLIENT_TO_PUNCH_THROUGH = 0,
			WAITING_FOR_CLIENT_TO_CONNECT = 1,
			WAITING_FOR_FACILITATOR_TO_DISCONNECT = 2,
			DONE = 3
		}

		private const string TAG = "NATHelper: ";

		private const float TICK = 0.05f;

		public string facilitatorIP = "ms.spiderlinggames.co.uk";

		public ushort facilitatorPort = 61111;

		public float facilitatorTimeOut = 10f;

		public bool portForwardingEnabled = true;

		public float portForwardingTimeOut = 10f;

		public float punchthroughTimeout = 30f;

		private float attempts;

		private int numActiveMappings;

		[NonSerialized]
		public bool isDoneFindingNATDevice;

		[NonSerialized]
		public ulong guid;

		[NonSerialized]
		public SystemAddress facilitatorAddress;

		[NonSerialized]
		public RakPeerInterface rakPeer;

		[NonSerialized]
		public NatDevice natDevice;

		[NonSerialized]
		public bool isListeningForPunchthrough;

		[NonSerialized]
		public bool isPunchingThrough;

		[NonSerialized]
		public bool isConnectingToFacilitator;

		private NatPunchthroughClient natPunchthroughClient;

		private Action<int, ulong> onHolePunchedServer;

		private Action<int, int, bool> onHolePunchedClient;

		private Task portForwardingTask;

		private CancellationTokenSource portForwardingCancellationToken;

		private List<Mapping> portMappings = new List<Mapping>();

		private List<PortMappingDoneInfo> portMappingDoneInfo = new List<PortMappingDoneInfo>();

		public static NATHelper singleton;

		private PunchthroughStateServer punchthroughStateServer;

		private PunchthroughStateClient punchthroughStateClient;

		private bool hasWarned;

		private float facilitatorConnectStartTime;

		private float punchthroughStartTime;

		private ushort natListenPort;

		private ushort natConnectPort;

		private ulong latestConnectingClientGUID;

		public bool isConnectedToFacilitator
		{
			get
			{
				return !isConnectingToFacilitator && guid != 0;
			}
		}

		public bool hasFailedToConnectToFacilitator
		{
			get
			{
				return !isConnectingToFacilitator && guid == 0;
			}
		}

		public bool isForwardingPort
		{
			get
			{
				return numActiveMappings > 0;
			}
		}

		public event Action<ulong> OnDoneConnectingToFacilitator;

		public virtual void Awake()
		{
			singleton = this;
			StartCoroutine(messageLoop());
		}

		public virtual void Update()
		{
			for (int num = portMappingDoneInfo.Count - 1; num >= 0; num--)
			{
				if (portMappingDoneInfo[num].isDone)
				{
					portMappingDoneInfo[num].fireEvent();
					portMappingDoneInfo.RemoveAt(num);
				}
			}
		}

		public IEnumerator messageLoop()
		{
			while (true)
			{
				if (rakPeer == null)
				{
					yield return new WaitForSeconds(0.05f);
					continue;
				}
				if (isConnectingToFacilitator && Time.realtimeSinceStartup - facilitatorConnectStartTime > facilitatorTimeOut)
				{
					isConnectingToFacilitator = false;
					OnDoneConnectingToFacilitatorInternal(0uL, null);
					attempts += 1f;
				}
				if (isPunchingThrough && Time.realtimeSinceStartup - punchthroughStartTime > punchthroughTimeout)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("NATHelper: Punchthrough attempt timed out");
					}
					isPunchingThrough = false;
					onHolePunchedClient(0, 0, false);
				}
				if (isListeningForPunchthrough && punchthroughStateServer != PunchthroughStateServer.WAITING_FOR_CLIENT_TO_PUNCH_THROUGH && Time.realtimeSinceStartup - punchthroughStartTime > punchthroughTimeout)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("NATHelper: Client punchthrough attempt timed out");
					}
					punchthroughStateServer = PunchthroughStateServer.WAITING_FOR_CLIENT_TO_PUNCH_THROUGH;
				}
				Packet packet = rakPeer.Receive();
				if (packet == null)
				{
					yield return new WaitForSeconds(0.05f);
					continue;
				}
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log(string.Concat("NATHelper: Received RakNet message: ", packet.systemAddress, ": ", (DefaultMessageIDTypes)packet.data[0]));
				}
				if (isConnectingToFacilitator)
				{
					if (!hasWarned && Time.realtimeSinceStartup - facilitatorConnectStartTime > facilitatorTimeOut / 2f)
					{
						if (BesiegeLogFilter.logDev)
						{
							Debug.Log("NATHelper: Facilitator connection is taking an unusually long time.");
						}
						hasWarned = true;
					}
					if (packet.data[0] == 16)
					{
						OnDoneConnectingToFacilitatorInternal(rakPeer.GetMyGUID().g, packet);
					}
					else
					{
						OnDoneConnectingToFacilitatorInternal(0uL, packet);
					}
				}
				else if (isListeningForPunchthrough)
				{
					listenForPunchthrough(packet);
				}
				else if (isPunchingThrough)
				{
					listenForPunchthroughResponse(packet);
				}
				else
				{
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("NATHelper: Ignoring message: " + (DefaultMessageIDTypes)packet.data[0]);
					}
					if (facilitatorAddress == null || packet.systemAddress != facilitatorAddress)
					{
						rakPeer.CloseConnection(packet.systemAddress, true, 0);
					}
				}
				yield return new WaitForSeconds(0.05f);
			}
		}

		public IEnumerator connectToNATFacilitator()
		{
			if (attempts > 9f)
			{
				yield break;
			}
			isListeningForPunchthrough = false;
			isPunchingThrough = false;
			if (rakPeer == null)
			{
				rakPeer = RakPeerInterface.GetInstance();
				rakPeer.SetMaximumIncomingConnections(2);
			}
			StartupResult startResult = rakPeer.Startup(2u, new SocketDescriptor(), 1u);
			if (startResult != StartupResult.RAKNET_STARTED)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: Failed to initialize network interface: " + startResult);
				}
				yield break;
			}
			ConnectionAttemptResult connectResult = rakPeer.Connect(facilitatorIP, facilitatorPort, string.Empty, 0);
			if (connectResult != ConnectionAttemptResult.CONNECTION_ATTEMPT_STARTED)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: Failed to initialize connection to NAT Facilitator: " + connectResult);
				}
				yield break;
			}
			isConnectingToFacilitator = true;
			facilitatorConnectStartTime = Time.realtimeSinceStartup;
			hasWarned = false;
			while (isConnectingToFacilitator)
			{
				yield return new WaitForEndOfFrame();
			}
		}

		public void findNatDevice(Action<bool> onDoneSearchingForNATDevice = null)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("NATHelper: Searching for nat devices");
			}
			NatDiscoverer natDiscoverer = new NatDiscoverer();
			stopPortForwarding();
			isDoneFindingNATDevice = false;
			try
			{
				portForwardingCancellationToken = new CancellationTokenSource();
				portForwardingCancellationToken.CancelAfter((int)(portForwardingTimeOut * 1000f));
				portForwardingTask = natDiscoverer.DiscoverDeviceAsync(PortMapper.Pmp | PortMapper.Upnp, portForwardingCancellationToken).ContinueWith(delegate(Task<NatDevice> task)
				{
					if (!task.IsFaulted && !task.IsCanceled && task.Result != null)
					{
						doneFindingNATDeviceInternal(task.Result, true, onDoneSearchingForNATDevice);
					}
					else
					{
						doneFindingNATDeviceInternal(null, false, onDoneSearchingForNATDevice);
					}
				});
			}
			catch (Exception ex)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATHelper: NAT Device not found: " + ex.Message);
				}
				isDoneFindingNATDevice = true;
				if (onDoneSearchingForNATDevice != null)
				{
					onDoneSearchingForNATDevice(false);
				}
			}
		}

		public void stopPortForwarding()
		{
			isDoneFindingNATDevice = true;
			if (portForwardingCancellationToken != null)
			{
				portForwardingCancellationToken.Cancel();
			}
		}

		public void mapPort(int privatePort, int publicPort = 0, int lifetime = 0, Protocol protocol = Protocol.Both, string desc = "", Action<Mapping, bool, Exception> onPortMappingDone = null)
		{
			if (!portForwardingEnabled && BesiegeLogFilter.logDev)
			{
				Debug.Log("NATHelper: Can not mapPort. portForwardingEnabled = false");
			}
			if (publicPort == 0)
			{
				publicPort = privatePort;
			}
			if (portForwardingTask == null)
			{
				if (LogFilter.logWarn && BesiegeLogFilter.logDev)
				{
					Debug.LogWarning("NATHelper: Port mapping will finish faster if you call findNatDevice() when your game starts.");
				}
				findNatDevice();
			}
			portForwardingTask.ContinueWith(delegate
			{
				mapPortInternal(privatePort, publicPort, lifetime, protocol, desc, onPortMappingDone);
			});
		}

		public void printPortMappings()
		{
			if (portForwardingTask == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATHelper: Printing port mappings will finish faster if you call findNatDevice() when your game starts.");
				}
				findNatDevice();
			}
			if (natDevice == null)
			{
				return;
			}
			portForwardingTask.ContinueWith(delegate
			{
				natDevice.GetAllMappingsAsync().ContinueWith(delegate(Task<IEnumerable<Mapping>> task)
				{
					foreach (Mapping item in task.Result)
					{
						Debug.Log(string.Concat("NATHelper: ", item.Description, " ", item.PrivateIP, ":", item.PrivatePort, " ", item.PublicIP, ":", item.PublicPort, " (", item.Protocol, ") ", item.Lifetime, " ", item.Expiration));
					}
				});
			});
		}

		public IEnumerator startListeningForPunchthrough(Action<int, ulong> onHolePunched)
		{
			if (!isConnectingToFacilitator && guid == 0L)
			{
				yield return StartCoroutine(connectToNATFacilitator());
			}
			if (guid != 0L)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: Listening for punchthrough");
				}
				onHolePunchedServer = onHolePunched;
				isListeningForPunchthrough = true;
				if (natPunchthroughClient == null)
				{
					natPunchthroughClient = new NatPunchthroughClient();
					rakPeer.AttachPlugin(natPunchthroughClient);
				}
				punchthroughStateServer = PunchthroughStateServer.WAITING_FOR_CLIENT_TO_PUNCH_THROUGH;
			}
		}

		public IEnumerator punchThroughToServer(ulong hostGUIDlong, Action<int, int, bool> onHolePunched)
		{
			if (!isConnectingToFacilitator && guid == 0L)
			{
				yield return StartCoroutine(connectToNATFacilitator());
			}
			if (guid != 0L)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: Attempting to punch through to host: " + hostGUIDlong);
				}
				onHolePunchedClient = onHolePunched;
				RakNetGUID hostGUID = new RakNetGUID(hostGUIDlong);
				punchthroughStartTime = Time.realtimeSinceStartup;
				isPunchingThrough = true;
				if (natPunchthroughClient == null)
				{
					natPunchthroughClient = new NatPunchthroughClient();
					rakPeer.AttachPlugin(natPunchthroughClient);
				}
				natPunchthroughClient.OpenNAT(hostGUID, facilitatorAddress);
				punchthroughStateClient = PunchthroughStateClient.PUNCHING_THROUGH_TO_SERVER;
			}
		}

		public void StopListeningForPunchthrough()
		{
			isListeningForPunchthrough = false;
			if (natPunchthroughClient != null)
			{
				if (rakPeer.IsActive())
				{
					rakPeer.DetachPlugin(natPunchthroughClient);
				}
				natPunchthroughClient = null;
			}
		}

		public void DisconnectFromFacilitator(uint blockDuration = 0, bool sendDisconnectNotification = false)
		{
			if (rakPeer != null && rakPeer.IsActive())
			{
				if (!(facilitatorAddress == null))
				{
					rakPeer.CloseConnection(facilitatorAddress, sendDisconnectNotification);
				}
				if (natPunchthroughClient != null)
				{
					rakPeer.DetachPlugin(natPunchthroughClient);
					natPunchthroughClient = null;
				}
				rakPeer.Shutdown(blockDuration);
				guid = 0uL;
			}
		}

		public void StopPunchingThrough()
		{
			isPunchingThrough = false;
			onHolePunchedClient = null;
			if (natPunchthroughClient != null)
			{
				if (rakPeer.IsActive())
				{
					rakPeer.DetachPlugin(natPunchthroughClient);
				}
				natPunchthroughClient = null;
			}
		}

		public void RemoveAllPortMappings()
		{
			if (natDevice != null)
			{
				try
				{
					foreach (Mapping portMapping in portMappings)
					{
						if (portMapping != null)
						{
							natDevice.DeletePortMapAsync(portMapping).Wait();
						}
					}
				}
				catch (OverflowException)
				{
				}
				catch (ArgumentNullException)
				{
				}
				catch (AggregateException)
				{
				}
			}
			portMappings.Clear();
		}

		public void OnDestroy()
		{
			if (!isDoneFindingNATDevice)
			{
				stopPortForwarding();
			}
			if (rakPeer != null)
			{
				rakPeer.Shutdown(0u);
				guid = 0uL;
				rakPeer.Dispose();
			}
			this.OnDoneConnectingToFacilitator = null;
			singleton = null;
		}

		public void MapFacilitatorPort()
		{
			mapPort(rakPeer.GetInternalID().GetPort(), 0, 0, Protocol.Both, string.Empty);
		}

		private void OnDoneConnectingToFacilitatorInternal(ulong guid, Packet packet)
		{
			isConnectingToFacilitator = false;
			this.guid = guid;
			if (guid == 0L)
			{
				if (LogFilter.logWarn)
				{
					if (packet == null)
					{
						if (BesiegeLogFilter.logDev)
						{
							Debug.LogWarning("NATHelper: Connection to facilitator timed out.");
						}
					}
					else
					{
						DefaultMessageIDTypes defaultMessageIDTypes = (DefaultMessageIDTypes)packet.data[0];
						DefaultMessageIDTypes defaultMessageIDTypes2 = defaultMessageIDTypes;
						if (defaultMessageIDTypes2 == DefaultMessageIDTypes.ID_CONNECTION_ATTEMPT_FAILED)
						{
							Debug.LogWarning("NATHelper: Failed to connect to Facilitator at " + facilitatorIP + ":" + facilitatorPort + ". Are you sure that it is running and that the address and port are correct?");
						}
						else
						{
							Debug.LogWarning("NATHelper: Can't connect to Facilitator for some reason: " + defaultMessageIDTypes);
						}
					}
				}
				if (this.OnDoneConnectingToFacilitator != null)
				{
					this.OnDoneConnectingToFacilitator(0uL);
				}
			}
			else
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: Connected to Facilitator: " + guid);
				}
				facilitatorAddress = packet.systemAddress;
				if (portForwardingEnabled)
				{
					MapFacilitatorPort();
				}
				natPunchthroughClient = new NatPunchthroughClient();
				rakPeer.AttachPlugin(natPunchthroughClient);
				natPunchthroughClient.FindRouterPortStride(facilitatorAddress);
				if (this.OnDoneConnectingToFacilitator != null)
				{
					this.OnDoneConnectingToFacilitator(guid);
				}
			}
		}

		private void listenForPunchthrough(Packet packet)
		{
			DefaultMessageIDTypes defaultMessageIDTypes = (DefaultMessageIDTypes)packet.data[0];
			try
			{
				switch (punchthroughStateServer)
				{
				case PunchthroughStateServer.WAITING_FOR_CLIENT_TO_PUNCH_THROUGH:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_NAT_PUNCHTHROUGH_SUCCEEDED)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (packet.data[1] != 1)
					{
						natListenPort = rakPeer.GetInternalID().GetPort();
						if (BesiegeLogFilter.logDev)
						{
							Debug.Log("NATHelper: Received punch-through: " + packet.systemAddress);
						}
						punchthroughStartTime = Time.realtimeSinceStartup;
						punchthroughStateServer = PunchthroughStateServer.WAITING_FOR_CLIENT_TO_CONNECT;
					}
					break;
				case PunchthroughStateServer.WAITING_FOR_CLIENT_TO_CONNECT:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_NEW_INCOMING_CONNECTION)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("NATHelper: Received incoming RakNet connection.");
					}
					latestConnectingClientGUID = packet.guid.g;
					rakPeer.CloseConnection(packet.guid, true);
					rakPeer.CloseConnection(facilitatorAddress, true);
					punchthroughStateServer = PunchthroughStateServer.WAITING_FOR_FACILITATOR_TO_DISCONNECT;
					break;
				case PunchthroughStateServer.WAITING_FOR_FACILITATOR_TO_DISCONNECT:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (packet.systemAddress == facilitatorAddress)
					{
						if (BesiegeLogFilter.logDev)
						{
							Debug.Log("NATHelper: Hole is punched on port: " + natListenPort);
						}
						rakPeer.DetachPlugin(natPunchthroughClient);
						rakPeer.Shutdown(200u);
						natPunchthroughClient = null;
						guid = 0uL;
						if (onHolePunchedServer != null)
						{
							onHolePunchedServer(natListenPort, latestConnectingClientGUID);
						}
						isListeningForPunchthrough = false;
						StartCoroutine(startListeningForPunchthrough(onHolePunchedServer));
						punchthroughStateServer = PunchthroughStateServer.DONE;
					}
					break;
				default:
					throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
				}
			}
			catch (Exception ex)
			{
				if (punchthroughStateServer != PunchthroughStateServer.WAITING_FOR_CLIENT_TO_PUNCH_THROUGH)
				{
					if (onHolePunchedClient != null)
					{
						onHolePunchedClient(0, 0, false);
					}
					StopListeningForPunchthrough();
				}
				Debug.Log("NATHelper: " + ex.Message);
			}
		}

		public void listenForPunchthroughResponse(Packet packet)
		{
			DefaultMessageIDTypes defaultMessageIDTypes = (DefaultMessageIDTypes)packet.data[0];
			try
			{
				switch (punchthroughStateClient)
				{
				case PunchthroughStateClient.PUNCHING_THROUGH_TO_SERVER:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_NAT_PUNCHTHROUGH_SUCCEEDED)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("NATHelper: Listening for incoming RakNet connection.");
					}
					natConnectPort = packet.systemAddress.GetPort();
					rakPeer.Connect(packet.systemAddress.ToString(false), natConnectPort, string.Empty, 0);
					punchthroughStateClient = PunchthroughStateClient.CONNECTING_TO_SERVER;
					break;
				case PunchthroughStateClient.CONNECTING_TO_SERVER:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("NATHelper: RakNet connection received.");
					}
					natListenPort = rakPeer.GetExternalID(packet.systemAddress).GetPort();
					punchthroughStateClient = PunchthroughStateClient.WAITING_FOR_SERVER_TO_DISCONNECT;
					break;
				case PunchthroughStateClient.WAITING_FOR_SERVER_TO_DISCONNECT:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (packet.systemAddress == facilitatorAddress)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (BesiegeLogFilter.logDev)
					{
						Debug.Log("NATHelper: Hole punched: " + natListenPort + "->" + natConnectPort);
					}
					rakPeer.CloseConnection(facilitatorAddress, true);
					punchthroughStateClient = PunchthroughStateClient.WAITING_FOR_FACILITATOR_TO_DISCONNECT;
					break;
				case PunchthroughStateClient.WAITING_FOR_FACILITATOR_TO_DISCONNECT:
					if (defaultMessageIDTypes != DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					if (packet.systemAddress != facilitatorAddress)
					{
						throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
					}
					rakPeer.DetachPlugin(natPunchthroughClient);
					rakPeer.Shutdown(200u);
					natPunchthroughClient = null;
					onHolePunchedClient(natListenPort, natConnectPort, true);
					StartCoroutine(connectToNATFacilitator());
					isPunchingThrough = false;
					punchthroughStateClient = PunchthroughStateClient.DONE;
					break;
				default:
					throw new Exception("Unexpected raknet message received: " + defaultMessageIDTypes);
				}
			}
			catch (Exception ex)
			{
				onHolePunchedClient(0, 0, false);
				StopPunchingThrough();
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATHelper: " + ex.Message);
				}
			}
		}

		private void doneFindingNATDeviceInternal(NatDevice device, bool success, Action<bool> onDoneSearchingForNATDevice)
		{
			if (isDoneFindingNATDevice)
			{
				return;
			}
			if (success && natDevice == null)
			{
				natDevice = device;
				isDoneFindingNATDevice = true;
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log("NATHelper: NAT device found");
				}
				if (onDoneSearchingForNATDevice != null)
				{
					onDoneSearchingForNATDevice(success);
				}
			}
			else
			{
				isDoneFindingNATDevice = true;
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogWarning("NATHelper: NAT Device not found");
				}
				if (onDoneSearchingForNATDevice != null)
				{
					onDoneSearchingForNATDevice(success);
				}
			}
		}

		private void mapPortInternal(int privatePort, int publicPort, int lifetime, Protocol protocol, string desc, Action<Mapping, bool, Exception> onPortMappingDone)
		{
			if (natDevice != null)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.Log(string.Concat("NATHelper: Attempting to map port ", privatePort, "->", publicPort, " (", protocol, ")"));
				}
				if (protocol != Protocol.Tcp)
				{
					internalCreatePortMapping(privatePort, publicPort, lifetime, Open.Nat.Protocol.Udp, desc, onPortMappingDone);
				}
				if (protocol != Protocol.Udp)
				{
					internalCreatePortMapping(privatePort, publicPort, lifetime, Open.Nat.Protocol.Tcp, desc, onPortMappingDone);
				}
				if (LogFilter.currentLogLevel == 0)
				{
					printPortMappings();
				}
			}
		}

		private void internalCreatePortMapping(int privatePort, int publicPort, int lifetime, Open.Nat.Protocol protocol, string desc, Action<Mapping, bool, Exception> onPortMappingDone)
		{
			Mapping mapping = null;
			mapping = new Mapping(protocol, privatePort, publicPort, lifetime, desc);
			portMappings.Add(mapping);
			numActiveMappings++;
			PortMappingDoneInfo info = new PortMappingDoneInfo();
			info.onPortMappingDone += onPortMappingDone;
			info.mapping = mapping;
			portMappingDoneInfo.Add(info);
			natDevice.CreatePortMapAsync(mapping, portForwardingTimeOut).ContinueWith(delegate(Task t)
			{
				OnPortMappingDoneInternal(t, mapping, info);
			});
		}

		private void OnPortMappingDoneInternal(Task task, Mapping mapping, PortMappingDoneInfo onPortMappingDoneInfo)
		{
			numActiveMappings--;
			if (task.IsFaulted)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogWarning(string.Concat("NATHelper: Something went wrong mapping port ", mapping.PrivatePort, "->", mapping.PublicPort, " (", mapping.Protocol, ") ", mapping.Description));
				}
			}
			else if (BesiegeLogFilter.logDev)
			{
				Debug.Log(string.Concat("NATHelper: Port mapping finished ", mapping.PrivatePort, "->", mapping.PublicPort, " (", mapping.Protocol, ") ", mapping.Description));
			}
			onPortMappingDoneInfo.success = task.IsCompleted && !task.IsFaulted;
			onPortMappingDoneInfo.exception = task.Exception;
			onPortMappingDoneInfo.isDone = true;
		}

		public void OnDrawGizmos()
		{
		}
	}
}
