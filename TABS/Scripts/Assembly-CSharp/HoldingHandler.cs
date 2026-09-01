using System;
using Landfall.MonoBatch;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

public class HoldingHandler : BatchedMonobehaviour
{
	public enum HandActivity
	{
		NotHolding = 0,
		HoldingRightObject = 1,
		HoldingLeftObject = 2
	}

	public enum HandType
	{
		Right = 0,
		Left = 1
	}

	public delegate void OnHolding(Holdable holdable, HandType handType);

	public Holdable rightObject;

	public Holdable leftObject;

	public HandActivity rightHandActivity;

	public HandActivity leftHandActivity;

	public bool simpleAttach;

	private Rigidbody rightHand;

	private Rigidbody leftHand;

	private Transform rightHandTransform;

	private Transform leftHandTransform;

	private Rigidbody torso;

	private ConfigurableJoint rightHandJoint;

	private ConfigurableJoint leftHandJoint;

	private DataHandler data;

	private Unit unit;

	private float unitSize = 1f;

	private WeaponHandler weaponHandler;

	private Vector3 usedForwardDirection;

	private Vector3 currentForce;

	private Vector3 currentTorque;

	private Vector3 currentTorqueUp;

	protected override void Start()
	{
		base.Start();
		unit = base.transform.root.GetComponent<Unit>();
		weaponHandler = GetComponent<WeaponHandler>();
		data = base.transform.GetComponent<DataHandler>();
		unitSize = Mathf.Clamp(unit.unitBlueprint.sizeMultiplier, 1f, 2f);
		rightHandTransform = GetComponentInChildren<HandRight>().transform;
		leftHandTransform = GetComponentInChildren<HandLeft>().transform;
		rightHand = rightHandTransform.GetComponent<Rigidbody>();
		leftHand = leftHandTransform.GetComponent<Rigidbody>();
		torso = GetComponentInChildren<Torso>().GetComponent<Rigidbody>();
	}

	public override void BatchedFixedUpdate()
	{
		if (!data.Dead)
		{
			CheckHands();
		}
	}

	public void LetGoOfWeapon(GameObject weapon)
	{
		bool flag = (bool)leftObject && weapon == leftObject.gameObject;
		bool flag2 = (bool)rightObject && weapon == rightObject.gameObject;
		if ((!flag || !leftObject.ignoreDissarm) && (!flag2 || !rightObject.ignoreDissarm))
		{
			if ((bool)rightObject && flag2)
			{
				rightObject.WasDropped();
			}
			if ((bool)leftObject && flag)
			{
				leftObject.WasDropped();
			}
			if (flag)
			{
				leftHandActivity = HandActivity.NotHolding;
			}
			if (flag2)
			{
				rightHandActivity = HandActivity.NotHolding;
			}
			if (rightObject != null && flag2)
			{
				rightObject.rig.isKinematic = false;
				rightObject.gameObject.AddComponent<SetInterpolation>();
				rightObject = null;
			}
			if (leftObject != null && flag)
			{
				leftObject.rig.isKinematic = false;
				leftObject.gameObject.AddComponent<SetInterpolation>();
				leftObject = null;
			}
			if ((bool)rightHandJoint && flag2)
			{
				UnityEngine.Object.Destroy(rightHandJoint);
			}
			if ((bool)leftHandJoint && flag)
			{
				UnityEngine.Object.Destroy(leftHandJoint);
			}
			if (!rightObject && !leftObject && (bool)weaponHandler)
			{
				weaponHandler.UseHands();
			}
		}
	}

	public void LetGoOfAll()
	{
		if ((bool)rightObject && !rightObject.ignoreDissarm)
		{
			rightHandActivity = HandActivity.NotHolding;
			rightObject.WasDropped();
		}
		if ((bool)leftObject && !leftObject.ignoreDissarm)
		{
			leftHandActivity = HandActivity.NotHolding;
			leftObject.WasDropped();
		}
		bool flag = false;
		if ((bool)rightObject && rightObject.ignoreDissarm)
		{
			flag = true;
		}
		if ((bool)leftObject && leftObject.ignoreDissarm)
		{
			flag = true;
		}
		if (rightObject != null && !rightObject.ignoreDissarm)
		{
			rightObject.rig.isKinematic = false;
			rightObject.gameObject.AddComponent<SetInterpolation>();
			rightObject = null;
		}
		if (leftObject != null && !leftObject.ignoreDissarm)
		{
			leftObject.rig.isKinematic = false;
			leftObject.gameObject.AddComponent<SetInterpolation>();
			leftObject = null;
		}
		if (!flag)
		{
			if ((bool)leftHandJoint)
			{
				UnityEngine.Object.Destroy(leftHandJoint);
			}
			if ((bool)rightHandJoint)
			{
				UnityEngine.Object.Destroy(rightHandJoint);
			}
			if ((bool)weaponHandler)
			{
				weaponHandler.UseHands();
			}
		}
	}

