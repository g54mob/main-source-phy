using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class AttackSpeedOverTimeEffect : UnitEffectBase
{
	public float attackSpeedToAdd = 0.1f;

	public float decaySpeed;

	public float scalingDecaySpeed;

	public float dragWhenAdd = 0.9f;

	public UnityEvent procEvent;

	private Unit unit;

	private bool inited;

	private void Init()
	{
		if (!inited)
		{
			inited = true;
			unit = base.transform.root.GetComponentInChildren<Unit>();
		}
	}

	public override void DoEffect()
	{
		AddAttackSpeed();
	}

	public override void Ping()
	{
		AddAttackSpeed();
	}

	private void AddAttackSpeed()
	{
		Init();
		procEvent.Invoke();
		if ((bool)unit)
		{
			unit.AddAttackSpeed(attackSpeedToAdd);
			float delta = unit.attackSpeedMultiplier * dragWhenAdd - unit.attackSpeedMultiplier;
			unit.AddAttackSpeed(delta);
		}
	}

	private void Update()
	{
		if ((bool)unit && unit.attackSpeedMultiplier > 1f)
		{
			unit.attackSpeedMultiplier -= Time.deltaTime * decaySpeed;
			unit.attackSpeedMultiplier -= Time.deltaTime * scalingDecaySpeed * unit.attackSpeedMultiplier;
			unit.AddAttackSpeed(0f);
		}
	}
}
