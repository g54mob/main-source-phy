using System;
using System.Text;
using Besiege.Networking;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

public class MultiverseConnectionInfo : MonoBehaviour, ILocalisationAware
{
	public Action ConnectionTestDone;

	[SerializeField]
	private Text upnpStatusText;

	[SerializeField]
	private Text natStatusText;

	[SerializeField]
	private Image natIconImage;

	[SerializeField]
	private Image upnpIconImage;

	[SerializeField]
	private Color failureColor = new Color(61f / 85f, 0f, 0f);

	[SerializeField]
	private Color successColor = new Color(1f / 15f, 0.5803922f, 0f);

	[SerializeField]
	private Color neutralColor = new Color(0.8784314f, 0.8784314f, 0.8784314f);

	[SerializeField]
	private Color warningColor = new Color(1f, 0.678f, 0.129f);

	[SerializeField]
	private Button connectionInfoButton;

	[SerializeField]
	private Text connectionInfoLabel;

	private NATHelper natHelper;

	private NATConnectionTester natConnectionTester;

	private bool isWindowOpen;

	private void Awake()
	{
		natIconImage.color = neutralColor;
		connectionInfoButton.onClick.AddListener(OnConnectionInfoButtonClicked);
		ReferenceMaster.UPNPStatusChanged = (Action<UPNPStatus>)Delegate.Combine(ReferenceMaster.UPNPStatusChanged, new Action<UPNPStatus>(UPNPStatusChanged));
		ReferenceMaster.ConnectionTesterStatusChanged = (Action<ConnectionTesterStatus>)Delegate.Combine(ReferenceMaster.ConnectionTesterStatusChanged, new Action<ConnectionTesterStatus>(OnConnectionTesterStatusChanged));
	}

	private void Start()
	{
		UpdateInformation();
		SetButtonAndLabelColors();
		if (!SteamManager.Initialized)
		{
			natHelper = SingleInstanceFindOnly<NetworkAnalyser>.Instance.NATHelper;
			natHelper.OnDoneConnectingToFacilitator += OnDoneConnectingToFacilitator;
			natConnectionTester = SingleInstanceFindOnly<NetworkAnalyser>.Instance.NATConnectionTester;
			if (natConnectionTester.DoneTesting)
			{
				OnConnectionTesterStatusChanged(natConnectionTester.ConnectionTestResult);
			}
			else
			{
				Invoke("TimeoutConnectionTester", 5f);
			}
		}
		ToggleWindow(false);
	}

	private void SetButtonAndLabelColors()
	{
		if (ReferenceMaster.IsPlatformReady())
		{
			connectionInfoLabel.text = LocalisationManager.GetTranslation(3260);
			connectionInfoLabel.color = neutralColor;
			connectionInfoButton.image.color = neutralColor;
			connectionInfoButton.interactable = false;
		}
		else if (!(natConnectionTester == null))
		{
			string empty = string.Empty;
			Color color;
			switch (natConnectionTester.NATStatus)
			{
			case NATConnectionTester.NATSimpleStatus.NotCapable:
				color = failureColor;
				empty = LocalisationManager.GetTranslation(3187);
				break;
			case NATConnectionTester.NATSimpleStatus.Limited:
				color = warningColor;
				empty = LocalisationManager.GetTranslation(3187);
				break;
			case NATConnectionTester.NATSimpleStatus.Good:
				color = successColor;
				empty = LocalisationManager.GetTranslation(3188);
				break;
			default:
				color = neutralColor;
				empty = LocalisationManager.GetTranslation(3190);
				break;
			}
			connectionInfoLabel.text = empty;
			connectionInfoLabel.color = color;
			connectionInfoButton.image.color = color;
		}
	}

	private void OnConnectionInfoButtonClicked()
	{
		ToggleWindow(!isWindowOpen);
	}

	private void ToggleWindow(bool open)
	{
		isWindowOpen = open;
		base.gameObject.SetActive(isWindowOpen);
	}

