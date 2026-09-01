using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeaponSpawn : CollisionWeaponEffect
{
	public enum Rot
	{
		TowardsHit = 0,
		Normal = 1,
		InverseNormal = 2
	}

	public enum Pos
	{
		TransformPos = 0,
		ContactPoint = 1
	}

	public Rot rot;

	public Pos pos;

	public GameObject objectToSpawn;

	public float cd = 0.1f;

	public UnityEvent SpawnEvent;

	private float counter;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform, ref unit, ref rootTeamHolder);
	}

	private void Update()
	{
		counter += Time.deltaTime;
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		if (!(counter < cd))
		{
			counter = 0f;
			Quaternion rotation = Quaternion.identity;
			if (rot == Rot.Normal)
			{
				rotation = Quaternion.LookRotation(collision.GetContact(0).normal);
			}
			else if (rot == Rot.InverseNormal)
			{
				rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
			}
			else if (rot == Rot.TowardsHit)
			{
				rotation = Quaternion.LookRotation(hitTransform.position - base.transform.position);
			}
			Vector3 position = Vector3.zero;
			if (pos == Pos.TransformPos)
			{
				position = base.transform.position;
			}
			else if (pos == Pos.ContactPoint)
			{
				position = collision.GetContact(0).point;
			}
			TeamHolder.AddTeamHolder(Object.Instantiate(objectToSpawn, position, rotation), unit, rootTeamHolder);
			SpawnEvent.Invoke();
		}
	}
}
