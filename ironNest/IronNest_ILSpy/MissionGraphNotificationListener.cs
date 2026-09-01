using System;
using UnityEngine;
using UnityEngine.Events;

public class MissionGraphNotificationListener : MonoBehaviour
{
	public string MessageID;

	public UnityEvent OnMessage;

	public void Trigger(string messageID)
	{
		if (string.Equals(MessageID, messageID, StringComparison.OrdinalIgnoreCase) && OnMessage != null)
		{
			OnMessage.Invoke();
		}
	}
}
