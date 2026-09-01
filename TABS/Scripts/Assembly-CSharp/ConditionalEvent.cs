using System;
using System.Collections;
using Landfall.TABS;
using Landfall.TABS.GameState;
using UnityEngine;

public class ConditionalEvent : MonoBehaviour
{
	public delegate void TurnedConditionalEventOnEventHandler(ConditionalEvent conditionalEvent, ConditionalEventInstance eventInstance);

	public delegate void TurnedConditionalEventOffEventHandler(ConditionalEvent conditionalEvent, ConditionalEventInstance eventInstance);

	public bool controllableByPlayer = true;

	public ConditionalEventInstance[] events;

	[HideInInspector]
	public DataHandler data;

	private bool done;

	private ConditionalEvent[] allOtherEvents;

	[HideInInspector]
	public float extraRange;

	public float failChancePer100Hp;

	public bool ignorePossession;

	public bool ignoreDead;

	public bool isStunnable = true;

	private GameStateManager man;

	[HideInInspector]
	public Rigidbody cachedEnemyWeapon;

	private WeaponHandler weaponHandler;

	private float isStunnedFor;

	public int NetworkId { get; set; }

	public event TurnedConditionalEventOnEventHandler TurnedConditionalEventOn;

	public event TurnedConditionalEventOffEventHandler TurnedConditionalEventOff;

	private void Start()
	{
		man = ServiceLocator.GetService<GameStateManager>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		allOtherEvents = base.transform.root.GetComponentsInChildren<ConditionalEvent>();
		for (int i = 0; i < events.Length; i++)
		{
			ConditionalEventInstance conditionalEventInstance = events[i];
			conditionalEventInstance.NetworkId = i;
			conditionalEventInstance.moves = GetComponents<Move>();
			for (int j = 0; j < conditionalEventInstance.conditions.Length; j++)
			{
				EventCondition eventCondition = conditionalEventInstance.conditions[j];
				if (!eventCondition.startOnCD)
				{
					eventCondition.counter = float.MaxValue;
				}
			}
		}
		data.unit.AddWasAttackedAction(OnUnitAttacked);
		data.unit.AddWasDamagedAction(OnUnitDamaged);
		data.unit.AddAttackAction(OnUnitAttack);
		data.healthHandler.AddDieAction(OnUnitDead);
		Unit unit = data.unit;
		unit.DealDamageAction = (Action<float>)Delegate.Combine(unit.DealDamageAction, new Action<float>(OnDealDamage));
	}

	private void OnDestroy()
	{
		if (!(data == null) && !(data.unit == null))
		{
			data.unit.RemoveWasAttackedAction(OnUnitAttacked);
			data.unit.RemoveWasDamagedAction(OnUnitDamaged);
			data.unit.RemoveAttackAction(OnUnitAttack);
			data.healthHandler.RemoveDieAction(OnUnitDead);
			Unit unit = data.unit;
			unit.DealDamageAction = (Action<float>)Delegate.Remove(unit.DealDamageAction, new Action<float>(OnDealDamage));
		}
	}

	public void Update()
	{
		IncrementCounters();
		bool hasControl = data.input.hasControl;
		bool isRemotelyControlled = data.input.IsRemotelyControlled;
		if ((!hasControl && !isRemotelyControlled) || (hasControl && ignorePossession))
		{
			CheckConditionsUpdate();
		}
		if (isStunnedFor > 0f)
		{
			isStunnedFor -= Time.deltaTime;
		}
	}

	public void CheckConditionsUpdate(bool forceFail = false)
	{
		if (data == null)
		{
			data = base.transform.root.GetComponentInChildren<DataHandler>();
		}
		bool hasControl = data.input.hasControl;
		for (int i = 0; i < events.Length; i++)
		{
			if (events[i].checkAutomatically || hasControl)
			{
				CheckConditions(events[i], EventCondition.ConditionType.None, justCheck: false, forceFail);
			}
		}
	}

