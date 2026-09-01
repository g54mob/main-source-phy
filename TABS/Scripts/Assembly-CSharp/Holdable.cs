using System;
using System.Collections;
using Landfall.MonoBatch;
using UnityEngine;
using UnityEngine.Events;

public class Holdable : BatchedMonobehaviour
{
	public Collider col;

	public HoldableDataInstance holdableData;

	public PidDataInstance pidData;

	public JointData jointData;

	[HideInInspector]
	public Rigidbody rig;

	public bool held;

	public bool ignoreDissarm;

	public bool useAlternaticeForIceGiant;

	public Vector3 iceGiantRelativePosition = new Vector3(0f, 0.1f, 0.2f);

	public DataHandler holderData;

	public AnimationCurve holdingVariationCurve;

	public UnityEvent dropEvent;

	private Action WasDroppedAction;

	private HoldableDataInstance defaultData;

	[HideInInspector]
	public MoveToHandsWhenGrabbed moveTo;

	[HideInInspector]
	public HoldingHandler holdingHandler;

	[HideInInspector]
	public HandRight hr;

	[HideInInspector]
	public HandLeft hl;

	private HandRightAlternative hra;

	private HandLeftAlternative hla;

	private Action WasGrabbedAction;

	private Vector3 lastLocalPos = Vector3.one * 1337f;

	private void Awake()
	{
		hr = GetComponentInChildren<HandRight>(includeInactive: true);
		if ((bool)hr)
		{
			holdableData.rightHand = hr.transform;
		}
		hl = GetComponentInChildren<HandLeft>(includeInactive: true);
		if ((bool)hl)
		{
			holdableData.leftHand = hl.transform;
		}
		else if ((bool)hr)
		{
			holdableData.leftHand = hr.transform;
		}
		else
		{
			holdableData.rightHand = base.transform;
			holdableData.leftHand = base.transform;
		}
		hra = GetComponentInChildren<HandRightAlternative>(includeInactive: true);
		hla = GetComponentInChildren<HandLeftAlternative>(includeInactive: true);
		if ((bool)holdableData.rightHand)
		{
			holdableData.rightHand.gameObject.SetActive(value: false);
		}
		if ((bool)holdableData.leftHand)
		{
			holdableData.leftHand.gameObject.SetActive(value: false);
		}
		if ((bool)hra)
		{
			hra.gameObject.SetActive(value: false);
		}
		if ((bool)hla)
		{
			hla.gameObject.SetActive(value: false);
		}
		col = GetComponentInChildren<Collider>();
		if ((bool)col)
		{
			_ = col.bounds.size.magnitude;
		}
		rig = GetComponent<Rigidbody>();
		pidData.rig = rig;
		defaultData = holdableData;
	}

	protected override void Start()
	{
		base.Start();
		holdableData.forwardRotation += UnityEngine.Random.insideUnitSphere * holdingVariationCurve.Evaluate(UnityEngine.Random.value);
		holdableData.upRotation += UnityEngine.Random.insideUnitSphere * holdingVariationCurve.Evaluate(UnityEngine.Random.value);
		holdableData.relativePosition += UnityEngine.Random.insideUnitSphere * holdingVariationCurve.Evaluate(UnityEngine.Random.value);
		if ((bool)hra && (bool)holdingHandler && holdingHandler.leftHandActivity == HoldingHandler.HandActivity.HoldingLeftObject)
		{
			holdableData.rightHand = hra.transform;
		}
		if ((bool)hla && (bool)holdingHandler && holdingHandler.leftHandActivity == HoldingHandler.HandActivity.HoldingLeftObject)
		{
			holdableData.leftHand = hla.transform;
		}
		rig.maxAngularVelocity = 250f;
	}

	public void AddWasGrabbedAction(Action action)
	{
		WasGrabbedAction = (Action)Delegate.Combine(WasGrabbedAction, action);
	}

	public void WasGrabbed(DataHandler holder, Transform handTransform = null)
	{
		held = true;
		holderData = holder;
		if ((bool)moveTo && (bool)handTransform)
		{
			moveTo.transform.SetParent(handTransform, worldPositionStays: true);
		}
		if (WasGrabbedAction != null)
		{
			WasGrabbedAction();
		}
	}

