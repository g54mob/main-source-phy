using UnityEngine;

public class EntityAIBird : EntityAI
{
	[Header("Flying Variables")]
	public float ascendForce = 20f;

	public float descendForce = 12f;

	public bool normallyKinematic = true;

	public override void Start()
	{
		base.Start();
		if (StatMaster.levelSimulating && normallyKinematic && !my.basicInfo.noRigidbody && !my.Rigidbody.isKinematic && (disposition.myState == EntityState.Stationary || disposition.myState == EntityState.Idle) && grounded)
		{
			BasicInfo basicInfo = my.basicInfo;
			bool isKinematic = true;
			my.Rigidbody.isKinematic = isKinematic;
			basicInfo.isKinematic = isKinematic;
		}
	}

	public override void Update()
	{
		base.Update();
		if (!StatMaster.levelSimulating || isDead || selfRighting.Grabbed || my.basicInfo.noRigidbody)
		{
			return;
		}
		if (StatMaster.isMP && !my.aiGenEntity.PhysicsEnabled)
		{
			Strip();
			my.killingHandler.enabled = false;
			base.enabled = false;
		}
		else if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			if (!isDead && !StatMaster.GodTools.GravityDisabled && bob.Able && !bob.pause)
			{
				AnimateBob();
			}
		}
		else if (my.basicInfo.isKinematic && ((disposition.myState != EntityState.Stationary && disposition.myState != EntityState.Idle) || !grounded))
		{
			BasicInfo basicInfo = my.basicInfo;
			bool isKinematic = false;
			my.Rigidbody.isKinematic = isKinematic;
			basicInfo.isKinematic = isKinematic;
		}
	}

	public override void Idle()
	{
		if (!movement.Able || my.basicInfo.InWater)
		{
			return;
		}
		if (disposition.currentBehaviour.Action == Action.WalkAround)
		{
			if (movement.randomWalkTimer >= movement.randomWalkRate + movement.randomWalkPeriod)
			{
				movement.randomWalkTimer = 0f;
				movement.randomWalkDir = movement.avoidanceOffsetAngle * new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z);
				movement.randomWalkDir.Normalize();
			}
			else
			{
				movement.randomWalkTimer += Time.deltaTime;
			}
			if (movement.randomWalkTimer < movement.randomWalkPeriod)
			{
				my.Rigidbody.AddForce(movement.randomWalkDir * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
		else if (movement.idleDampenTimer > 0.066f && (double)movement.VelocitySqr > 0.1)
		{
			movement.idleDampenTimer -= Time.deltaTime;
			my.Rigidbody.AddForce(-movement.CurrentVelocity * 0.75f, ForceMode.Acceleration);
		}
		else if (!movement.returnToIdle)
		{
			movement.returnToIdle = true;
			my.Rigidbody.velocity = Vector3.zero;
			my.Rigidbody.angularVelocity = Vector3.zero;
			my.Rigidbody.Sleep();
		}
		if (!grounded)
		{
			my.Rigidbody.AddForce(new Vector3(0f - movement.Direction.x, 0f, 0f - movement.Direction.z) * movement.Speed - movement.CurrentVelocity, ForceMode.Force);
			my.Rigidbody.AddForce(Vector3.up * descendForce, ForceMode.Force);
		}
	}

	public override void Stationary()
	{
		if (movement.Able && !my.basicInfo.InWater && !grounded)
		{
			my.Rigidbody.AddForce(new Vector3(0f - movement.Direction.x, 0f, 0f - movement.Direction.z) * movement.Speed - movement.CurrentVelocity, ForceMode.Force);
			my.Rigidbody.AddForce(Vector3.up * descendForce, ForceMode.Force);
		}
	}

	public override void Flee()
	{
		if (movement.Able)
		{
			if (!TargetBlock.gotTarget && my.fireController.onFire && movement.randomWalkDir == Vector3.zero)
			{
				movement.Direction = (movement.randomWalkDir = movement.avoidanceOffsetAngle * new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z));
				movement.Direction.Normalize();
			}
			my.Rigidbody.AddForce(-movement.Direction * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Force);
			my.Rigidbody.AddForce(Vector3.up * ascendForce, ForceMode.Force);
		}
	}

	protected override void Animate()
	{
		if (!isDead && StatMaster.levelSimulating && !StatMaster.GodTools.GravityDisabled && !selfRighting.Grabbed && disposition.myState != EntityState.Ungrounded && disposition.myState != EntityState.Fallen && !selfRighting.Fallen)
		{
			if (TargetBlock.gotTarget && ((looking.Focus != FocusOn.Target && looking.Focus != FocusOn.TargetOpposite) || !(movement.DifferenceToTargetSqr > base.BehavioursMaxDistance)))
			{
				SetFocus();
			}
			if (bob.Able && (bool)my.killingHandler.my.Poser && my.killingHandler.my.Poser.animateWhileMoving && !my.basicInfo.InWater)
			{
				AnimateOnMovement();
			}
		}
	}

	public override bool GroundedCheck()
	{
		if (useKinematicAsGround && my.basicInfo.isKinematic)
		{
			return true;
		}
		if (!movement.inJump)
		{
			if (disposition.myState == EntityState.Grabbed)
			{
				return false;
			}
			if (movement.CurrentVelocity.y < (0f - gcVelocityThreshold) * 2f || movement.CurrentVelocity.y > gcVelocityThreshold)
			{
				return false;
			}
			if (movement.CurrentVelocity.y < 2f && movement.CurrentVelocity.y > -2f && grounded)
			{
				return true;
			}
			if ((grounded && firstGroundTouch) || my.basicInfo.InWater)
			{
				return true;
			}
			CastRay();
		}
		return false;
	}

	protected override void EnterWater()
	{
		if (my.killingHandler.my.Poser != null)
		{
			my.killingHandler.my.Poser.ChangeMesh(disposition.myState);
		}
	}

	public override void Grabbed(MonoBehaviour grabber)
	{
		if (useJointAsGround && (bool)groundJoint)
		{
			Object.Destroy(groundJoint);
			groundJoint = null;
		}
		if (!movement.keepInterpolation)
		{
			my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		selfRighting.Grabbed = true;
	}

	protected override void OnCollisionReact(Collision collision)
	{
		if (my.basicInfo.isKinematic)
		{
			BasicInfo basicInfo = my.basicInfo;
			bool isKinematic = false;
			my.Rigidbody.isKinematic = isKinematic;
			basicInfo.isKinematic = isKinematic;
		}
	}

	public override void Die()
	{
		base.Die();
		if (!StatMaster.isMP && my.killingHandler.activeType == InjuryType.Fire)
		{
			AchievementHelper.Increment(16, 1);
		}
	}
}
