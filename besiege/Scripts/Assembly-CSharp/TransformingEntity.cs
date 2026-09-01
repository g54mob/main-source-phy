using UnityEngine;

public class TransformingEntity : GenericEntity
{
	public float distance = 20f;

	public float timeDown = 0.5f;

	public float timeUp = 1f;

	public float wait = 1f;

	public override void SetupDefault()
	{
		base.SetupDefault();
		EntityLogic entityLogic = new EntityLogic(TriggerType.LevelStart, this);
		EntityEvent entityEvent;
		if (wait > 0f)
		{
			entityEvent = new EntityEvent(EventContainer.EventType.Wait);
			EventContainer.WaitEvent waitEvent = entityEvent.eventData as EventContainer.WaitEvent;
			if (waitEvent != null)
			{
				waitEvent.waitTime = wait;
			}
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
		}
		entityEvent = new EntityEvent(EventContainer.EventType.Transform);
		EventContainer.TransformEvent transformEvent = entityEvent.eventData as EventContainer.TransformEvent;
		if (transformEvent != null)
		{
			transformEvent.positionType = EventContainer.TransformEvent.TransformPositionType.LocalDirection;
			transformEvent.transformType = EventContainer.TransformEvent.TransformType.Lerp;
			transformEvent.lerpTime = timeDown;
			transformEvent.position = Vector3.down * distance;
		}
		entityLogic.events.Add(entityEvent);
		entityEvent.ApplyValue();
		if (wait > 0f)
		{
			entityEvent = new EntityEvent(EventContainer.EventType.Wait);
			EventContainer.WaitEvent waitEvent = entityEvent.eventData as EventContainer.WaitEvent;
			if (waitEvent != null)
			{
				waitEvent.waitTime = wait;
			}
			entityLogic.events.Add(entityEvent);
			entityEvent.ApplyValue();
		}
		entityEvent = new EntityEvent(EventContainer.EventType.Transform);
		transformEvent = entityEvent.eventData as EventContainer.TransformEvent;
		if (transformEvent != null)
		{
			transformEvent.positionType = EventContainer.TransformEvent.TransformPositionType.LocalDirection;
			transformEvent.transformType = EventContainer.TransformEvent.TransformType.Lerp;
			transformEvent.lerpTime = timeUp;
			transformEvent.position = Vector3.up * distance;
		}
		entityLogic.events.Add(entityEvent);
		entityEvent.ApplyValue();
		entityEvent = new EntityEvent(EventContainer.EventType.Repeat);
		entityLogic.events.Add(entityEvent);
		entityEvent.ApplyValue();
		logicData.Add(entityLogic);
		entityLogic.ApplyValue();
		hasLogic = true;
		OnSaveLogic(new XDataHolder());
	}
}
