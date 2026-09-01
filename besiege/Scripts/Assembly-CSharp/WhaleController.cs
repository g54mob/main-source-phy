using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Water/Objects/Whale Controller")]
public class WhaleController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField]
	private float movementRange = 90f;

	[SerializeField]
	private float swimSpeed = 1f;

	[SerializeField]
	[Header("Path")]
	private GameObject checkpointVis;

	[SerializeField]
	private GameObject intermediatePointvis;

	[SerializeField]
	private int pathPoints = 20;

	public List<Vector3> pathPositions = new List<Vector3>();

	private Coroutine corPathCreate;

	public Vector3 currentTarget;

	private Rigidbody whaleRigidbody;

	private Vector3 targetPosition;

	public float pathLength;

	private float pathDuration = 10f;

	private float angle;

	private Vector3 startPathPos;

	private void Start()
	{
		whaleRigidbody = GetComponent<Rigidbody>();
		pathDuration /= swimSpeed;
		whaleRigidbody.centerOfMass = Vector3.zero;
		startPathPos = base.transform.position;
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			if (whaleRigidbody.velocity == Vector3.zero)
			{
			}
			if (corPathCreate == null)
			{
				corPathCreate = StartCoroutine(CreatePath());
			}
		}
	}

	private Vector3 PickTarget()
	{
		float f = (float)Math.PI * 2f * UnityEngine.Random.Range(0f, movementRange);
		float num = UnityEngine.Random.Range(0f, movementRange) + UnityEngine.Random.Range(0f, movementRange);
		float num2 = ((!(num > movementRange)) ? 0f : (2f - num));
		return new Vector3(num2 * Mathf.Cos(f), 0f, num2 * Mathf.Sin(f));
	}

	private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		return Mathf.Pow(1f - t, 2f) * p0 + 2f * (1f - t) * t * p1 + Mathf.Pow(t, 2f) * p2;
	}

	private Vector3 FindCrossPoint(Vector3 a, Vector3 b)
	{
		return new Vector3((a.x + b.x) / 2f, 0f, (a.z + b.z) / 2f);
	}

	private IEnumerator CreatePath()
	{
		while (targetPosition == Vector3.zero)
		{
			targetPosition = PickTarget();
		}
		targetPosition.y = 0f;
		GameObject b = UnityEngine.Object.Instantiate(checkpointVis, targetPosition, Quaternion.identity) as GameObject;
		pathPositions.Add(base.transform.position);
		Vector3 startVector = startPathPos;
		Vector3 endVector = b.transform.position;
		Vector3 crossPoint = FindCrossPoint(startVector, endVector);
		Vector3 crossPointPos = ((!(endVector.z - startVector.z > endVector.x - startVector.x)) ? new Vector3(crossPoint.x, 0f, crossPoint.z + Vector3.Distance(startVector, crossPoint)) : new Vector3(crossPoint.x + Vector3.Distance(startVector, crossPoint), 0f, crossPoint.z));
		for (int i = 1; i < pathPoints; i++)
		{
			float t = (float)i / (float)pathPoints;
			Vector3 newPos = CalculateQuadraticBezierPoint(t, startVector, crossPointPos, endVector);
			pathPositions.Add(newPos);
		}
		for (int j = 0; j < pathPositions.Count; j++)
		{
			UnityEngine.Object.Instantiate(position: pathPositions[j], original: intermediatePointvis, rotation: Quaternion.identity);
			yield return new WaitForSecondsRealtime(0.1f);
			if (j + 1 < pathPositions.Count)
			{
				pathLength += Vector3.Distance(pathPositions[j], pathPositions[j + 1]);
			}
		}
		pathPositions.Add(targetPosition);
	}

	public void ResetAndRestart()
	{
		pathLength = 0f;
		targetPosition = Vector3.zero;
		StopAllCoroutines();
		corPathCreate = null;
	}
}
