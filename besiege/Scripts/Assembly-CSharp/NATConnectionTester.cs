using System;
using Localisation;
using UnityEngine;

public class NATConnectionTester : MonoBehaviour, IConnectionController
{
	public enum NATSimpleStatus
	{
		Undetermined = 0,
		NotCapable = 1,
		Limited = 2,
		Good = 3
	}

	public NATSimpleStatus NATStatus;

	private ConnectionTesterStatus connectionTestResult = ConnectionTesterStatus.Undetermined;

	private float natTestTimer;

	public bool DoneTesting { get; private set; }

	public bool IsInitialized { get; private set; }

	public string NatTestStatus { get; private set; }

	public string NatTestMessage { get; private set; }

	public ConnectionTesterStatus ConnectionTestResult
	{
		get
		{
			return connectionTestResult;
		}
	}

	public void Retest()
	{
		CancelInvoke("TestConnection");
		connectionTestResult = ConnectionTesterStatus.Undetermined;
		NATStatus = NATSimpleStatus.Undetermined;
		Initialize();
	}

	public void Setup(ExtendedNATHelper natHelper)
	{
		Initialize();
		IsInitialized = true;
	}

	public void TestConnection()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			CancelTestConnection();
			return;
		}
		connectionTestResult = Network.TestConnection();
		switch (connectionTestResult)
		{
		case ConnectionTesterStatus.Error:
			NatTestMessage = LocalisationManager.GetTranslation(1923);
			DoneTesting = true;
			NATStatus = NATSimpleStatus.NotCapable;
			break;
		case ConnectionTesterStatus.Undetermined:
			NatTestMessage = LocalisationManager.GetTranslation(1924);
			DoneTesting = false;
			break;
		case ConnectionTesterStatus.PublicIPIsConnectable:
			NatTestMessage = LocalisationManager.GetTranslation(1925);
			DoneTesting = true;
			NATStatus = NATSimpleStatus.Good;
			break;
		case ConnectionTesterStatus.PublicIPPortBlocked:
			NatTestMessage = string.Format(LocalisationManager.GetTranslation(1926), 9999);
			NATStatus = NATSimpleStatus.NotCapable;
			DoneTesting = true;
			break;
		case ConnectionTesterStatus.PublicIPNoServerStarted:
			NatTestMessage = LocalisationManager.GetTranslation(1928);
			NATStatus = NATSimpleStatus.Limited;
			break;
		case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
			NatTestMessage = LocalisationManager.GetTranslation(1929);
			DoneTesting = true;
			NATStatus = NATSimpleStatus.Limited;
			break;
		case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
			NatTestMessage = LocalisationManager.GetTranslation(1929);
			DoneTesting = true;
			NATStatus = NATSimpleStatus.Limited;
			break;
		case ConnectionTesterStatus.NATpunchthroughFullCone:
		case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
			NatTestMessage = LocalisationManager.GetTranslation(1930);
			DoneTesting = true;
			NATStatus = NATSimpleStatus.Good;
			break;
		default:
			NatTestMessage = string.Format(LocalisationManager.GetTranslation(1931), connectionTestResult);
			NATStatus = NATSimpleStatus.NotCapable;
			break;
		}
		if (DoneTesting)
		{
			StopStubServer();
			NatTestStatus = LocalisationManager.GetTranslation(1932);
			CancelInvoke("TestConnection");
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Done testing NAT capabilities, ConnectionTesterResult: " + ConnectionTestResult);
			}
		}
		InvokeStatusChanged();
	}

	private void InitStubServer()
	{
		int num = 49152;
		int max = 65535;
		int num2 = num;
		NetworkConnectionError networkConnectionError = NetworkConnectionError.AlreadyConnectedToAnotherServer;
		if (Network.isServer)
		{
			Network.Disconnect();
		}
		do
		{
			num2 = UnityEngine.Random.Range(num, max);
			try
			{
				networkConnectionError = Network.InitializeServer(4, num2, true);
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception occurred: " + ex.ToString());
			}
		}
		while (networkConnectionError != NetworkConnectionError.NoError);
	}

	private void StopStubServer()
	{
		Network.Disconnect();
	}

	private void Initialize()
	{
		DoneTesting = false;
		Network.connectionTesterIP = OptionsMaster.BesiegeConfig.ConnectiontesterIP;
		Network.connectionTesterPort = OptionsMaster.BesiegeConfig.ConnectiontesterPort;
		Network.natFacilitatorIP = OptionsMaster.BesiegeConfig.FacilitatorIP;
		Network.natFacilitatorPort = OptionsMaster.BesiegeConfig.FacilitatorPort;
		MasterServer.ipAddress = OptionsMaster.BesiegeConfig.MasterserverIP;
		MasterServer.port = OptionsMaster.BesiegeConfig.MasterserverPort;
		InitStubServer();
		if (BesiegeLogFilter.logDev)
		{
			Debug.Log(string.Format("Testing connection with the host '{0}:{1} and {2}:{3}'.", Network.connectionTesterIP, Network.connectionTesterPort, Network.natFacilitatorIP, Network.natFacilitatorPort));
		}
		NatTestStatus = LocalisationManager.GetTranslation(1920);
		NatTestMessage = LocalisationManager.GetTranslation(1921);
		InvokeRepeating("TestConnection", 0.5f, 0.1f);
	}

	private void CancelTestConnection()
	{
		CancelInvoke("TestConnection");
		NatTestMessage = LocalisationManager.GetTranslation(1933);
		DoneTesting = true;
		NATStatus = NATSimpleStatus.NotCapable;
		InvokeStatusChanged();
		if (BesiegeLogFilter.logWarn)
		{
			Debug.LogWarning("Could not test connection because internet is not reachable.\nSome features like NAT punchthrough or connect through Steam won't work.");
		}
	}

	private void InvokeStatusChanged()
	{
		if (ReferenceMaster.ConnectionTesterStatusChanged != null)
		{
			ReferenceMaster.ConnectionTesterStatusChanged(connectionTestResult);
		}
	}
}
