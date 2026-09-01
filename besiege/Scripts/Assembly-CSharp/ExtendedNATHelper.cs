using System;
using System.Collections;
using Besiege.Networking;
using UnityEngine;

public class ExtendedNATHelper : NATHelper
{
	private const float PunchthroughCheckInterval = 10f;

	private Action<int, ulong> onHolePunchedCallback;

	private float lastNatPunchthroughCheck;

	private NetworkAnalyser networkAnalyser;

	public override void Awake()
	{
		base.Awake();
		networkAnalyser = SingleInstanceFindOnly<NetworkAnalyser>.Instance;
	}

	public new IEnumerator startListeningForPunchthrough(Action<int, ulong> onHolePunched)
	{
		onHolePunchedCallback = onHolePunched;
		yield return base.startListeningForPunchthrough((Action<int, ulong>)OnHolePunched);
	}

	private void OnHolePunched(int port, ulong clientGuid)
	{
		onHolePunchedCallback(port, clientGuid);
	}

	private bool IsConnectedToFacilitator()
	{
		if (rakPeer == null)
		{
			return false;
		}
		if (rakPeer.IsActive() && rakPeer.NumberOfConnections() == 0)
		{
			return false;
		}
		if (!rakPeer.IsActive())
		{
			return false;
		}
		return true;
	}

	public override void Update()
	{
		base.Update();
		if (!StatMaster.isMP || SteamManager.Initialized || !networkAnalyser.DoneTesting || isConnectingToFacilitator || ((StatMaster.isHosting || StatMaster.isClient) && OptionsMaster.networkType != PlayerNetworkType.DirectConnect && StatMaster.networkActive) || !(Time.realtimeSinceStartup - lastNatPunchthroughCheck > 10f))
		{
			return;
		}
		lastNatPunchthroughCheck = Time.realtimeSinceStartup;
		if (!IsConnectedToFacilitator() && Application.internetReachability != NetworkReachability.NotReachable)
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Connection to facilitator timed out, trying to reconnect...");
			}
			if (!Network.isServer)
			{
				StopPunchingThrough();
			}
			else
			{
				StopListeningForPunchthrough();
			}
			DisconnectFromFacilitator(0u);
			if (Network.isServer)
			{
				StartCoroutine(startListeningForPunchthrough(onHolePunchedCallback));
			}
			else
			{
				StartCoroutine(connectToNATFacilitator());
			}
		}
	}
}
