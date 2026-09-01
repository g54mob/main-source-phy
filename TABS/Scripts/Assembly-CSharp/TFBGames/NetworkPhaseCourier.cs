using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class NetworkPhaseCourier : GlobalEventListener
	{
		private INetworkService m_networkService;

		public NetworkGamePhase Phase { get; private set; }

		public NetworkGamePhase RemotePhase { get; private set; }

		public event PhaseChangedEventHandler PhaseChanged;

		public event PhaseChangedEventHandler RemotePhaseChanged;

		private void Start()
		{
			m_networkService = ServiceLocator.GetService<INetworkService>();
		}

		public void SetPhase(NetworkGamePhase phase)
		{
			if (Phase != phase)
			{
				NetworkGamePhase phase2 = Phase;
				Phase = phase;
				SendGamePhaseEvent(phase);
				this.PhaseChanged?.Invoke(phase2, phase);
			}
		}

		public void DestroyCourier()
		{
			Object.Destroy(base.gameObject);
		}

		public override void Disconnected(BoltConnection connection)
		{
			if (connection.DisconnectReason == UdpConnectionDisconnectReason.Timeout && connection.ConnectionType == UdpConnectionType.Unknown)
			{
				Debug.Log("Ignoring unknown timeout disconnection message.");
				return;
			}
			base.Disconnected(connection);
			SetRemotePhase(NetworkGamePhase.Disconnected);
		}

		public override void OnEvent(GamePhaseEvent phaseEvent)
		{
			base.OnEvent(phaseEvent);
			NetworkGamePhase phase = (NetworkGamePhase)phaseEvent.Phase;
			if (RemotePhase == NetworkGamePhase.Initializing)
			{
				SendGamePhaseEvent(Phase);
			}
			SetRemotePhase(phase);
		}

		private void SetRemotePhase(NetworkGamePhase phase)
		{
			if (RemotePhase != phase)
			{
				NetworkGamePhase remotePhase = RemotePhase;
				RemotePhase = phase;
				this.RemotePhaseChanged?.Invoke(remotePhase, phase);
			}
		}

		private void SendGamePhaseEvent(NetworkGamePhase phase)
		{
			if (m_networkService == null || (m_networkService.IsRunning && m_networkService.IsConnected))
			{
				GamePhaseEvent gamePhaseEvent = GamePhaseEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
				if (gamePhaseEvent != null)
				{
					gamePhaseEvent.Phase = (int)phase;
					gamePhaseEvent.Send();
				}
			}
		}
	}
}
