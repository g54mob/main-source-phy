using UnityEngine;

public class VehicleStopTriggerCollide : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		HandleTriggerEvent(other, isOnEnter: true);
	}

	private void OnTriggerStay(Collider other)
	{
		HandleTriggerEvent(other, isOnEnter: false);
	}

	private void HandleTriggerEvent(Collider other, bool isOnEnter)
	{
		VehicleStopTrigger componentInParent = GetComponentInParent<VehicleStopTrigger>();
		if ((bool)componentInParent)
		{
			if (TriggerCallbackManager.doDelayAndSortTriggerEvents)
			{
				TriggerEventInfo item = new TriggerEventInfo(TriggerType.VehicleStop, isOnEnter, componentInParent, other);
				TriggerCallbackManager.eventsFromLastFrame.Add(item);
			}
			else
			{
				componentInParent.DoOnTriggerStay(other, isOnEnter);
			}
		}
	}
}
