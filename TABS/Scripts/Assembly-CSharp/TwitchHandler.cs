using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwitchHandler : ServicePrefab
{
	public class ParsedMsgEvent : UnityEvent<string, string>
	{
	}

	public class ParsedBitEvent : UnityEvent<string, int>
	{
	}

	public class ParsedSubEvent : UnityEvent<string>
	{
	}

	public class ConnectEvent : UnityEvent<string>
	{
	}

	public class DisconnectEvent : UnityEvent
	{
	}

	public ParsedMsgEvent OnMessage = new ParsedMsgEvent();

	public ParsedBitEvent OnBitDonation = new ParsedBitEvent();

	public ParsedSubEvent OnSubscribe = new ParsedSubEvent();

	public ConnectEvent OnConnect = new ConnectEvent();

	public DisconnectEvent OnDisconnect = new DisconnectEvent();

	public string DebugChannel;

	[HideInInspector]
	private string Channel;

	public bool AutoConnectToDebugChannel = true;

	[HideInInspector]
	public TwitchViewerInfo ViewerInfo;

	[HideInInspector]
	public TwitchIRC IRC;

	public TwitchChatters ActiveChatters = new TwitchChatters();

	private string TwitchToken;

	public bool isConnected;

	public override void OnStart()
	{
		ViewerInfo = base.gameObject.AddComponent<TwitchViewerInfo>();
		IRC = base.gameObject.AddComponent<TwitchIRC>();
		if (AutoConnectToDebugChannel)
		{
			ConnectToStream(DebugChannel);
		}
	}

	public void Disconnect()
	{
		IRC.Disconnect();
		isConnected = false;
		ActiveChatters = new TwitchChatters();
		OnMessage.RemoveAllListeners();
		OnSubscribe.RemoveAllListeners();
		OnBitDonation.RemoveAllListeners();
		OnDisconnect.Invoke();
	}

	public void ConnectToStream(string streamName, string authString = "")
	{
		streamName = streamName.Replace("\u200b", "");
		streamName = streamName.ToLower();
		authString = authString.Replace("\u200b", "");
		if (streamName.Contains("www.twitch.tv"))
		{
			streamName.IndexOf("/");
			streamName = streamName.Split(new string[1] { "www.twitch.tv/" }, StringSplitOptions.None)[1];
			if (streamName.Contains("?"))
			{
				streamName = streamName.Split('?')[0];
			}
			else if (streamName.Contains("/"))
			{
				streamName = streamName.Split('/')[0];
			}
		}
		Channel = streamName;
		TwitchToken = authString;
		DoConnect(authString);
	}

	private void DoConnect(string authString = "")
	{
		IRC.channelName = Channel;
		if (!string.IsNullOrEmpty(authString))
		{
			IRC.StartIRC(Channel, TwitchToken);
		}
		else
		{
			IRC.StartIRC();
		}
		if (!isConnected)
		{
			isConnected = true;
			IRC.messageRecievedEvent.AddListener(HandleMessage);
		}
		StartCoroutine(UpdateViewerList());
		OnConnect.Invoke(Channel);
	}

	public void FakeConnect()
	{
		if (!isConnected)
		{
			isConnected = true;
			IRC.messageRecievedEvent.AddListener(HandleMessage);
		}
	}

	private IEnumerator UpdateViewerList()
	{
		ViewerInfo.channelName = Channel;
		while (isConnected)
		{
			if (!ViewerInfo.IsCurrentlyGettingPop)
			{
				ViewerInfo.PopulateViewers();
			}
			yield return new WaitForSeconds(60f);
		}
	}

	public bool IsCurrentlyUpdatingViewerList()
	{
		return ViewerInfo.IsCurrentlyGettingPop;
	}

	private void AddActiveChatter(IRC_MessageData msg)
	{
		Dictionary<string, string> tags = msg.tags;
		if (!tags.ContainsKey("display-name"))
		{
			return;
		}
		string text = tags["display-name"];
		if (ActiveChatters.hashes.Add(text))
		{
			ActiveChatter value = new ActiveChatter
			{
				name = text
			};
			if (tags.ContainsKey("color"))
			{
				ColorUtility.TryParseHtmlString(tags["color"], out value.color);
			}
			else
			{
				value.color = Color.white;
			}
			if (tags.ContainsKey("mod") && tags["mod"] == "1")
			{
				ActiveChatters.moderators.Add(text, value);
			}
			else if (tags.ContainsKey("subscriber") && tags["subscriber"] == "1")
			{
				ActiveChatters.subscribers.Add(text, value);
			}
			else if (tags.ContainsKey("badges") && tags["badges"].Contains("broadcaster"))
			{
				ActiveChatters.broadcaster.Add(text, value);
			}
			else
			{
				ActiveChatters.viewers.Add(text, value);
			}
		}
	}

	private void HandleMessage(IRC_MessageData msg)
	{
		Dictionary<string, string> tags = msg.tags;
		AddActiveChatter(msg);
		if (!tags.ContainsKey("display-name"))
		{
			return;
		}
		if (tags.ContainsKey("bits"))
		{
			OnBitDonation.Invoke(tags["display-name"], int.Parse(tags["bits"]));
		}
		else if (msg.command == "PRIVMSG")
		{
			if (tags.ContainsKey("display-name"))
			{
				OnMessage.Invoke(tags["display-name"], msg.text);
			}
		}
		else if (msg.command == "USERNOTICE" && tags.ContainsKey("msg-id"))
		{
			switch (tags["msg-id"])
			{
			case "sub":
			case "resub":
			case "subgift":
			case "anonsubgift":
				OnSubscribe.Invoke(tags["display-name"]);
				break;
			}
		}
	}
}
