using System;
using UnityEngine;

[Serializable]
public class EventCondition
{
	public enum ConditionType
	{
		Cooldown = 0,
		UnitDistanceToTarget = 1,
		UnitTargetHP = 2,
		UnitWasAttacked = 3,
		UnitWasDamaged = 4,
		UnitDeath = 5,
		UnitAngleToTarget = 6,
		UnitAttack = 7,
		None = 8,
		Chance = 9,
		ChancePerSecond = 10,
		UnitDealtDamageTotal = 11,
		UnitHealth = 12
	}

	public enum ValueType
	{
		Min = 0,
		Max = 1
	}

	public enum RangeType
	{
		Range = 0,
		RangePlusUnitRange = 1
	}

	public enum WhichRange
	{
		UnitRange = 0,
		Specified = 1
	}

	[Space(10f)]
	public ConditionType conditionType;

	public ValueType valueType;

	public float value;

	public RangeType rangeType;

	public float extraRandomCooldown;

	[Space(10f)]
	public bool onlyCountWhenUnitInRange;

	public bool alwaysResetCounter;

	public WhichRange whichRange;

	public float cooldownRange = 10f;

	[HideInInspector]
	public float counter;

	public bool startOnCD;

	public bool ignoreInPossession;
}
