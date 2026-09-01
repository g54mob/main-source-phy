using UnityEngine;

public class EditLogicHandler : MonoBehaviour
{
	public static EditLogicHandler Instance;

	public virtual void OnCloseMapper()
	{
	}

	public virtual void OnAddLogic()
	{
	}

	public virtual void OnEditLogic(EntityLogic logic)
	{
	}

	public virtual void OnRemoveLogic(EntityLogic logic)
	{
	}

	public virtual void OnAddTarget(EntityLogic logic)
	{
	}

	public virtual void OnEditTarget(EntityLogic logic, TriggerTarget trigger)
	{
	}

	public virtual void OnRemoveTarget(EntityLogic logic, TriggerTarget trigger)
	{
	}

	public virtual void OnAddEvent(EntityLogic logic)
	{
	}

	public virtual void OnEditEvent(EntityLogic logic, EntityEvent evt)
	{
	}

	public virtual void OnRemoveEvent(EntityLogic logic, EntityEvent evt)
	{
	}

	public virtual void OnMoveEvent(EntityLogic logic, EntityEvent evt, bool isDown)
	{
	}

	public virtual void OnSortBehaviour(EntityLogic logic, EntityEvent evt)
	{
	}
}
