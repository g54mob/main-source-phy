using UnityEngine;

[AddComponentMenu("UI/Tutorial/Tutorial Step (Prerequisite)")]
public class TutorialStepPrerequisite : TutorialStep
{
	public enum PreType
	{
		HasBlock = 0
	}

	public PreType prerequisite;

	[SerializeField]
	protected BlockType preblock;

	private bool PrerequisiteMet(int maxIterations)
	{
		Machine machine = Machine.Active();
		int blockCount = machine.BlockCount;
		blockCount = ((blockCount <= maxIterations) ? blockCount : maxIterations);
		for (int i = 0; i < blockCount; i++)
		{
			if (machine.BuildingBlocks[i].BlockID == (int)preblock)
			{
				return true;
			}
		}
		return false;
	}

	public override void Complete()
	{
		if (PrerequisiteMet(50))
		{
			base.Complete();
		}
	}
}
