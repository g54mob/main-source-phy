using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class EntityAIFish : EntityAI
{
	[Header("Fish Variables")]
	[SerializeField]
	protected float circleForwardVectorScale = 0.5f;

	[Range(0f, 2f)]
	[SerializeField]
	protected float spinDir = 1f;

	[SerializeField]
	protected float waterHeightOffset = 1f;

	[SerializeField]
	protected AudioSource swimSfx;

	[SerializeField]
	protected float turningSpeed = 3f;

	protected bool hasSwimSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected Vector3 circleDirectionIdle;

	private float waterHeight;

	private bool isSimulating
	{
		get
		{
			return my.basicInfo.isSimulating;
		}
	}

	public override Vector3 HeadPosition
	{
		get
		{
			return my.Transform.position + my.Transform.forward * my.Transform.localScale.z;
		}
	}

	public override void Start()
	{
		if (!isSimulating)
		{
			return;
		}
		hasSwimSfx = swimSfx != null;
		if (hasSwimSfx)
		{
			mixer = swimSfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
		movement.jumpHeight *= base.transform.localScale.y;
		bob.deSync = UnityEngine.Random.Range(-1f, 1f) * ((float)Math.PI / 2f);
		isDead = false;
		my.basicInfo = GetComponent<BasicInfo>();
		if (object.ReferenceEquals(my.basicInfo, null))
		{
			Debug.LogError("AI is missing BasicInfo");
			base.enabled = false;
			return;
		}
		my.basicInfo.hasAiScript = true;
		my.basicInfo.aiEntity = this;
		my.timeSlider = TimeSlider.Instance;
		my.worldUp = Vector3.up;
		my.Transform = base.transform;
		my.ActiveMachine = Machine.Active();
		if (!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim)
		{
			if (FactionsController.setupComplete)
			{
				FactionsController.AddNewAIToFaction(this);
				faction.suddenLoss -= 1f;
				GetMoral();
			}
			my.Rigidbody = my.basicInfo.Rigidbody;
			if (!StatMaster.isMP)
			{
				my.Rigidbody.isKinematic = false;
			}
			my.Rigidbody.constraints = RigidbodyConstraints.None;
			movement.Initialize();
			retreating.Initialize();
			if (isSimulating && StatMaster.GodTools.GravityDisabled)
			{
				ActivateZeroG();
			}
			else if (isSimulating && wasGravDisabled && !StatMaster.GodTools.GravityDisabled)
			{
				DeActivateZeroG();
			}
		}
		if (object.ReferenceEquals(my.killingHandler, null))
		{
			my.killingHandler = GetComponent<KillingHandler>();
		}
		if (selfRighting.useMeshBounds)
		{
			CalculateHeight(my.VisObject.GetComponentInChildren<SkinnedMeshRenderer>());
		}
		else
		{
			CalculateHeight(my.Collider);
		}
		bob.startY = my.VisObject.localPosition.y;
		bob.visPosX = my.VisObject.localPosition.x;
		bob.visPosZ = my.VisObject.localPosition.z;
		bob.bobRateMultiphi = (float)Math.PI * 2f;
		bob.previousBobPos = my.Transform.position;
		if (!my.basicInfo.noRigidbody)
		{
			my.Rigidbody.constraints = RigidbodyConstraints.None;
		}
		if (freezRigidbody && groundJoint != null)
		{
			if (!looking.rotateRigidbody)
			{
				my.VisObject.rotation *= my.Rigidbody.rotation;
				my.Transform.rotation = movement.identityQuat;
				if (!my.basicInfo.noRigidbody)
				{
					my.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
				}
			}
			else
			{
				my.Transform.rotation *= my.VisObject.localRotation;
				my.VisObject.localRotation = movement.identityQuat;
				if (!my.basicInfo.noRigidbody)
				{
					my.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
				}
			}
		}
		selfRighting.StartRotation = ((!looking.rotateRigidbody) ? my.VisObject.rotation : my.Transform.rotation);
		looking.TargetRotation = selfRighting.StartRotation;
		if (!my.basicInfo.noRigidbody)
		{
			selfRighting.ResetDrag = my.Rigidbody.angularDrag;
		}
		movement.DifferenceToTarget = base.transform.forward;
		disposition.behavioursArray = disposition.behaviours.ToArray();
		for (int i = 0; i < disposition.behavioursArray.Length; i++)
		{
			disposition.behavioursArray[i].Initialize(i);
		}
		if (!my.attackScript || !my.attackScript.enabled)
		{
			disposition.canAttack = false;
		}
		if (!my.basicInfo.noRigidbody)
		{
			my.Rigidbody.centerOfMass = new Vector3(0f, aiBaseCenterOffset.y, 0f);
		}
		waitForFirstRotation = false;
		grounded = GroundedCheck();
	}

	public override void Update()
	{
		if (!isSimulating || !StatMaster.levelSimulating)
		{
			return;
		}
		if (StatMaster.isMP && !my.aiGenEntity.PhysicsEnabled)
		{
			Strip();
			my.killingHandler.enabled = false;
			base.enabled = false;
		}
		else
		{
			if (StatMaster.isClient && !StatMaster.isLocalSim)
			{
				return;
			}
			if (hasSwimSfx)
			{
				if (selfRighting.Grabbed || isDead)
				{
					if (swimSfx.isPlaying)
					{
						swimSfx.Stop();
					}
				}
				else
				{
					if (!swimSfx.isPlaying)
					{
						swimSfx.Play();
						swimSfx.timeSamples = UnityEngine.Random.Range(0, swimSfx.clip.samples);
					}
					if (my.basicInfo.submergedPercent > 0.6f)
					{
						if (swimSfx.outputAudioMixerGroup != underwaterMixer)
						{
							swimSfx.outputAudioMixerGroup = underwaterMixer;
						}
					}
					else if (swimSfx.outputAudioMixerGroup != mixer)
					{
						swimSfx.outputAudioMixerGroup = mixer;
					}
					float num = disposition.currentBehaviour.parameters.Speed;
					if (!my.basicInfo.noRigidbody)
					{
						num = (num + my.Rigidbody.velocity.sqrMagnitude * 0.1f) * 0.5f;
					}
					swimSfx.volume = num * 0.05f;
					swimSfx.pitch = 0.35f + num * 0.04f;
				}
			}
			if (StatMaster.isMP && !my.aiGenEntity.PhysicsEnabled)
			{
				Strip();
				my.killingHandler.enabled = false;
				base.enabled = false;
				return;
			}
			if (StatMaster.isClient && !StatMaster.isLocalSim)
			{
				if (!isDead && !StatMaster.GodTools.GravityDisabled && bob.Able && !bob.pause)
				{
					AnimateBob();
				}
				return;
			}
			if (isSimulating && !wasGravDisabled && StatMaster.GodTools.GravityDisabled)
			{
				ActivateZeroG();
			}
			else if (isSimulating && wasGravDisabled && !StatMaster.GodTools.GravityDisabled)
			{
				DeActivateZeroG();
			}
			if (isDead)
			{
				if (UTBisRunning)
				{
					StopCoroutines();
				}
				if (TargetBlock.gotTarget)
				{
					ClearTargetsTargetedBy();
					TargetBlock.Null();
				}
				if (!movement.keepInterpolation && my.Rigidbody.interpolation == RigidbodyInterpolation.None)
				{
					my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				}
				return;
			}
			if (!movement.keepInterpolation)
			{
				if ((my.timeSlider.delegateTimeScale < 0.3f && my.Rigidbody.interpolation == RigidbodyInterpolation.None) || disposition.currentBehaviour.parameters.Speed > 15f)
				{
					my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				}
				else if (!selfRighting.Grabbed && my.timeSlider.delegateTimeScale > 0.3f && my.Rigidbody.interpolation == RigidbodyInterpolation.Interpolate && disposition.currentBehaviour.parameters.Speed <= 15f)
				{
					my.Rigidbody.interpolation = RigidbodyInterpolation.None;
				}
			}
			if (StatMaster.GodTools.GravityDisabled || wasGravDisabled)
			{
				return;
			}
			my.visRotation = ((!looking.rotateRigidbody) ? my.VisObject.rotation : my.Rigidbody.rotation);
			my.visRight = my.visRotation * Vector3.right;
			if (TargetBlock.gotTarget)
			{
				Transform trans = TargetBlock.trans;
				if ((bool)trans && (!TargetBlock.isBlock || !TargetBlock.Block.IsDestroyed))
				{
					movement.TargetPos = TargetBlock.trans.position;
				}
				else
				{
					TargetBlock.Null();
				}
			}
			movement.PreviousPosition = HeadPosition;
			waterHeight = my.basicInfo.waterDepth;
			movement.TargetPos.y = Mathf.Min(movement.TargetPos.y, waterHeight - aiBaseHight - waterHeightOffset);
			Vector3 b = movement.TargetPos - movement.PreviousPosition;
			movement.DifferenceToTargetSqr = b.sqrMagnitude - aiBaseWidth;
			movement.DifferenceToTarget = Vector3.Slerp(movement.DifferenceToTarget, b, Time.deltaTime * turningSpeed);
			movement.DifferenceToTarget += UnityEngine.Random.insideUnitSphere * movement.VarianceAmount;
			if (movement.avoidanceOffset != 0f)
			{
				movement.avoidanceOffsetAngle = Quaternion.AngleAxis(movement.avoidanceOffset, my.worldUp);
			}
			else if (movement.avoidanceOffsetAngle != movement.identityQuat)
			{
				movement.avoidanceOffsetAngle = movement.identityQuat;
			}
			movement.upRightAngle = Vector3.Dot(my.TransformUP, my.worldUp);
			movement.Direction = movement.avoidanceOffsetAngle * movement.DifferenceToTarget;
			movement.Direction.Normalize();
			movement.hitHighObject = false;
			if (disposition.myState == EntityState.FactionCharge && movement.Able && TargetBlock.gotTarget)
			{
				Vector3 vector = (TargetBlock.isAI ? (TargetBlock.AI.faction.Center - faction.Center) : ((!TargetBlock.isBlock) ? (movement.TargetPos - faction.Center) : (FactionsController.GetMiddleOfClosestMachine(this) - faction.Center)));
				vector.Normalize();
				movement.factionchargeDir = movement.avoidanceOffsetAngle * vector;
				float num2 = Vector3.Dot(movement.factionchargeDir, movement.Direction);
				if (num2 > 0.75f)
				{
					movement.skipToCharge = false;
				}
				else
				{
					movement.skipToCharge = true;
				}
			}
			if (disposition.myState != EntityState.Strafing)
			{
				movement.dampened = false;
			}
			if (disposition.myState != EntityState.Idle && movement.returnToIdle)
			{
				movement.idleDampenTimer = 2f;
				movement.returnToIdle = false;
			}
			Animate();
			if (!movement.AntiStuckRunning && !movement.inJump && movement.avoidanceOffset != 0f && (disposition.myState == EntityState.Pursuing || disposition.myState == EntityState.Strafing || disposition.myState == EntityState.TacticalRetreat))
			{
				StartCoroutine(AntiStuck());
			}
			if (disposition.myState != EntityState.Grabbed)
			{
				CostumOnCollisionStay();
			}
			if (selfRighting.LockedRotation && !selfRighting.Grabbed && grounded)
			{
				if (!looking.rotateRigidbody)
				{
					my.VisObject.rotation = Quaternion.Slerp(my.visRotation, looking.TargetRotation, Time.deltaTime * looking.Smoothing);
				}
				else
				{
					my.Rigidbody.MoveRotation(Quaternion.Slerp(my.Rigidbody.rotation, looking.TargetRotation, Time.deltaTime * looking.Smoothing));
				}
			}
			my.TransformUP = my.Rigidbody.rotation * my.worldUp;
			if (disposition.canAttack && !selfRighting.Fallen && (disposition.currentBehaviour.attackState || disposition.myState == EntityState.Close) && TargetBlock.gotTarget)
			{
				my.attackScript.Attack(TargetBlock, movement.DifferenceToTargetSqr);
			}
			if (disposition.currentBehaviour.parameters.Speed == 0f)
			{
				disposition.currentBehaviour.parameters.Speed = movement.Speed;
			}
			if (!selfRighting.Grabbed && !grounded && !movement.inJump && !selfRighting.Fallen && selfRighting.AllowedToFall)
			{
				FallOver(true);
			}
			if (retreating.useMoral && retreating.moralWasCalculated && retreating.currentMoral < retreating.MentalFortutude && !retreating.exeededMoralValue)
			{
				retreating.exeededMoralValue = true;
			}
			if (!wasSimulating)
			{
				wasSimulating = isSimulating;
				OnSimulateStart();
			}
		}
	}

	public override bool GroundedCheck()
	{
		if (!movement.inJump)
		{
			if (disposition.myState == EntityState.Grabbed)
			{
				return false;
			}
			if (my.basicInfo.InWater)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public override void FallOver(bool clear)
	{
		if (selfRighting.enabled && selfRighting.AllowedToFall && !isDead && !my.basicInfo.noRigidbody && !grounded)
		{
			selfRighting.RandomWait = UnityEngine.Random.Range(0f, 0.25f);
			selfRighting.Timer = 0f;
			selfRighting.LockedRotation = false;
			selfRighting.Fallen = true;
		}
	}

	protected override void FallenCheck()
	{
		if (!grounded)
		{
			if (movement.CurrentVelocity.y < 0.1f && movement.CurrentVelocity.y > -0.1f)
			{
				FishFlop();
			}
		}
		else if (selfRighting.Fallen)
		{
			selfRighting.LockedRotation = true;
			selfRighting.Fallen = false;
		}
	}

	protected override void Animate()
	{
		if (!isDead && isSimulating && !StatMaster.GodTools.GravityDisabled && !selfRighting.Grabbed && disposition.myState != EntityState.Ungrounded && disposition.myState != EntityState.Fallen && !selfRighting.Fallen && grounded)
		{
			if ((looking.Focus != FocusOn.Target && looking.Focus != FocusOn.TargetOpposite) || !(movement.DifferenceToTargetSqr > base.BehavioursMaxDistance))
			{
				SetFocus();
			}
			if (bob.Able && (bool)my.killingHandler.my.Poser && my.killingHandler.my.Poser.animateWhileMoving)
			{
				AnimateOnMovement();
			}
		}
	}

	protected override void SetFocus()
	{
		FocusOn focusOn = looking.Focus;
		Vector3 targetPos = movement.TargetPos;
		Vector3 position = my.Transform.position;
		Vector3 forward = zero;
		targetPos.y = Mathf.Min(targetPos.y, waterHeight - aiBaseHight - waterHeightOffset);
		if (my.fireController.onFire)
		{
			looking.Focus = FocusOn.Velocity;
		}
		if (disposition.myState == EntityState.Fleeing)
		{
			focusOn = FocusOn.TargetOpposite;
		}
		else if (disposition.myState == EntityState.Idle)
		{
			focusOn = FocusOn.Velocity;
		}
		switch (focusOn)
		{
		case FocusOn.Target:
			forward = targetPos - position;
			break;
		case FocusOn.Velocity:
			if (movement.VelocitySqr > 10f)
			{
				forward = movement.CurrentVelocity;
			}
			else if (my.basicInfo.InWater)
			{
				Vector3 forward2 = my.Transform.forward;
				forward2.y = 0f;
				if ((int)forward.x != 0 || (int)forward.y != 0 || (int)forward.z != 0)
				{
					looking.TargetRotation = Quaternion.LookRotation(forward2.normalized, my.worldUp);
				}
				return;
			}
			break;
		case FocusOn.TargetOpposite:
			forward = position - targetPos;
			break;
		case FocusOn.ReverseVelocity:
			if (movement.CurrentVelocity != zero)
			{
				forward = -movement.CurrentVelocity;
			}
			break;
		}
		if ((int)forward.x != 0 || (int)forward.y != 0 || (int)forward.z != 0)
		{
			looking.TargetRotation = Quaternion.LookRotation(forward, my.Transform.up);
			Vector3 eulerAngles = looking.TargetRotation.eulerAngles;
			looking.TargetRotation = Quaternion.Euler(new Vector3(eulerAngles.x, eulerAngles.y, 0f));
		}
		waitForFirstRotation = false;
	}

	public override void Grabbed(MonoBehaviour grabber = null)
	{
		base.Grabbed(grabber);
	}

	public override IEnumerator BreakGrab(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (selfRighting.Grabbed && selfRighting.StopBeingGrabbedBy != null)
		{
			selfRighting.StopBeingGrabbedBy();
		}
	}

	public override void StopBeingGrabbed()
	{
		selfRighting.LockedRotation = true;
		selfRighting.Grabbed = false;
		selfRighting.StopBeingGrabbedBy = null;
		if (!movement.keepInterpolation)
		{
			my.Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
	}

	protected override void Ungrounded()
	{
		FallenCheck();
	}

	protected override void WaterRight()
	{
		FallenCheck();
	}

	public override void Idle()
	{
		if (movement.Able)
		{
			if (disposition.currentBehaviour.Action == Action.WalkAround)
			{
				circleDirectionIdle = my.Transform.forward * circleForwardVectorScale + my.Transform.right;
				my.Rigidbody.AddForce(movement.avoidanceOffsetAngle * Vector3.Cross(circleDirectionIdle, Vector3.up) * spinDir * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
			else if (movement.idleDampenTimer > 0.066f && (double)movement.VelocitySqr > 0.1)
			{
				movement.idleDampenTimer -= Time.deltaTime;
				my.Rigidbody.AddForce(-movement.CurrentVelocity * 0.75f, ForceMode.Acceleration);
			}
			else if (!movement.returnToIdle)
			{
				movement.returnToIdle = true;
			}
			else
			{
				my.Rigidbody.AddForce(Mathf.Cos(Time.timeSinceLevelLoad + bob.deSync) * Vector3.up - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
	}

	public override void CircleStrafe()
	{
		if (movement.Able)
		{
			my.Rigidbody.AddForce(movement.avoidanceOffsetAngle * Vector3.Cross(movement.Direction, Vector3.up) * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
		}
	}

	protected void FishFlop()
	{
		selfRighting.LockedRotation = false;
		if (grounded)
		{
			selfRighting.Fallen = false;
			return;
		}
		selfRighting.Timer += Time.deltaTime;
		if (selfRighting.Timer > selfRighting.SleepTime + selfRighting.RandomWait)
		{
			my.Rigidbody.AddForce((my.worldUp * 2f + UnityEngine.Random.insideUnitSphere + UnityEngine.Random.insideUnitSphere) * selfRighting.Torque - movement.CurrentVelocity, ForceMode.Impulse);
			selfRighting.Timer = 0f;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (Application.isPlaying && isSimulating)
		{
			Debug.DrawLine(my.Transform.position, movement.TargetPos, Color.green * 0.5f);
			Debug.DrawLine(HeadPosition, movement.TargetPos, Color.green);
			Debug.DrawRay(my.Rigidbody.worldCenterOfMass, movement.Direction * 2f, Color.yellow);
		}
	}
}
