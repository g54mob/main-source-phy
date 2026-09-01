using System;
using System.Diagnostics;
using System.Text;
using Besiege.Networking;
using Localisation;
using Open.Nat;
using UnityEngine;

public class UPNPController : MonoBehaviour, IConnectionController
{
	private const float UPNPCheckTimeout = 2f;

	private int port = StatMaster.DefaultPort;

	private ExtendedNATHelper natHelper;

	private UnityMainThreadDispatcher dispatcher;

	private UPNPStatus upnpStatus;

	private bool isDestroyed;

	public bool DoneTesting { get; private set; }

	public bool IsInitialized { get; private set; }

	public int Port
	{
		get
		{
			return port;
		}
		set
		{
			port = value;
		}
	}

	public void Setup(ExtendedNATHelper natHelper)
	{
		this.natHelper = natHelper;
		AddMainThreadDispatcher();
		SetNatDiscovererLogWriter();
		natHelper.findNatDevice(OnDoneSearchingForNATDevice);
		ReferenceMaster.onTogglePortForwarding = ToggleUPNP;
		InvokeUPNPChanged();
		Invoke("EnforceUPNPTimeout", 2f);
		IsInitialized = true;
	}

	private void AddMainThreadDispatcher()
	{
		if (UnityMainThreadDispatcher.Exists())
		{
			dispatcher = UnityMainThreadDispatcher.Instance();
			return;
		}
		GameObject gameObject = new GameObject("MainThreadDispatcher");
		dispatcher = gameObject.AddComponent<UnityMainThreadDispatcher>();
	}

	private void EnforceUPNPTimeout()
	{
		if (upnpStatus == UPNPStatus.Initializing || upnpStatus == UPNPStatus.ForwardingPort)
		{
			DoneTesting = true;
			upnpStatus = UPNPStatus.FailedToInitialize;
			InvokeUPNPChanged();
		}
	}

	private void OnDoneSearchingForNATDevice(bool deviceFound)
	{
		DoneTesting = true;
		if (isDestroyed)
		{
			return;
		}
		dispatcher.Enqueue(delegate
		{
			if (!deviceFound)
			{
				upnpStatus = UPNPStatus.FailedToInitialize;
				StringBuilder stringBuilder = new StringBuilder("Failed to initialize UPnP.\n");
				stringBuilder.Append("Device not found. Common reasons:\n");
				stringBuilder.Append(" * No device is present or,\n");
				stringBuilder.Append(" * Upnp is disabled in the router or\n");
				stringBuilder.Append(" * Antivirus software is filtering SSDP (discovery protocol).");
				if (BesiegeLogFilter.logDev)
				{
					UnityEngine.Debug.LogWarning(stringBuilder.ToString());
				}
				InvokeUPNPChanged();
			}
			else
			{
				ToggleUPNP(true);
			}
		});
	}

	public void ToggleUPNP(bool enable)
	{
		if (enable)
		{
			upnpStatus = UPNPStatus.ForwardingPort;
			natHelper.portForwardingEnabled = true;
			natHelper.mapPort(port, port, 0, Besiege.Networking.Protocol.Udp, "Besiege", OnPortMappingDone);
		}
		else
		{
			RemovePortMappings();
			if (upnpStatus == UPNPStatus.PortforwardingSucceeded)
			{
				upnpStatus = UPNPStatus.Initialized;
			}
		}
		InvokeUPNPChanged();
	}

	private string TranslateMappingException(MappingException me)
	{
		StringBuilder stringBuilder = new StringBuilder("Port forwarding failed, error: ");
		StringBuilder stringBuilder2 = new StringBuilder(LocalisationManager.GetTranslation(2945));
		switch (me.ErrorCode)
		{
		case 718:
			stringBuilder.AppendLine("The external port already in use.");
			stringBuilder2.AppendLine(LocalisationManager.GetTranslation(2946));
			break;
		case 728:
			stringBuilder.AppendLine("The router's mapping table is full.");
			stringBuilder2.AppendLine(LocalisationManager.GetTranslation(2947));
			break;
		case 401:
			stringBuilder.AppendLine("UPnP was not enabled in the router.");
			stringBuilder2.AppendLine(LocalisationManager.GetTranslation(2948));
			break;
		default:
			stringBuilder.AppendLine("Unknown error(" + me.ErrorCode + "): " + me.ErrorText);
			stringBuilder2.AppendLine(string.Format(LocalisationManager.GetTranslation(2949), me.ErrorCode, me.ErrorText));
			break;
		}
		if (BesiegeLogFilter.logDebug)
		{
			UnityEngine.Debug.LogWarning(stringBuilder.ToString());
		}
		return stringBuilder2.ToString();
	}

	private void OnPortMappingDone(Mapping mapping, bool wasSuccessful, Exception taskException)
	{
		if (!wasSuccessful)
		{
			natHelper.portForwardingEnabled = false;
			upnpStatus = UPNPStatus.PortforwardingFailed;
			MappingException ex = taskException.InnerException as MappingException;
			if (ex != null)
			{
				string uPNPError = TranslateMappingException(ex);
				ReferenceMaster.UPNPError = uPNPError;
			}
			else
			{
				ReferenceMaster.UPNPError = LocalisationManager.GetTranslation(1916);
			}
		}
		else
		{
			upnpStatus = UPNPStatus.PortforwardingSucceeded;
			if (BesiegeLogFilter.logInfo)
			{
				UnityEngine.Debug.Log("Port forwarded: " + mapping.ToString());
			}
		}
		dispatcher.Enqueue(delegate
		{
			InvokeUPNPChanged();
		});
	}

	protected void OnDestroy()
	{
		isDestroyed = true;
		if (IsInitialized && natHelper.isForwardingPort)
		{
			RemovePortMappings();
		}
	}

	private void RemovePortMappings()
	{
		if (BesiegeLogFilter.logInfo)
		{
			UnityEngine.Debug.Log("Removing all port mappings");
		}
		natHelper.RemoveAllPortMappings();
	}

	private void InvokeUPNPChanged()
	{
		ReferenceMaster.UPNPStatus = upnpStatus;
		if (ReferenceMaster.UPNPStatusChanged != null)
		{
			ReferenceMaster.UPNPStatusChanged(upnpStatus);
		}
	}

	private void SetNatDiscovererLogWriter()
	{
		if (SingleInstance<StatMaster>.Instance.isDeveloper)
		{
			TextWriterTraceListener listener = new TextWriterTraceListener(new UnityLogWriter());
			NatDiscoverer.TraceSource.Switch.Level = SourceLevels.Verbose;
			NatDiscoverer.TraceSource.Listeners.Add(listener);
		}
	}

	public void Retest()
	{
		natHelper.findNatDevice(OnDoneSearchingForNATDevice);
		CancelInvoke("EnforceUPNPTimeout");
		Invoke("EnforceUPNPTimeout", 2f);
	}
}
