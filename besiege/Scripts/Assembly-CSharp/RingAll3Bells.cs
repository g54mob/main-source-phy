using System;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/RingAll3Bells")]
internal class RingAll3Bells : LevelAchievementTrigger
{
	private const int AwakeningBellsIndex = 35;

	private const float RingThreshold = 0.5f;

	private BellRing[] bellScripts;

	internal override int AchievementId
	{
		get
		{
			return 13;
		}
	}

	protected override int LevelIndex
	{
		get
		{
			return 35;
		}
	}

	public override void OnEnterGlobalSimulation(int levelIndex)
	{
		if (levelIndex != 35)
		{
			return;
		}
		Transform transform = ReferenceMaster.physicsGoalInstance.FindChild("Bells");
		bellScripts = transform.GetComponentsInChildren<BellRing>();
		if (bellScripts.Length != 3)
		{
			Debug.Log("Incorrect number of bells found (" + bellScripts.Length + ", expected 3)!");
			return;
		}
		for (int i = 0; i < bellScripts.Length; i++)
		{
			BellRing obj = bellScripts[i];
			obj.onTrigger = (Action)Delegate.Combine(obj.onTrigger, new Action(OnTrigger));
		}
	}

	public override void OnExitGlobalSimulation(int levelIndex)
	{
		if (levelIndex == 35 && bellScripts.Length == 3)
		{
			for (int i = 0; i < bellScripts.Length; i++)
			{
				BellRing obj = bellScripts[i];
				obj.onTrigger = (Action)Delegate.Remove(obj.onTrigger, new Action(OnTrigger));
			}
		}
	}

	private void OnTrigger()
	{
		for (int i = 0; i < bellScripts.Length; i++)
		{
			if (!bellScripts[i].haveRung || bellScripts[i].timeSinceLast > 0.5f)
			{
				return;
			}
		}
		Trigger();
	}
}
