using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GenericTimerSceneSync : MonoBehaviour
{
	public string TimerID;

	public float CurrentTime;

	public UnityEvent OnTimeUpdate;

	public float CurrentHours
	{
		get
		{
			//IL_002b: Expected F4, but got I4
			TimeSpan timeSpan = TimeSpan.FromSeconds(CurrentTime);
			TimeSpan timeSpan2 = default(TimeSpan);
			int hours = timeSpan2.Hours;
			return hours;
		}
	}

	public float CurrentMins
	{
		get
		{
			//IL_002b: Expected F4, but got I4
			TimeSpan timeSpan = TimeSpan.FromSeconds(CurrentTime);
			TimeSpan timeSpan2 = default(TimeSpan);
			int minutes = timeSpan2.Minutes;
			return minutes;
		}
	}

	public float CurrentSeconds
	{
		get
		{
			//IL_002b: Expected F4, but got I4
			TimeSpan timeSpan = TimeSpan.FromSeconds(CurrentTime);
			TimeSpan timeSpan2 = default(TimeSpan);
			int seconds = timeSpan2.Seconds;
			return seconds;
		}
	}

	public void LateUpdate()
	{
		//IL_0109: Expected F4, but got I4
		//IL_0022: Expected F4, but got I4
		//IL_0060: Expected F4, but got I4
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		bool flag = (object)FireMission._003CInstance_003Ek__BackingField == null;
		float num = 0f;
		if (!flag)
		{
			bool flag2 = fireMission.RunningTimers == null;
			num = 0f;
			if (!flag2)
			{
				bool flag3 = fireMission.RunningTimers.TryGetValue(TimerID, out var value);
				bool flag4 = !flag3;
				num = 0f;
				if (!flag4)
				{
					num = value.CurrentSeconds;
				}
			}
		}
		bool flag5 = CurrentTime == num;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018054AFC8h\"");
		if (!flag5)
		{
			CurrentTime = num;
			if (OnTimeUpdate != null)
			{
				OnTimeUpdate.Invoke();
			}
		}
	}
}
