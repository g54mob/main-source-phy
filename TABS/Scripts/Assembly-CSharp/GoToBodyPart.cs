using Landfall.TABS;
using UnityEngine;

public class GoToBodyPart : MonoBehaviour
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
		ElbowRight = 10,
		RightWeapon = 11,
		LeftWeapon = 12
	}

	public TargetPart targetPart;

	public bool playOnStart;

	public bool setRotation;

	public bool useCenterOfMass;

	public Vector3 customUnitOffset;

	public Vector3 customUnitRotation;

	public bool keepLocalTransformOffset;

	private bool done;

	private WeaponHandler weaponHandler;

	public void Start()
	{
		weaponHandler = base.transform.root.GetComponentInChildren<WeaponHandler>();
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
		Transform transform = base.transform;
		if (targetPart == TargetPart.Head)
		{
			transform = base.transform.root.GetComponentInChildren<Head>().transform;
		}
		else if (targetPart == TargetPart.Torso)
		{
			transform = base.transform.root.GetComponentInChildren<Torso>().transform;
		}
		else if (targetPart == TargetPart.Hip)
		{
			transform = base.transform.root.GetComponentInChildren<Hip>().transform;
		}
		else if (targetPart == TargetPart.ArmLeft)
		{
			transform = base.transform.root.GetComponentInChildren<ArmLeft>().transform;
		}
		else if (targetPart == TargetPart.ArmRight)
		{
			transform = base.transform.root.GetComponentInChildren<ArmRight>().transform;
		}
		else if (targetPart == TargetPart.ElbowLeft)
		{
			transform = base.transform.root.GetComponentInChildren<HandLeft>().transform;
		}
		else if (targetPart == TargetPart.ElbowRight)
		{
			transform = base.transform.root.GetComponentInChildren<HandRight>().transform;
		}
		else if (targetPart == TargetPart.LegLeft)
		{
			transform = base.transform.root.GetComponentInChildren<LegLeft>().transform;
		}
		else if (targetPart == TargetPart.LegRight)
		{
			transform = base.transform.root.GetComponentInChildren<LegRight>().transform;
		}
		else if (targetPart == TargetPart.KneeLeft)
		{
			transform = base.transform.root.GetComponentInChildren<KneeLeft>().transform;
		}
		else if (targetPart == TargetPart.KneeRight)
		{
			transform = base.transform.root.GetComponentInChildren<KneeRight>().transform;
		}
		else if (targetPart == TargetPart.RightWeapon)
		{
			if ((bool)weaponHandler && (bool)weaponHandler.rightWeapon)
			{
				transform = weaponHandler.rightWeapon.transform;
			}
		}
		else if (targetPart == TargetPart.LeftWeapon && (bool)weaponHandler && (bool)weaponHandler.leftWeapon)
		{
			transform = weaponHandler.leftWeapon.transform;
		}
		if ((bool)transform)
		{
			done = true;
			base.transform.SetParent(transform);
			if (!keepLocalTransformOffset)
			{
				Rigidbody component = transform.GetComponent<Rigidbody>();
				if ((bool)component && useCenterOfMass)
				{
					base.transform.position = component.worldCenterOfMass;
				}
				else
				{
					base.transform.position = transform.position;
				}
				if (setRotation)
				{
					base.transform.rotation = transform.rotation;
				}
			}
		}
		if (GetComponentInParent<ConfigurableJoint>() != null)
		{
			base.transform.localPosition += customUnitOffset;
			base.transform.Rotate(customUnitRotation);
		}
	}

	public void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}
}