	public void WasDropped()
	{
		held = false;
		rig.drag = 0f;
		rig.angularDrag = 0f;
		if (WasDroppedAction != null)
		{
			WasDroppedAction();
		}
		dropEvent.Invoke();
		Collider[] componentsInChildren = GetComponentsInChildren<Collider>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].sharedMaterial = null;
		}
	}

	public void AddWasDroppedAction(Action action)
	{
		WasDroppedAction = (Action)Delegate.Combine(WasDroppedAction, action);
	}

	public void Dissarm()
	{
		if (!ignoreDissarm && held)
		{
			holderData.GetComponent<HoldingHandler>().LetGoOfWeapon(base.gameObject);
		}
	}

	public void LetGoOfall()
	{
		if (!ignoreDissarm && held)
		{
			holderData.GetComponent<HoldingHandler>().LetGoOfAll();
		}
	}

	public void ReduceHoldable(float swingTime)
	{
		StopAllCoroutines();
		StartCoroutine(ReduceHoldingFactor(swingTime));
	}

	private IEnumerator ReduceHoldingFactor(float swingTime)
	{
		if (pidData.disableForceDuringSwing)
		{
			pidData.holdingForceMultiplier = 0f;
		}
		if (pidData.disableTorqueDuringSwing)
		{
			pidData.holdingTorqueMultiplier = 0f;
		}
		yield return new WaitForSeconds(swingTime);
		float time = 0.5f;
		while (time > 0f)
		{
			if (pidData.disableForceDuringSwing)
			{
				pidData.holdingForceMultiplier = Mathf.Lerp(1f, 0f, time / 0.5f);
			}
			if (pidData.disableTorqueDuringSwing)
			{
				pidData.holdingTorqueMultiplier = Mathf.Lerp(1f, 0f, time / 0.5f);
			}
			time -= Time.deltaTime * 0.7f;
			yield return null;
		}
		if (pidData.disableForceDuringSwing)
		{
			pidData.holdingForceMultiplier = 1f;
		}
		if (pidData.disableTorqueDuringSwing)
		{
			pidData.holdingTorqueMultiplier = 1f;
		}
	}

	public override void BatchedUpdate()
	{
		if (holdableData.lookAtTargetWhenWithin != 0f && (bool)holderData && (bool)holderData.targetMainRig)
		{
			if (holderData.distanceToTarget < holdableData.lookAtTargetWhenWithin)
			{
				holdableData.forwardRotation = holderData.mainRig.transform.InverseTransformDirection((holderData.targetMainRig.position - base.transform.position).normalized);
			}
			else
			{
				holdableData.forwardRotation = defaultData.forwardRotation;
			}
		}
	}

	private void SnapToClosestHead()
	{
		Vector3 vector = base.transform.right * holdableData.relativePosition.x + base.transform.up * holdableData.relativePosition.y + base.transform.forward * holdableData.relativePosition.z - Vector3.up * 0.44f;
		float num = float.PositiveInfinity;
		Head[] array = UnityEngine.Object.FindObjectsOfType<Head>();
		Transform transform = null;
		for (int i = 0; i < array.Length; i++)
		{
			float num2 = Vector3.Distance(base.transform.position - vector, array[i].transform.position);
			if (num2 < num)
			{
				transform = array[i].transform;
				num = num2;
			}
		}
		if (transform != null)
		{
			base.transform.position = transform.position + vector;
			transform.transform.root.GetComponentInChildren<HoldingHandler>().rightObject = this;
		}
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
		{
			if (lastLocalPos == Vector3.one * 1337f)
			{
				lastLocalPos = holdableData.relativePosition;
			}
			Vector3 vector = holdableData.relativePosition - lastLocalPos;
			base.transform.position += base.transform.right * vector.x + base.transform.up * vector.y + base.transform.forward * vector.z;
			lastLocalPos = holdableData.relativePosition;
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(base.transform.TransformPoint(new Vector3((0f - holdableData.relativePosition.x) / base.transform.localScale.x, (0f - holdableData.relativePosition.y) / base.transform.localScale.y, (0f - holdableData.relativePosition.z) / base.transform.localScale.z)) + Vector3.up * 0.44f, new Vector3(0.3f, 0.3f, 0.3f));
		}
	}
}
