using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[HideInInspector]
	public bool movementIsControlledElseWhere;

	public float drag = 1f;

	public float movememtForce = 1f;

	public float crouchSpeedMutiplier = 0.5f;

	public float springMultiplier = 1.5f;

	public float climbSpeed = 1f;

	public float climbSpeedDecay = 1f;

	public float climbSpeedDropThreshold = 0.1f;

	public float climbNormalDropForce;

	public float climbDropForce;

	public float standForce = 1f;

	public float crouchForce;

	public float jumpForce;

	public float upTime = 0.5f;

	public float gravity = 10f;

	public float scalingGravity;

	public float airControl = 0.4f;

	public float slideControl = 0.2f;

	public float downHillSpeedBonus;

	public float upHillSpeedPenalty;

	public AnimationCurve collisionForceCurve;

	[HideInInspector]
	public Vector3 velocity;

	private CharacterData data;

	public LayerMask mask;

	private Collider capsule;

	private float controlMultiplier = 1f;

	private float timeMultiplier = 0.01666f;

	private Action<RaycastHit> collisionAction;

	private Vector3 lastClimbSpeed;

	private float climbFallAmount;

	private float climbStep;

	private bool climbRight;

	private void Start()
	{
		data = GetComponent<CharacterData>();
		capsule = GetComponentInChildren<CapsuleCollider>(includeInactive: true);
	}

	private void FixedUpdate()
	{
		if (movementIsControlledElseWhere)
		{
			JustMoveCharacter();
		}
		else if (data.isWallClimbing)
		{
			WallClimbMovement();
		}
		else
		{
			GroundAndAirMovement();
		}
		CheckCollision(new Vector3(base.transform.position.x, capsule.bounds.min.y, base.transform.position.z), new Vector3(base.transform.position.x, capsule.bounds.max.y, base.transform.position.z));
	}

	private void JustMoveCharacter()
	{
		base.transform.position += velocity * timeMultiplier;
	}

	private void GroundAndAirMovement()
	{
		if (data.isGrounded)
		{
			velocity += data.standAmount * standForce * Vector3.up;
		}
		Vector3 vector = data.groundNormalTransform.TransformDirection(data.input.normalizedDirection);
		if (vector.y > 0f)
		{
			vector *= 1f - vector.y * upHillSpeedPenalty;
		}
		if (vector.y < 0f)
		{
			vector *= 1f + (0f - vector.y) * downHillSpeedBonus;
		}
		velocity += controlMultiplier * movememtForce * timeMultiplier * vector;
		MovementGameFeel();
		Gravity();
		velocity -= controlMultiplier * drag * timeMultiplier * velocity;
		base.transform.position += velocity * timeMultiplier;
	}

	private void Update()
	{
		CheckCrouch();
		SetControlMultiplier();
		if (data.isWallClimbing && !data.input.spaceIsDown)
		{
			DropClimb();
		}
		if (!movementIsControlledElseWhere && !data.isWallClimbing && (data.isGrounded || (data.jumps > 0 && data.sinceJump > 0.5f)) && data.input.spaceWasPressed)
		{
			Jump();
		}
	}

	private void SetControlMultiplier()
	{
		controlMultiplier = 1f;
		if (!data.isGrounded)
		{
			controlMultiplier = airControl;
		}
		else if (data.isSliding)
		{
			controlMultiplier = slideControl;
		}
	}

	private void CheckCrouch()
	{
	}

	private void Gravity(float gravityMultiplier = 1f)
	{
		if (!data.isGrounded)
		{
			velocity += ((data.sinceGrounded + Mathf.Clamp(data.sinceJump / upTime - 1f, -1f, 0f) * upTime) * scalingGravity + gravity) * gravityMultiplier * timeMultiplier * Vector3.down;
		}
	}

	private void MovementGameFeel()
	{
		data.mediumRotationShake.AddForce((0f - data.camera.InverseTransformDirection(velocity).x) * timeMultiplier * Vector3.up, data.camera.position + data.camera.right);
		data.mediumRotationShake.AddForce((0f - data.camera.InverseTransformDirection(velocity).z) * timeMultiplier * Vector3.up, data.camera.position + data.camera.forward);
	}

	private void Jump()
	{
		velocity = new Vector3(velocity.x, jumpForce, velocity.z);
		data.jumps--;
		data.sinceJump = 0f;
		data.sinceGrounded = 0f;
	}

	public void AddCollisionAction(Action<RaycastHit> action)
	{
		collisionAction = (Action<RaycastHit>)Delegate.Combine(collisionAction, action);
	}

	public void RemoveCollisionAction(Action<RaycastHit> action)
	{
		collisionAction = (Action<RaycastHit>)Delegate.Remove(collisionAction, action);
	}

	private void CheckCollision(Vector3 pos1, Vector3 pos2)
	{
		Collider[] array = Physics.OverlapCapsule(pos1, pos2, 0.5f, mask);
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = (pos1 + pos2) * 0.5f;
			float num = Vector3.Distance(vector, array[i].ClosestPoint(vector));
			Vector3 normalized = (vector - array[i].ClosestPoint(vector)).normalized;
			float num2 = collisionForceCurve.Evaluate(num / 0.5f);
			Vector3 vector2 = normalized * num2;
			Vector3 vector3 = Vector3.Project(velocity, -normalized);
			if (movementIsControlledElseWhere)
			{
				Physics.Raycast(new Ray(vector, -normalized.normalized), out var hitInfo, 1f);
				if ((bool)hitInfo.transform && collisionAction != null)
				{
					collisionAction(hitInfo);
				}
			}
			else if (Mathf.Abs(normalized.y) < 0.3f && !data.isGrounded && data.sinceWallSlide > 0.3f && velocity.y < 4f && Vector3.Angle(data.camera.forward, normalized) > 100f && data.input.spaceIsDown && (data.climbs > 0 || lastClimbSpeed.magnitude > climbSpeedDropThreshold))
			{
				StartWallClimb(normalized, vector3);
			}
			else if (Vector3.Angle(velocity, -normalized) < 90f)
			{
				velocity -= 5f * drag * num2 * timeMultiplier * vector3;
			}
			velocity += 200f * timeMultiplier * vector2;
		}
	}

	private void StartWallClimb(Vector3 directionToAddForce, Vector3 projectedVelocity)
	{
		data.slowPositionShake.AddForce(velocity * 0.05f);
		data.wallClimbNormal = directionToAddForce.normalized;
		velocity -= projectedVelocity;
		data.isWallClimbing = true;
		climbFallAmount = 0f - ((velocity.y < 0f) ? (velocity.y * 1f) : 0f);
		if (data.climbs > 0)
		{
			velocity = Vector3.up * climbSpeed;
		}
		else
		{
			velocity = Vector3.Lerp(lastClimbSpeed, Vector3.zero, data.sinceWallSlide);
		}
		data.climbs--;
	}

	private void WallClimbMovement()
	{
		climbStep += timeMultiplier;
		if (climbStep > 0.2f)
		{
			climbRight = !climbRight;
			climbStep = 0f;
			data.mediumRotationShake.AddForce(velocity.magnitude / climbSpeed * 0.5f * ((climbRight ? 1f : (-1f)) * 0.4f) * data.camera.transform.forward + data.camera.transform.up * 0.7f);
		}
		data.sinceGrounded = 0f;
		if (climbFallAmount > 0f)
		{
			climbFallAmount -= timeMultiplier * 30f;
		}
		else
		{
			climbFallAmount = 0f;
		}
		base.transform.position += velocity * timeMultiplier - climbFallAmount * timeMultiplier * velocity.normalized;
		velocity -= climbSpeedDecay * timeMultiplier * Vector3.up;
		if (velocity.magnitude < climbSpeedDropThreshold)
		{
			DropClimb();
		}
		else
		{
			ClimbingRaycasts();
		}
	}

	private void DropClimb()
	{
		lastClimbSpeed = velocity;
		velocity = Vector3.zero;
		velocity += Vector3.up * climbDropForce;
		velocity += data.wallClimbNormal * climbNormalDropForce;
		data.isWallClimbing = false;
	}

	private void ClimbUp()
	{
		lastClimbSpeed = velocity;
		velocity += Vector3.up * climbDropForce;
		data.isWallClimbing = false;
	}

	private void ClimbingRaycasts()
	{
		Physics.Raycast(new Ray(data.camera.position - Vector3.up * 0.8f, -data.wallClimbNormal.normalized), out var hitInfo, 1.5f);
		if (!hitInfo.transform)
		{
			Physics.Raycast(new Ray(data.camera.position - Vector3.up * 0.8f + Vector3.Cross(data.wallClimbNormal, Vector3.up) * 0.5f, -data.wallClimbNormal.normalized), out hitInfo, 1.5f);
		}
		if (!hitInfo.transform)
		{
			Physics.Raycast(new Ray(data.camera.position - Vector3.up * 0.8f - Vector3.Cross(data.wallClimbNormal, Vector3.up) * 0.5f, -data.wallClimbNormal.normalized), out hitInfo, 1.5f);
		}
		if (!hitInfo.transform)
		{
			Physics.Raycast(new Ray(data.camera.position - Vector3.up * 0.3f, -data.wallClimbNormal.normalized), out hitInfo, 1.5f);
		}
		if ((bool)hitInfo.transform)
		{
			data.wallClimbNormal = hitInfo.normal;
			velocity = -Vector3.Cross(data.wallClimbNormal, Vector3.Cross(data.wallClimbNormal, Vector3.up)).normalized * velocity.magnitude;
		}
		else
		{
			ClimbUp();
		}
	}
}
