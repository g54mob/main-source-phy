using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

public class DarkPHands : MonoBehaviour
{
	public delegate void TargetedEventHandler(Unit primeTarget, Unit target, Vector3 positionOrDirection);

	public GameObject hand;

	private Unit unit;

	private DataHandler data;

	private TeamSystem m_teamSystem;

	private List<Unit> unitBanlist = new List<Unit>();

	public event TargetedEventHandler Targeted;

	private void Start()
	{
		unit = base.transform.root.GetComponent<Unit>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
	}

	public void Go()
	{
		if (!unit.data.targetData)
		{
			return;
		}
		Unit closestOtherUnit = GetClosestOtherUnit(unit);
		Unit closestOtherUnit2 = GetClosestOtherUnit(unit.data.targetData.unit);
		if ((bool)closestOtherUnit)
		{
			if (!closestOtherUnit2)
			{
				Target(closestOtherUnit, null, new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)).normalized);
			}
			else
			{
				Target(closestOtherUnit, closestOtherUnit2, Vector3.zero);
			}
		}
	}

	public void Target(Unit primeTarget, Unit target, Vector3 positionOrDirection)
	{
		if (!(primeTarget == null))
		{
			Vector3 position = base.transform.position;
			Object.Instantiate(hand, position, Quaternion.identity).GetComponent<DarkPHand>().Target(primeTarget.transform.root.gameObject, (target != null) ? target.gameObject : null, positionOrDirection);
			this.Targeted?.Invoke(primeTarget, target, positionOrDirection);
		}
	}

	private Unit GetClosestOtherUnit(Unit currentTarget, bool ignoreList = false)
	{
		List<Unit> teamUnits = m_teamSystem.GetTeamUnits((this.unit.Team == Team.Red) ? Team.Blue : Team.Red);
		float num = float.MaxValue;
		Unit unit = null;
		for (int i = 0; i < teamUnits.Count; i++)
		{
			if (!(teamUnits[i] == null) && !(teamUnits[i] == currentTarget) && (ignoreList || !unitBanlist.Contains(teamUnits[i])))
			{
				float num2 = Vector3.Distance(currentTarget.data.mainRig.position, teamUnits[i].data.mainRig.position);
				if (num2 < num)
				{
					unit = teamUnits[i];
					num = num2;
				}
			}
		}
		if ((bool)unit && !WilhelmPhysicsFunctions.CanSee(this.unit.data.mainRig.position, unit.data.mainRig.position))
		{
			return null;
		}
		StartCoroutine(IBanUnitForAbit(unit));
		return unit;
	}

	private IEnumerator IBanUnitForAbit(Unit unitToBanForABit)
	{
		if ((bool)unitToBanForABit)
		{
			unitBanlist.Add(unitToBanForABit);
		}
		yield return new WaitForSeconds(2f);
		if ((bool)unitToBanForABit)
		{
			unitBanlist.Remove(unitToBanForABit);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if ((bool)attachedRigidbody && (bool)attachedRigidbody.GetComponent<CollisionWeapon>() && (bool)attachedRigidbody.GetComponent<Compensation>() && (bool)attachedRigidbody.GetComponent<RemoveAfterSeconds>())
		{
			TeamHolder componentInChildren = attachedRigidbody.transform.root.GetComponentInChildren<TeamHolder>();
			if (!componentInChildren || componentInChildren.team != data.team)
			{
				Unit closestOtherUnit = GetClosestOtherUnit(unit, ignoreList: true);
				Object.Instantiate(hand, base.transform.position, Quaternion.identity).GetComponent<DarkPHand>().GrabProjectile(attachedRigidbody, closestOtherUnit);
				Object.Instantiate(hand, base.transform.position, Quaternion.identity).GetComponent<DarkPHand>().GrabProjectile(attachedRigidbody, closestOtherUnit);
				Object.Instantiate(hand, base.transform.position, Quaternion.identity).GetComponent<DarkPHand>().GrabProjectile(attachedRigidbody, closestOtherUnit);
			}
		}
	}
}
