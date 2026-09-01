using System;
using UnityEngine;

public class ProjectilePhysArc : MonoBehaviour
{
	public Vector3 BallisticVel(Vector3 target, float angle)
	{
		Vector3 vector = target - base.transform.position;
		float y = vector.y;
		vector.y = 0f;
		float magnitude = vector.magnitude;
		float num = angle * ((float)Math.PI / 180f);
		vector.y = magnitude * Mathf.Tan(num);
		magnitude += y / Mathf.Tan(num);
		float num2 = Mathf.Sqrt(magnitude * Physics.gravity.magnitude / Mathf.Sin(2f * num));
		if (float.IsNaN(num2))
		{
			num2 = 1f;
		}
		return num2 * vector.normalized;
	}
}
