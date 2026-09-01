using System.Collections.Generic;
using UnityEngine;

public class DesertMirror : MonoBehaviour
{
	public int maxReflectionCount;

	public float maxStepDistance = 200f;

	public LineRenderer TrailA;

	public List<Vector3> HitPositions;

	public List<GameObject> hitMirrors;

	public int count;

	private void Start()
	{
		TrailA = GetComponent<LineRenderer>();
		TrailA.SetVertexCount(maxReflectionCount + 1);
	}

	private void Update()
	{
		TrailA.SetVertexCount(maxReflectionCount + 1);
		HitPositions[0] = base.transform.position;
		for (int i = 0; i <= maxReflectionCount; i++)
		{
			TrailA.SetPosition(i, HitPositions[i]);
		}
		DrawPredictedReflectionPattern(base.transform.position + base.transform.forward * 0.75f, base.transform.TransformDirection(Vector3.forward), maxReflectionCount);
	}

	private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
	{
		if (reflectionsRemaining <= 0)
		{
			count = 0;
			return;
		}
		Ray ray = new Ray(position, direction);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, maxStepDistance))
		{
			direction = Vector3.Reflect(direction, hitInfo.normal);
			position = hitInfo.point;
		}
		else
		{
			position += direction * maxStepDistance;
		}
		int num = maxReflectionCount + 1;
		HitPositions[num - reflectionsRemaining] = position;
		DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
	}
}