	public void GrabObject(Holdable holdable, bool left, bool right, Action doneCallBack)
	{
		if (!rightObject)
		{
			leftHand.isKinematic = false;
			rightHand.isKinematic = false;
			rightObject = holdable;
			if (left)
			{
				leftHandActivity = HandActivity.HoldingRightObject;
			}
			else
			{
				leftHandActivity = HandActivity.NotHolding;
			}
			if (right)
			{
				rightHandActivity = HandActivity.HoldingRightObject;
			}
			else
			{
				rightHandActivity = HandActivity.NotHolding;
			}
		}
	}

	public void CancelGrab()
	{
		Debug.Log("Cancel grab!");
		if (!leftHandJoint && !rightHandJoint)
		{
			LetGoOfAll();
		}
	}

	private void CheckHands()
	{
		if ((bool)rightObject)
		{
			if (rightHandActivity == HandActivity.HoldingRightObject)
			{
				if ((bool)rightHandJoint)
				{
					DoPID(rightObject.pidData, rightObject.holdableData);
				}
				else
				{
					ReachForObject(rightObject, HandType.Right);
				}
			}
			if (leftHandActivity == HandActivity.HoldingRightObject && !leftHandJoint)
			{
				ReachForObject(rightObject, HandType.Left);
			}
		}
		if ((bool)leftObject && leftHandActivity == HandActivity.HoldingLeftObject)
		{
			if ((bool)leftHandJoint)
			{
				DoPID(leftObject.pidData, leftObject.holdableData);
			}
			else
			{
				ReachForObject(leftObject, HandType.Left);
			}
		}
	}

	private void SimpleAttach(Holdable holdable, HandType handType)
	{
		if (handType == HandType.Right)
		{
			rightHandJoint = JointActions.AttachJoint(rightHandTransform.GetComponentInParent<Rigidbody>(), holdable, rightHandTransform.position, rightHandTransform.rotation, simpleAttach: true);
		}
		if (handType == HandType.Left)
		{
			leftHandJoint = JointActions.AttachJoint(leftHandTransform.GetComponentInParent<Rigidbody>(), holdable, leftHandTransform.position, leftHandTransform.rotation, simpleAttach: true);
		}
		holdable.WasGrabbed(data, (handType == HandType.Left) ? leftHandTransform : rightHandTransform);
	}

