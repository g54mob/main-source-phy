using System;
using System.Collections.Generic;
using Landfall.MonoBatch;
using Landfall.TABS;
using Landfall.TABS.AI;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHandler : BatchedMonobehaviour
{
	public delegate void AttackStartedDelegate(Unit unit, Vector3 position, Rigidbody targetRigidbody, Vector3 forceDirection, ForceWeapon forceWeapon);

	public enum ForceWeapon
	{
		None = 0,
		Left = 1,
		Right = 2
	}

	public Weapon rightWeapon;

	public Weapon leftWeapon;

	public GameObject fistRefernce;

	public UnityEvent deathEvent;

	public bool forceUnitAttackDistance;

	private Weapon activeWeapon;

	private Weapon otherWeapon;

	private DataHandler data;

	private Unit unit;

	private UnitAPI m_unitAPI;

	private List<Weapon> allWeapons = new List<Weapon>();

	[HideInInspector]
	public float attackSpeedMultiplier = 1f;

	public event AttackStartedDelegate AttackStarted;

	public event Action<Unit> ChangeWeapons;

	private void Awake()
	{
		data = GetComponent<DataHandler>();
		unit = GetComponentInParent<Unit>();
		m_unitAPI = unit.GetComponent<UnitAPI>();
	}

	protected override void Start()
	{
		base.Start();
		if ((bool)rightWeapon)
		{
			Unit componentInParent = GetComponentInParent<Unit>();
			if (forceUnitAttackDistance)
			{
				componentInParent.m_AttackDistance = rightWeapon.maxRange;
				componentInParent.m_PreferedDistance = rightWeapon.maxRange - 0.3f;
			}
			componentInParent.targetYourFriends = rightWeapon.m_weaponTargetType == Weapon.WeaponTargetType.Fridemies;
			if ((bool)leftWeapon && !componentInParent.targetYourFriends)
			{
				componentInParent.targetYourFriends = leftWeapon.m_weaponTargetType == Weapon.WeaponTargetType.Fridemies;
			}
		}
		if (!rightWeapon && !leftWeapon)
		{
			UseHands();
		}
		allWeapons.Clear();
		AddWeapon(rightWeapon);
		AddWeapon(leftWeapon);
	}

	internal void SetAttackCounters(float internalCounter)
	{
		if ((bool)rightWeapon)
		{
			rightWeapon.internalCounter = internalCounter;
		}
		if ((bool)leftWeapon)
		{
			leftWeapon.internalCounter = internalCounter;
		}
		unit.attackCounter = internalCounter;
	}

	public Weapon GetWeapon(int index)
	{
		if (allWeapons == null || index < 0 || index >= allWeapons.Count)
		{
			return null;
		}
		return allWeapons[index];
	}

	public void UseHands()
	{
		if ((bool)fistRefernce && !data.Dead)
		{
			MeleeWeapon component = fistRefernce.GetComponent<MeleeWeapon>();
			CollisionWeapon component2 = fistRefernce.GetComponent<CollisionWeapon>();
			CollisionSound component3 = fistRefernce.GetComponent<CollisionSound>();
			MeleeWeapon meleeWeapon = GetComponentInChildren<HandRight>().gameObject.AddComponent<MeleeWeapon>();
			CollisionWeapon collisionWeapon = GetComponentInChildren<HandRight>().gameObject.AddComponent<CollisionWeapon>();
			if ((bool)component3)
			{
				meleeWeapon.gameObject.AddComponent<CollisionSound>().SoundEffectRef = component3.SoundEffectRef;
			}
			meleeWeapon.forceCurve = component.forceCurve;
			meleeWeapon.curveForce = component.curveForce;
			collisionWeapon.impactMultiplier = component2.impactMultiplier;
			collisionWeapon.damage = component2.damage;
			collisionWeapon.onImpactForce = component2.onImpactForce;
			meleeWeapon.internalCooldown = component.internalCooldown;
			meleeWeapon.maxRange = component.maxRange;
			meleeWeapon.setDirectionContinious = component.setDirectionContinious;
			meleeWeapon.SoundRef = component.SoundRef;
			meleeWeapon.soundDelay = component.soundDelay;
			rightWeapon = meleeWeapon;
			SetWeapon(rightWeapon, HoldingHandler.HandType.Right);
			MeleeWeapon meleeWeapon2 = GetComponentInChildren<HandLeft>().gameObject.AddComponent<MeleeWeapon>();
			CollisionWeapon collisionWeapon2 = GetComponentInChildren<HandLeft>().gameObject.AddComponent<CollisionWeapon>();
			if ((bool)component3)
			{
				collisionWeapon2.gameObject.AddComponent<CollisionSound>().SoundEffectRef = component3.SoundEffectRef;
			}
			meleeWeapon2.forceCurve = component.forceCurve;
			meleeWeapon2.curveForce = component.curveForce;
			collisionWeapon2.impactMultiplier = component2.impactMultiplier;
			collisionWeapon2.damage = component2.damage;
			collisionWeapon2.onImpactForce = component2.onImpactForce;
			meleeWeapon2.internalCooldown = component.internalCooldown;
			meleeWeapon2.maxRange = component.maxRange;
			meleeWeapon2.setDirectionContinious = component.setDirectionContinious;
			meleeWeapon2.SoundRef = component.SoundRef;
			meleeWeapon2.soundDelay = component.soundDelay;
			leftWeapon = meleeWeapon2;
			SetWeapon(leftWeapon, HoldingHandler.HandType.Left);
			unit.m_AttackDistance = rightWeapon.maxRange;
			unit.m_PreferedDistance = rightWeapon.maxRange - 0.3f;
			m_unitAPI.UpdateECSValues();
			this.ChangeWeapons?.Invoke(unit);
		}
	}

	public void SetAttackSpeedMultiplier(float newValue)
	{
		attackSpeedMultiplier = newValue;
	}

	public void SetWeapon(Weapon weapon, HoldingHandler.HandType handType)
	{
		if ((bool)weapon)
		{
			if (handType == HoldingHandler.HandType.Right)
			{
				rightWeapon = weapon;
			}
			else
			{
				leftWeapon = weapon;
			}
		}
		float lowestAttackTimeOfWeapons = weapon.GetLowestAttackTimeOfWeapons();
		if (lowestAttackTimeOfWeapons < unit.lowestAttackTimeOfWeapons)
		{
			unit.lowestAttackTimeOfWeapons = lowestAttackTimeOfWeapons;
		}
	}

	public override void BatchedUpdate()
	{
		if ((bool)rightWeapon)
		{
			rightWeapon.UpdateCounters(attackSpeedMultiplier);
		}
		if ((bool)leftWeapon)
		{
			leftWeapon.UpdateCounters(attackSpeedMultiplier);
		}
	}

	public void DelayAttackCounter(float delay)
	{
		if ((bool)rightWeapon)
		{
			rightWeapon.internalCounter -= delay;
		}
		if ((bool)leftWeapon)
		{
			leftWeapon.internalCounter -= delay;
		}
	}

	public void Attack(Vector3 position, Rigidbody targetRig, Vector3 forceDirection, ForceWeapon forceWeapon = ForceWeapon.None)
	{
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		bool flag4 = false;
		bool isRemotelyControlled = data.input.IsRemotelyControlled;
		flag = !isRemotelyControlled;
		flag2 = !isRemotelyControlled;
		flag3 = isRemotelyControlled;
		flag4 = isRemotelyControlled;
		if ((flag2 && data.muscleControl < 0.7f) || (!leftWeapon && !rightWeapon))
		{
			return;
		}
		if (!leftWeapon)
		{
			activeWeapon = rightWeapon;
		}
		else if (!rightWeapon)
		{
			activeWeapon = leftWeapon;
		}
		else
		{
			activeWeapon = (((bool)leftWeapon && UnityEngine.Random.value > 0.5f) ? leftWeapon : rightWeapon);
		}
		activeWeapon = activeWeapon.GetBestWeaponOnWeapon();
		if ((bool)leftWeapon && (bool)rightWeapon && flag)
		{
			otherWeapon = ((activeWeapon == rightWeapon) ? leftWeapon : rightWeapon);
			if (activeWeapon.internalCounter < activeWeapon.internalCooldown)
			{
				if (!otherWeapon || !(otherWeapon.internalCounter > otherWeapon.internalCooldown))
				{
					return;
				}
				activeWeapon = otherWeapon;
			}
			if (otherWeapon.maxRange > activeWeapon.maxRange && !otherWeapon.IsOnCooldown())
			{
				activeWeapon = otherWeapon;
			}
		}
		if ((bool)leftWeapon && (bool)rightWeapon)
		{
			if (forceWeapon == ForceWeapon.Left)
			{
				activeWeapon = leftWeapon;
			}
			if (forceWeapon == ForceWeapon.Right)
			{
				activeWeapon = rightWeapon;
			}
		}
		if (activeWeapon != null && (!activeWeapon.IsOnCooldown() || flag3) && (data.input.hasControl || !data.targetData || data.distanceToTarget < activeWeapon.maxRange + data.targetData.unit.thickness || flag4))
		{
			_ = activeWeapon;
			if (activeWeapon.randomCooldown)
			{
				activeWeapon.internalCounter = Mathf.Clamp(UnityEngine.Random.Range(activeWeapon.internalCooldown * -0.8f, activeWeapon.internalCooldown * 0.2f), -1f, 1f);
			}
			else
			{
				activeWeapon.internalCounter = 0f;
			}
			activeWeapon.Attack(position, targetRig, forceDirection);
			this.AttackStarted?.Invoke(unit, position, targetRig, forceDirection, (activeWeapon == leftWeapon) ? ForceWeapon.Left : ForceWeapon.Right);
		}
	}

	public void DelayAttack(float damageStrength)
	{
		if ((bool)rightWeapon)
		{
			rightWeapon.internalCounter = Mathf.Clamp(rightWeapon.internalCounter - UnityEngine.Random.Range(0.5f, 1f) * damageStrength, -1f, float.PositiveInfinity);
		}
		if ((bool)leftWeapon)
		{
			leftWeapon.internalCounter = Mathf.Clamp(leftWeapon.internalCounter - UnityEngine.Random.Range(0.5f, 1f) * damageStrength, -1f, float.PositiveInfinity);
		}
	}

	public void StopAttacksFor(float seconds)
	{
		if ((bool)rightWeapon)
		{
			rightWeapon.internalCounter = Mathf.Clamp(rightWeapon.internalCounter, -1000f, rightWeapon.internalCooldown - seconds);
		}
		if ((bool)leftWeapon)
		{
			leftWeapon.internalCounter = Mathf.Clamp(leftWeapon.internalCounter, -1000f, leftWeapon.internalCooldown - seconds);
		}
	}

	private void AddWeapon(Weapon weapon)
	{
		if (!(weapon == null) && allWeapons != null)
		{
			allWeapons.Add(weapon);
			weapon.SetWeaponIndex((byte)(allWeapons.Count - 1));
		}
	}
}
