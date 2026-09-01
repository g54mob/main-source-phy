using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour, GameObjectPooling.IPoolable
{
	[Tooltip("This event is fired when the object is released back into the pool.Use this to clean up events triggered in delayedEvent.")]
	[SerializeField]
	private UnityEvent releaseEvent;

	public bool playOnAwake = true;

	public UnityEvent delayedEvent;

	public float delay;

	public float randomDelay;

	private Holdable holdable;

	private DataHandler unitdata;

	public bool getUnitData;

	private float originalDelay;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		originalDelay = delay;
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public void Go()
	{
		holdable = GetComponent<Holdable>();
		if ((bool)holdable)
		{
			unitdata = holdable.holderData;
		}
		if (!unitdata && getUnitData)
		{
			unitdata = base.transform.GetComponentInParent<DataHandler>();
		}
		if (randomDelay > 0f)
		{
			delay = originalDelay + UnityEngine.Random.Range(0f, randomDelay);
		}
		StartCoroutine(DelayTheEvent());
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		releaseEvent?.Invoke();
	}

	private void InitializeOnSpawn()
	{
		if (playOnAwake)
		{
			Go();
		}
	}

	private IEnumerator DelayTheEvent()
	{
		float t = 0f;
		while (t < delay)
		{
			if ((bool)unitdata && unitdata.Dead)
			{
				StopAllCoroutines();
			}
			t += Time.deltaTime;
			yield return null;
		}
		for (int i = 0; i < delayedEvent.GetPersistentEventCount(); i++)
		{
			if (delayedEvent.GetPersistentTarget(i) == null)
			{
				delayedEvent.SetPersistentListenerState(i, UnityEventCallState.Off);
			}
		}
		delayedEvent.Invoke();
	}
}
