using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBlockKinematic : SimBehaviour
{
	[HideInInspector]
	public bool hideVisuals;

	[HideInInspector]
	public bool allowMultiPin;

	public BlockVisualController VisualController;

	public Renderer vis;

	public Transform pinTransform;

	public Vector3 startScale;

	public float wobbleAmount = 0.5f;

	public float wobbleDuration = 0.5f;

	public Rigidbody parentRigidbody;

	public float fadeDuration = 0.5f;

	public float fadeTransparency = 0.3f;

	public SphereCollider myCollider;

	public Rigidbody myBody;

	public float simRadius = 0.6f;

	private HashSet<PinLock> pinLocks = new HashSet<PinLock>();

	private float initialRadius;

	private LayerMask layermask;

	private Collider[] hitColliders;

	protected override void Awake()
	{
		layermask = AddPiece.CreateLayerMask(new int[5] { 0, 12, 14, 25, 26 });
		base.Awake();
		if (!basicInfo.stripped)
		{
			initialRadius = myCollider.radius;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (base.SimPhysics && !basicInfo.stripped)
		{
			myCollider.radius = ((!base.isSimulating) ? initialRadius : simRadius);
		}
		if (!base.isSimulating)
		{
			vis.transform.parent.up = Vector3.up;
			startScale = pinTransform.localScale;
			StartCoroutine(WobblePinVis());
		}
		else
		{
			pinTransform.localScale = startScale;
			StartCoroutine(LerpTransparency());
			CheckTriggers();
		}
	}

	public void CheckTriggers()
	{
		if (!base.SimPhysics || basicInfo.stripped)
		{
			return;
		}
		float radius = ((!allowMultiPin) ? myCollider.radius : (myCollider.radius * Mathf.Max(base.transform.parent.localScale.x, base.transform.parent.localScale.y, base.transform.parent.localScale.z)));
		hitColliders = Physics.OverlapSphere(base.transform.position, radius, layermask);
		if (!allowMultiPin)
		{
			Array.Sort(hitColliders, CompareColliderByDistance);
		}
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i] == null)
			{
				continue;
			}
			Rigidbody attachedRigidbody = hitColliders[i].attachedRigidbody;
			if (attachedRigidbody == null)
			{
				return;
			}
			if ((!hitColliders[i].isTrigger || !(attachedRigidbody.name != "BuildSurface")) && attachedRigidbody != parentRigidbody && (bool)attachedRigidbody.GetComponent<BlockBehaviour>())
			{
				CreatePinLock(attachedRigidbody);
				if (!allowMultiPin)
				{
					break;
				}
			}
		}
		if (pinLocks.Count > 0)
		{
			DestroyComponents();
		}
		hitColliders = null;
	}

	public void OnDisable()
	{
		if (!base.isSimulating)
		{
			pinTransform.localScale = startScale;
			StopAllCoroutines();
		}
	}

	protected void OnDestroy()
	{
		foreach (PinLock pinLock in pinLocks)
		{
			if (pinLock != null)
			{
				pinLock.gameObject.tag = "Untagged";
				UnityEngine.Object.Destroy(pinLock);
			}
		}
	}

	public void CreatePinLock(Rigidbody otherBody)
	{
		if (!otherBody.gameObject.CompareTag("StayKinematic"))
		{
			PinLock pinLock = otherBody.gameObject.AddComponent<PinLock>();
			pinLock.gameObject.tag = "StayKinematic";
			pinLock.myRigidbody = otherBody;
			pinLock.pinBlock = this;
			pinLocks.Add(pinLock);
		}
	}

	public void Release()
	{
		bool flag = false;
		foreach (PinLock pinLock in pinLocks)
		{
			if (!(pinLock != null))
			{
				continue;
			}
			pinLock.Release();
			if (pinLock.myRigidbody != null)
			{
				BasicInfo component = pinLock.myRigidbody.GetComponent<BasicInfo>();
				if (!object.ReferenceEquals(component, null))
				{
					if (component.infoType == BasicInfo.BasicInfoType.Block)
					{
						(component as BlockBehaviour).SetNonJoining();
					}
					component.isKinematic = false;
				}
			}
			flag = true;
		}
		if (!flag && this != null && base.transform != null)
		{
			flag = true;
		}
		if (flag)
		{
			base.ParentMachine.RemoveSimBlock(basicInfo as BlockBehaviour, base.SimPhysics);
		}
	}

	private IEnumerator WobblePinVis()
	{
		float i = 0f;
		float rate = 1f / wobbleDuration;
		while (i < 1f)
		{
			i += Time.deltaTime * rate;
			pinTransform.localScale = startScale * (1f + Mathfx.SmoothBounce(i) * wobbleAmount);
			yield return null;
		}
		pinTransform.localScale = startScale;
	}

	private void DestroyComponents()
	{
		if (!basicInfo.stripped)
		{
			UnityEngine.Object.Destroy(myBody);
			UnityEngine.Object.Destroy(myCollider);
		}
	}

	private IEnumerator LerpTransparency()
	{
		float i = 0f;
		float rate = 1f / fadeDuration;
		if (hideVisuals)
		{
			fadeTransparency = 0f;
		}
		while (i < 1f)
		{
			i += Time.deltaTime * rate;
			if (VisualController != null)
			{
				VisualController.SetTransparency(Mathf.Lerp(1f, fadeTransparency, i));
			}
			yield return null;
		}
		if (base.SimPhysics)
		{
			yield return new WaitForFixedUpdate();
			if (pinLocks.Count == 0)
			{
				(basicInfo as BlockBehaviour).RemoveSimBlock(true);
			}
		}
	}

	private int CompareColliderByDistance(Collider x, Collider y)
	{
		if (x == null)
		{
			if (y == null)
			{
				return 0;
			}
			return -1;
		}
		if (y == null)
		{
			return 1;
		}
		float sqrMagnitude = (base.transform.position - x.transform.position).sqrMagnitude;
		float sqrMagnitude2 = (base.transform.position - y.transform.position).sqrMagnitude;
		if (sqrMagnitude > sqrMagnitude2)
		{
			return 1;
		}
		if (sqrMagnitude == sqrMagnitude2)
		{
			return 0;
		}
		return -1;
	}
}
