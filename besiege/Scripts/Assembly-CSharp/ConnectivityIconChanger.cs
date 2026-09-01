using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ConnectivityIconChanger : MonoBehaviour
{
	private const int MaxPing = 999;

	private const float UpdateRate = 1f;

	[SerializeField]
	private Text pingText;

	[SerializeField]
	private Sprite[] connectivityIcons;

	private Image connectivityImage;

	private int lastPing = -1;

	private void Awake()
	{
		connectivityImage = GetComponent<Image>();
		if (pingText == null || connectivityIcons.Length == 0)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		InvokeRepeating("UpdatePing", 0f, 1f);
	}

	private void UpdatePing()
	{
		int ping = GetPing();
		if (lastPing != ping)
		{
			lastPing = ping;
			SetConnectivityImage(ping);
		}
	}

	private void SetConnectivityImage(int ping)
	{
		int connectivityIndex = GetConnectivityIndex(ping);
		connectivityImage.sprite = connectivityIcons[connectivityIndex];
	}

	private int GetConnectivityIndex(int ping)
	{
		if (ping > 500)
		{
			return 0;
		}
		if (ping > 200)
		{
			return 1;
		}
		if (ping > 100)
		{
			return 2;
		}
		return 3;
	}

	private int GetPing()
	{
		int result = 999;
		int.TryParse(pingText.text, out result);
		return result;
	}
}
