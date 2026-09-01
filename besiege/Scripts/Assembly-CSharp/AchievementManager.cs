using System;
using System.Collections.Generic;
using GameGrind;
using UnityEngine;

[AddComponentMenu("Achievements/AchievementManager")]
internal class AchievementManager : SingleInstance<AchievementManager>
{
	private List<IAchievementTrigger> triggers = new List<IAchievementTrigger>();

	private bool wasInGlobalSim;

	private int levelIndex = -1;

	public override string Name
	{
		get
		{
			return "AchievementMananger";
		}
	}

	internal void Register(IAchievementTrigger trigger)
	{
		triggers.Add(trigger);
	}

	internal void Unregister(IAchievementTrigger trigger)
	{
		triggers.Remove(trigger);
	}

	private void Awake()
	{
		ReferenceMaster.onLevelWon = (Action)Delegate.Combine(ReferenceMaster.onLevelWon, new Action(OnLevelWon));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		SingleInstance<AchievementManager>.Initialize(this);
	}

	private void OnSimulationToggle(bool enteringSimulation)
	{
		if (StatMaster.isMP && ((enteringSimulation && !StatMaster.InGlobalPlayMode) || (!enteringSimulation && !wasInGlobalSim)))
		{
			return;
		}
		wasInGlobalSim = enteringSimulation;
		if (StatMaster.isMP)
		{
			levelIndex = -1;
		}
		else
		{
			levelIndex = WinCondition.Instance.myLevelIndex;
		}
		for (int i = 0; i < triggers.Count; i++)
		{
			if (enteringSimulation)
			{
				triggers[i].OnEnterGlobalSimulation(levelIndex);
			}
			else
			{
				triggers[i].OnExitGlobalSimulation(levelIndex);
			}
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelWon = (Action)Delegate.Remove(ReferenceMaster.onLevelWon, new Action(OnLevelWon));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		Journal.Save();
	}

	private void OnLevelWon()
	{
		if (!StatMaster.isMP)
		{
			float timeTaken = WinScreen.GetTimeTaken();
			Machine machine = Machine.Active();
			for (int i = 0; i < triggers.Count; i++)
			{
				triggers[i].OnSinglePlayerLevelComplete(levelIndex, timeTaken, machine);
			}
		}
	}

	private void Update()
	{
		for (int i = 0; i < triggers.Count; i++)
		{
			triggers[i].OnUpdate(levelIndex);
		}
	}
}
