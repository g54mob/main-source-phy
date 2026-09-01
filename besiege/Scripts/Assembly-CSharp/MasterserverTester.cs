using System;
using UnityEngine;

public class MasterserverTester : MonoBehaviour
{
	private void Awake()
	{
		BesiegeLogFilter.currentLogLevel = 0;
		RegisterMasterserver();
	}

	private void RegisterMasterserver()
	{
		MasterServer.ipAddress = IPAddressHelper.ResolveOrFallback(OptionsMaster.BesiegeConfig.MasterserverIP, "91.121.78.210");
		MasterServer.port = OptionsMaster.BesiegeConfig.MasterserverPort;
		MasterServer.ClearHostList();
		InitStubServer();
		MasterServer.UnregisterHost();
		MasterServer.RegisterHost("joehoe", "test");
	}

	private void InitStubServer()
	{
		int num = 49152;
		int max = 65535;
		int num2 = num;
		NetworkConnectionError networkConnectionError = NetworkConnectionError.AlreadyConnectedToAnotherServer;
		do
		{
			num2 = UnityEngine.Random.Range(num, max);
			try
			{
				networkConnectionError = Network.InitializeServer(0, num2, false);
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception occurred: " + ex.ToString());
			}
		}
		while (networkConnectionError != NetworkConnectionError.NoError);
	}

	private void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("OnMasterServerEvent, msEvent=" + msEvent);
		}
	}

	private void OnDisconnectedFromMasterServer(NetworkDisconnection info)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("OnDisconnectedFromMasterServer, info=" + info);
		}
	}

	private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
	{
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("OnFailedToConnectToMasterServer, error=" + error);
		}
	}

	private void Update()
	{
		MasterServer.RequestHostList("joehoe");
		if (MasterServer.PollHostList().Length != 0)
		{
			HostData[] array = MasterServer.PollHostList();
			for (int i = 0; i < array.Length; i++)
			{
				Debug.Log("Game name: " + array[i].gameName);
			}
			MasterServer.ClearHostList();
		}
	}
}
