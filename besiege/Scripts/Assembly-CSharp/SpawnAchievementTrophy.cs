using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Achievements/Other/SpawnAchievementTrophy")]
internal class SpawnAchievementTrophy : MonoBehaviour
{
	private enum PositionType
	{
		LocalOffset = 0,
		GlobalOffset = 1,
		GlobalPosition = 2,
		PrefabOffset = 3,
		LastBreakOffset = 4
	}

	[SerializeField]
	[Header("General Settings")]
	internal GameObject achievementTrophyPrefab;

	[SerializeField]
	private float timeDelay;

	[SerializeField]
	internal BreakBase[] breakScriptDependency = new BreakBase[0];

	[SerializeField]
	internal KillingHandler[] deathDependency = new KillingHandler[0];

	[SerializeField]
	internal MonoBehaviour[] incrementCallbacks = new MonoBehaviour[0];

	[SerializeField]
	internal int requiredBreaks;

	[SerializeField]
	private PositionType positionType;

	public Vector3 spawnVector = Vector3.zero;

	private AchievementTrophyPickup pickupScript;

	[Header("TrophySettings")]
	public float mass = 1f;

	public float drag = 0.5f;

	public float angularDrag;

	internal int progress;

	private void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			WaterStaticBob component = achievementTrophyPrefab.GetComponent<WaterStaticBob>();
			if ((bool)component)
			{
				component.SetDefaults();
				component.SetPivot();
			}
			return;
		}
		bool flag = requiredBreaks > 0;
		if (breakScriptDependency != null)
		{
			for (int i = 0; i < breakScriptDependency.Length; i++)
			{
				BreakBase obj = breakScriptDependency[i];
				obj.OnBreakTrigger = (Action<BreakBase>)Delegate.Combine(obj.OnBreakTrigger, new Action<BreakBase>(Progress));
				if (!flag)
				{
					requiredBreaks++;
				}
			}
		}
		if (deathDependency != null)
		{
			for (int j = 0; j < deathDependency.Length; j++)
			{
				KillingHandler obj2 = deathDependency[j];
				obj2.OnDeath = (Action<MonoBehaviour>)Delegate.Combine(obj2.OnDeath, new Action<MonoBehaviour>(Progress));
				if (!flag)
				{
					requiredBreaks++;
				}
			}
		}
		for (int k = 0; k < incrementCallbacks.Length; k++)
		{
			if (incrementCallbacks[k] is TrophyIncrement)
			{
				TrophyIncrement trophyIncrement = incrementCallbacks[k] as TrophyIncrement;
				trophyIncrement.trophyIncrease = (Action<MonoBehaviour>)Delegate.Combine(trophyIncrement.trophyIncrease, new Action<MonoBehaviour>(Progress));
				if (!flag)
				{
					requiredBreaks++;
				}
			}
		}
	}

	internal virtual void Progress(MonoBehaviour b)
	{
		progress++;
		if (progress == requiredBreaks)
		{
			SpawnTrophy(b.transform.position);
		}
	}

	internal void SpawnTrophy(Vector3 position)
	{
		if (timeDelay != 0f)
		{
			Spawn(position);
		}
		else
		{
			StartCoroutine(DelayedStart(position));
		}
	}

	private IEnumerator DelayedStart(Vector3 position)
	{
		yield return new WaitForSeconds(timeDelay);
		Spawn(position);
	}

	private void Spawn(Vector3 position)
	{
		Quaternion rotation = Quaternion.identity;
		switch (positionType)
		{
		case PositionType.LocalOffset:
			position = base.transform.position + base.transform.InverseTransformVector(spawnVector);
			break;
		case PositionType.GlobalOffset:
			position = base.transform.position + spawnVector;
			break;
		case PositionType.GlobalPosition:
			position = spawnVector;
			break;
		case PositionType.PrefabOffset:
			position = achievementTrophyPrefab.transform.position + spawnVector;
			rotation = achievementTrophyPrefab.transform.rotation;
			break;
		case PositionType.LastBreakOffset:
			position += spawnVector;
			break;
		}
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(achievementTrophyPrefab, position, rotation, ReferenceMaster.physicsGoalInstance.transform);
		pickupScript = gameObject.GetComponent<AchievementTrophyPickup>();
		pickupScript.level = WinCondition.Instance.myLevelIndex;
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		component.mass = mass;
		component.drag = drag;
		component.angularDrag = angularDrag;
	}

	private void OnDisable()
	{
		for (int i = 0; i < breakScriptDependency.Length; i++)
		{
			if (breakScriptDependency[i] != null)
			{
				BreakBase obj = breakScriptDependency[i];
				obj.OnBreakTrigger = (Action<BreakBase>)Delegate.Remove(obj.OnBreakTrigger, new Action<BreakBase>(Progress));
			}
		}
	}
}
