using System;
using UnityEngine;

public class DragRigidbody : MonoBehaviour
{
	public float spring = 50f;

	public float damper = 5f;

	public float drag = 10f;

	public float angularDrag = 5f;

	public float distance = 0.2f;

	public bool attachToCenterOfMass;

	public Rigidbody springBody;

	public SpringJoint springJoint;

	private Rigidbody extraBody;

	private FixedJoint extraJoint;

	public bool breakOnForce;

	private Camera mainCamera;

	private Ray ray;

	private RaycastHit hit;

	private float oldDrag;

	private float oldAngularDrag;

	private Vector3 anchor;

	private Vector3 hitPointClamped;

	private LayerMask mask;

	private Rigidbody lastBody;

	private float lastDistance;

	private void Start()
	{
		mainCamera = Camera.main;
		spring *= 10f;
		damper *= 10f;
		mask = AddPiece.CreateLayerMask(new int[13]
		{
			0, 10, 12, 14, 15, 16, 17, 18, 24, 25,
			26, 28, 29
		});
		MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Combine(MouseOrbit.CameraMoved, new Action<Vector3>(UpdateDrag));
	}

	private void OnDestroy()
	{
		MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Remove(MouseOrbit.CameraMoved, new Action<Vector3>(UpdateDrag));
	}

	private void FixedUpdate()
	{
		if (InputManager.LeftMouseButtonReleased() && (bool)lastBody)
		{
			BasicInfo component = lastBody.GetComponent<BasicInfo>();
			if ((bool)component && component.hasAiScript)
			{
				component.aiEntity.StopBeingGrabbed();
			}
			lastBody = null;
		}
		if (!StatMaster.levelSimulating)
		{
			if ((bool)extraJoint)
			{
				Rigidbody connectedBody = extraJoint.connectedBody;
				if (connectedBody != null)
				{
					lastDistance = 0f;
					connectedBody.WakeUp();
					extraJoint.connectedBody = null;
					springJoint.connectedBody = null;
				}
			}
		}
		else
		{
			DragObject();
		}
	}

	public bool hasJoint()
	{
		return (bool)extraJoint && (bool)extraJoint.connectedBody;
	}

