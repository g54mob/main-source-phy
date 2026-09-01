using UnityEngine;

public class PlayerHolding : MonoBehaviour
{
	public enum WeaponTargetType
	{
		Swing = 0,
		Stab = 1
	}

	public FPSWeapon weapon;

	private Transform weaponTarget;

	public WeaponTargetType targetType;

	public Transform swingTarget;

	public Transform stabTarget;

	public Transform leftElbowGoal;

	public Transform rightElbowGoal;

	public FpsHoldingDataPreset[] presets;

	private FpsHoldingData currentHoldinigData;

	public float holdingForce;

	public float holdingTorque;

	private Transform cam;

	[HideInInspector]
	public bool followWeaponTarget;

	private PlayerIKHandler IKHandler;

	private FpsHoldingAnimation fpsHoldingAnim;

	private Vector3 defL;

	private Vector3 defR;

	private Vector3 targetPosLocal;

	private Vector3 targetForwardLocal;

	private Vector3 targetUpLocal;

	private Vector3 targetPos;

	private Vector3 targetForward;

	private Vector3 targetUp;

	public float moveVelForce;

	public float moveVelDeltaForce;

	private CharacterMovement move;

	private Vector3 lastMoveVel;

	private void Start()
	{
		defL = leftElbowGoal.localPosition;
		defR = rightElbowGoal.localPosition;
		cam = GetComponentInChildren<Camera>().transform;
		move = GetComponent<CharacterMovement>();
		fpsHoldingAnim = GetComponentInChildren<FpsHoldingAnimation>();
		IKHandler = GetComponent<PlayerIKHandler>();
		StartHolding(weapon);
	}

	public void StartHolding(FPSWeapon newHoldable)
	{
		weapon = newHoldable.GetComponent<FPSWeapon>();
		if ((bool)weapon)
		{
			weapon.isHeld = true;
			weapon.Init();
			CollisionWeapon component = newHoldable.GetComponent<CollisionWeapon>();
			if ((bool)component)
			{
				component.impactMultiplier *= 1.5f;
			}
			IKHandler.StartHolding(newHoldable);
			weapon.rig.drag = weapon.heldDrag;
			weapon.rig.angularDrag = weapon.heldDrag;
			weapon.rig.useGravity = false;
			weapon.rig.inertiaTensor = Vector3.one;
			weapon.rig.centerOfMass = Vector3.zero;
			weapon.rig.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			weapon.rig.interpolation = RigidbodyInterpolation.Interpolate;
			Collider[] componentsInChildren = weapon.rig.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 26;
			}
			weapon.rig.gameObject.layer = 26;
			if (weapon.leftElbowGoal != Vector3.zero)
			{
				leftElbowGoal.localPosition = weapon.leftElbowGoal;
			}
			else if (weapon.rightElbowGoal != Vector3.zero)
			{
				rightElbowGoal.localPosition = weapon.leftElbowGoal;
			}
			else
			{
				weapon = newHoldable;
			}
			weapon.rig.interpolation = RigidbodyInterpolation.Interpolate;
			SetHoldingData();
			SnapTargets();
		}
	}

	private void FixedUpdate()
	{
		if ((bool)weapon)
		{
			SetWeaponTargets();
			AddMoveForce();
			AddForceTowards();
			AddTorqueTowards();
		}
	}

	private void SetWeaponTargets()
	{
		if (targetType == WeaponTargetType.Stab)
		{
			weaponTarget = stabTarget;
		}
		else if (targetType == WeaponTargetType.Swing)
		{
			weaponTarget = swingTarget;
		}
		if (followWeaponTarget)
		{
			targetPos = weaponTarget.position;
			targetForward = weaponTarget.forward;
			targetUp = weaponTarget.up;
			return;
		}
		float maxDistanceDelta = 3f * Time.deltaTime;
		targetPosLocal = Vector3.MoveTowards(targetPosLocal, currentHoldinigData.holdPos, maxDistanceDelta);
		targetForwardLocal = Vector3.MoveTowards(targetForwardLocal, currentHoldinigData.holdForward, maxDistanceDelta);
		targetUpLocal = Vector3.MoveTowards(targetUpLocal, currentHoldinigData.holdUp, maxDistanceDelta);
		targetPos = cam.TransformPoint(targetPosLocal) + fpsHoldingAnim.GetAnimationOffset();
		targetForward = cam.TransformDirection(targetForwardLocal) + fpsHoldingAnim.GetAnimationOffset() * 5f;
		targetUp = cam.TransformDirection(targetUpLocal) + fpsHoldingAnim.GetAnimationOffset() * 5f;
	}

	public void SetTargetsToHoldable()
	{
		targetPosLocal = cam.InverseTransformPoint(weapon.transform.position);
		targetForwardLocal = cam.InverseTransformDirection(weapon.transform.forward);
		targetUpLocal = cam.InverseTransformDirection(weapon.transform.up);
	}

	public void SetHoldingData(string extension = "")
	{
		if (weapon.customHoldingata)
		{
			currentHoldinigData = weapon.holdingData;
			return;
		}
		if (weapon.holdingPresetName == "")
		{
			currentHoldinigData = presets[0].holdingData;
			return;
		}
		for (int i = 0; i < presets.Length; i++)
		{
			if (i == 0 || presets[i].holdablePresetName == weapon.holdingPresetName + extension)
			{
				currentHoldinigData = presets[i].holdingData;
			}
		}
	}

	private void AddForceTowards()
	{
		weapon.rig.AddForce((targetPos - weapon.transform.position) * holdingForce, ForceMode.VelocityChange);
	}

	private void AddTorqueTowards()
	{
		Vector3 vector = (0f - Vector3.Angle(targetForward, weapon.transform.forward)) * Vector3.Cross(targetForward, weapon.transform.forward);
		vector += 0.25f * (0f - Vector3.Angle(targetUp, weapon.transform.up)) * Vector3.Cross(targetUp, weapon.transform.up);
		weapon.rig.AddTorque(vector * holdingTorque, ForceMode.VelocityChange);
	}

	private void AddMoveForce()
	{
		Vector3 vector = move.velocity - lastMoveVel;
		weapon.rig.AddForce(vector * moveVelDeltaForce, ForceMode.Acceleration);
		weapon.rig.AddForce(move.velocity * moveVelForce, ForceMode.Acceleration);
		lastMoveVel = move.velocity;
	}

	public void SnapTargets()
	{
		targetPos = cam.TransformPoint(currentHoldinigData.holdPos);
		targetForward = cam.TransformDirection(currentHoldinigData.holdForward);
		targetUp = cam.TransformDirection(currentHoldinigData.holdUp);
		targetPos = currentHoldinigData.holdPos;
		targetForward = currentHoldinigData.holdForward;
		targetUp = currentHoldinigData.holdUp;
	}
}
