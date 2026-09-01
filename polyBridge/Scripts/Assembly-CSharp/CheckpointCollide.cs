using UnityEngine;

public class CheckpointCollide : MonoBehaviour
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
		Checkpoint componentInParent = GetComponentInParent<Checkpoint>();
		if ((bool)componentInParent)
		{
			if (TriggerCallbackManager.doDelayAndSortTriggerEvents)
			{
				TriggerEventInfo item = new TriggerEventInfo(TriggerType.Checkpoint, isOnEnter, componentInParent, other);
				TriggerCallbackManager.eventsFromLastFrame.Add(item);
			}
			else
			{
				componentInParent.DoOnTriggerStay(other, isOnEnter);
			}
		}
	}
}
