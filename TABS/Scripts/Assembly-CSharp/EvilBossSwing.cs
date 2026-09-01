using Landfall.TABS;
using UnityEngine;

public class EvilBossSwing : MonoBehaviour
{
	public LayerMask mask;

	public GameObject spawnedObj;

	public float cd = 0.1f;

	public float startAt = 0.2f;

	private float counter;

	private MeleeWeapon meleeWeapon;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref unit, ref rootTeamHolder);
		meleeWeapon = GetComponentInParent<MeleeWeapon>();
	}

	private void Update()
	{
		if (!meleeWeapon.isSwinging || meleeWeapon.sinceSwing < startAt)
		{
			return;
		}
		counter += Time.deltaTime;
		if (!(counter < cd))
		{
			counter -= cd;
			Vector3 position = base.transform.GetChild(Random.Range(0, base.transform.childCount)).position;
			Physics.Raycast(new Ray(position + Vector3.up, Vector3.down), out var hitInfo, 10f, mask);
			if ((bool)hitInfo.transform)
			{
				TeamHolder.AddTeamHolder(Object.Instantiate(spawnedObj, hitInfo.point, Quaternion.identity), unit, rootTeamHolder);
			}
		}
	}
}
