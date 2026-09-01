using System;
using UnityEngine;
using UnityEngine.Events;

public class ConditionNumber : MonoBehaviour, GameObjectPooling.IPoolable
{
	[Tooltip("The time between eventToCall being triggered and releasing the object back into the pool.")]
	[SerializeField]
	private float timeToRelease;

	[Tooltip("Event called when the object is released back into the pool.Use this to \"clean up events\" triggered in eventToCall.")]
	[SerializeField]
	private UnityEvent releaseEvent;

	public UnityEvent eventToCall;

	public int numberNeeded = 5;

	private int currentNumber;

	private bool hasReturnedToPool;

	private bool shouldCountDown;

	private float currentTime;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public void Go()
	{
		currentNumber++;
		if (currentNumber >= numberNeeded)
		{
			eventToCall.Invoke();
			if (IsManagedByPool && !hasReturnedToPool)
			{
				shouldCountDown = true;
				currentTime = timeToRelease;
			}
		}
	}

	private void Update()
	{
		if (shouldCountDown && !hasReturnedToPool)
		{
			currentTime -= Time.deltaTime;
			if (currentTime <= 0f)
			{
				shouldCountDown = false;
				ReleaseSelf?.Invoke();
			}
		}
	}

	public void Initialize()
	{
		hasReturnedToPool = false;
	}

	public void Reset()
	{
	}

	public void Release()
	{
		hasReturnedToPool = true;
		shouldCountDown = false;
		currentTime = timeToRelease;
		currentNumber = 0;
		releaseEvent?.Invoke();
	}
}
