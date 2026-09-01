using UnityEngine;
using UnityEngine.Events;

public class CountEvent : MonoBehaviour
{
	public bool startwaiting = true;

	public bool auto = true;

	public bool startOnCooldown = true;

	public bool turnOffIfUnitDead;

	private bool waiting = true;

	public float seconds = 1f;

	public float failChance;

	public bool useRandom;

	public float randomMin = 1f;

	public float randomMax = 1f;

	public bool repeat;

	public UnityEvent countEvent;

	public UnityEvent failEvent;

	private float counter;

	private DataHandler unitdata;

	private void Start()
	{
		unitdata = base.transform.root.GetComponentInChildren<DataHandler>();
		if (!startOnCooldown)
		{
			counter = seconds;
		}
		waiting = startwaiting;
		if (useRandom)
		{
			SetRandom();
		}
	}

	private void Update()
	{
		if ((bool)unitdata && unitdata.Dead && turnOffIfUnitDead)
		{
			StopCounting();
		}
		if (!auto && waiting)
		{
			return;
		}
		counter += Time.deltaTime;
		if (counter > seconds)
		{
			if (failChance != 0f && failChance >= Random.Range(0f, 1f))
			{
				failEvent.Invoke();
			}
			else
			{
				countEvent.Invoke();
			}
			counter = 0f;
			if (useRandom)
			{
				SetRandom();
			}
			if (!repeat)
			{
				base.enabled = false;
			}
		}
	}

	public void StartCounting()
	{
		waiting = false;
	}

	public void StopCounting()
	{
		waiting = true;
	}

	public void SetRandom()
	{
		seconds = Random.Range(randomMin, randomMax);
	}
}
