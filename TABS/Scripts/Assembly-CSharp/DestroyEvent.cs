using System;
using UnityEngine;
using UnityEngine.Events;

public class DestroyEvent : MonoBehaviour
{
	public UnityEvent destroyEvent;

	private Action destryAction;

	private void OnDestroy()
	{
		if (destroyEvent != null)
		{
			destroyEvent.Invoke();
		}
		if (destryAction != null)
		{
			destryAction();
		}
	}

	public void AddDestroyAction(Action action)
	{
		destryAction = (Action)Delegate.Combine(destryAction, action);
	}

	public void RemoveDestroyAction(Action action)
	{
		destryAction = (Action)Delegate.Remove(destryAction, action);
	}
}
