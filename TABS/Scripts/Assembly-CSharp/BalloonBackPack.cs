using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BalloonBackPack : MonoBehaviour
{
	public delegate void MeteorEventHandler();

	private float flyingFor;

	public float willFlyFor = 3f;

	private bool hasBeenStarted;

	private bool isFlyingUpwards;

	private bool shouldFly = true;

	private CodeAnimation anim;

	private Rigidbody balloonRig;

	public float force;

	public AnimationCurve forceCurve;

	private DataHandler dataHandler;

	private float startHeight;

	public MeleeWeaponStick stickLeft;

	public MeleeWeaponStick stickRight;

	private Rigidbody otherRigidbodyLeft;

	private Rigidbody otherRigidbodyRight;

	private DataHandler dataLeft;

	private DataHandler dataRight;

	private float startMass;

	private bool shouldMeteor;

	public float meteorChance;

	public string soundEffect;

	private ConfigurableJoint balloonJoint;

	public UnityEvent LetGoEvent;

	public UnityEvent MeteorEvent;

	private MeleeWeaponStickStats stickstats;

	private bool done;

	public event MeteorEventHandler networkMeteorEvent;

	private void Start()
	{
		anim = GetComponentInChildren<CodeAnimation>(includeInactive: true);
		balloonRig = GetComponentInChildren<Rigidbody>(includeInactive: true);
		balloonJoint = GetComponentInChildren<ConfigurableJoint>(includeInactive: true);
		dataHandler = base.transform.GetComponentInParent<DataHandler>();
		stickstats = base.transform.GetComponent<MeleeWeaponStickStats>();
		dataHandler.takeFallDamage = false;
		startMass = balloonRig.mass;
		if (meteorChance != 0f && meteorChance >= Random.value)
		{
			shouldMeteor = true;
		}
		WeaponHandler componentInChildren = base.transform.root.GetComponentInChildren<WeaponHandler>();
		if (!componentInChildren)
		{
			return;
		}
		if ((bool)componentInChildren.leftWeapon)
		{
			MeleeWeaponStick meleeWeaponStick = componentInChildren.leftWeapon.GetComponent<MeleeWeaponStick>();
			if (!meleeWeaponStick)
			{
				meleeWeaponStick = componentInChildren.rightWeapon.transform.gameObject.AddComponent<MeleeWeaponStick>();
				StickPosition stickPosition = componentInChildren.rightWeapon.transform.gameObject.AddComponent<StickPosition>();
				meleeWeaponStick.fixPositionAmount = stickstats.fixPositionAmount;
				meleeWeaponStick.breakForce = stickstats.breakForce;
				meleeWeaponStick.onlyOtherTeam = stickstats.onlyOtherTeam;
				meleeWeaponStick.walkBackwardsWhenStuck = stickstats.walkBackwardsWhenStuck;
				meleeWeaponStick.downwardsForceOnStuckRig = stickstats.downwardsForceOnStuckRig;
				meleeWeaponStick.time = stickstats.time;
				meleeWeaponStick.stickEvent = stickstats.stickEvent;
				stickPosition.lockRotation = stickstats.lockRotation;
				stickPosition.radius = stickstats.radius;
			}
			stickLeft = meleeWeaponStick;
		}
		if ((bool)componentInChildren.rightWeapon)
		{
			MeleeWeaponStick meleeWeaponStick2 = componentInChildren.rightWeapon.GetComponent<MeleeWeaponStick>();
			if (!meleeWeaponStick2)
			{
				meleeWeaponStick2 = componentInChildren.rightWeapon.transform.gameObject.AddComponent<MeleeWeaponStick>();
				StickPosition stickPosition2 = componentInChildren.rightWeapon.transform.gameObject.AddComponent<StickPosition>();
				meleeWeaponStick2.fixPositionAmount = stickstats.fixPositionAmount;
				meleeWeaponStick2.breakForce = stickstats.breakForce;
				meleeWeaponStick2.onlyOtherTeam = stickstats.onlyOtherTeam;
				meleeWeaponStick2.walkBackwardsWhenStuck = stickstats.walkBackwardsWhenStuck;
				meleeWeaponStick2.downwardsForceOnStuckRig = stickstats.downwardsForceOnStuckRig;
				meleeWeaponStick2.time = stickstats.time;
				meleeWeaponStick2.stickEvent = stickstats.stickEvent;
				stickPosition2.lockRotation = stickstats.lockRotation;
				stickPosition2.radius = stickstats.radius;
			}
			stickRight = meleeWeaponStick2;
		}
	}

	private void GetData()
	{
		if ((bool)stickLeft && (bool)stickRight)
		{
			return;
		}
		if ((bool)stickLeft && (bool)stickLeft.otherRigidbody && stickLeft.otherRigidbody != otherRigidbodyLeft)
		{
			otherRigidbodyLeft = stickLeft.otherRigidbody;
			DataHandler componentInChildren = stickLeft.otherRigidbody.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren)
			{
				dataLeft = componentInChildren;
			}
		}
		if ((bool)stickRight && (bool)stickRight.otherRigidbody && stickRight.otherRigidbody != otherRigidbodyRight)
		{
			otherRigidbodyRight = stickRight.otherRigidbody;
			DataHandler componentInChildren2 = stickRight.otherRigidbody.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren2)
			{
				dataRight = componentInChildren2;
			}
		}
		if ((bool)stickLeft && (bool)dataLeft && (bool)stickLeft.otherRigidbody)
		{
			dataLeft.sinceGrounded = Mathf.Clamp(dataLeft.sinceGrounded, 0f, 1f);
		}
		if ((bool)stickRight && (bool)dataRight && (bool)stickRight.otherRigidbody)
		{
			dataRight.sinceGrounded = Mathf.Clamp(dataRight.sinceGrounded, 0f, 1f);
		}
	}

	private void Update()
	{
		if (!balloonRig)
		{
			return;
		}
		StartBackPack();
		GetData();
		if (!dataHandler.Dead && (bool)stickLeft && (bool)stickRight)
		{
			if ((bool)stickLeft.joint && (bool)dataLeft)
			{
				dataLeft.sinceGrounded = 0f;
			}
			if ((bool)stickRight.joint && (bool)dataRight)
			{
				dataRight.sinceGrounded = 0f;
			}
		}
		if (isFlyingUpwards && flyingFor > willFlyFor)
		{
			if (shouldMeteor)
			{
				this.networkMeteorEvent?.Invoke();
				MeteorAttack();
				willFlyFor = forceCurve[forceCurve.length - 1].time;
			}
			LetGo();
		}
		if (flyingFor < 2f || dataHandler.mainRig.position.y > startHeight + 10f)
		{
			flyingFor += Time.deltaTime;
		}
		if ((bool)balloonJoint)
		{
			dataHandler.sinceGrounded = Mathf.Clamp(dataHandler.sinceGrounded, 0f, 1f);
		}
	}

	private void FixedUpdate()
	{
		if ((bool)balloonRig && isFlyingUpwards)
		{
			if (dataHandler.muscleControl < 0.5f)
			{
				balloonRig.AddForce(Vector3.up * force * forceCurve.Evaluate(flyingFor) * 0.6f, ForceMode.Acceleration);
			}
			else
			{
				balloonRig.AddForce(Vector3.up * force * forceCurve.Evaluate(flyingFor), ForceMode.Acceleration);
			}
		}
	}

	private void LetGo()
	{
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundEffect, 1f, base.transform.position, SoundEffectVariations.MaterialType.Default, balloonRig.transform);
		balloonRig.mass = startMass * 0.2f;
		isFlyingUpwards = false;
		stickLeft?.RemoveStickJoint();
		stickRight?.RemoveStickJoint();
		if ((bool)balloonJoint)
		{
			Object.Destroy(balloonJoint);
			balloonRig.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
			balloonRig.gameObject.GetComponent<RemoveAfterSeconds>().shrink = true;
		}
		LetGoEvent.Invoke();
	}

	public void StartBackPack()
	{
		if (done || !balloonRig || isFlyingUpwards)
		{
			return;
		}
		if ((bool)stickLeft || (bool)stickRight)
		{
			bool flag = false;
			if ((bool)stickLeft && (bool)stickLeft.otherRigidbody)
			{
				flag = true;
				dataLeft = stickLeft.otherRigidbody.GetComponentInParent<DataHandler>();
			}
			if ((bool)stickRight && (bool)stickRight.otherRigidbody)
			{
				flag = true;
				dataRight = stickRight.otherRigidbody.GetComponentInParent<DataHandler>();
			}
			if (!flag)
			{
				return;
			}
		}
		done = true;
		StartCoroutine(DelayStart());
	}

	public void MeteorAttack()
	{
		MeteorEvent?.Invoke();
		shouldMeteor = false;
	}

	private IEnumerator DelayStart()
	{
		yield return new WaitForSeconds(0.5f);
		balloonRig.transform.parent.gameObject.SetActive(value: true);
		flyingFor = 0f;
		hasBeenStarted = true;
		isFlyingUpwards = true;
		startHeight = dataHandler.mainRig.transform.position.y;
		balloonRig.mass = startMass;
	}
}
