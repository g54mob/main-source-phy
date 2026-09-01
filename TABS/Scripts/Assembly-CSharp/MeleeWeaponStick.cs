using System.Collections;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeaponStick : CollisionWeaponEffect
{
	public bool hardStick = true;

	public float fixPositionAmount;

	public float breakForce = 20000f;

	public bool onlyOtherTeam;

	private Rigidbody rig;

	public Rigidbody otherRigidbody;

	[HideInInspector]
	public ConfigurableJoint joint;

	private StickPosition stickPosition;

	public bool walkBackwardsWhenStuck;

	private Unit unit;

	private GeneralInput input;

	private DataHandler data;

	public float downwardsForceOnStuckRig;

	public float time = 3f;

	private Holdable holdable;

	public UnityEvent stickEvent;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
		if (onlyOtherTeam)
		{
			unit = GetComponentInParent<Unit>();
		}
		stickPosition = GetComponentInChildren<StickPosition>();
		input = base.transform.root.GetComponentInChildren<GeneralInput>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		holdable = GetComponent<Holdable>();
		if ((bool)holdable)
		{
			holdable.AddWasGrabbedAction(Grab);
		}
	}

	public void Grab()
	{
		if (onlyOtherTeam)
		{
			unit = holdable.holderData.GetComponentInParent<Unit>();
		}
	}

	private void FixedUpdate()
	{
		if ((bool)joint && (bool)joint.connectedBody && (!data || !data.Dead))
		{
			joint.connectedBody.AddForce(Vector3.down * downwardsForceOnStuckRig, ForceMode.Force);
			if (walkBackwardsWhenStuck && (bool)input)
			{
				input.inputDirection = Vector3.forward * -1f;
			}
		}
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		if ((bool)joint)
		{
			return;
		}
		if (onlyOtherTeam)
		{
			Unit componentInParent = collision.transform.GetComponentInParent<Unit>();
			if ((bool)componentInParent && (bool)unit && unit.Team == componentInParent.Team)
			{
				return;
			}
		}
		float sqrMagnitude = (stickPosition.transform.position - collision.GetContact(0).point).sqrMagnitude;
		if ((bool)collision.rigidbody && sqrMagnitude < stickPosition.radius * stickPosition.radius)
		{
			otherRigidbody = collision.rigidbody;
			joint = AttachJoint(rig, collision.rigidbody, collision.GetContact(0).point, fixPositionAmount, stickPosition.transform, hardStick);
			joint.breakForce = breakForce;
			stickEvent.Invoke();
			StartCoroutine(KillJoint(joint, Random.Range(time - 1f, time + 1f)));
		}
	}

	public static ConfigurableJoint AttachJoint(Rigidbody myRig, Rigidbody otherRig, Vector3 hitPos, float fix = 0f, Transform stickPos = null, bool hardStick = false)
	{
		if (fix != 0f)
		{
			otherRig.position = otherRig.transform.position + (stickPos.position - otherRig.position).normalized * fix;
		}
		ConfigurableJoint configurableJoint = myRig.gameObject.AddComponent<ConfigurableJoint>();
		configurableJoint.anchor = myRig.transform.InverseTransformPoint(hitPos);
		configurableJoint.xMotion = ConfigurableJointMotion.Locked;
		configurableJoint.yMotion = ConfigurableJointMotion.Locked;
		configurableJoint.zMotion = ConfigurableJointMotion.Locked;
		if (hardStick)
		{
			configurableJoint.angularXMotion = ConfigurableJointMotion.Locked;
			configurableJoint.angularYMotion = ConfigurableJointMotion.Locked;
			configurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
		}
		else
		{
			configurableJoint.angularXMotion = ConfigurableJointMotion.Free;
			configurableJoint.angularYMotion = ConfigurableJointMotion.Free;
			configurableJoint.angularZMotion = ConfigurableJointMotion.Free;
		}
		if ((bool)otherRig)
		{
			configurableJoint.connectedBody = otherRig;
		}
		configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
		JointDrive angularXDrive = configurableJoint.angularXDrive;
		angularXDrive.positionSpring = 10f;
		angularXDrive.positionDamper = 2f;
		configurableJoint.angularXDrive = angularXDrive;
		configurableJoint.angularYZDrive = angularXDrive;
		configurableJoint.enablePreprocessing = false;
		return configurableJoint;
	}

	public void RemoveStickJoint()
	{
		KillJoint(joint, 0f);
	}

	private IEnumerator KillJoint(ConfigurableJoint joint, float time)
	{
		yield return new WaitForSeconds(time);
		if ((bool)joint)
		{
			Object.Destroy(joint);
		}
	}
}
