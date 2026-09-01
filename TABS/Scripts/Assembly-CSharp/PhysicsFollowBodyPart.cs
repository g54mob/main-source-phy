using Landfall.TABS;
using UnityEngine;

public class PhysicsFollowBodyPart : MonoBehaviour
{
	public enum TargetPart
	{
		Head = 0,
		Torso = 1,
		Hip = 2,
		LegLeft = 3,
		LegRight = 4,
		KneeLeft = 5,
		KneeRight = 6,
		ArmLeft = 7,
		ArmRight = 8,
		ElbowLeft = 9,
		ElbowRight = 10
	}

	public float force;

	public float angularForce;

	public float drag = 0.8f;

	public Vector3 offset;

	public TargetPart targetPart;

	public bool playOnStart;

	public bool setRotation;

	public bool useCenterOfMass;

	private bool done;

	[HideInInspector]
	public Transform target;

	private Rigidbody rig;

	private Vector3 startPos;

	private Vector3 startUp;

	public void Start()
	{
		rig = GetComponent<Rigidbody>();
		startPos += offset;
		if (playOnStart)
		{
			GoToPart();
		}
	}

	public void GoToPart()
	{
		if (done || !base.transform.root.GetComponent<Unit>())
		{
			return;
		}
		done = true;
		target = base.transform;
		if (targetPart == TargetPart.Head)
		{
			target = base.transform.root.GetComponentInChildren<Head>().transform;
		}
		else if (targetPart == TargetPart.Torso)
		{
			target = base.transform.root.GetComponentInChildren<Torso>().transform;
		}
		else if (targetPart == TargetPart.Hip)
		{
			target = base.transform.root.GetComponentInChildren<Hip>().transform;
		}
		else if (targetPart == TargetPart.ArmLeft)
		{
			target = base.transform.root.GetComponentInChildren<ArmLeft>().transform;
		}
		else if (targetPart == TargetPart.ArmRight)
		{
			target = base.transform.root.GetComponentInChildren<ArmRight>().transform;
		}
		else if (targetPart == TargetPart.ElbowLeft)
		{
			target = base.transform.root.GetComponentInChildren<HandLeft>().transform;
		}
		else if (targetPart == TargetPart.ElbowRight)
		{
			target = base.transform.root.GetComponentInChildren<HandRight>().transform;
		}
		else if (targetPart == TargetPart.LegLeft)
		{
			target = base.transform.root.GetComponentInChildren<LegLeft>().transform;
		}
		else if (targetPart == TargetPart.LegRight)
		{
			target = base.transform.root.GetComponentInChildren<LegRight>().transform;
		}
		else if (targetPart == TargetPart.KneeLeft)
		{
			target = base.transform.root.GetComponentInChildren<KneeLeft>().transform;
		}
		else if (targetPart == TargetPart.KneeRight)
		{
			target = base.transform.root.GetComponentInChildren<KneeRight>().transform;
		}
		if ((bool)target)
		{
			Rigidbody component = target.GetComponent<Rigidbody>();
			if ((bool)component && useCenterOfMass)
			{
				base.transform.position = component.worldCenterOfMass;
			}
			else
			{
				base.transform.position = target.position;
			}
			if (setRotation)
			{
				base.transform.rotation = target.rotation;
			}
		}
	}

	private void FixedUpdate()
	{
		if ((bool)target)
		{
			rig.AddForce(force * (target.TransformPoint(startPos) - base.transform.position), ForceMode.Acceleration);
			rig.velocity *= drag;
			rig.angularVelocity *= drag;
			rig.AddTorque(angularForce * Vector3.Angle(base.transform.forward, target.forward) * Vector3.Cross(base.transform.forward, target.forward).normalized, ForceMode.Acceleration);
			rig.AddTorque(0.2f * angularForce * Vector3.Angle(base.transform.up, Vector3.up) * Vector3.Cross(base.transform.up, Vector3.up).normalized, ForceMode.Acceleration);
		}
	}
}
