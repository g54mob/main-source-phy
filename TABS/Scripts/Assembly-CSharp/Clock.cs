using System;
using Landfall.TABS;
using UnityEngine;

public class Clock : MonoBehaviour
{
	public static Clock instance;

	public float secondsPerTick = 15f;

	public float counter;

	private Action tickAction;

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void Start()
	{
		counter = 0f;
	}

	private void Update()
	{
		CastleFightClock.SetFillAmount(counter / secondsPerTick);
		counter += Time.deltaTime;
		if (counter > secondsPerTick)
		{
			counter = 0f;
			if (tickAction != null)
			{
				tickAction();
			}
		}
	}

	public void AssignTickAction(Action action)
	{
		tickAction = (Action)Delegate.Combine(tickAction, action);
	}
}
