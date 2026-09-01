using UnityEngine;

public class OnlyEnableWithTwitch : MonoBehaviour
{
	private void Start()
	{
		TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
		bool isConnected = service.isConnected;
		SetVisibility(isConnected);
		service.OnConnect.AddListener(OnConnect);
		service.OnDisconnect.AddListener(OnDisconnect);
	}

	private void Update()
	{
	}

	private void SetVisibility(bool visible)
	{
		base.gameObject.SetActive(visible);
	}

	private void OnConnect(string channel)
	{
		SetVisibility(visible: true);
	}

	private void OnDisconnect()
	{
		SetVisibility(visible: false);
	}
}
