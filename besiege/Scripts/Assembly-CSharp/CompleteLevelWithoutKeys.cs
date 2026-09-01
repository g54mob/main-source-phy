using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Achievements/Trigger/CompleteLevelWithoutKeys")]
internal abstract class CompleteLevelWithoutKeys : AchievementTrigger
{
	private List<KeyCode> machineKeys = new List<KeyCode>();

	private bool pressedKey;

	public override void OnUpdate(int levelIndex)
	{
		if (pressedKey)
		{
			return;
		}
		for (int i = 0; i < machineKeys.Count; i++)
		{
			if (Input.GetKeyDown(machineKeys[i]))
			{
				pressedKey = true;
				break;
			}
		}
	}

	public override void OnEnterGlobalSimulation(int levelIndex)
	{
		if (pressedKey)
		{
			return;
		}
		machineKeys.Clear();
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return;
		}
		pressedKey = false;
		List<BlockBehaviour> buildingBlocks = machine.BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			for (int j = 0; j < blockBehaviour.KeyList.Count; j++)
			{
				MKey mKey = blockBehaviour.KeyList[j];
				for (int k = 0; k < mKey.KeysCount; k++)
				{
					KeyCode key = mKey.GetKey(k);
					if (!machineKeys.Contains(key))
					{
						machineKeys.Add(key);
					}
				}
			}
		}
	}

	public override void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine)
	{
		if (!pressedKey)
		{
			Trigger();
		}
	}
}