	private void TimeoutConnectionTester()
	{
		if (ConnectionTestDone != null)
		{
			ConnectionTestDone();
		}
		if (natConnectionTester.NATStatus == NATConnectionTester.NATSimpleStatus.Undetermined)
		{
			natStatusText.text = "Connection tester timed out...";
			natIconImage.color = failureColor;
			SetButtonAndLabelColors();
		}
		else
		{
			UpdateNATStatusText();
		}
	}

	private void UpdateNATInfo()
	{
		if (!(natConnectionTester == null))
		{
			if (natConnectionTester.DoneTesting && ConnectionTestDone != null)
			{
				ConnectionTestDone();
			}
			UpdateNATStatusText();
			SetButtonAndLabelColors();
		}
	}

	private void OnConnectionTesterStatusChanged(ConnectionTesterStatus testerStatus)
	{
		CancelInvoke("TimeoutConnectionTester");
		UpdateNATInfo();
	}

	private void OnDoneConnectingToFacilitator(ulong guid)
	{
		if (guid == 0L)
		{
			natStatusText.text = LocalisationManager.GetTranslation(1911);
			natIconImage.color = failureColor;
			SetButtonAndLabelColors();
		}
		else
		{
			UpdateNATStatusText();
		}
	}

	private void UpdateNATStatusText()
	{
		if (!(natConnectionTester == null))
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!natConnectionTester.DoneTesting)
			{
				stringBuilder.Append(LocalisationManager.GetTranslation(1918));
				stringBuilder.Append(natConnectionTester.NatTestMessage);
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine(natConnectionTester.NatTestMessage);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(LocalisationManager.GetTranslation(1919));
			stringBuilder.Append(natConnectionTester.ConnectionTestResult.ToString());
			natStatusText.text = stringBuilder.ToString();
			switch (natConnectionTester.NATStatus)
			{
			case NATConnectionTester.NATSimpleStatus.Undetermined:
				natIconImage.color = neutralColor;
				break;
			case NATConnectionTester.NATSimpleStatus.NotCapable:
				natIconImage.color = failureColor;
				break;
			case NATConnectionTester.NATSimpleStatus.Limited:
				natIconImage.color = warningColor;
				break;
			case NATConnectionTester.NATSimpleStatus.Good:
				natIconImage.color = successColor;
				break;
			}
		}
	}

	private void UPNPStatusChanged(UPNPStatus status)
	{
		upnpStatusText.text = ReferenceMaster.TranslateUPNPStatus(status);
		Color color;
		switch (status)
		{
		case UPNPStatus.FailedToInitialize:
		case UPNPStatus.PortforwardingFailed:
			color = failureColor;
			break;
		case UPNPStatus.Initialized:
		case UPNPStatus.PortforwardingSucceeded:
			color = successColor;
			break;
		default:
			color = neutralColor;
			break;
		}
		upnpIconImage.color = color;
	}

	private void OnDestroy()
	{
		ReferenceMaster.UPNPStatusChanged = (Action<UPNPStatus>)Delegate.Remove(ReferenceMaster.UPNPStatusChanged, new Action<UPNPStatus>(UPNPStatusChanged));
		ReferenceMaster.ConnectionTesterStatusChanged = (Action<ConnectionTesterStatus>)Delegate.Remove(ReferenceMaster.ConnectionTesterStatusChanged, new Action<ConnectionTesterStatus>(OnConnectionTesterStatusChanged));
		if (natHelper != null)
		{
			natHelper.OnDoneConnectingToFacilitator -= OnDoneConnectingToFacilitator;
		}
		CancelInvoke("TimeoutConnectionTester");
	}

	private void UpdateInformation()
	{
		UpdateNATStatusText();
		UPNPStatusChanged(ReferenceMaster.UPNPStatus);
	}

	public void OnLocalisationChange()
	{
		if (base.enabled && Application.isPlaying)
		{
			UpdateInformation();
		}
	}
}
