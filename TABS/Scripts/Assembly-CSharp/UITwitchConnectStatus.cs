using UnityEngine;

public class UITwitchConnectStatus : MonoBehaviour
{
	private bool setupForConnected;

	public LocalizeText Text;

	private void Start()
	{
	}

	private void Update()
	{
		TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
		if ((bool)service)
		{
			if (service.isConnected && !setupForConnected)
			{
				Text.Text.color = Color.green;
				Text.Args = new string[1] { service.IRC.channelName.ToUpper() };
				Text.LocaleID = "LABEL_CONNECTEDTO";
				setupForConnected = !setupForConnected;
			}
			else if (!service.isConnected && setupForConnected)
			{
				Text.Text.color = Color.red;
				Text.LocaleID = "LABEL_NOTCONNECTED";
				setupForConnected = !setupForConnected;
			}
		}
	}
}