	private void ReachForObject(Holdable holdable, HandType handType)
	{
		Rigidbody rigidbody = ((handType == HandType.Right) ? rightHand : leftHand);
		if (simpleAttach)
		{
			SimpleAttach(holdable, handType);
			return;
		}
		holdable.rig.isKinematic = true;
		if (holdable.holdableData.snapConnect)
		{
			Transform obj = holdable.transform;
			Transform transform = rigidbody.transform;
			obj.SetPositionAndRotation(transform.position, transform.rotation);
		}
		else
		{
			holdable.rig.position = torso.transform.position + (0.5f + holdable.holdableData.extraGrabDistance) * (0.5f * unitSize) * torso.transform.forward + holdable.holdableData.relativePosition.x * 0.5f * unitSize * holdable.transform.right;
			if (holdable.holdableData.setRotation)
			{
				holdable.rig.rotation = Quaternion.LookRotation(torso.transform.TransformDirection(holdable.holdableData.forwardRotation), torso.transform.TransformDirection(holdable.holdableData.upRotation));
			}
		}
		rigidbody.GetComponentInChildren<Collider>().enabled = false;
		Vector3 vector = holdable.transform.TransformPoint((handType == HandType.Right) ? holdable.holdableData.rightHand.localPosition : holdable.holdableData.leftHand.localPosition);
		if (Vector3.Distance(vector, rigidbody.position) > 15f)
		{
			return;
		}
		Vector3 vector2 = vector - rigidbody.position;
		rigidbody.AddForceAtPosition(FixedTimeStepService.SmallForceCoefficient * vector2.normalized * 100f, rigidbody.position, ForceMode.Acceleration);
		Vector3 vector3 = holdable.transform.position - torso.position;
		vector3.y *= 3f;
		torso.AddForce(FixedTimeStepService.SmallForceCoefficient * vector3.normalized * 25f, ForceMode.Acceleration);
		if (Vector3.Distance(rigidbody.position, vector) < 0.4f)
		{
			if (handType == HandType.Right)
			{
				rightHandJoint = JointActions.AttachJoint(rigidbody, holdable, vector, holdable.holdableData.rightHand.rotation);
			}
			if (handType == HandType.Left)
			{
				leftHandJoint = JointActions.AttachJoint(rigidbody, holdable, vector, holdable.holdableData.leftHand.rotation);
			}
			if (float.IsNaN(rigidbody.velocity.x))
			{
				Debug.LogError("VELOCITY NaN", rigidbody.gameObject);
				rigidbody.velocity = Vector3.zero;
			}
			if (float.IsNaN(holdable.rig.velocity.x))
			{
				Debug.LogError("VELOCITY NaN", holdable.rig.gameObject);
				holdable.rig.velocity = Vector3.zero;
			}
			rigidbody.velocity *= 0f;
			rigidbody.angularVelocity *= 0f;
			holdable.rig.velocity *= 0f;
			holdable.rig.angularVelocity *= 0f;
			holdable.WasGrabbed(data, rigidbody.transform);
		}
	}

	private void DoPID(PidDataInstance pidData, HoldableDataInstance holdableData, float multiplier = 1f)
	{
		AddForce(pidData, data.torso.TransformPoint(holdableData.relativePosition), multiplier * data.muscleControl * pidData.holdingForceMultiplier);
		usedForwardDirection = holdableData.forwardRotation;
		if (pidData.extraUpPerMeter != 0f)
		{
			usedForwardDirection += Vector3.up * data.distanceToTarget;
		}
		AddTorque(pidData, data.characterForwardObject.TransformDirection(usedForwardDirection), multiplier * data.muscleControl * pidData.holdingTorqueMultiplier);
		AddTorqueUp(pidData, data.characterForwardObject.TransformDirection(holdableData.upRotation), multiplier * data.muscleControl * pidData.holdingTorqueMultiplier);
	}

	public void AddForce(PidDataInstance pidData, Vector3 targetPosition, float multiplier = 1f)
	{
		Vector3 vector = targetPosition - pidData.rig.position;
		currentForce = vector * pidData.proportionalForce;
		pidData.rig.AddForce(FixedTimeStepService.SmallForceCoefficient * 60f * multiplier * Time.fixedDeltaTime * currentForce, ForceMode.Acceleration);
	}

	public void AddTorque(PidDataInstance pidData, Vector3 targetRotation, float multiplier = 1f)
	{
		Vector3 forward = pidData.rig.transform.forward;
		Vector3 vector = Vector3.Angle(targetRotation, forward) * Vector3.Cross(targetRotation, forward).normalized;
		if (pidData.capAngle != 0f)
		{
			vector = Vector3.ClampMagnitude(vector, pidData.capAngle);
		}
		currentTorque = vector * pidData.proportionalTorque;
		pidData.rig.AddTorque((0f - FixedTimeStepService.TorqueCoefficient) * multiplier * Time.fixedDeltaTime * 60f * currentTorque, ForceMode.Acceleration);
	}

	public void AddTorqueUp(PidDataInstance pidData, Vector3 targetRotation, float multiplier = 1f)
	{
		Vector3 up = pidData.rig.transform.up;
		Vector3 vector = Vector3.Angle(targetRotation, up) * Vector3.Cross(targetRotation, up).normalized;
		if (pidData.capAngle != 0f)
		{
			vector = Vector3.ClampMagnitude(vector, pidData.capAngle);
		}
		currentTorqueUp = vector * pidData.proportionalTorque;
		pidData.rig.AddTorque((0f - FixedTimeStepService.TorqueCoefficient) * multiplier * Time.fixedDeltaTime * 60f * currentTorqueUp, ForceMode.Acceleration);
	}
}
