using TMPro;
using UnityEngine;

public class DebugChatFeed : MonoBehaviour
{
	private TextMeshProUGUI text;

	private int currIndex;

	private bool hookedUp;

	private void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
		text.text = "";
	}

	private void BitDonationPrint(string name, int bit)
	{
		string line = name + " Donated : " + bit + " bits!";
		AddTextLine(line);
	}

	private void SubscriberPrint(string name)
	{
		AddTextLine(name + " Subscribed!");
	}

	private void TextPrint(string name, string message)
	{
		AddTextLine(name + " : " + message);
	}

	private void AddTextLine(string line)
	{
		currIndex++;
		text.text = currIndex + ". " + line + "\n" + text.text;
	}

	private void Update()
	{
		if (!hookedUp)
		{
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			if ((bool)service)
			{
				hookedUp = true;
				service.OnBitDonation.AddListener(BitDonationPrint);
				service.OnSubscribe.AddListener(SubscriberPrint);
				service.OnMessage.AddListener(TextPrint);
			}
		}
	}
}
