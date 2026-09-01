using System;
using System.Collections;
using Landfall.TABS;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : Weapon, IValidatable
{
	public AnimationCurve forceCurve;

	public float curveForce = 100f;

	private Transform forcePoint;

	private Rigidbody rig;

	public RigidbodyWithMultiplier[] additionalRigs;

	[HideInInspector]
	public Vector3 swingDirection;

	[HideInInspector]
	public bool isSwinging;

	[HideInInspector]
	public float sinceSwing;

	[HideInInspector]
	public bool canDealDamage;

	public bool setDirectionContinious;

	public float directionContiniousLerp;

	public bool setDirectionContiniousDuringWindup;

	public float yTargetOffset;

	public bool canDealDamageOutSideOfSwing;

	public float requiredPowerToParry = 100f;

	[Space(30f)]
	[SerializeField]
	private string soundRef = "";

	[SerializeField]
	private AudioPathData soundPathData;

	public float soundDelay;

	public UnityEvent swingEvent;

	private CollisionWeapon collision;

	private SoundPlayer m_soundPlayer;

	private FixedTimeStepService m_fixedTimeStepService;

	private bool hasSentAttackCall;

	private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

	public Action swingAction;

	public string SoundRef
	{
		get
		{
			return soundRef;
		}
		set
		{
			soundRef = value;
			AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
		}
	}

	public AudioPathData SoundPathData => soundPathData;

	public bool Validate()
	{
		return AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
	}

	protected override void Awake()
	{
		base.Awake();
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		m_fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
	}

	protected override void Start()
	{
		base.Start();
		rig = GetComponent<Rigidbody>();
		collision = GetComponent<CollisionWeapon>();
		ForcePoint componentInChildren = GetComponentInChildren<ForcePoint>();
		if ((bool)componentInChildren)
		{
			forcePoint = componentInChildren.transform;
		}
		else
		{
			forcePoint = base.transform;
		}
		if (canDealDamageOutSideOfSwing)
		{
			canDealDamage = true;
		}
	}

	private void SetDirection(Vector3 targetPos, bool dontLerp = false)
	{
		if ((bool)base.connectedData && (bool)forcePoint)
		{
			if (directionContiniousLerp != 0f && !dontLerp)
			{
				swingDirection = Vector3.Lerp(swingDirection, (targetPos + base.connectedData.characterForwardObject.forward * 0.5f + yTargetOffset * Vector3.up - forcePoint.position).normalized, Time.deltaTime * directionContiniousLerp);
			}
			else
			{
				swingDirection = (targetPos + base.connectedData.characterForwardObject.forward * 0.5f + yTargetOffset * Vector3.up - forcePoint.position).normalized;
			}
		}
	}

	public void SetSpecificSwingDirection(Vector3 targetDirection)
	{
		swingDirection = targetDirection;
	}

	public override void Attack(Vector3 position, Rigidbody targetRig, Vector3 forcedDirection, bool recursive = false)
	{
		if (((bool)holdable && !holdable.held) || (maxAngle != 360f && (bool)base.connectedData && base.connectedData.angleToTarget > maxAngle && !base.connectedData.input.hasControl))
		{
			return;
		}
		if (swingEvent != null)
		{
			swingEvent.Invoke();
		}
		SetDirection(position);
		if (callAttackEffects)
		{
			for (int i = 0; i < attackEffects.Length; i++)
			{
				attackEffects[i].DoEffect(targetRig, forcedDirection);
			}
		}
		Swing(position, targetRig);
	}

	private void Swing(Vector3 target, Rigidbody targetRig)
	{
		if ((bool)collision)
		{
			collision.lastHitHealth = null;
		}
		float time = forceCurve[forceCurve.length - 1].time;
		StopAllCoroutines();
		StartCoroutine(DoSwing(target, targetRig, time));
		StartCoroutine(PlaySound());
	}

	public void StopSwing()
	{
		isSwinging = false;
		StopAllCoroutines();
		if (!canDealDamageOutSideOfSwing)
		{
			canDealDamage = false;
		}
	}

	private IEnumerator DoSwing(Vector3 target, Rigidbody targetRig, float t)
	{
		if (swingAction != null)
		{
			swingAction();
		}
		SetDirection(target, dontLerp: true);
		hasSentAttackCall = false;
		float attackTime = UnityEngine.Random.Range(0.8f, 1.2f);
		float forceAmount = UnityEngine.Random.Range(0.8f, 1.2f);
		isSwinging = true;
		float counter = 0f;
		bool hasReducedTime = false;
		while (counter < t && (!holdable || holdable.held))
		{
			float forceCurveValue = forceCurve.Evaluate(counter);
			if (setDirectionContinious || (setDirectionContiniousDuringWindup && forceCurveValue < 0.1f))
			{
				SetDirection(target);
			}
			if ((bool)holdable && (bool)holdable.holderData && holdable.holderData.ragdollControl < 0.7f)
			{
				counter = float.PositiveInfinity;
				if (m_fixedTimeStepService.CurrentFixedTimeStep == FixedTimeStepService.FixedTimeStep.SixtyUpdates)
				{
					yield return null;
				}
				else
				{
					yield return waitForFixedUpdate;
				}
			}
			if (!hasReducedTime && forceCurveValue > 0.1f)
			{
				if (!hasSentAttackCall)
				{
					if ((bool)holdable && (bool)holdable.holderData && (bool)base.connectedData && (bool)targetRig && (bool)base.connectedData.targetData && (bool)holdable.holderData.mainRig)
					{
						Unit componentInParent = targetRig.GetComponentInParent<Unit>();
						if (componentInParent != null)
						{
							componentInParent.WasAttacked(rig, holdable.holderData.mainRig);
						}
					}
					hasSentAttackCall = true;
					canDealDamage = true;
				}
				if ((bool)holdable)
				{
					holdable.ReduceHoldable(t - counter);
				}
			}
			float num = (2f + attackSpeedM) * 0.33f * Time.deltaTime;
			Vector3 vector = FixedTimeStepService.SmallForceCoefficient * curveForce * forceAmount * forceCurve.Evaluate(counter) * num * swingDirection;
			if ((bool)rig && (bool)forcePoint && !float.IsNaN(vector.x) && !float.IsNaN(vector.y) && !float.IsNaN(vector.z))
			{
				rig.AddForceAtPosition(vector, forcePoint.position, ForceMode.Acceleration);
			}
			if (additionalRigs != null && additionalRigs.Length != 0)
			{
				for (int i = 0; i < additionalRigs.Length; i++)
				{
					additionalRigs[i].rig.AddForce(FixedTimeStepService.SmallForceCoefficient * additionalRigs[i].multiplier * vector, ForceMode.Acceleration);
				}
			}
			counter = (sinceSwing = counter + num * attackTime);
			if (m_fixedTimeStepService.CurrentFixedTimeStep == FixedTimeStepService.FixedTimeStep.SixtyUpdates)
			{
				yield return null;
			}
			else
			{
				yield return waitForFixedUpdate;
			}
		}
		isSwinging = false;
		if (!canDealDamageOutSideOfSwing)
		{
			canDealDamage = false;
		}
	}

	private IEnumerator PlaySound()
	{
		yield return new WaitForSeconds(soundDelay);
		if (soundRef != string.Empty)
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(soundPathData, 1f, base.transform.position);
		}
	}

	public void EnableDamageOutOfSwing()
	{
		canDealDamageOutSideOfSwing = true;
		canDealDamage = true;
	}

	public void DisableDamageOutOfSwing()
	{
		canDealDamageOutSideOfSwing = false;
		canDealDamage = false;
	}
}
