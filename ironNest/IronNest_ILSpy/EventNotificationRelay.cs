using UnityEngine;

public class EventNotificationRelay : MonoBehaviour
{
	public string MessageID;

	public void Trigger()
	{
		if (FireMission._003CInstance_003Ek__BackingField != null)
		{
			FireMission._003CInstance_003Ek__BackingField.ProcessNotification(MessageID);
		}
	}
}
