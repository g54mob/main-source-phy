using UnityEngine;

public class TimeTracker : SingleInstance<TimeTracker>
{
	private float previousTime;

	private float currentTime;

	public override string Name
	{
		get
		{
			return "TimeTracker";
		}
	}

	public static float RawDeltaTime
	{
		get
		{
			if (Time.timeScale > 0f)
			{
				return Time.deltaTime / Time.timeScale;
			}
			return Time.realtimeSinceStartup - SingleInstance<TimeTracker>.Instance.previousTime;
		}
	}

	private void Awake()
	{
		previousTime = Time.realtimeSinceStartup;
		currentTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		previousTime = currentTime;
		currentTime = Time.realtimeSinceStartup;
	}
}
