using UnityEngine;

public class IkLeg : MonoBehaviour
{
	private float legLenth;

	public Transform footTarget;

	public LayerMask mask;

	public AnimationCurve upCurve;

	public Transform moveDeltaTransform;

	private float stepTime;

	public float stepSpeed = 1f;

	private float footDownTime;

	[HideInInspector]
	public bool footDown = true;

	public IkLeg otherLeg;

	public float prediction = 1f;

	private Vector3 raycastPosLocal;

	private Vector3 raycastPosWorld;

	private Vector3 previousRaycastPosLocal;

	private Vector3 previousRaycastPosWorld;

	private Transform raycastTransform;

	private Transform previousRaycastTransform;

	private Vector3 footPosition;

	private Vector3 deltaPos;

	private Vector3 lastPos;

	private void Start()
	{
		legLenth = Vector3.Distance(base.transform.position, footTarget.position);
	}

	private void FixedUpdate()
	{
		SetValuesFixed();
	}

	private void LateUpdate()
	{
		DoRayCast();
		UpdateRayCastWorldPos();
		DoStep();
		UpdatePreviousRayCastWorldPos();
		SetFootPos();
		Apply();
	}

	private void DoStep()
	{
		if (footDown)
		{
			footDownTime += Time.deltaTime * stepSpeed;
			if (footDownTime > 1f && otherLeg.footDown)
			{
				StartStep();
				footDownTime = 0f;
				footDown = false;
			}
		}
		else
		{
			stepTime += Time.deltaTime * stepSpeed;
			if (stepTime > 1f)
			{
				EndStep();
				stepTime = 0f;
				footDown = true;
			}
		}
	}

	private void EndStep()
	{
		if ((bool)raycastTransform)
		{
			previousRaycastTransform = raycastTransform;
			previousRaycastPosLocal = raycastPosLocal;
		}
	}

	private void StartStep()
	{
	}

	private void SetFootPos()
	{
		if ((bool)raycastTransform)
		{
			if (footDown)
			{
				footPosition = previousRaycastPosWorld;
			}
			else
			{
				footPosition = Vector3.Lerp(previousRaycastPosWorld, raycastPosWorld, stepTime) + Vector3.up * upCurve.Evaluate(stepTime);
			}
		}
	}

	private void Apply()
	{
		footTarget.position = footPosition;
	}

	private void DoRayCast()
	{
		Vector3 vector = deltaPos * prediction;
		Physics.Raycast(new Ray(base.transform.position, Vector3.down + vector), out var hitInfo, legLenth * 1.5f + vector.magnitude, mask);
		if ((bool)hitInfo.transform)
		{
			HitGround(hitInfo);
		}
		else
		{
			HitNothing();
		}
	}

	private void HitNothing()
	{
	}

	private void HitGround(RaycastHit hit)
	{
		if ((bool)raycastTransform && raycastTransform != hit.transform)
		{
			MigrateGroundHit(raycastTransform, hit.transform, hit);
		}
		raycastTransform = hit.transform;
		raycastPosLocal = raycastTransform.InverseTransformPoint(hit.point);
	}

	private void MigrateGroundHit(Transform from, Transform to, RaycastHit hit)
	{
	}

	private void UpdateRayCastWorldPos()
	{
		if ((bool)raycastTransform)
		{
			raycastPosWorld = raycastTransform.TransformPoint(raycastPosLocal);
		}
	}

	private void UpdatePreviousRayCastWorldPos()
	{
		if ((bool)previousRaycastTransform)
		{
			previousRaycastPosWorld = previousRaycastTransform.TransformPoint(previousRaycastPosLocal);
		}
	}

	private void SetValuesFixed()
	{
		deltaPos = moveDeltaTransform.position - lastPos;
		lastPos = moveDeltaTransform.position;
	}
}