	private void UpdateDrag(Vector3 mainCamPos)
	{
		if (!StatMaster.levelSimulating || !StatMaster.GodTools.DragMode)
		{
			return;
		}
		if (!InputManager.LeftMouseButton())
		{
			if (lastDistance != 0f && !SingleInstanceFindOnly<MouseOrbit>.Instance.MouseControlUsed())
			{
				Vector3 position = mainCamera.WorldToScreenPoint(extraBody.transform.position);
				position.z = mainCamera.nearClipPlane;
				position = mainCamera.ScreenToWorldPoint(position);
				float b = Vector3.Distance(position, extraBody.transform.position);
				lastDistance = Mathf.Lerp(lastDistance, b, Time.fixedDeltaTime);
			}
			return;
		}
		int num = 0;
		float num2 = 0f;
		bool flag = false;
		Ray fixedUpdateRelativeRay = SingleInstanceFindOnly<MouseOrbit>.Instance.GetFixedUpdateRelativeRay();
		RaycastHit[] array = Physics.RaycastAll(fixedUpdateRelativeRay.origin, fixedUpdateRelativeRay.direction, 500f, mask);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit = array[i];
			float sqrMagnitude = (raycastHit.point - mainCamPos).sqrMagnitude;
			if ((bool)raycastHit.rigidbody && !raycastHit.collider.isTrigger && !(raycastHit.collider.transform.root.name == "HUD") && (!flag || sqrMagnitude < num2))
			{
				num = i;
				num2 = sqrMagnitude;
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		hit = array[num];
		breakOnForce = hit.rigidbody.GetComponent<BreakOnForce>() != null;
		Rigidbody rigidbody = hit.rigidbody;
		Rigidbody rigidbody2 = null;
		bool flag2 = false;
		if (rigidbody.isKinematic)
		{
			if (rigidbody.transform.parent != null)
			{
				rigidbody2 = rigidbody.transform.parent.GetComponentInParent<Rigidbody>();
				if ((bool)rigidbody2)
				{
					if (rigidbody2.isKinematic)
					{
						return;
					}
					flag2 = true;
				}
				else if (!breakOnForce)
				{
					return;
				}
			}
			else if (!breakOnForce)
			{
				return;
			}
		}
		StatMaster.GodTools.HasBeenUsed = true;
		if (springJoint == null)
		{
			GameObject gameObject = new GameObject("Rigidbody dragger");
			springBody = gameObject.AddComponent<Rigidbody>();
			springJoint = gameObject.AddComponent<SpringJoint>();
			springBody.interpolation = RigidbodyInterpolation.Interpolate;
			springBody.useGravity = false;
			springBody.isKinematic = true;
			gameObject = new GameObject("Rigidbody drag anchor");
			extraBody = gameObject.AddComponent<Rigidbody>();
			extraJoint = gameObject.AddComponent<FixedJoint>();
			extraBody.interpolation = RigidbodyInterpolation.Interpolate;
			extraBody.useGravity = false;
			extraBody.isKinematic = false;
			extraBody.mass = 2f;
			extraBody.inertiaTensor = new Vector3(1f, 1f, 1f);
			extraBody.drag = 2f;
			extraBody.angularDrag = 1f;
		}
		if (lastBody != rigidbody)
		{
			BasicInfo component;
			if ((bool)lastBody)
			{
				component = lastBody.GetComponent<BasicInfo>();
				if ((bool)component && component.hasAiScript)
				{
					component.aiEntity.StopBeingGrabbed();
				}
			}
			component = rigidbody.GetComponent<BasicInfo>();
			if ((bool)component && component.hasAiScript)
			{
				component.aiEntity.Grabbed();
				flag2 = false;
			}
		}
		rigidbody2 = ((!flag2) ? rigidbody : rigidbody2);
		lastDistance = hit.distance;
		Vector3 position2 = SingleInstanceFindOnly<MouseOrbit>.Instance.FixedPointToCameraPoint(hit.point);
		springBody.transform.position = position2;
		extraBody.transform.position = position2;
		Rigidbody rigidbody3 = ((!flag2) ? rigidbody : rigidbody2);
		Rigidbody rigidbody4 = extraBody;
		Vector3 velocity = rigidbody3.velocity;
		springBody.velocity = velocity;
		rigidbody4.velocity = velocity;
		extraBody.angularVelocity = rigidbody3.angularVelocity;
		springJoint.anchor = Vector3.zero;
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = extraBody;
		extraJoint.connectedBody = rigidbody3;
		lastBody = rigidbody;
	}

	private void DragObject()
	{
		if (!springJoint)
		{
			return;
		}
		Transform transform = springJoint.transform;
		Rigidbody connectedBody = extraJoint.connectedBody;
		bool flag = connectedBody != null;
		if (InputManager.LeftMouseButtonHeld() && flag && lastDistance > 0f)
		{
			ray = mainCamera.ScreenPointToRay(InputManager.CursorPosition());
			springBody.MovePosition(ray.GetPoint(lastDistance));
			if (breakOnForce && (transform.position - connectedBody.position).sqrMagnitude > 500f)
			{
				BreakOnForce component = connectedBody.GetComponent<BreakOnForce>();
				if (component != null)
				{
					component.BreakExplosion(200f, hit.point, 6f, 0f);
				}
			}
			if (connectedBody.gameObject.activeSelf)
			{
				return;
			}
		}
		if (flag)
		{
			lastDistance = 0f;
			connectedBody.WakeUp();
			extraJoint.connectedBody = null;
			springJoint.connectedBody = null;
		}
	}
}
