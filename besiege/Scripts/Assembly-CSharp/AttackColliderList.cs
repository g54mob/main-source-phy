using System.Collections.Generic;
using UnityEngine;

public class AttackColliderList : MonoBehaviour
{
	[HideInInspector]
	public SortedList<float, EntityAI.Targeting> attackColliderTargets = new SortedList<float, EntityAI.Targeting>();

	public EntityAI entityAI;

	public Collider attackCollider;

	private EntityAI.Targeting tempTarget;

	public void Awake()
	{
		if (StatMaster.levelSimulating)
		{
			attackCollider.enabled = true;
		}
	}

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			if (object.ReferenceEquals(entityAI, null))
			{
				entityAI = base.transform.parent.transform.parent.GetComponentInParent<EntityAI>();
			}
			if (object.ReferenceEquals(entityAI, null))
			{
				base.enabled = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!StatMaster.levelSimulating || !base.enabled || (StatMaster.isClient && !StatMaster.isLocalSim))
		{
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (!(attachedRigidbody != null) || attachedRigidbody.isKinematic || checkInList(attachedRigidbody.transform))
		{
			return;
		}
		tempTarget = new EntityAI.Targeting(entityAI);
		tempTarget.NewTargetBlock(attachedRigidbody.transform, attachedRigidbody);
		if (tempTarget.isAI)
		{
			if (tempTarget.AI.faction.Name != entityAI.faction.Name && !tempTarget.AI.isDead)
			{
				Vector3 vector = other.transform.position - entityAI.transform.position;
				attackColliderTargets.Add(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z, tempTarget);
			}
		}
		else
		{
			if (!tempTarget.isBlock)
			{
				return;
			}
			if (!object.ReferenceEquals(tempTarget.BlockHealth, null))
			{
				if (tempTarget.Block.Prefab.isArmor || other.CompareTag("ArmourTag"))
				{
					tempTarget.isArmored = true;
				}
				Vector3 vector2 = other.transform.position - entityAI.transform.position;
				attackColliderTargets.Add(vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z, tempTarget);
			}
			else if (!object.ReferenceEquals(tempTarget.Block, null) && (tempTarget.Block.Prefab.isArmor || other.CompareTag("ArmourTag")))
			{
				tempTarget.isArmored = true;
				Vector3 vector3 = other.transform.position - entityAI.transform.position;
				attackColliderTargets.Add(vector3.x * vector3.x + vector3.y * vector3.y + vector3.z * vector3.z, tempTarget);
			}
		}
	}

	private bool checkInList(Transform trans)
	{
		for (int i = 0; i < attackColliderTargets.Count; i++)
		{
			if (attackColliderTargets.Values[i].trans == trans)
			{
				return true;
			}
		}
		return false;
	}

	private void OnTriggerExit(Collider other)
	{
		if (StatMaster.levelSimulating && base.enabled)
		{
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (!object.ReferenceEquals(attachedRigidbody, null))
			{
				CleanList(attachedRigidbody.transform);
			}
		}
	}

	private void CleanList(Transform trans)
	{
		for (int num = attackColliderTargets.Count - 1; num >= 0; num--)
		{
			if (attackColliderTargets.Values[num].trans == trans)
			{
				attackColliderTargets.RemoveAt(num);
			}
			else if (attackColliderTargets.Values[num].isAI && attackColliderTargets.Values[num].AI.isDead)
			{
				attackColliderTargets.RemoveAt(num);
			}
		}
	}
}
