using System.Collections;
using Landfall.TABS;
using UnityEngine;

public class ReviveStick : MonoBehaviour
{
	private ProjectileStick stick;

	private UnitSpawner spawner;

	private Unit unit;

	public ParticleSystem part;

	public ParticleSystem part2;

	private TeamHolder teamHolder;

	public Material red;

	public Material blue;

	private void Start()
	{
		teamHolder = GetComponent<TeamHolder>();
		stick = GetComponent<ProjectileStick>();
		spawner = GetComponent<UnitSpawner>();
	}

	private void Update()
	{
		if (!unit)
		{
			if ((bool)stick.target)
			{
				Unit componentInParent = stick.target.GetComponentInParent<Unit>();
				if ((bool)componentInParent)
				{
					unit = componentInParent;
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}
		else if (unit.data.Dead && !unit.data.hasBeenRevived)
		{
			StartCoroutine(ReviveAfterTime());
			unit.data.hasBeenRevived = true;
			unit.data.healthHandler.willBeRewived = true;
		}
	}

	private IEnumerator ReviveAfterTime()
	{
		part.Play();
		yield return new WaitForSeconds(2f);
		unit.GetComponentInChildren<HealthHandler>().StopAllCoroutines();
		float t = 0f;
		SkinnedMeshRenderer mesh = unit.GetComponentInChildren<SkinnedMeshRenderer>();
		Material startMat = mesh.material;
		while (t < 1f)
		{
			t += Time.deltaTime;
			mesh.material.Lerp(startMat, (teamHolder.team == Team.Blue) ? blue : red, t);
			yield return null;
		}
		unit.InitializeUnit(teamHolder.team);
		unit.data.Dead = false;
		unit.data.health = 50f;
		Object.Destroy(this);
		part2.Play();
	}
}