	public bool CheckConditions(ConditionalEventInstance eventToCheck, EventCondition.ConditionType conditionCall = EventCondition.ConditionType.None, bool justCheck = false, bool forceFail = false)
	{
		if (base.gameObject == null || eventToCheck == null)
		{
			return false;
		}
		for (int i = 0; i < eventToCheck.conditions.Length; i++)
		{
			if (!forceFail && CheckCondition(eventToCheck.conditions[i], eventToCheck, conditionCall, justCheck))
			{
				continue;
			}
			if (eventToCheck.isOn && !justCheck)
			{
				if (base.gameObject.activeInHierarchy)
				{
					StartCoroutine(DelayTurnOffEvent(eventToCheck));
				}
				eventToCheck.isOn = false;
			}
			return false;
		}
		if (done)
		{
			return false;
		}
		if (!justCheck)
		{
			if (!eventToCheck.isOn)
			{
				if (base.gameObject.activeInHierarchy)
				{
					StartCoroutine(DelayTurnOnEvent(eventToCheck));
				}
				eventToCheck.isOn = true;
			}
			for (int j = 0; j < eventToCheck.conditions.Length; j++)
			{
				EventCondition eventCondition = eventToCheck.conditions[j];
				eventCondition.counter = UnityEngine.Random.Range(0f, 0f - eventCondition.extraRandomCooldown);
			}
			if (eventToCheck.stunAllEventsFor != 0f)
			{
				StunAllCombatMovesForSeconds(eventToCheck.stunAllEventsFor);
			}
			if (eventToCheck.stopWeaponAttacksFor != 0f)
			{
				StunWeaponAttacksForSecons(eventToCheck.stopWeaponAttacksFor);
			}
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(DelayMovesAndContinuousEvent(eventToCheck));
			}
		}
		return true;
	}

	private IEnumerator DelayTurnOffEvent(ConditionalEventInstance eventToCheck)
	{
		this.TurnedConditionalEventOff?.Invoke(this, eventToCheck);
		yield return new WaitForSeconds(eventToCheck.delay);
		eventToCheck.turnOffEvent?.Invoke();
	}

	private IEnumerator DelayTurnOnEvent(ConditionalEventInstance eventToCheck)
	{
		this.TurnedConditionalEventOn?.Invoke(this, eventToCheck);
		yield return new WaitForSeconds(eventToCheck.delay);
		eventToCheck.turnOnEvent?.Invoke();
	}

	private IEnumerator DelayMovesAndContinuousEvent(ConditionalEventInstance eventToCheck)
	{
		yield return new WaitForSeconds(eventToCheck.delay);
		eventToCheck.continuousEvent?.Invoke();
		for (int i = 0; i < eventToCheck.moves.Length; i++)
		{
			eventToCheck.moves[i].DoMove(cachedEnemyWeapon, data.targetMainRig, data ? data.targetData : null);
		}
	}

	public void TurnOffEvent(int eventInstanceNetworkId)
	{
		ConditionalEventInstance eventInstance = GetEventInstance(eventInstanceNetworkId);
		if (eventInstance != null && eventInstance.isOn)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(DelayTurnOffEvent(eventInstance));
			}
			eventInstance.isOn = false;
		}
	}

	public void TurnOnEvent(int eventInstanceNetworkId)
	{
		ConditionalEventInstance eventInstance = GetEventInstance(eventInstanceNetworkId);
		if (eventInstance != null && !eventInstance.isOn)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(DelayTurnOnEvent(eventInstance));
			}
			eventInstance.isOn = true;
		}
	}

	public void DoMovesAndContinuousEvent(int eventInstanceNetworkId)
	{
		ConditionalEventInstance eventInstance = GetEventInstance(eventInstanceNetworkId);
		if (eventInstance != null && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(DelayMovesAndContinuousEvent(eventInstance));
		}
	}

	private ConditionalEventInstance GetEventInstance(int eventInstanceNetworkId)
	{
		int i = 0;
		for (int num = events.Length; i < num; i++)
		{
			ConditionalEventInstance conditionalEventInstance = events[i];
			if (conditionalEventInstance != null && conditionalEventInstance.NetworkId == eventInstanceNetworkId)
			{
				return conditionalEventInstance;
			}
		}
		return null;
	}

	private bool CheckCondition(EventCondition condition, ConditionalEventInstance eventToCheck, EventCondition.ConditionType conditionCall = EventCondition.ConditionType.None, bool justCheck = false)
	{
		if (!data)
		{
			return false;
		}
		if (data.Dead && !ignoreDead)
		{
			return false;
		}
		if (isStunnedFor > 0f)
		{
			return false;
		}
		if (man.GameState != GameState.BattleState)
		{
			return false;
		}
		if (condition.ignoreInPossession && data.input.hasControl)
		{
			return true;
		}
		if (condition.conditionType == EventCondition.ConditionType.UnitDistanceToTarget)
		{
			if (condition.valueType == EventCondition.ValueType.Max)
			{
				if (data.distanceToTarget > condition.value + extraRange + ((condition.rangeType == EventCondition.RangeType.RangePlusUnitRange) ? data.unit.m_AttackDistance : 0f))
				{
					return false;
				}
			}
			else if (condition.valueType == EventCondition.ValueType.Min && data.distanceToTarget < condition.value + ((condition.rangeType == EventCondition.RangeType.RangePlusUnitRange) ? data.unit.m_AttackDistance : 0f))
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitTargetHP)
		{
			if (condition.valueType == EventCondition.ValueType.Max)
			{
				if (!data.targetData || data.targetData.maxHealth > condition.value)
				{
					return false;
				}
			}
			else if (condition.valueType == EventCondition.ValueType.Min && (!data.targetData || data.maxHealth < condition.value))
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitHealth)
		{
			if (!data || data.health / data.maxHealth > condition.value)
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitDeath)
		{
			if (conditionCall != EventCondition.ConditionType.UnitDeath)
			{
				eventToCheck.checkAutomatically = false;
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitWasDamaged)
		{
			if (conditionCall != EventCondition.ConditionType.UnitWasDamaged)
			{
				eventToCheck.checkAutomatically = false;
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitWasAttacked)
		{
			if (conditionCall != EventCondition.ConditionType.UnitWasAttacked && !data.input.hasControl && !data.input.IsRemotelyControlled)
			{
				eventToCheck.checkAutomatically = false;
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitAttack)
		{
			if (conditionCall != EventCondition.ConditionType.UnitAttack)
			{
				eventToCheck.checkAutomatically = false;
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitAngleToTarget)
		{
			if (condition.valueType == EventCondition.ValueType.Min)
			{
				if (data.angleToTarget < condition.value)
				{
					return false;
				}
			}
			else if (condition.valueType == EventCondition.ValueType.Max && data.angleToTarget > condition.value)
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.Cooldown)
		{
			if (!(condition.counter > condition.value))
			{
				return false;
			}
			if (condition.alwaysResetCounter && !justCheck)
			{
				condition.counter = UnityEngine.Random.Range(0f, 0f - condition.extraRandomCooldown);
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.Chance)
		{
			if (condition.value < UnityEngine.Random.value)
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.ChancePerSecond)
		{
			if (condition.value * Mathf.Clamp(Time.deltaTime, 0f, 0.02f) < UnityEngine.Random.value)
			{
				return false;
			}
		}
		else if (condition.conditionType == EventCondition.ConditionType.UnitDealtDamageTotal && data.unit.damageDealt < condition.value)
		{
			return false;
		}
		if ((bool)data && (bool)data.targetData && data.targetData.maxHealth * failChancePer100Hp * UnityEngine.Random.value * 0.01f > 1f)
		{
			return false;
		}
		return true;
	}

	private void IncrementCounters()
	{
		for (int i = 0; i < events.Length; i++)
		{
			for (int j = 0; j < events[i].conditions.Length; j++)
			{
				EventCondition eventCondition = events[i].conditions[j];
				if (eventCondition.conditionType == EventCondition.ConditionType.Cooldown)
				{
					if (!eventCondition.onlyCountWhenUnitInRange)
					{
						eventCondition.counter += Time.deltaTime;
					}
					else if (eventCondition.whichRange == EventCondition.WhichRange.UnitRange && data.inRange)
					{
						eventCondition.counter += Time.deltaTime;
					}
					else if (eventCondition.whichRange == EventCondition.WhichRange.Specified && data.distanceToTarget < eventCondition.cooldownRange)
					{
						eventCondition.counter += Time.deltaTime;
					}
				}
			}
		}
	}

	public void AddRangeToAllConditions(float range)
	{
		for (int i = 0; i < events.Length; i++)
		{
			for (int j = 0; j < events[i].conditions.Length; j++)
			{
				if (events[i].conditions[j].conditionType == EventCondition.ConditionType.UnitDistanceToTarget && events[i].conditions[j].valueType == EventCondition.ValueType.Max)
				{
					events[i].conditions[j].value += range;
				}
			}
		}
	}

	public void OnUnitAttacked(Rigidbody enemyWeapon, Rigidbody enemyTorso)
	{
		if (!data.input.IsRemotelyControlled && !data.input.hasControl)
		{
			cachedEnemyWeapon = enemyWeapon;
			for (int i = 0; i < events.Length; i++)
			{
				CheckConditions(events[i], EventCondition.ConditionType.UnitWasAttacked);
			}
		}
	}

	public void OnUnitDamaged(Rigidbody enemyWeapon, Rigidbody enemyTorso)
	{
		if (!data.input.IsRemotelyControlled && (!data.input.hasControl || !controllableByPlayer))
		{
			for (int i = 0; i < events.Length; i++)
			{
				CheckConditions(events[i], EventCondition.ConditionType.UnitWasDamaged);
			}
		}
	}

	public void OnDealDamage(float dealtDamage)
	{
		if (!data.input.IsRemotelyControlled && (!data.input.hasControl || !controllableByPlayer))
		{
			for (int i = 0; i < events.Length; i++)
			{
				CheckConditions(events[i], EventCondition.ConditionType.UnitDealtDamageTotal);
			}
		}
	}

	public void OnUnitAttack(Vector3 position, Rigidbody targetRig, Vector3 attackDireciton)
	{
		if (!data.input.IsRemotelyControlled && !data.input.hasControl)
		{
			for (int i = 0; i < events.Length; i++)
			{
				CheckConditions(events[i], EventCondition.ConditionType.UnitAttack);
			}
		}
	}

	public void OnUnitDead()
	{
		if (!data.input.hasControl || ignorePossession)
		{
			for (int i = 0; i < events.Length; i++)
			{
				CheckConditions(events[i], EventCondition.ConditionType.UnitDeath);
				events[i].turnOffEvent.Invoke();
			}
			done = true;
		}
	}

	private void StunAllCombatMovesForSeconds(float eventSeconds)
	{
		for (int i = 0; i < allOtherEvents.Length; i++)
		{
			if (allOtherEvents[i].isStunnable)
			{
				allOtherEvents[i].StunAllOfMyMovesFor(eventSeconds);
			}
		}
	}

	private void StunWeaponAttacksForSecons(float weaponSeconds)
	{
		if (!weaponHandler)
		{
			weaponHandler = base.transform.root.GetComponentInChildren<WeaponHandler>();
		}
		if ((bool)weaponHandler)
		{
			weaponHandler.StopAttacksFor(weaponSeconds);
		}
	}

	public void StunAllOfMyMovesFor(float seconds)
	{
		isStunnedFor = seconds;
	}
}
