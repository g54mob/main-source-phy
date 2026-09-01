using BitCode.Extensions;
using UnityEngine;

public class TeslaCannon : MonoBehaviour
{
	[SerializeField]
	private bool spawnedObjectsInheritLevelScriptValues;

	public bool playOnStart;

	private DataHandler spawnerData;

	private TeamHolder teamHolder;

	private LineEffects lineEffect;

	private Transform shootPos;

	private Transform targetPos;

	private Vector3 hitRot;

	public GameObject hitEffect;

	[HideInInspector]
	public Tesla_MaxTargets maxTargetChecker;

	private void Start()
	{
		if (maxTargetChecker == null)
		{
			maxTargetChecker = base.transform.GetComponent<Tesla_MaxTargets>();
		}
		if (playOnStart)
		{
			Zap();
		}
	}

	public void PlayEffect(Transform TargetPos, Transform shootPos, GameObject spawner)
	{
		lineEffect = GetComponentInChildren<LineEffects>(includeInactive: true);
		if (lineEffect != null)
		{
			lineEffect.gameObject.SetActive(value: true);
		}
		hitRot = TargetPos.position - shootPos.position;
		lineEffect.Play(shootPos, TargetPos);
		if ((bool)hitEffect)
		{
			GameObject gameObject = Object.Instantiate(hitEffect, TargetPos.position, Quaternion.LookRotation(hitRot), TargetPos);
			Level component = GetComponent<Level>();
			if (spawnedObjectsInheritLevelScriptValues && (bool)component)
			{
				Level orAddComponent = gameObject.GetOrAddComponent<Level>();
				orAddComponent.levelMultiplier = component.levelMultiplier;
				orAddComponent.level = component.level;
				orAddComponent.ignoreTeam = component.ignoreTeam;
			}
			TeamHolder.AddTeamHolder(gameObject, spawner);
			SpawnObject component2 = gameObject.transform.GetComponent<SpawnObject>();
			if ((bool)component2)
			{
				component2.spawnerProjecile = base.gameObject;
			}
		}
	}

	public void Zap()
	{
		teamHolder = GetComponent<TeamHolder>();
		spawnerData = teamHolder.spawner.GetComponentInChildren<DataHandler>();
		if ((bool)teamHolder.spawnerWeapon)
		{
			shootPos = teamHolder.spawnerWeapon.GetComponentInChildren<ShootPosition>().transform;
		}
		if (!shootPos)
		{
			shootPos = teamHolder.transform;
		}
		if ((bool)spawnerData)
		{
			targetPos = spawnerData.targetMainRig.transform;
		}
		PlayEffect(targetPos, shootPos, teamHolder.spawner);
	}
}
