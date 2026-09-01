using UnityEngine;

public class MoveToComplete : MonoBehaviour
{
	public int distanceToWin = 20;

	public float currentDistance;

	public string levelName;

	public Transform sourceCube;

	private float prevFurthestDistance;

	private void Start()
	{
		WinCondition.Instance.objectiveObjectCount = distanceToWin;
		if (StatMaster.levelSimulating)
		{
			Machine machine = Machine.Active();
			sourceCube = machine.GetBlocks(BlockType.StartingBlock)[0].transform;
		}
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			CheckDistance();
		}
	}

	private void CheckDistance()
	{
		currentDistance = sourceCube.position.z;
		if (currentDistance > prevFurthestDistance + 2f)
		{
			WinCondition.currentObjsCompleted++;
			prevFurthestDistance = currentDistance;
		}
	}
}
