using System;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
	[Header("General")]
	public GameObject aiToSpawn;

	public BreakBase breakScript;

	public FireController fireController;

	public float interval = 2f;

	public float fireMult = 3f;

	public int limit = 10;

	public bool addVictoryValueOnBreak = true;

	public float distanceToMachineLimitSqr = 50f;

	public Vector3 spawnLocalPosition;

	public Vector3 eulerRotation;

	[Header("OnBreakEffect")]
	public GameObject[] bodies;

	private float timer;

	private bool broken;

	private int aiVictoryValue = 1;

	private void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			if (addVictoryValueOnBreak)
			{
				aiVictoryValue = aiToSpawn.GetComponent<EntityAI>().victoryValue;
				WinCondition.Instance.objectiveObjectCount += limit * aiVictoryValue;
			}
		}
		else
		{
			BreakBase breakBase = breakScript;
			breakBase.OnBreakTrigger = (Action<BreakBase>)Delegate.Combine(breakBase.OnBreakTrigger, new Action<BreakBase>(Break));
			timer = interval - 1f;
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		if (StatMaster.isMP)
		{
			int closestMachine = FactionsController.GetClosestMachine(base.transform.position);
			if (closestMachine == -1)
			{
				return;
			}
			ServerMachine machine;
			if (NetworkAddPiece.Instance.GetActiveMachine((uint)closestMachine, out machine))
			{
				vector = machine.MachineCenterPos;
			}
		}
		else
		{
			vector = Machine.Active().MachineCenterPos;
		}
		Vector3 vector2 = vector - base.transform.position;
		float num = vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z;
		if (!(num > distanceToMachineLimitSqr))
		{
			if (fireController != null && fireController.onFire)
			{
				timer += Time.deltaTime * fireMult;
			}
			else
			{
				timer += Time.deltaTime;
			}
			if (timer > interval)
			{
				SpawnAI();
				timer = 0f;
				limit--;
			}
			if (limit == 0 || broken)
			{
				base.enabled = false;
			}
		}
	}

	private void SpawnAI()
	{
		UnityEngine.Object.Instantiate(aiToSpawn, base.transform.TransformPoint(spawnLocalPosition), base.transform.rotation * Quaternion.Euler(eulerRotation), ReferenceMaster.physicsGoalInstance);
	}

	public void Break(BreakBase bb)
	{
		broken = true;
		if (addVictoryValueOnBreak)
		{
			WinCondition.currentObjsCompleted += limit * aiVictoryValue;
		}
		for (int i = 0; i < limit; i++)
		{
			Vector3 vector = UnityEngine.Random.insideUnitSphere * 2f;
			vector.y = ((!(vector.y < 0f)) ? vector.y : (0f - vector.y));
			UnityEngine.Object.Instantiate(bodies[UnityEngine.Random.Range(0, 2)], vector + base.transform.position, Quaternion.Euler(UnityEngine.Random.onUnitSphere * 180f), ReferenceMaster.physicsGoalInstance);
		}
	}
}
