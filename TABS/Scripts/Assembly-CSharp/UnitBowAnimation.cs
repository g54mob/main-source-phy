using UnityEngine;
using UnityEngine.Events;

public class UnitBowAnimation : MonoBehaviour
{
	public bool drawOnStart;

	[HideInInspector]
	public bool drawn;

	public bool callEvents = true;

	public UnityEvent DrawEvent;

	[HideInInspector]
	public bool stopAim;

	private bool hasRightHand;

	private DataHandler data;

	private Transform target;

	private Transform startPos;

	private Transform originalParent;

	private ConfigurableJoint rightHandJoint;

	private Rigidbody handToCheck;

	private HealthHandler healthHandler;

	private RangeWeapon bow;

	private ProjectileRotation arrowAim;

	private ConfigurableJoint[] handjoints;

	private void Start()
	{
		if (drawOnStart)
		{
			GoToHand(callEvents);
		}
	}

	public void GoToHand(bool callEvents)
	{
		if (!hasRightHand)
		{
			bow = base.transform.GetComponentInParent<RangeWeapon>();
			if (bow == null)
			{
				return;
			}
			startPos = bow.transform.GetComponentInChildren<StringStartPos>().transform;
			originalParent = base.transform.parent;
			target = base.transform.root.GetComponentInChildren<HandRight>().transform;
			arrowAim = bow.GetComponentInChildren<ProjectileRotation>();
			healthHandler = base.transform.root.GetComponentInChildren<HealthHandler>();
			if ((bool)healthHandler)
			{
				healthHandler.AddDieAction(ResetPos);
			}
			handjoints = bow.transform.GetComponents<ConfigurableJoint>();
			for (int i = 0; i < handjoints.Length; i++)
			{
				if (handjoints.Length > 1)
				{
					handToCheck = handjoints[i].connectedBody;
					if ((bool)handToCheck && (bool)handToCheck.transform.GetComponent<HandRight>())
					{
						rightHandJoint = handjoints[i];
						hasRightHand = true;
					}
				}
			}
		}
		if (drawn || !hasRightHand)
		{
			return;
		}
		if ((bool)rightHandJoint)
		{
			rightHandJoint.zMotion = ConfigurableJointMotion.Free;
		}
		if ((bool)target)
		{
			base.transform.SetParent(target);
			if (callEvents)
			{
				DrawEvent.Invoke();
			}
		}
		drawn = true;
	}

	public void GoToStartPos()
	{
		if (drawn)
		{
			if ((bool)rightHandJoint)
			{
				rightHandJoint.zMotion = ConfigurableJointMotion.Locked;
			}
			base.transform.SetParent(originalParent);
			base.transform.localPosition = startPos.localPosition;
			base.transform.localRotation = startPos.localRotation;
			base.transform.localScale = startPos.localScale;
			drawn = false;
			arrowAim.ResetArrowRot();
		}
	}

	public void ResetPos()
	{
		base.transform.SetParent(originalParent);
		base.transform.localPosition = startPos.localPosition;
		base.transform.localRotation = startPos.localRotation;
		if ((bool)rightHandJoint)
		{
			rightHandJoint.zMotion = ConfigurableJointMotion.Locked;
		}
		stopAim = true;
		drawn = false;
	}
}
