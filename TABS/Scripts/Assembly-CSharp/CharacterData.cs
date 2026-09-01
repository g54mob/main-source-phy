using UnityEngine;

public class CharacterData : MonoBehaviour
{
	public AnimationCurve standForceCurve;

	public float crouchSize = 0.5f;

	public RotationShake screenShake;

	public RotationShake mediumRotationShake;

	public RotationShake slowRotationShake;

	public PositionShake slowPositionShake;

	public float sinceJump;

	public Transform camera;

	public Rigidbody groundRig;

	public Vector3 localGroundPos;

	public Vector3 localGroundPosDelta;

	public Vector3 groundNormal;

	public float distanceToGround;

	public float standAmount;

	public PlayerInput input;

	public bool isGrounded;

	private bool wasGroundedLastFrame;

	public float sinceGrounded;

	public int jumps;

	public int defaultJumps = 1;

	public int climbs;

	public int defaultClimbs = 1;

	public CharacterMovement move;

	public bool isCrouching;

	public bool isSliding;

	public bool isWallClimbing;

	public Vector3 wallClimbNormal = Vector3.zero;

	public Transform groundNormalTransform;

	public float landedWithForce;

	public float sinceWallSlide;

	public bool isADS;

	private void Start()
	{
		camera = GetComponentInChildren<Camera>().transform;
		input = GetComponentInChildren<PlayerInput>();
		move = GetComponent<CharacterMovement>();
		screenShake = GetComponentInChildren<RotationShake>();
		slowPositionShake = GetComponentInChildren<PositionShake>();
		groundNormalTransform = GetComponentInChildren<GroundNormalObject>().transform;
	}

	private void FixedUpdate()
	{
		sinceJump += Time.fixedDeltaTime;
		if (!wasGroundedLastFrame)
		{
			isGrounded = false;
			sinceGrounded += Time.fixedDeltaTime;
			ResetPositions();
		}
		wasGroundedLastFrame = false;
		if (!isGrounded)
		{
			groundNormal = Vector3.up;
			SetGroundNormalObject();
		}
		if (!isWallClimbing)
		{
			sinceWallSlide += Time.deltaTime;
		}
		else
		{
			sinceWallSlide = 0f;
		}
	}

	public void TouchGround(RaycastHit hit)
	{
		TouchGround(hit.normal, hit.point, hit.rigidbody, hit.distance);
	}

	public void TouchGround(Vector3 normal, Vector3 point, Rigidbody rig, float distance)
	{
		if (sinceJump < 0.25f)
		{
			return;
		}
		standAmount = standForceCurve.Evaluate(distance + (isCrouching ? crouchSize : 0f));
		landedWithForce = 0f;
		if (sinceGrounded > 0.3f)
		{
			float num = Mathf.Clamp(1f * sinceGrounded, 0f, 2f);
			slowRotationShake.AddForce(Vector3.down * num, camera.position + camera.forward);
			landedWithForce = num;
		}
		isGrounded = true;
		wasGroundedLastFrame = true;
		sinceGrounded = 0f;
		groundNormal = normal;
		distanceToGround = distance;
		isWallClimbing = false;
		jumps = defaultJumps;
		climbs = defaultClimbs;
		if (rig != groundRig)
		{
			ResetPositions();
		}
		if ((bool)rig)
		{
			if ((bool)groundRig)
			{
				localGroundPosDelta = groundRig.transform.InverseTransformPoint(point) - localGroundPos;
			}
			groundRig = rig;
			localGroundPos = groundRig.transform.InverseTransformPoint(point);
		}
		else
		{
			ResetPositions();
		}
		SetGroundNormalObject();
	}

	private void SetGroundNormalObject()
	{
		Vector3 forward = Vector3.Cross(camera.transform.right, groundNormal);
		groundNormalTransform.transform.rotation = Quaternion.LookRotation(forward, groundNormal);
	}

	private void ResetPositions()
	{
		groundRig = null;
		localGroundPos = Vector3.zero;
		localGroundPosDelta = Vector3.zero;
		SetGroundNormalObject();
	}
}
